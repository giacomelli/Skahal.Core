#region Usings
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
#endregion

namespace Skahal.Infrastructure.Unity.Serialization
{
	/// <summary>
	/// A serialization helper.
	/// </summary>
	public static class SerializationHelper
	{
		#region Delegate
		delegate string SerializeHandler<TObject>(TObject obj);
		delegate TObject DeserializeHandler<TObject>(string serialized);
		#endregion
		
		#region System.Type
		/// <summary>
		/// Serializes the type.
		/// </summary>
		/// <returns>The type.</returns>
		/// <param name="type">Type.</param>
		public static string SerializeType(System.Type type)
		{
			return type.FullName;
		}

		/// <summary>
		/// Deserializes the type.
		/// </summary>
		/// <returns>The type.</returns>
		/// <param name="typeSerialized">Type serialized.</param>
		public static System.Type DeserializeType(string typeSerialized)
		{
			return System.Type.GetType(typeSerialized);
		}

		/// <summary>
		/// Serializes the types.
		/// </summary>
		/// <returns>The types.</returns>
		/// <param name="types">Types.</param>
		public static string SerializeTypes(IList<System.Type> types)
		{
			return SerializeMany<System.Type>(types, SerializeType);
		}

		/// <summary>
		/// Deserializes the types.
		/// </summary>
		/// <returns>The types.</returns>
		/// <param name="typesSerialized">Types serialized.</param>
		public static IList<System.Type> DeserializeTypes(string typesSerialized)
		{
			return DeserializeMany<System.Type>(typesSerialized, DeserializeType);
		}
		#endregion
		
		#region Vector2
		/// <summary>
		/// Serializes the Vector2.
		/// </summary>
		/// <returns>The vector2.</returns>
		/// <param name="vector2">Vector2.</param>
		public static string SerializeVector2(Vector2 vector2)
		{
			return vector2.x + "#" + vector2.y;
		}

		/// <summary>
		/// Deserializes the Vector2.
		/// </summary>
		/// <returns>The vector2.</returns>
		/// <param name="serialized">Serialized.</param>
		public static Vector2 DeserializeVector2(string serialized)
		{
			var parts = serialized.Split('#');
			return new Vector2(System.Convert.ToSingle(parts[0]), System.Convert.ToSingle(parts[1]));
		}

		/// <summary>
		/// Serializes the Vector2s.
		/// </summary>
		/// <returns>The vector2s.</returns>
		/// <param name="vector2s">Vector2s.</param>
		public static string SerializeVector2s(IList<Vector2> vector2s)
		{
			return SerializeMany<Vector2>(vector2s, SerializeVector2);
		}

		/// <summary>
		/// Deserializes the Vector2s.
		/// </summary>
		/// <returns>The vector2s.</returns>
		/// <param name="serialized">Serialized.</param>
		public static IList<Vector2> DeserializeVector2s(string serialized)
		{
			return DeserializeMany<Vector2>(serialized, DeserializeVector2);
		}
		#endregion
		
		#region To and from Objects
		/// <summary>
		/// Serializes to string.
		/// </summary>
		/// <returns>The to string.</returns>
		/// <param name="target">Target.</param>
		public static string SerializeToString (object target)
		{
			var bytes = SerializeToBytes (target);
			var base64 = System.Convert.ToBase64String(bytes);
			return base64;
		}

		/// <summary>
		/// Deserializes from string.
		/// </summary>
		/// <returns>The from string.</returns>
		/// <param name="targetString">Target string.</param>
		/// <typeparam name="TTarget">The 1st type parameter.</typeparam>
		public static TTarget DeserializeFromString<TTarget> (string targetString)
		{
			byte[] targetBytes = System.Convert.FromBase64String(targetString);
			return DeserializeFromBytes<TTarget>(targetBytes);
		}
		#endregion
		
		#region To and from bytes
		/// <summary>
		/// Serializes to bytes.
		/// </summary>
		/// <returns>The to bytes.</returns>
		/// <param name="target">Target.</param>
		/// <typeparam name="TTarget">The 1st type parameter.</typeparam>
		public static byte[] SerializeToBytes<TTarget> (TTarget target)
		{
			var formatter = new BinaryFormatter ();
			
			using (var stream = new MemoryStream ()) {
				formatter.Serialize (stream, target);
				var bytes = stream.ToArray ();	
				return bytes;
			}
		}

		/// <summary>
		/// Deserializes from bytes.
		/// </summary>
		/// <returns>The from bytes.</returns>
		/// <param name="targetBytes">Target bytes.</param>
		/// <typeparam name="TTarget">The 1st type parameter.</typeparam>
		public static TTarget DeserializeFromBytes<TTarget> (byte[] targetBytes)
		{
			var formatter = new BinaryFormatter ();
			
			using (var stream = new MemoryStream (targetBytes)) {
				return (TTarget) formatter.Deserialize(stream);
			}
		}
		#endregion
		
		#region Helpers
		static string SerializeMany<TObject>(IList<TObject> objects, SerializeHandler<TObject> serializeHandler)
		{
			var length = objects.Count;
			var serialization = new string[length];
			
			for (int i = 0; i < length; i++)
			{
				var obj = objects[i];
				//LogService.Debug("Serializer.SerializeMany - Calling item serializer: " +  obj);
				serialization[i] = serializeHandler(obj);
			}
			
			return string.Join("|", serialization);
		}
		
		static IList<TObject> DeserializeMany<TObject>(string serialized, DeserializeHandler<TObject> deserializeHandler)
		{
			var serialization = serialized.Split('|');
			var length = serialization.Length;
			var objects = new List<TObject>(length);
			
			for (int i = 0; i < length; i++)
			{
				var ser = serialization[i];
				//LogService.Debug("Serializer.DeserializeMany - Calling item deserializer: " + ser);
				objects.Add(deserializeHandler(ser));
			}
			
			return objects;
		}
		#endregion
	}
}