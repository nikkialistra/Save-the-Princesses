using UnityEngine;
using System;

namespace Localizations
{
    public class LanguagePackageNotFoundException : Exception
    {
        public LanguagePackageNotFoundException(string packageLanguageCode) { Debug.LogException(this, null); }
    }

    public class LanguagePackageNotLoaded : Exception
    {
        public LanguagePackageNotLoaded() { Debug.LogException(this, null); }
    }

    public class LocalizedStringNotFound : Exception
    {
        public LocalizedStringNotFound() { Debug.LogException(this, null); }
    }
}
