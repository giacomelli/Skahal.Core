using System;
using ProtoBuf.Meta;
using Skahal.Infrastructure.Framework.People;
using System.IO;
using Skahal.Infrastructure.Framework.Domain;

namespace Skahal.Tools.ProtobufCompiledSerializerGen
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("<GENERATING COMPILED PROTOBUF SERIALIZERS>");

			var model = TypeModel.Create();

			Console.WriteLine ("ENTITYBASE");
			model.Add(typeof(EntityBase<string>), true).Add("Key");

			Console.WriteLine ("USER");
			model.Add(typeof(User), true).Add("Name", "RemoteKey");
			model.Compile("ProtobufSerializer", "Skahal.Infrastructure.Framework.ProtobufSerializer.dll");
			File.Copy ("Skahal.Infrastructure.Framework.ProtobufSerializer.dll", String.Format(@"..{0}..{0}..{0}References{0}Skahal.Infrastructure.Framework.ProtobufSerializer.dll", Path.DirectorySeparatorChar), true);

			Console.WriteLine ("</GENERATING COMPILED PROTOBUF SERIALIZERS>");
		}
	}
}