using System;
using ProtoBuf.Meta;
using Skahal.Infrastructure.Framework.People;
using System.IO;

namespace Skahal.Tools.ProtobufCompiledSerializerGen
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("<GENERATING COMPILED PROTOBUF SERIALIZERS>");

			var model = TypeModel.Create();

			Console.WriteLine ("USER");
			model.Add(typeof(User), true).Add("Id", "Name", "RemoteId");
			model.Compile("ProtobufSerializer", "Skahal.Infrastructure.Framework.ProtobufSerializer.dll");
			File.Copy ("Skahal.Infrastructure.Framework.ProtobufSerializer.dll", "../../../References/Skahal.Infrastructure.Framework.ProtobufSerializer.dll", true);

			Console.WriteLine ("</GENERATING COMPILED PROTOBUF SERIALIZERS>");
		}
	}
}