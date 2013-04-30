using UnityEngine;
 
public static class SHGUILayout
{
	#region Button
	public static bool Button(Texture image, string text)
	{
		if (GUILayout.Button(image, text))
		{
			//SHSoundManager.PlayButtonClickSound();
			return true;
		}
		
		return false;
	}
	
	public static bool Button(Texture image)
	{
		if (GUILayout.Button(image))
		{
		//	SHSoundManager.PlayButtonClickSound();
			return true;
		}
		
		return false;
	}
	
	public static bool Button(string text)
	{
		if (GUILayout.Button(text))
		{
			//SHSoundManager.PlayButtonClickSound();
			return true;
		}
		
		return false;
	}
	
	public static bool Button(GUIContent content, params GUILayoutOption[] options)
	{
		if (GUILayout.Button(content, options))
		{
		//	SHSoundManager.PlayButtonClickSound();
			return true;
		}
		
		return false;
	}
	#endregion
}

