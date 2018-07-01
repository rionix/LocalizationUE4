using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LocalizationUE4
{
    public class InternalText
    {
        public string Culture { get; set; }
        public string Text { set; get; }
    }

    public class InternalKey
    {
        public string Key { get; set; }
        public string Path { get; set; }
        public List<InternalText> Translations { get; set; }
        public InternalRecord parent { get; set; }

        public string GetTranslationForCulture(string Culture)
        {
            foreach (var t in Translations)
                if (t.Culture == Culture)
                    return t.Text;
            return "";
        }

        public void SetTranslationForCulture(string Culture, string Value)
        {
            foreach (var t in Translations)
                if (t.Culture == Culture)
                {
                    t.Text = Value;
                    return;
                }
        }
    }

    public class InternalRecord
    {
        public string Source { get; set; }
        public List<InternalKey> Keys { get; set; }
    }

    public class InternalNamespace
    {
        public string Name { get; set; }
        public List<InternalRecord> Children { get; set; }

        public InternalKey GetKey(string Key)
        {
            foreach (var rec in Children)
                foreach (var key in rec.Keys)
                    if (key.Key == Key)
                        return key;
            return null;
        }
    }

    public class InternalFormat
    {
        public const int ManifestVersion = 1;
        public const string ManifestNamespace = "";
        public const int ArchiveVersion = 2;
        public const string ArchiveNamespace = "";

        public string FileName { get; set; }
        public List<InternalNamespace> Subnamespaces { get; set; }
        public List<string> Cultures { get; set; }

        public string NativeCulture { get; set; }
        public string NativeLocRes { get; set; }

        public void Clear()
        {
            FileName = null;
            Subnamespaces = null;
            Cultures = null;
            NativeCulture = null;
            NativeLocRes = null;
        }

        public void LoadFromManifest(string InFileName, string FileText)
        {
            Clear();

            LocaleManifest manifest = JsonConvert.DeserializeObject<LocaleManifest>(FileText);

            if (ManifestVersion != manifest.FormatVersion)
                throw new FormatException("Invalid Manifest::FormatVersion.");

            if (ManifestNamespace != manifest.Namespace)
                throw new FormatException("Invalid Manifest::Namespace. Must be empty.");

            Subnamespaces = new List<InternalNamespace>(manifest.Subnamespaces.Count);
            foreach (var ns in manifest.Subnamespaces)
            {
                InternalNamespace ins = new InternalNamespace();
                ins.Name = ns.Namespace;
                ins.Children = new List<InternalRecord>(manifest.Subnamespaces.Count);

                foreach(var child in ns.Children)
                {
                    InternalRecord record = new InternalRecord();
                    record.Source = child.Source.Text;
                    record.Keys = new List<InternalKey>(child.Keys.Count);

                    foreach (var key in child.Keys)
                    {
                        InternalKey ikey = new InternalKey();
                        ikey.Key = key.Key;
                        ikey.Path = key.Path;
                        ikey.Translations = new List<InternalText>();
                        ikey.parent = record;
                        record.Keys.Add(ikey);
                    }

                    ins.Children.Add(record);
                }

                Subnamespaces.Add(ins);
            }

            FileName = InFileName;
            Cultures = new List<string>();
        }

        public string SaveToManifest()
        {
            if (Subnamespaces != null && Subnamespaces.Count > 0)
            {
                LocaleManifest manifest = new LocaleManifest();
                manifest.FormatVersion = ManifestVersion;
                manifest.Namespace = ManifestNamespace;
                manifest.Subnamespaces = new List<LocaleManifestNamespace>(Subnamespaces.Count);

                foreach (var ins in Subnamespaces)
                {
                    LocaleManifestNamespace ns = new LocaleManifestNamespace();
                    ns.Namespace = ins.Name;

                    if (ins.Children != null && ins.Children.Count > 0)
                    {
                        ns.Children = new List<LocaleManifestChild>(ins.Children.Count);
                        foreach (var rec in ins.Children)
                        {
                            LocaleManifestChild child = new LocaleManifestChild();
                            child.Source = new LocaleSource();
                            child.Source.Text = rec.Source;
                            child.Keys = new List<LocaleKey>(rec.Keys.Count);
                            foreach (var ikey in rec.Keys)
                            {
                                LocaleKey key = new LocaleKey();
                                key.Key = ikey.Key;
                                key.Path = ikey.Path;
                                child.Keys.Add(key);
                            }
                            ns.Children.Add(child);
                        }
                    }
                    manifest.Subnamespaces.Add(ns);
                }

                return JsonConvert.SerializeObject(manifest, Formatting.Indented);
            }

            return "";
        }

        public void LoadFromArchive(string Culture, string FileText)
        {
            if (Subnamespaces == null)
                throw new System.InvalidOperationException("Load manifest first.");

            if (Cultures.Contains(Culture))
                throw new System.ArgumentException("Culture already appended: " + Culture);

            LocaleArchive archive = JsonConvert.DeserializeObject<LocaleArchive>(FileText);

            if (ArchiveVersion != archive.FormatVersion)
                throw new FormatException("Invalid Archive::FormatVersion.");

            if (ArchiveNamespace != archive.Namespace)
                throw new FormatException("Invalid Archive::Namespace. Must be empty.");

            foreach (var ns in archive.Subnamespaces)
            {
                InternalNamespace ins = GetNamespace(ns.Namespace);

                if (ins == null)
                    throw new FormatException("Archive::Subnamespace not found: " + ns.Namespace + "!");

                foreach (var child in ns.Children)
                {
                    InternalKey ikey = ins.GetKey(child.Key);

                    if (ikey == null)
                        throw new FormatException("Invalid key (" + child.Key + ") in Archive::Subnamespace::Child " + child.Source.Text + "!");

                    InternalText text = new InternalText();
                    text.Culture = Culture;
                    text.Text = child.Translation.Text;
                    ikey.Translations.Add(text);
                }
            }

            Cultures.Add(Culture);
        }

        public string SaveToArchive(string Culture)
        {
            if (Cultures == null || Cultures.Contains(Culture) == false)
                throw new System.ArgumentException("Culture not found: " + Culture);

            if (Subnamespaces != null && Subnamespaces.Count > 0)
            {
                LocaleArchive archive = new LocaleArchive();
                archive.FormatVersion = ArchiveVersion;
                archive.Namespace = ArchiveNamespace;
                archive.Subnamespaces = new List<LocaleArchiveNamespace>(Subnamespaces.Count);

                foreach (var ins in Subnamespaces)
                {
                    LocaleArchiveNamespace ns = new LocaleArchiveNamespace();
                    ns.Namespace = ins.Name;
                    if (ins.Children != null && ins.Children.Count > 0)
                    {
                        ns.Children = new List<LocaleArchiveChild>(ins.Children.Count);
                        foreach (var rec in ins.Children)
                        {
                            foreach (var ikey in rec.Keys)
                            {
                                LocaleArchiveChild child = new LocaleArchiveChild();
                                child.Source = new LocaleSource();
                                child.Source.Text = (Culture == NativeCulture) ? rec.Source : ikey.GetTranslationForCulture(NativeCulture);
                                child.Translation = new LocaleTranslation();
                                child.Translation.Text = ikey.GetTranslationForCulture(Culture);
                                child.Key = ikey.Key;
                                ns.Children.Add(child);
                            }
                        }
                    }
                    archive.Subnamespaces.Add(ns);
                }

                return JsonConvert.SerializeObject(archive, Formatting.Indented);
            }

            return "";
        }

        public void LoadFromLocMeta(byte[] FileData)
        {
            int index = 0;

            // Magic Number from:
            // UnrealEngine\Engine\Source\Runtime\Core\Private\Internationalization\TextLocalizationResource.cpp
            UInt32[] LocMetaMagic = { 0xA14CEE4F, 0x83554868, 0xBD464C6C, 0x7C50DA70 };
            if (   LocMetaMagic[0] != BitConverter.ToUInt32(FileData, index +  0)
                || LocMetaMagic[1] != BitConverter.ToUInt32(FileData, index +  4)
                || LocMetaMagic[2] != BitConverter.ToUInt32(FileData, index +  8)
                || LocMetaMagic[3] != BitConverter.ToUInt32(FileData, index + 12))
                throw new FormatException("LocMeta magic number is not correct!");
            index += 16;

            Byte Version = FileData[index];
            if (Version > 0)
                throw new FormatException("Invalid file version!");
            index += 1;

            int offset = 0;
            NativeCulture = ReadStringFromBytes(FileData, index, out offset);
            index += offset;
            NativeLocRes = ReadStringFromBytes(FileData, index, out offset);
            index += offset;
        }

        //
        // Utils
        //

        public InternalNamespace GetNamespace(string Name)
        {
            foreach (var ns in Subnamespaces)
                if (ns.Name == Name)
                    return ns;
            return null;
        }

        public string Title
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FileName);
            }
        }

        public string ReadStringFromBytes(byte[] src, int index, out int offset)
        {
            Int32 SaveNum = BitConverter.ToInt32(src, index);
            if (SaveNum < 0)
            {
                // load LoadUCS2Char
                SaveNum = -SaveNum;
                if (SaveNum < 1)
                    throw new FormatException("String length is too short.");
                offset = SaveNum * 2 + 4;
                return System.Text.Encoding.Unicode.GetString(src, index + 4, (SaveNum - 1) * 2);
            }
            else
            {
                // load ANSICHAR
                if (SaveNum < 1)
                    throw new FormatException("String length is too short.");
                offset = SaveNum + 4;
                return System.Text.Encoding.UTF8.GetString(src, index + 4, SaveNum - 1);
            }
        }
    }
}
