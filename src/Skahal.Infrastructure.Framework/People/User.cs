using System;
using System.Collections.Generic;
using Skahal.Infrastructure.Framework.Domain;
using System.Linq;

namespace Skahal.Infrastructure.Framework.People
{
	/// <summary>
	/// Represents a user.
	/// </summary>
	[Serializable]
	public class User : EntityBase, IAggregateRoot
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Framework.People.User"/> class.
		/// </summary>
		public User()
		{
			Preferences = new List<UserPreference>();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the remote id.
		/// </summary>
		/// <value>The name.</value>
		public string RemoteId { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets the preferences.
		/// </summary>
		/// <value>The preferences.</value>
		public IList<UserPreference> Preferences { get; internal protected set; }
		#endregion

		#region Methods
		/// <summary>
		/// Gets a preference by name.
		/// </summary>
		/// <returns>The preference by name.</returns>
		/// <param name="name">Name.</param>
		public UserPreference GetPreference(string name)
		{
			return Preferences.FirstOrDefault(p => p.Name.Equals(name, StringComparison.CurrentCulture));
		} 

		/// <summary>
		/// Determines whether this instance has preference with the specified name.
		/// </summary>
		/// <returns><c>true</c> if this instance has preference with the specified name; otherwise, <c>false</c>.</returns>
		/// <param name="name">Name.</param>
		public bool HasPreference(string name)
		{
			return Preferences.Any(p => p.Name.Equals(name, StringComparison.CurrentCulture));
		}

		/// <summary>
		/// Sets the preference.
		/// </summary>
		/// <returns><c>true</c>, if was set a new preference (there is no preference with the specified name before), <c>false</c> otherwise.</returns>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		public bool SetPreference(string name, object value)
		{
			bool result = false;
			var preference = GetPreference(name);

			if(preference == null)
			{
				preference = new UserPreference();
				Preferences.Add(preference);
				result = true;
			}

			preference.Name = name;
			preference.Value = value;

			return result;
		}
		#endregion
	}
}