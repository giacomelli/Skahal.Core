#region Usings
using UnityEngine;
using Skahal.Infrastructure.Framework.Logging;
using System.Text;
using System.Collections.Generic;
using System;
#endregion

[AddComponentMenu("Skahal/Logging/LoggingConsole")]
public class LoggingConsole : MonoBehaviour
{
	#region Fields
	private List<string> m_log;
	private int m_lineCounter;
	#endregion

	#region Constructors
	/// <summary>
	/// Initializes a new instance of the <see cref="LoggingConsole"/> class.
	/// </summary>
	public LoggingConsole()
	{
		Instance = this;
	}
	#endregion

	#region Editor properties
	public SHHUDControlProxyHolderBase Holder;
	public bool ShowDebug;
	public bool ShowWarning;
	public bool ShowError;
	public string TextFilter;
	public int MaxLines = 5; 
	#endregion

	#region Properties
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static LoggingConsole Instance { get; private set; } 
	#endregion
	
	#region Life cycle
	private void Start ()
	{
		DontDestroyOnLoad(this);
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
		if (showLog && (string.IsNullOrEmpty(TextFilter) || e.Message.Contains(TextFilter))) {
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