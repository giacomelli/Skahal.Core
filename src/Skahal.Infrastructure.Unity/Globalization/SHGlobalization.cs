using System;
using System.Globalization;
using UnityEngine;

public static class SHGlobalization
{
	#region Events
	public static event EventHandler CultureChanged;
	#endregion
	
	#region Fields
	public static readonly CultureInfo PtBrCultureInfo = new CultureInfo("pt-BR");
	public static readonly CultureInfo EnUsCultureInfo = new CultureInfo("en-US");
	private static CultureInfo s_currentCulture = new CultureInfo(SelectedCultureName);
	#endregion
	
	#region Properties
	private static string SelectedCultureName 
	{
		get { return PlayerPrefs.GetString("SHGlobalizationCultureName", "en-US"); }

		set { PlayerPrefs.SetString("SHGlobalizationCultureName", value); }
	}
	
	public static CultureInfo CurrentCulture {
		get {
			return s_currentCulture;
		}
		
		private set {
			if (!s_currentCulture.Name.Equals (value.Name))
			{
				SelectedCultureName = value.Name;
				s_currentCulture = value;
				
				if (CultureChanged != null)
				{
					CultureChanged (typeof(SHGlobalization), EventArgs.Empty);
				}
			}
		}
	}
	#endregion
	
	#region Methods
	public static void ChangeCulture (string cultureName)
	{
		CultureInfo toCulture;
		
		switch (cultureName)
		{
		case "en-US":
			toCulture = EnUsCultureInfo;
			break;
				
		case "pt-BR":
			toCulture = PtBrCultureInfo;
			break;
				
		default:
			toCulture = new CultureInfo (cultureName);
			break;
		}
		
		ChangeCulture (toCulture);
	}
	
	public static void ChangeCulture (CultureInfo toCulture)
	{
		CurrentCulture = toCulture;
	}
	#endregion
}