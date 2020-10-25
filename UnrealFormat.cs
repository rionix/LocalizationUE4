using System.Collections.Generic;
using Newtonsoft.Json;

namespace TranslationEditor
{
    public class UnrealText
    {
        [JsonProperty("Text")]
        public string Text { get; set; }
    }

    public class UnrealKey
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("Path")]
        public string Path { get; set; }
    }

    public class UnrealNamespace<T>
    {
        [JsonProperty("FormatVersion", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int FormatVersion { get; set; }

        [JsonProperty("Namespace")]
        public string Namespace { get; set; }

        [JsonProperty("Subnamespaces", NullValueHandling = NullValueHandling.Ignore)]
        public List<UnrealNamespace<T>> Subnamespaces { get; set; }

        [JsonProperty("Children", NullValueHandling = NullValueHandling.Ignore)]
        public List<T> Children { get; set; }

        public UnrealNamespace() { }

        public UnrealNamespace(int InFormatVersion, string InNamespace)
        {
            FormatVersion = InFormatVersion;
            Namespace = InNamespace;
            Subnamespaces = new List<UnrealNamespace<T>>();
        }

        public UnrealNamespace<T> CreateSubnamespace(string SubnamespaceName)
        {
            if (Subnamespaces == null)
                Subnamespaces = new List<UnrealNamespace<T>>();

            foreach (var sns in Subnamespaces)
                if (sns.Namespace == SubnamespaceName)
                    return sns;

            var nns = new UnrealNamespace<T>();
            nns.Namespace = SubnamespaceName;
            Subnamespaces.Add(nns);
            return nns;
        }
    }

    public class UnrealManifest
    {
        [JsonProperty("Source")]
        public UnrealText Source { get; set; }

        [JsonProperty("Keys")]
        public List<UnrealKey> Keys { get; set; }
    }

    public class UnrealArchive
    {
        [JsonProperty("Source")]
        public UnrealText Source { get; set; }

        [JsonProperty("Translation")]
        public UnrealText Translation { get; set; }

        [JsonProperty("Key")]
        public string Key { get; set; }
    }
}
