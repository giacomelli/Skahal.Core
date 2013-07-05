using System;
using Skahal.Infrastructure.Framework.Globalization;
using System.IO.Abstractions;

namespace Skahal.Infrastructure.Framework.Globalization
{
	public class FileGlobalizationLabelRepository : TextGlobalizationLabelRepositoryBase
	{
		#region Fields
		private string m_filesDirectory;
		#endregion

		#region Constructors
		public FileGlobalizationLabelRepository (string filesDirectory)
		{
			m_filesDirectory = filesDirectory;
		}
		#endregion

		#region implemented abstract members of TextGlobalizationLabelRepositoryBase
		/// <summary>
		/// Gets the culture text.
		/// </summary>
		/// <returns>The culture text.</returns>
		/// <param name="cultureName">Culture name.</param>
		protected internal override string GetCultureText (string cultureName)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}