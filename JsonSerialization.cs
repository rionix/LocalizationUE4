using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;

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
        [JsonProperty("FormatVersion", NullValueHandling = NullValueHandling.Ignore)]
        public int FormatVersion { get; set; }

        [JsonProperty("Namespace")]
        public string Namespace { get; set; }

        [JsonProperty("Subnamespaces", NullValueHandling = NullValueHandling.Ignore)]
        public List<LocaleManifestNamespace> Subnamespaces { get; set; }

        [JsonProperty("Children", NullValueHandling = NullValueHandling.Ignore)]
        public List<LocaleManifestChild> Children { get; set; }
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
        [JsonProperty("FormatVersion", NullValueHandling = NullValueHandling.Ignore)]
        public int FormatVersion { get; set; }

        [JsonProperty("Namespace")]
        public string Namespace { get; set; }

        [JsonProperty("Subnamespaces", NullValueHandling = NullValueHandling.Ignore)]
        public List<LocaleArchiveNamespace> Subnamespaces { get; set; }

        [JsonProperty("Children", NullValueHandling = NullValueHandling.Ignore)]
        public List<LocaleArchiveChild> Children { get; set; }
    }
}
