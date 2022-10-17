using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Localizations
{
    public static class LocalizationLoader
    {
        private static string _pathToPackages = "Localizations\\";
        
        private static List<string> _loadedLanguagesNames { get; } = new List<string>();
        private static List<string> _loadedLanguagesCodes { get; } = new List<string>();

        private static Dictionary<string, LocalizationPackage> _localizationPackages = new Dictionary<string, LocalizationPackage>();
        private static int _amountOfLoadedPackages = 0;
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
            _amountOfLoadedPackages++;

            Debug.Log($"LOCALIZATION_LOADER: Loaded default english localization package with {englishPackage.Strings.Count} entries.");
        }


        public static void LoadAllAvailablePackages()
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }


        public static LocalizationPackage GetPackageByCode(string languageCode)
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }


        public static bool ReloadAllPackages()
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }


        public static bool ReloadSpecificLanguagePackageByLanguageCode(string languageCode)
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }
    }
}
