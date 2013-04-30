#region usings
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Skahal.Infrastructure.Framework.Logging;
#endregion
		  
namespace Skahal.Infrastructure.Framework.Commons
{
	/// <summary>
	/// Infrastructure service for dependency injection.
	/// </summary>
	public static class DependencyService
	{
		#region Fields
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		private static Dictionary<Type, Func<object>> s_mapping = new Dictionary<Type, Func<object>>();
		#endregion

		#region Methods
		/// <summary>
		/// Register the specified type implementation.
		/// </summary>
		/// <param name="createImplementation">Create implementation.</param>
		/// <typeparam name="TType">The 1st type parameter.</typeparam>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public static void Register<TType>(Func<object> createImplementation)
		{
			s_mapping[typeof(TType)] = createImplementation;
		}

		/// <summary>
		/// Register the specified type instance.
		/// </summary>
		/// <param name="implementationInstance">Implementation instance.</param>
		/// <typeparam name="TType">The 1st type parameter.</typeparam>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public static void Register<TType>(object implementationInstance)
		{
			s_mapping[typeof(TType)] = () => { return implementationInstance; };
		}

		/// <summary>
		/// Create this instance for type specified.
		/// </summary>
		/// <typeparam name="TType">The 1st type parameter.</typeparam>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public static TType Create<TType> ()
		{
			var type = typeof(TType);

			if (s_mapping.ContainsKey (type)) {
				return (TType)s_mapping [type] ();
			} else {
				var msg = String.Format(CultureInfo.InvariantCulture, "There is no implementation for the type'{0}' registered in DependencyService.", type);
				throw new ArgumentException (msg, "TType");
			}
		}
		#endregion
	}
}