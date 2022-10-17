using System.Collections.Generic;
using UnityEngine;

namespace Localizations
{
    public static class LocalizationProvider
    {
        private static string _PackageName { get; } = "Default";
        private static string _PackageCode { get; } = "en_US";
        private static string _PackageAuthor { get; } = "Default";
        private static LocalizationPackage _loadedPackage = null;

        public static string GetLocalizedString(string stringId)
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }

        public static bool LoadNewLanguagePackage(string languageCode)
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }
    }
}
