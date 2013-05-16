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
	}
}