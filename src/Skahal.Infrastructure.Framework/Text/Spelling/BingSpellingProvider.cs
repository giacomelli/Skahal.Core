//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Bing;
//using System.Globalization;
//using Skahal.Infrastructure.Framework.Commons;
//using Skahal.Infrastructure.Framework.Logging;
//
//namespace Skahal.Infrastructure.Framework.Text.Spelling
//{
//    public class BingSpellingProvider : ISpellingProvider
//    {
//        #region ISpellingProvider Members
//
//        public void Initialize()
//        {
//            
//        }
//
//        public string GetCorrect(string text, CultureInfo culture)
//        {                     
//            SearchRequest searchRequest = new SearchRequest
//            {
//                AppId = AppInfo.Instance.SpellingApiId,
//                Query = text,
//                Market = culture.Name,
//                UILanguage = culture.TwoLetterISOLanguageName
//            };
//			
//			try
//			{
//	            var response = API.Spell(searchRequest, new SpellRequest());
//	
//		        if (response.Total > 0)
//	            {
//	                return response.Results[0].Value;
//	            }
//	            else
//	            {
//	                return text;
//	            }
//			}
//			catch(Exception ex)
//			{
//				SHLog.Debug("BingSpellingProvider.GetCorrect: cant check the spelling for '{0}': {1}", text, ex.Message);
//			}
//			
//			return text;
//        }
//
//        #endregion
//    }
//}