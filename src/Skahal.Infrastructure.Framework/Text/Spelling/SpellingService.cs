#region Usings
using System;
using System.Globalization;
using Skahal.Infrastructure.Framework.Commons;
#endregion

namespace Skahal.Infrastructure.Framework.Text.Spelling
{
    public static class SpellingService
    {
        #region Fields
        private static ISpellingProvider s_provider;
        #endregion

        #region Public Methods
        public static void Initialize(ISpellingProvider provider)
        {
            s_provider = provider;
            provider.Initialize();
        }

        public static void Initialize()
        {
           // s_provider = new BingSpellingProvider();
        }

        public static string GetCorrect(string text, CultureInfo culture)
        {
			return s_provider.GetCorrect(text.Trim(), culture);
        }

        public static string GetCorrect(string text)
        {
            return GetCorrect(text, CultureInfo.CurrentUICulture);
        }

        public static bool IsCorrect(string text, CultureInfo culture)
        {
			var textTrimmed = text.Trim();
        	return s_provider.GetCorrect(textTrimmed, culture).Equals(textTrimmed, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsCorrect(string text)
        {
            return IsCorrect(text, CultureInfo.CurrentUICulture);
        }
        #endregion
    }
}