//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2011 Skahal Studios
using System;

namespace Skahal.Infrastructure.Framework.IO
{
	public class OfxTransaction : IOfxTransaction
	{
		#region IOfxTransaction implementation
		public DateTime Date { get; set; }

		public float Value { get; set; }

		public string Description { get; set; }
		#endregion
	}
}

