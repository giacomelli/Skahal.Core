#region Usings
using UnityEngine;
using Skahal.Infrastructure.Unity.Configuration;


#endregion

/// <summary>
/// Unity bootstrapper controller.
/// </summary>
[AddComponentMenu("Skahal/Configuration/UnityBootstrapperController")]
public class UnityBootstrapperController : MonoBehaviour
{
	#region Editor properties
	/// <summary>
	/// If should configure the bootstrapper for debug purposes.
	/// </summary>
	public bool IsDebug;
	#endregion

	#region Methods
	private void Awake()
	{
		var bootstrapper = new UnityBootstrapper();

		bootstrapper.IsDebug = IsDebug;

		if(!bootstrapper.Setup())
		{
			Destroy(gameObject);
		}
	}
	#endregion
}