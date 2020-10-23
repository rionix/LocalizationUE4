using System.Collections.Generic;
using Newtonsoft.Json;

namespace LocalizationUE4
{
    //
    // Common
    //

    public class LocaleSource
    {
        [JsonProperty("Text")]
        public string Text { get; set; }
    }

    public class LocaleTranslation
    {
        [JsonProperty("Text")]
        public string Text { get; set; }
    }

    //
    // Locale Manifest
    //

    public class LocaleKey
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("Path")]
        public string Path { get; set; }
    }

    public class LocaleManifestChild
    {
        [JsonProperty("Source")]
        public LocaleSource Source { get; set; }

        [JsonProperty("Keys")]
        public List<LocaleKey> Keys { get; set; }
    }

    public class LocaleManifestNamespace
    {
        [JsonProperty("FormatVersion", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int FormatVersion { get; set; }

        [JsonProperty("Namespace")]
        public string Namespace { get; set; }

        [JsonProperty("Subnamespaces", NullValueHandling = NullValueHandling.Ignore)]
        public List<LocaleManifestNamespace> Subnamespaces { get; set; }

        [JsonProperty("Children", NullValueHandling = NullValueHandling.Ignore)]
        public List<LocaleManifestChild> Children { get; set; }

        public LocaleManifestNamespace CreateSubnamespace(string SubnamespaceName)
        {
            if (Subnamespaces == null)
                Subnamespaces = new List<LocaleManifestNamespace>();

            foreach (var sns in Subnamespaces)
                if (sns.Namespace == SubnamespaceName)
                    return sns;

            var nns = new LocaleManifestNamespace();
            nns.Namespace = SubnamespaceName;
            Subnamespaces.Add(nns);
            return nns;
        }
    }

    //
    // Locale Archive
    //

    public class LocaleArchiveChild
    {
        [JsonProperty("Source")]
        public LocaleSource Source { get; set; }

        [JsonProperty("Translation")]
        public LocaleTranslation Translation { get; set; }

        [JsonProperty("Key")]
        public string Key { get; set; }
    }

    public class LocaleArchiveNamespace
    {
        [JsonProperty("FormatVersion", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int FormatVersion { get; set; }

        [JsonProperty("Namespace")]
        public string Namespace { get; set; }

        [JsonProperty("Subnamespaces", NullValueHandling = NullValueHandling.Ignore)]
        public List<LocaleArchiveNamespace> Subnamespaces { get; set; }

        [JsonProperty("Children", NullValueHandling = NullValueHandling.Ignore)]
        public List<LocaleArchiveChild> Children { get; set; }

        public LocaleArchiveNamespace CreateSubnamespace(string SubnamespaceName)
        {
            if (Subnamespaces == null)
                Subnamespaces = new List<LocaleArchiveNamespace>();

            foreach (var sns in Subnamespaces)
                if (sns.Namespace == SubnamespaceName)
                    return sns;

            var nns = new LocaleArchiveNamespace();
            nns.Namespace = SubnamespaceName;
            Subnamespaces.Add(nns);
            return nns;
        }
    }
}
