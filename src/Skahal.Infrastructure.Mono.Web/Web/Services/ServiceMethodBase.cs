//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios
using System;
using System.Web;
using Vici.Core.Json;
using System.Net;
using System.Net.Sockets;

namespace Skahal.Infrastructure.Mono.Web.Services
{
	/// <summary>
	/// Service method base.
	/// </summary>
	public abstract class ServiceMethodBase<TContext, TResult> : IHttpHandler 
		where TContext : ServiceMethodContextBase, new()
	{
		#region IHttpHandler implementation
		/// <summary>
		/// Execute the specified context.
		/// </summary>
		/// <param name="context">Context.</param>
		protected abstract TResult Execute(TContext context);

		/// <summary>
		/// Processes the request.
		/// </summary>
		/// <param name="context">Context.</param>
		public virtual void ProcessRequest (HttpContext context)
		{
			try
			{	
				var ctx = new TContext();
				ctx.Initialize(context.Request);
				var result = Execute(ctx);
				
				if(result != null)
				{
					context.Response.Write(Json.Stringify(result));
				}
			}
			catch(Exception ex)
			{
				context.Response.Write(ex.Message + "<br><br>" + ex.StackTrace);
			}
		}
 
		/// <summary>
		/// Gets a value indicating whether this instance is reusable.
		/// </summary>
		/// <value><c>true</c> if this instance is reusable; otherwise, <c>false</c>.</value>
		public bool IsReusable {
			get {
				return true;
			}
		}
		#endregion		
	}
}