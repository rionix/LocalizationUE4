using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TranslationEditor
{
    public static class JsonSerializer
    {
        public const int ManifestVersion = 1;
        public const string ManifestNamespace = "";
        public const int ArchiveVersion = 2;
        public const string ArchiveNamespace = "";

        private static UnrealNamespace<T> CreateSubnamespace<T>(string FullName, UnrealNamespace<T> Root)
        {
            UnrealNamespace<T> result = Root;
            string[] splittedName = InternalNamespace.SplitName(FullName);

            if (splittedName.Length == 1 && splittedName[0] == "")
                return result;

            foreach (var namespaceName in splittedName)
                result = result.CreateSubnamespace(namespaceName);

            return result;
        }

        //
        // Load from Manifest
        //

        private static void AppendNamespaceFromManifest(InternalFormat document,
            string parentNamespaceName, UnrealNamespace<UnrealManifest> sourceNamespace)
        {
            InternalNamespace resultNamespace = new InternalNamespace();

            // generate namespace name

            resultNamespace.Name = InternalNamespace.MakeName(parentNamespaceName, sourceNamespace.Namespace);

            // fill from all Childrens records

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

            document.Namespaces.Add(resultNamespace);

            // recursive add all subnamespaces

            if (sourceNamespace.Subnamespaces != null)
            {
                foreach (var subnamespace in sourceNamespace.Subnamespaces)
                    AppendNamespaceFromManifest(document, resultNamespace.Name, subnamespace);
            }
        }

        public static void LoadFromManifest(InternalFormat document, string InFileName, string FileText)
        {
            var manifest = JsonConvert.DeserializeObject<UnrealNamespace<UnrealManifest>>(FileText);

            if (ManifestVersion != manifest.FormatVersion)
                throw new FormatException("Invalid Manifest::FormatVersion.");
            if (ManifestNamespace != manifest.Namespace)
                throw new FormatException("Invalid Manifest::Namespace. Must be empty.");

            document.Namespaces = new List<InternalNamespace>();
            AppendNamespaceFromManifest(document, ManifestNamespace, manifest);

            document.Cultures = new List<string>();
        }

        //
        // Save to Manifest
        //

        private static UnrealManifest CreateManifestChild(string Source, UnrealNamespace<UnrealManifest> Root)
        {
            if (Root.Children != null)
            {
                foreach (var child in Root.Children)
                    if (child.Source.Text == Source)
                        return child;
            }

            if (Root.Children == null)
                Root.Children = new List<UnrealManifest>();

            var newChild = new UnrealManifest();
            newChild.Source = new UnrealText();
            newChild.Source.Text = Source;
            newChild.Keys = new List<UnrealKey>();
            Root.Children.Add(newChild);

            return newChild;
        }

        public static string SaveToManifest(InternalFormat document)
        {
            if (document.Namespaces == null)
                throw new ArgumentException("Can't save empty archieve");

            var manifest = new UnrealNamespace<UnrealManifest>(ManifestVersion, ManifestNamespace);

            foreach (var internalNamespace in document.Namespaces)
            {
                var sns = CreateSubnamespace(internalNamespace.Name, manifest);

                foreach (var record in internalNamespace.Children)
                {
                    var child = CreateManifestChild(record.Source, sns);

                    var key = new UnrealKey();
                    key.Key = record.Key;
                    key.Path = record.Path;

                    child.Keys.Add(key);
                }
            }

            return JsonConvert.SerializeObject(manifest, Formatting.Indented);
        }

        //
        // Load from Archive
        //

        private static void FillNamespaceFromArchieve(InternalFormat document, string Culture, string parentNamespaceName, UnrealNamespace<UnrealArchive> sourceArchieve)
        {
            // find namespace

            string resultNamespaceName = InternalNamespace.MakeName(parentNamespaceName, sourceArchieve.Namespace);

            InternalNamespace resultNamespace = null;
            foreach (var ns in document.Namespaces)
                if (ns.Name == resultNamespaceName)
                {
                    resultNamespace = ns;
                    break;
                }

            if (resultNamespace == null)
                throw new FormatException("Can't find namespace for parent: '" + parentNamespaceName +
                    "' and source '" + sourceArchieve.Namespace + "'" );

            // fill namespace from childs

            if (sourceArchieve.Children != null)
            {
                foreach (var child in sourceArchieve.Children)
                {
                    InternalText text = new InternalText();
                    InternalRecord record = resultNamespace[child.Key];

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
                    FillNamespaceFromArchieve(document, Culture, resultNamespaceName, subnamespace);
            }
        }

        public static void LoadFromArchive(InternalFormat document, string Culture, string FileText)
        {
            if (document.Namespaces == null)
                throw new System.InvalidOperationException("Load manifest first.");

            if (document.Cultures.Contains(Culture))
                throw new System.ArgumentException("Culture already appended: " + Culture);

            var archive = JsonConvert.DeserializeObject<UnrealNamespace<UnrealArchive>>(FileText);

            if (ArchiveVersion != archive.FormatVersion)
                throw new FormatException("Invalid Archive::FormatVersion.");

            if (ArchiveNamespace != archive.Namespace)
                throw new FormatException("Invalid Archive::Namespace. Must be empty.");

            FillNamespaceFromArchieve(document, Culture, ArchiveNamespace, archive);

            document.Cultures.Add(Culture);
        }

        public static string SaveToArchive(InternalFormat document, string Culture)
        {
            if (document.Cultures == null || document.Cultures.Contains(Culture) == false)
                throw new ArgumentException("Culture not found: " + Culture);

            if (document.Namespaces == null)
                throw new ArgumentException("Can't save empty archieve");

            var archive = new UnrealNamespace<UnrealArchive>(ArchiveVersion, ArchiveNamespace);

            foreach (var internalNamespace in document.Namespaces)
            {
                var sns = CreateSubnamespace(internalNamespace.Name, archive);

                foreach (var record in internalNamespace.Children)
                {
                    UnrealArchive child = new UnrealArchive();
                    child.Source = new UnrealText();
                    child.Source.Text = record.Source;
                    child.Translation = new UnrealText();
                    child.Translation.Text = record[Culture];
                    child.Key = record.Key;

                    if (sns.Children == null)
                        sns.Children = new List<UnrealArchive>();
                    sns.Children.Add(child);
                }
            }

            return JsonConvert.SerializeObject(archive, Formatting.Indented);
        }

        //
        // Load from Locmeta
        //

        private static string ReadStringFromBytes(byte[] src, int index, out int offset)
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

        public static void LoadFromLocMeta(InternalFormat document, byte[] FileData)
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

            // Skip file version
            //Byte Version = FileData[index];
            //if (Version > 0)
            //    throw new FormatException("Invalid file version!");
            index += 1;

            int offset = 0;
            document.NativeCulture = ReadStringFromBytes(FileData, index, out offset);
            index += offset;
            document.NativeLocRes = ReadStringFromBytes(FileData, index, out offset);
            index += offset;
        }
    }
}
