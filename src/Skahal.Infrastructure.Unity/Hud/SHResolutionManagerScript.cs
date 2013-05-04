#region Usings
using System;
using System.Collections.Generic;
using System.Reflection;
using Skahal.Infrastructure.Framework.Logging;
using UnityEngine;
#endregion 

/// <summary>
/// Manager that keeps the GUI stuffs, textures, texts and fonts in correct size depending of current resolution.
/// </summary>
[AddComponentMenu("Skahal/HUD/SHResolutionManagerScript")]
public class SHResolutionManagerScript : MonoBehaviour
{
	#region Fields
	private Dictionary<string, Font> m_fontsMap = new Dictionary<string, Font>();
	#endregion
	
	#region Properties
	public Vector2 NormalResolution = new Vector2(480, 320);
	public static float WidthScale { get; private set;}
	public static float HeightScale { get; private set; }
	
	/// <summary>
	/// Gets or sets the font used when is in HD.
	/// <remarks>
	/// The font name convetion is "<font name>_<font size>". 
	/// Then if font component in normal resolution is "font_16", you will need set a font component here like "font_32".
	/// </remarks>
	/// </summary>
	public Font[] Fonts;
	public GUISkin[] Skins;
	#endregion
	
	#region Life cicle
	void Awake()
	{
		WidthScale = Screen.width / NormalResolution.x;
		HeightScale = Screen.height / NormalResolution.y;
		
		if (WidthScale > 1)
		{
			LogService.Debug("SHResolutionManagerScript: using HD resolution.");
			
			BuildFontsMap();
			WorkOnGUISkins();
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			LogService.Debug("SHResolutionManagerScript: normal resolution, removing GameObject.");
			Destroy(this);
		}
	}
	
	void OnLevelWasLoaded(int level)
	{
		WorkOnGUITextures();
		WorkOnGUITexts();
		WorkOnScriptsFonts();
	}
	#endregion
	
	#region The real work

	void BuildFontsMap()
	{
		var length = Fonts.Length;
		
		for (var i = 0; i < length; i++)
		{
			var f = Fonts[i];
			
			m_fontsMap.Add(f.name, f);
		}	
	}
	
	void WorkOnGUITextures()
	{
		if (!Application.isEditor)
		{
			var guiTextures = (GUITexture[]) Resources.FindObjectsOfTypeAll(typeof(GUITexture));
			var length = guiTextures.Length;
		
			for (var i = 0; i < length; i++)
			{
				var t = guiTextures[i];
				t.pixelInset = RectangleHelper.Multiply(t.pixelInset, WidthScale, HeightScale);
			}
		}
	}
	
	void WorkOnGUITexts()
	{
		if (!Application.isEditor)
		{
			LogService.Debug("Working on GUITexts...");
			var guiTexts = (GUIText[]) Resources.FindObjectsOfTypeAll(typeof(GUIText));
			var length = guiTexts.Length;
			LogService.Debug(String.Format("Found {0} GUITexts on current scene.", length));
		
			for (var i = 0; i < length; i++)
			{
				var t = guiTexts[i];
				t.font = GetDoubleSizeFont(t, t.font);
				t.pixelOffset = Prepare(t.pixelOffset);
			}
		
			LogService.Debug("Work on GUITexts done.");
		}
	}

	void WorkOnGUISkins()
	{
		if (!Application.isEditor)
		{
			var length = Skins.Length;
		
			for (var i = 0; i < length; i++)
			{
				var s = Skins[i];
				s.font = GetDoubleSizeFont(s, s.font);
				s.box.font = GetDoubleSizeFont(s, s.box.font);
				s.label.font = GetDoubleSizeFont(s, s.label.font);
			}
		}
	}
	
	void WorkOnScriptsFonts()
	{
		var gos = (GameObject[]) Resources.FindObjectsOfTypeAll(typeof(GameObject));
		var gosLength = gos.Length;
		var fontType = typeof(Font);
		
		for (var i = 0; i < gosLength; i++)
		{
			var scripts = gos[i].GetComponents<MonoBehaviour>();
			var scriptsLength = scripts.Length;
			
			for (var j = 0; j < scriptsLength; j++)
			{
				var script = scripts[j];
				var properties = script.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
				var propertiesLength = properties.Length;
				
				for (var k = 0; k < propertiesLength; k++)
				{
					var p = properties[k];
					
					if (p.FieldType == fontType)
					{
						p.SetValue(script, GetDoubleSizeFont(script, (Font)p.GetValue(script)));
					}
				}
			}
		}
	}
	
	Font GetDoubleSizeFont(UnityEngine.Object parent, Font font)
	{
		if (font == null)
		{
			return null;
		}
		
		LogService.Debug(String.Format("Changing font from '{0}'.", parent.name)); 
		var fontNameParts = font.name.Split('_');
		
		if (fontNameParts.Length != 2)
		{
			var errorMsg = String.Format("The font '{0}' was not in pattern '<font name>_<font size>'. Please, rename it to fit the pattern.", font.name);
			Debug.LogError(errorMsg);
		}
		
		var fontSize = int.Parse(fontNameParts[1]);
		var doubleSizeFontName = "font_" + fontSize * 2;
		
		var msg = String.Format("Current size: {0}. New size: {1}...", fontSize, doubleSizeFontName);
		LogService.Debug(msg);
		
		if (m_fontsMap.ContainsKey(doubleSizeFontName))
		{
			return m_fontsMap[doubleSizeFontName];
		}
		else
		{
			msg = String.Format("Error changing font from '{0}', there is no font with name '{1}'.", parent.name, doubleSizeFontName);
			Debug.LogError(msg);
			return font;
		}
	}
	#endregion
	
	#region Public methods
	public static Vector2 Prepare (Vector2 vector)
	{
		vector.x *= WidthScale;
		vector.y *= HeightScale;
		
		return vector;
	}
	#endregion
}

