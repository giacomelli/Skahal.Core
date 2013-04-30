using UnityEngine;
using System.Collections;

public static class SHRect
{
	public static Rect Multiply(Rect rect, float times)
	{
		rect.x *= times;
		rect.y *= times;
		rect.width *= times;
		rect.height *= times;
		
		return rect;
	}
	
	public static Rect Multiply(Rect rect, float xTimes, float yTimes)
	{
		rect.x *= xTimes;
		rect.y *= yTimes;
		rect.width *= xTimes;
		rect.height *= yTimes;
		
		return rect;
	}
}

