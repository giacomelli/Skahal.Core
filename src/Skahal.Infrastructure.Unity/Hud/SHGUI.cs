using UnityEngine;

[AddComponentMenu("Skahal/HUD/SHGUI")]
public class SHGUI : MonoBehaviour
{
	#region Campos
	//private static SHGUI s_instance;
	#endregion
	
	#region Propriedades
	#endregion

	#region Metodos
	void Awake()
	{
		DontDestroyOnLoad(this);
		//s_instance = this;
	}
	
	#region Button
	public static bool Button(Rect rect, Texture image)
	{
		if (GUI.Button(rect, image))
		{
			//SHSoundManager.PlayButtonClickSound();
			return true;
		}
		
		return false;
	}
	
	public static bool Button(Rect rect, GUIContent contents)
	{
		if (GUI.Button(rect, contents))
		{
			//SHSoundManager.PlayButtonClickSound();
			return true;
		}
		
		return false;
	}
	#endregion
	
	#endregion
}

