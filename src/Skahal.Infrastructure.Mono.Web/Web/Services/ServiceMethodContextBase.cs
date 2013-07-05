//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios
using System.Web;


#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Mono.Web.Services
{
	/// <summary>
	/// Service method context base.
	/// </summary>
	public abstract class ServiceMethodContextBase
	{	
		#region Methods
		/// <summary>
		/// Initialize the specified request.
		/// </summary>
		/// <param name="request">Request.</param>
		internal void Initialize (HttpRequest request)
		{
			Request = request;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the request.
		/// </summary>
		/// <value>The request.</value>
		protected HttpRequest Request { get; private set; }
		#endregion

	}
}