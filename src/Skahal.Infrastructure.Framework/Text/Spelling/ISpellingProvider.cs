using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Skahal.Infrastructure.Framework.Text.Spelling
{
    public interface ISpellingProvider
    {
        #region Methods
        void Initialize();
        string GetCorrect(string text, CultureInfo culture);
        #endregion
    }
}
