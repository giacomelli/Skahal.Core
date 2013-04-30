#region Usings
using UnityEngine;
#endregion

/// <summary>
/// Defines the base behaviors for menu controller.
/// </summary>
public abstract class SHMenuControllerBase : MonoBehaviour
{
	#region Properties
	/// <summary>
	/// Gets or sets a value indicating whether this instance is visible.
	/// </summary>
	public bool IsVisible { get; set; }
	#endregion
	
	#region Life cycle
	private void Start ()
	{
		SHGlobalization.CultureChanged += HandleCultureChanged;
		
		Initialize ();
		Globalize ();
	}
	
	private void OnDestroy ()
	{
		SHGlobalization.CultureChanged -= HandleCultureChanged;
	}
	
	private void HandleCultureChanged (object sender, System.EventArgs e)
	{
		Globalize();
	}
	
	public void MoveForward ()
	{	
		Globalize ();
		Show ();
		IsVisible = true;
	}
	
	public void MoveBackward ()
	{
		Hide ();
		IsVisible = false;
	}
	#endregion
	
	#region Virtual methods
	protected virtual void Initialize ()
	{
	
	}
	
	protected virtual void Show ()
	{
		
	}
	
	protected virtual void Hide ()
	{
		
	}
	
	protected virtual void Globalize ()
	{
		
	}
	#endregion
}