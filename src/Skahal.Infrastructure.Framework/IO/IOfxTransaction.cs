//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2011 Skahal Studios
using System;

namespace Skahal.Infrastructure.Framework.IO
{
	public interface IOfxTransaction
	{
		#region Properties
		DateTime Date { get; set; }
		float Value { get; set; }
		string Description { get; set; }
		#endregion
	}
}

