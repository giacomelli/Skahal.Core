#region Usings
using UnityEngine;
using System.Collections;
using System;
using System.Text;
using Skahal.Infrastructure.Framework.Pooling;


#endregion

/// <summary>
/// Pools stats hud controller.
/// </summary>
[AddComponentMenu("Skahal/Pooling/PoolsStatsHudController")]
public class PoolsStatsHudController : MonoBehaviour
{
   #region Editor properties
   public  float UpdateInterval = 1f;
   public SHHUDControlProxyHolderBase Holder;
   #endregion

   #region Methods
   private void Start ()
   {
	   StartCoroutine(UpdateStats());
   }

   private IEnumerator UpdateStats ()
   {
      // Wait for SHPoolsManager starts.
      yield return new WaitForSeconds(1);

      var text = new StringBuilder ();
		var pools = PoolService.GetAllPools();

      while (true)
      {
         text.Remove (0, text.Length);
         text.Append ("SHPoolsManager Stats");
         text.AppendFormat ("{0}Pools: {1}", Environment.NewLine, pools.Count);

         foreach (var p in pools)
         {
            text.AppendFormat ("{0}{1}: {2,3}", Environment.NewLine, p.Name, p.ItemsCount);
         }

         Holder.ControlProxy.SetText (text.ToString ());

         yield return new WaitForSeconds(UpdateInterval);
      }
   }
   #endregion
}

