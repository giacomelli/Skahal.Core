#region Usings
using UnityEngine;
using System.Collections;
#endregion

/// <summary>
/// Rectangle helper.
/// </summary>
public static class RectangleHelper
{
	#region Methods
	/// <summary>
	/// Multiply the rectangle by specified times.
	/// </summary>
	/// <param name="rectangle">Rectangle.</param>
	/// <param name="times">Times.</param>
	public static Rect Multiply(Rect rectangle, float times)
	{
		rectangle.x *= times;
		rectangle.y *= times;
		rectangle.width *= times;
		rectangle.height *= times;
		
		return rectangle;
	}

	/// <summary>
	/// Multiply the specified rectangle by the specified x and y times.
	/// </summary>
	/// <param name="rectangle">Rectangle.</param>
	/// <param name="xTimes">X times.</param>
	/// <param name="yTimes">Y times.</param>
	public static Rect Multiply(Rect rectangle, float xTimes, float yTimes)
	{
		rectangle.x *= xTimes;
		rectangle.y *= yTimes;
		rectangle.width *= xTimes;
		rectangle.height *= yTimes;
		
		return rectangle;
	}
	#endregion
}