using System.Collections.Generic;
using UnityEngine;

namespace Localizations
{
    public static class LocalizationProvider
    {
        private static List<string> _loadedLanguagesNames = new List<string>();
        private static List<string> _loadedLanguagesCodes = new List<string>();
        private static Dictionary<string, LocalizationPackage> _localizationPackages = new Dictionary<string, LocalizationPackage>();
        private static int _amountOfLoadedPackages = 0;

        public static List<string> LoadedLanguagesNames { get { return _loadedLanguagesNames; } }
        public static List<string> LoadedLanguagesCodes { get { return _loadedLanguagesCodes; } }
        public static int AmountOfLoadedPackages { get { return _amountOfLoadedPackages; } }

        private static void LoadEnglishPackage()
        {
            LocalizationPackage englishPackage = new LocalizationPackage("English", "en_US", "Cute Programmes");

            for (int i = 0; i < EnglishLocalization.EnglishPackage.Length; i++)
            {
                englishPackage.Strings.Add(EnglishLocalization.EnglishPackage[i], EnglishLocalization.EnglishPackage[i + 1]);
            }

            _loadedLanguagesNames.Add(englishPackage.PackageName);
            _loadedLanguagesCodes.Add(englishPackage.PackageLanguageCode);
            _localizationPackages.Add(englishPackage.PackageLanguageCode, englishPackage);
            _amountOfLoadedPackages++;

            Debug.Log($"LOCALIZATION_MANAGER: Loaded default english localization package with {englishPackage.Strings.Count} entries.");
        }

        public static void InitializeLocalizationPackages()
        {
            LoadEnglishPackage();
        }

        public static bool ReloadAllPackages()
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }

        public static bool ReloadSpecificLanguagePackageByLanguageCode(string languageCode)
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }

        public static string GetLocalizedString(string stringId)
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }
    }
}
