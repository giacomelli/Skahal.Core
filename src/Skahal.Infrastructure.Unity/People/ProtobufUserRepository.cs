#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.People;
using Skahal.Infrastructure.Framework.Repositories;
using UnityEngine;
using Skahal.Infrastructure.Unity.Repositories;
using Skahal.Infrastructure.Unity.Serialization;
using ProtoBuf.Meta;
#endregion

namespace Skahal.Infrastructure.Unity.People
{
	/// <summary>
	/// An IUserRepository implementation using Protobuf.
	/// </summary>
	public class ProtobufUserRepository : ProtobufRepositoryBase<User>, IUserRepository
	{ 
		#region Constructors
		/// <summary>
		/// Initializes the <see cref="Skahal.Infrastructure.Unity.People.ProtobufUserRepository"/> class.
		/// </summary>
		static ProtobufUserRepository() 
		{
			RuntimeTypeModel.Default.Add(typeof(User), true)
				.Add("Id", "Name", "RemoteId");
		}
		#endregion
	}
}