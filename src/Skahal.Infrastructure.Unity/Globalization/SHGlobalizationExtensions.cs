#region Usings
using System.Collections.Generic;
using UnityEngine;
using Skahal.Infrastructure.Framework.Logging;
#endregion

namespace Skahal.Globalization
{
	/// <summary>
	/// Globalization extensions methods
	/// </summary>
	public static class SHGlobalizationExtensions
	{
		#region Fields
		private static Dictionary<string, Dictionary<string, string>> s_languagesAndTexts;
		private static Dictionary<string, string> s_currentTexts;
		#endregion
		
		#region Constructors
		static SHGlobalizationExtensions ()
		{
			s_languagesAndTexts = new Dictionary<string, Dictionary<string, string>> ();
			PrepareCurrentCulture();
				
			GlobalizationService.CultureChanged += delegate {
				PrepareCurrentCulture();
			};
			
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Translate the specified text to the currente language.
		/// </summary>
		public static string Translate (this string text, params object[] args)
		{
			var result = string.Format(text, args);
			
			if (s_currentTexts.ContainsKey (result)) {
				result = s_currentTexts [result];
			}
			
			return result;	
		}
		#endregion
		
		#region Private methods
		private static void PrepareCurrentCulture ()
		{
			var key = GlobalizationService.CurrentCulture.TwoLetterISOLanguageName;
			
			if (!s_languagesAndTexts.ContainsKey (key)) {
				LogService.Debug ("SHGlobalizationExtensions :: Loading texts for language '{0}'...", key);
				
				var texts = new Dictionary<string, string> ();
				
				var textsFilePath = string.Format ("texts.{0}", key);
				var fileTextAsset = (TextAsset)UnityEngine.Resources.Load (textsFilePath) as TextAsset;
				var lines = fileTextAsset.text.Split (new string[] { System.Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries);
				
				LogService.Debug ("SHGlobalizationExtensions :: {0} texts founds...", lines.Length);
				
				foreach (var line in lines) {
					var lineParts = line.Split ('=');
					texts.Add (lineParts [0].Trim (), lineParts [1].Trim ().Replace(@"\n", System.Environment.NewLine));
				}

				s_languagesAndTexts.Add (key, texts);	
			}
			
			s_currentTexts = s_languagesAndTexts [key];
		}
		#endregion
	}
}