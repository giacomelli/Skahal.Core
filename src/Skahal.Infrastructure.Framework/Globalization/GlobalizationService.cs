#region Usings
using System;
using System.Globalization;
using Skahal.Infrastructure.Framework.People;


#endregion

/// <summary>
/// Globalization service.
/// </summary>
public static class GlobalizationService
{
	#region Events
	/// <summary>
	/// Occurs when culture changed.
	/// </summary>
	public static event EventHandler CultureChanged;
	#endregion

	#region Constants
	private const string CultureNamePreferenceKey = "SHGlobalizationCultureName";
	#endregion
	
	#region Fields
	/// <summary>
	/// Brazilian Portugues culture information.
	/// </summary>
	public static readonly CultureInfo PtBrCultureInfo = new CultureInfo("pt-BR");

	/// <summary>
	/// United States English culture information.
	/// </summary>
	public static readonly CultureInfo EnUsCultureInfo = new CultureInfo("en-US");


	private static CultureInfo s_currentCulture = new CultureInfo(SelectedCultureName);
	#endregion
	
	#region Properties
	/// <summary>
	/// Gets the current culture.
	/// </summary>
	/// <value>The current culture.</value>
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
					CultureChanged (typeof(GlobalizationService), EventArgs.Empty);
				}
			}
		}
	}

	private static string SelectedCultureName 
	{
		get 
		{
			var user = UserService.GetCurrentUser();

			if(user.HasPreference(CultureNamePreferenceKey))
			{
				return user.GetPreference(CultureNamePreferenceKey).Value.ToString();
			}

			return "en-US";
		}
		
		set 
		{ 
			var user = UserService.GetCurrentUser();
			user.SetPreference(CultureNamePreferenceKey, value);
			UserService.SaveCurrentUser(user);
		}
	}
	#endregion
	
	#region Public methods
	/// <summary>
	/// Changes the culture.
	/// </summary>
	/// <param name="cultureName">Culture name.</param>
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

	/// <summary>
	/// Changes the culture.
	/// </summary>
	/// <param name="toCulture">To culture.</param>
	public static void ChangeCulture (CultureInfo toCulture)
	{
		CurrentCulture = toCulture;
	}
	#endregion
}