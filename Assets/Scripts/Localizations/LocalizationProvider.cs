using System.Collections.Generic;
using UnityEngine;

namespace Localizations
{
    public static class LocalizationProvider
    {
        private static string _PackageName = "Default";
        private static string _PackageCode = "en_US";
        private static string _PackageAuthor = "Default";

        private static LocalizationPackage _loadedPackage = null;

        public static string PackageName { get { return _PackageName; } }
        public static string PackageCode { get { return _PackageCode; } }
        public static string PackageAuthor { get { return _PackageAuthor; } }

        public static string GetLocalizedString(string stringId)
        {
            if (_loadedPackage == null)
            {
                throw new LanguagePackageNotLoaded();
                return null;
            }

            string value = "";
            if (_loadedPackage.Strings.TryGetValue(stringId, out value))
                return value;
            else
            {
                throw new LocalizedStringNotFound();
                return null;
            }
        }

        public static bool LoadNewLanguagePackage(string languageCode)
        {
            try
            {
                _loadedPackage = LocalizationLoader.GetPackageByCode(languageCode);
                _PackageName = _loadedPackage.PackageName;
                _PackageCode = _loadedPackage.PackageLanguageCode;
                _PackageAuthor = _loadedPackage.PackageAuthor;

                Debug.Log($"LOCALIZATION_PROVIDER: Language package with language code {languageCode} was loaded into provider.");
                return true;
            }
            catch (LanguagePackageNotFoundException ex)
            {
                return false;
            }

            return false;
        }
    }
}
