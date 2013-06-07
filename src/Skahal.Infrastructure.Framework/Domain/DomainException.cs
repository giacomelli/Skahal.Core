//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2011 Skahal Studios

#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.Domain
{
	public class DomainException : Exception
	{
		#region Constructors
		public DomainException (string message) : base(message)
		{
		}
		
		public DomainException(string message, Exception innerException) : base(message, innerException)
		{
		}
		#endregion
	}
}