#region Usings
using System;
using System.Text;
#endregion

namespace Skahal.Infrastructure.Framework.Text
{
	/// <summary>
	/// Extensions methods for strings.
	/// </summary>
	public static class StringExtensions
	{
		#region Capitalize
		
		/// <summary>
		/// Capitalize the string.
		/// </summary>
		/// <param name="value">
		/// The original string. <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// The capitalized version of the string. <see cref="System.String"/>
		/// </returns>
		public static string Capitalize (this string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException ("value");
			}
			
			if (value.Length == 0)
			{
				return value;
			}
			
			var result = new StringBuilder (value);
			result[0] = char.ToUpper (result[0]);
			
			for (int i = 1; i < result.Length; ++i)
			{
				if (char.IsWhiteSpace (result[i - 1]))
				{
					result[i] = char.ToUpper(result[i]);
				}
				else
				{
					result[i] = char.ToLower(result[i]);
				}
			}
			
			return result.ToString();
		}
		#endregion
		
		#region Contains
		public static bool ContainsIgnoreCase(this string source, string value)
		{
			return source.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) > -1;
		}
		#endregion
		
		#region Limit
		public static string Limit(this string source, int maxLength)
		{
			if(!String.IsNullOrEmpty(source) && source.Length > maxLength)
			{
				return source.Substring(0, maxLength);
			}
			
			return source;
		}
		#endregion
	}
}

