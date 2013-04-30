using UnityEngine;
using System.Collections;

[AddComponentMenu("Skahal/Globalization/SHGlobalizationScript")]
public class SHGlobalizationScript : MonoBehaviour
{
	#region Propriedades
	public static SHGlobalizationScript Instance
	{
		get;
		private set;
	}
	
	public TextAsset[] Texts;
	#endregion

	void Awake()
	{
		DontDestroyOnLoad(this);
		Instance = this;
	}
}

