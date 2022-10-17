using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Localizations
{
    public static class LocalizationLoader
    {
        // Defines where Localization loader will search for packages.
        private static string _pathToPackages = "Localizations\\";
        
        private static List<string> _loadedLanguagesNames { get; } = new List<string>();
        private static List<string> _loadedLanguagesCodes { get; } = new List<string>();

        private static Dictionary<string, LocalizationPackage> _localizationPackages = new Dictionary<string, LocalizationPackage>();
        private static int _amountOfLoadedPackages = 0;
        public static int AmountOfLoadedPackages { get { return _amountOfLoadedPackages; } }

        /// <summary>
        /// English package present in EnglishLocalization as a default, always available language packages. So, this function loads it.
        /// </summary>
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

        /// <summary>
        /// This function will clear all already loaded packages, and then will try to load them from dev-desired folder.
        /// </summary>
        public static void LoadAllAvailablePackages()
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }

        /// <summary>
        /// This function can give entire package to work with it.
        /// </summary>
        /// <param name="languageCode">Language code like en_US.</param>
        /// <returns>Language package.</returns>
        public static LocalizationPackage GetPackageByCode(string languageCode)
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }

        /// <summary>
        /// This function will try to reload all packages. If they files was removed, loaded package will not be unloaded and reloaded at all!
        /// </summary>
        /// <returns>If everything is ok, you`ll get TRUE.</returns>
        public static bool ReloadAllPackages()
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }

        /// <summary>
        /// Reloads specific language package by language code like en_US.
        /// </summary>
        /// <param name="languageCode">Language code.</param>
        /// <returns>Returns TRUE if everything ok.</returns>
        public static bool ReloadSpecificLanguagePackageByLanguageCode(string languageCode)
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod().Name} not implemented!");
        }
    }
}
