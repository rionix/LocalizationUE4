using System;
using System.Collections.Generic;

namespace TranslationEditor
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

        // Get Record by Key
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

        static public string MakeName(string ParentName, string ChildName)
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

        static public string[] SplitName(string FullName)
        {
            if (!FullName.Contains("."))
                return new string[] { FullName };
            return FullName.Split('.');
        }
    }

    public class InternalFormat
    {
        public List<InternalNamespace> Namespaces { get; set; }
        public List<string> Cultures { get; set; }
        // locmeta
        public string NativeCulture { get; set; }
        public string NativeLocRes { get; set; }
    }
}
