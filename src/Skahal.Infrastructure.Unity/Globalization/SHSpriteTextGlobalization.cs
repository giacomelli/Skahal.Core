using UnityEngine;
using System.Collections;
using System.Globalization;
using System;
using Skahal.Infrastructure.Framework.Logging;

public static class SHSpriteTextGlobalization
{
	public static Func<CultureInfo, CultureInfo, string, string> GlobalizeText;
	
	public static void GlobalizeAllSceneSpriteTexts (CultureInfo fromCulture, CultureInfo toCulture)
	{
		var spriteTexts = (SpriteText[])GameObject.FindSceneObjectsOfType (typeof(SpriteText));
		LogService.Debug ("SHSpriteTextGlobalization: " + spriteTexts.Length);
		
		foreach (SpriteText t in spriteTexts)
		{
			t.Text = GlobalizeText(fromCulture, toCulture, t.text);	
		}
	}
}

