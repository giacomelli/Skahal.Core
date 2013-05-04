#region Usings
using UnityEngine;
using Skahal.Infrastructure.Framework.Logging;
using System.Text;
using System.Collections.Generic;
using System;
#endregion

[AddComponentMenu("Skahal/Logging/SHLoggingHUDController")]
public class SHLoggingHUDController : MonoBehaviour
{
	#region Fields
	private List<string> m_log;
	private int m_lineCounter;
	#endregion
	
	#region Properties
	public SHHUDControlProxyHolderBase Holder;
	public bool ShowDebug;
	public bool ShowWarning;
	public bool ShowError;
	public int MaxLines = 5;
	#endregion
	
	#region Life cycle
	private void Start ()
	{
		m_log = new List<string> ();
		
		LogService.DebugWritten += HandleDebugWritten;
		LogService.WarningWritten += HandleWarningWritten;	
		LogService.ErrorWritten += HandleErrorWritten;
	}

	void HandleDebugWritten (object sender, LogWrittenEventArgs e)
	{
		SetText (ShowDebug, "DEBUG", e);
	}
	
	void HandleWarningWritten (object sender, LogWrittenEventArgs e)
	{
		SetText (ShowWarning, "WARNING", e);
	}
	
	void HandleErrorWritten (object sender, LogWrittenEventArgs e)
	{
		SetText (ShowError, "ERROR", e);
	}
	
	private void OnDestroy ()
	{
		LogService.DebugWritten -= HandleDebugWritten;
		LogService.WarningWritten -= HandleWarningWritten;
		LogService.ErrorWritten -= HandleErrorWritten;
	}
		
	private void SetText (bool showLog, string prefix, LogWrittenEventArgs e)
	{
		if (showLog) {
			m_lineCounter++;
			m_log.Add (string.Format ("{0}: [{1}]{2}", m_lineCounter, prefix, e.Message));
			
			if (m_log.Count > MaxLines) {
				m_log.RemoveAt (0);
			}

			Holder.ControlProxy.SetText (string.Join (Environment.NewLine, m_log.ToArray ()));
		}
	}
	#endregion
}