//  Author: Diego Giacomelli <giacomelli@gmail.com>
//  Copyright (c) 2012 Skahal Studios

#region Usings
using System;
#endregion

namespace Skahal.Infrastructure.Framework.Domain.KeyGenerating
{
	public interface IEntityKeyGenerator
	{
		#region Usings
		long NextKey(Type entityType);
		#endregion
	}
}

