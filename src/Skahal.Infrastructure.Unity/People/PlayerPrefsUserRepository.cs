#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using UnityEngine;
using Skahal.Infrastructure.Unity.Serialization;
using Skahal.Infrastructure.Framework.People;
using Skahal.Infrastructure.Unity.Repositories;
#endregion

namespace Skahal.Infrastructure.Unity.People
{
	/// <summary>
	/// An IUserRepository implementation using PlayerPrefs
	/// </summary>
	public class PlayerPrefsUserRepository : PlayerPrefsRepositoryBase<User, string>, IUserRepository
	{
		#region implemented abstract members of PlayerPrefsRepositoryBase
		/// <summary>
		/// Converts from.
		/// </summary>
		/// <returns>The from.</returns>
		/// <param name="key">Key.</param>
		protected override string ConvertFrom (string key)
		{
			return key;
		}
		#endregion
 
	}
}