using System.Collections.Generic;

namespace Localizations
{
    public class LocalizationPackage
    {
        private string _packageName = "Default";
        private string _packageLanguageCode = "en_US";
        private string _packageAuthor = "Default";

        public string PackageName { get { return _packageName; } }
        public string PackageLanguageCode { get { return _packageLanguageCode; } }
        public string PackageAuthor { get { return _packageAuthor; } }

        public Dictionary<string, string> Strings = new Dictionary<string, string>();

        public LocalizationPackage(string packageName, string packageLanguageCode, string packageAuthor)
        {
            _packageName = packageName;
            _packageLanguageCode = packageLanguageCode;
            _packageAuthor = packageAuthor;
        }
    }
}