#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Skahal.Infrastructure.Framework.Domain.KeyGenerating;


#endregion

namespace Skahal.Infrastructure.Framework.Domain
{
	/// <summary>
	/// A domain entity base class.
	/// </summary>
	[DebuggerDisplay("{Key}")]
	[Serializable] 
	public abstract class EntityBase<TKey> : IEntity
	{
		#region Fields
		private TKey m_key;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Framework.Domain.EntityBase&lt;TKey&gt;"/> class.
		/// </summary>
		protected EntityBase() : this(null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Skahal.Infrastructure.Framework.Domain.EntityBase&lt;TKey&gt;"/> class.
		/// </summary>
		/// <param name="key">Key.</param>
		protected EntityBase(long? key)
		{
			if(!key.HasValue || key.Value <= 0)
			{
				Key = EntityKeyGenerator.NextKey(GetType());
			}
			else
			{
				Key = key.Value;
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>The key.</value>
		public TKey Key
		{
			get {
				return m_key;
			}

			set {
				m_key = value;
				EntityKeyGenerator.UseKey (GetType(), m_key);
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if(obj == null || !(obj is EntityBase))
			{
				return false;
			}

			return this == (EntityBase)obj;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return Key.GetHashCode();
		}
		#endregion

		#region Operators
		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="base1">The base1.</param>
		/// <param name="base2">The base2.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator == (EntityBase<TKey> base1, EntityBase<TKey> base2)
		{
			// Check for both null (need this casts to object or will run in a recursive loop).
			if ((object)base1 == null && (object)base2 == null)
			{
				return true;
			}

			if ((object)base1 == null || (object)base2 == null)
			{
				return false;
			}

			return base1.Key == base2.Key;
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="base1">The base1.</param>
		/// <param name="base2">The base2.</param>
		/// <returns>
		/// The result of the operator.
		/// </returns>
		public static bool operator !=(EntityBase<TKey> base1, EntityBase<TKey> base2)
		{
			return !(base1 == base2);
		}
		#endregion
	}
}