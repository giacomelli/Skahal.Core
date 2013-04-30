//#region Usings
//using UnityEngine;
//using System.Collections;
//
//#endregion
//
///// <summary>
///// Implements a classic pool for game objects derived from a Fx Maker's prefab.
///// </summary>
//[AddComponentMenu("Skahal/Pool/SHFxMakerPrefabPool")]
//public class SHFxMakerPrefabPool : SHPrefabPool
//{ 
//	protected override GameObject CreateObject ()
//	{
//		var obj = base.CreateObject ();
//		NcEffectBehaviour.PreloadTexture (obj);
//		
//		return obj;
//	}
//	
//	protected override void EnableObject (GameObject goInPool)
//	{
//		base.EnableObject(goInPool);
//	}
//	
//	protected override void DisableObject (GameObject goInPool)
//	{
//		base.DisableObject (goInPool);
//	}
//}