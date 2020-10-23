using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LocalizationUE4
{
    public class InternalText
    {
        public string Culture { get; set; }
        public string Text { set; get; }
    }

    public class InternalRecord
    {
        public string Source { get; set; }
        public string Key { get; set; }
        public List<InternalText> Translations { get; set; }
        public string Path { get; set; }

        // Get or Set translation text by Culture
        public string this[string Culture]
        {
            get
            {
                foreach (var t in Translations)
                    if (t.Culture == Culture)
                        return t.Text;
                throw new ArgumentException("Can't GET culture [" + Culture + "] in record: " + Source);
            }
            set
            {
                foreach (var t in Translations)
                    if (t.Culture == Culture)
                    {
                        t.Text = value;
                        return;
                    }
                throw new ArgumentException("Can't SET culture [" + Culture + "] in record: " + Source);
            }
        }
    }

    public class InternalNamespace
    {
        public string Name { get; set; }
        public List<InternalRecord> Children { get; set; }

        // Get internal record by Key
        public InternalRecord this[string Key]
        {
            get
            {
                foreach (var key in Children)
                    if (key.Key == Key)
                        return key;
                throw new ArgumentException("Can't GET record [" + Key + "] in namespace: " + Name);
            }
        }

        static public string MakeFullName(string Namespace, string Key)
        {
            return Namespace + ',' + Key;
        }

        static public string[] SplitFullName(string FullName)
        {
            string[] result = FullName.Split(',');
            if (result.Length != 2)
                throw new FormatException("Invalid FullName: " + FullName + "!");
            return result;
        }

        static public string MakeNamespaceName(string ParentName, string ChildName)
        {
            if (ParentName == "")
            {
                if (ChildName == "")
                    return "";
                else
                    return ChildName;
            }
            return ParentName + "." + ChildName;
        }

        static public string[] SplitNamespaceName(string FullName)
        {
            if (!FullName.Contains("."))
                return new string[] { FullName };
            return FullName.Split('.');
        }
    }

    public class InternalFormat
    {
        public const int ManifestVersion = 1;
        public const string ManifestNamespace = "";
        public const int ArchiveVersion = 2;
        public const string ArchiveNamespace = "";

        public List<InternalNamespace> Namespaces { get; set; }
        public List<string> Cultures { get; set; }

        public string NativeCulture { get; set; }
        public string NativeLocRes { get; set; }

        public void Clear()
        {
            Namespaces = null;
            Cultures = null;
            NativeCulture = null;
            NativeLocRes = null;
        }

        private void AppendNamespaceFromManifest(string parentNamespaceName, LocaleManifestNamespace sourceNamespace)
        {
            InternalNamespace resultNamespace = new InternalNamespace();

            // generate namespace name

            resultNamespace.Name = InternalNamespace.MakeNamespaceName(parentNamespaceName, sourceNamespace.Namespace);

            // fill fromm all Childrens records

            resultNamespace.Children = new List<InternalRecord>();
            if (sourceNamespace.Children != null)
            {
                foreach (var child in sourceNamespace.Children)
                    foreach (var key in child.Keys)
                    {
                        InternalRecord record = new InternalRecord();
                        record.Source = child.Source.Text;
                        record.Key = key.Key;
                        record.Translations = new List<InternalText>();
                        record.Path = key.Path;
                        resultNamespace.Children.Add(record);
                    }
            }

            // add this namespace to List

            Namespaces.Add(resultNamespace);

            // recursive add all subnamespaces

            if (sourceNamespace.Subnamespaces != null)
            {
                foreach (var subnamespace in sourceNamespace.Subnamespaces)
                    AppendNamespaceFromManifest(resultNamespace.Name, subnamespace);
            }
        }

        public void LoadFromManifest(string InFileName, string FileText)
        {
            LocaleManifestNamespace manifest = JsonConvert.DeserializeObject<LocaleManifestNamespace>(FileText);

            if (ManifestVersion != manifest.FormatVersion)
                throw new FormatException("Invalid Manifest::FormatVersion.");
            if (ManifestNamespace != manifest.Namespace)
                throw new FormatException("Invalid Manifest::Namespace. Must be empty.");

            Namespaces = new List<InternalNamespace>();
            AppendNamespaceFromManifest("", manifest);

            Cultures = new List<string>();
        }

        private LocaleManifestNamespace CreateManifestSubnamespace(string FullName, LocaleManifestNamespace Root)
        {
            LocaleManifestNamespace result = Root;
            string[] splittedName = InternalNamespace.SplitNamespaceName(FullName);

            if (splittedName.Length == 1 && splittedName[0] == "")
                return result;

            foreach (var namespaceName in splittedName)
                result = result.CreateSubnamespace(namespaceName);

            return result;
        }

        private LocaleManifestChild CreateManifestChild(string Source, LocaleManifestNamespace Root)
        {
            if (Root.Children != null)
            {
                foreach (var child in Root.Children)
                    if (child.Source.Text == Source)
                        return child;
            }

            if (Root.Children == null)
                Root.Children = new List<LocaleManifestChild>();

            var newChild = new LocaleManifestChild();
            newChild.Source = new LocaleSource();
            newChild.Source.Text = Source;
            newChild.Keys = new List<LocaleKey>();
            Root.Children.Add(newChild);

            return newChild;
        }

        public string SaveToManifest()
        {
            if (Namespaces == null)
                throw new ArgumentException("Can't save empty archieve");

            LocaleManifestNamespace manifest = new LocaleManifestNamespace();
            manifest.FormatVersion = ManifestVersion;
            manifest.Namespace = ManifestNamespace;
            manifest.Subnamespaces = new List<LocaleManifestNamespace>();

            foreach (var internalNamespace in Namespaces)
            {
                var sns = CreateManifestSubnamespace(internalNamespace.Name, manifest);

                foreach (var record in internalNamespace.Children)
                {
                    var child = CreateManifestChild(record.Source, sns);

                    var key = new LocaleKey();
                    key.Key = record.Key;
                    key.Path = record.Path;

                    child.Keys.Add(key);
                }
            }

            return JsonConvert.SerializeObject(manifest, Formatting.Indented);
        }

        private void FillNamespaceFromArchieve(string Culture, string parentNamespaceName, LocaleArchiveNamespace sourceArchieve)
        {
            // find namespace

            string destinationNamespaceName = InternalNamespace.MakeNamespaceName(parentNamespaceName, sourceArchieve.Namespace);

            InternalNamespace destinationNamespace = null;
            foreach (var ns in Namespaces)
                if (ns.Name == destinationNamespaceName)
                {
                    destinationNamespace = ns;
                    break;
                }

            if (destinationNamespace == null)
                throw new FormatException("Can't find namespace for parent: '" + parentNamespaceName +
                    "' and source '" + sourceArchieve.Namespace + "'" );

            // fill namespace from childs

            if (sourceArchieve.Children != null)
            {
                foreach (var child in sourceArchieve.Children)
                {
                    InternalText text = new InternalText();
                    InternalRecord record = destinationNamespace[child.Key];

                    text.Culture = Culture;
                    if (record.Source != child.Source.Text)
                        text.Text = "";
                    else
                        text.Text = child.Translation.Text;

                    record.Translations.Add(text);
                }
            }

            // recursively repeat for all Subnamespaces

            if (sourceArchieve.Subnamespaces != null)
            {
                foreach (var subnamespace in sourceArchieve.Subnamespaces)
                    FillNamespaceFromArchieve(Culture, destinationNamespaceName, subnamespace);
            }
        }

        public void LoadFromArchive(string Culture, string FileText)
        {
            if (Namespaces == null)
                throw new System.InvalidOperationException("Load manifest first.");

            if (Cultures.Contains(Culture))
                throw new System.ArgumentException("Culture already appended: " + Culture);

            LocaleArchiveNamespace archive = JsonConvert.DeserializeObject<LocaleArchiveNamespace>(FileText);

            if (ArchiveVersion != archive.FormatVersion)
                throw new FormatException("Invalid Archive::FormatVersion.");

            if (ArchiveNamespace != archive.Namespace)
                throw new FormatException("Invalid Archive::Namespace. Must be empty.");

            FillNamespaceFromArchieve(Culture, "", archive);

            Cultures.Add(Culture);
        }

        private LocaleArchiveNamespace CreateArchiveSubnamespace(string FullName, LocaleArchiveNamespace Root)
        {
            LocaleArchiveNamespace result = Root;
            string[] splittedName = InternalNamespace.SplitNamespaceName(FullName);

            if (splittedName.Length == 1 && splittedName[0] == "")
                return result;

            foreach (var namespaceName in splittedName)
                result = result.CreateSubnamespace(namespaceName);

            return result;
        }

        public string SaveToArchive(string Culture)
        {
            if (Cultures == null || Cultures.Contains(Culture) == false)
                throw new ArgumentException("Culture not found: " + Culture);

            if (Namespaces == null)
                throw new ArgumentException("Can't save empty archieve");

            LocaleArchiveNamespace archive = new LocaleArchiveNamespace();
            archive.FormatVersion = ArchiveVersion;
            archive.Namespace = ArchiveNamespace;
            archive.Subnamespaces = new List<LocaleArchiveNamespace>();

            foreach (var internalNamespace in Namespaces)
            {
                var sns = CreateArchiveSubnamespace(internalNamespace.Name, archive);

                foreach (var record in internalNamespace.Children)
                {
                    LocaleArchiveChild child = new LocaleArchiveChild();
                    child.Source = new LocaleSource();
                    child.Source.Text = record.Source;
                    child.Translation = new LocaleTranslation();
                    child.Translation.Text = record[Culture];
                    child.Key = record.Key;

                    if (sns.Children == null)
                        sns.Children = new List<LocaleArchiveChild>();
                    sns.Children.Add(child);
                }
            }

            return JsonConvert.SerializeObject(archive, Formatting.Indented);
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
