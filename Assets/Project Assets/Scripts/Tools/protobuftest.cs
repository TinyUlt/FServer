using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.Examples.AddressBook;
using Google.Protobuf.Gt;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Text;
public class protobuftest : MonoBehaviour {

	byte[] codeData( byte[] data )
	{
		byte []key = {69, 123, 132, 104, 67, 95, 33, 74, 120, 131, 61, 101, 55, 101, 69, 44};

		for (int i = 0; i< data.Length; i++)
		{

			data[i]^= key[(i) % key.Length];
		}
		return data;
	}
	void Start () {
		LoginRequest john = new LoginRequest
		{
			Name ="",
			Sessionid = "sess57b920f05ad4e",
			Password = "",
			Uid = "10002",
//			Id = 1234,
//			Name = "John Doe",
//			Email = "jdoe@example.com",
//			Phone = { new Person.Types.PhoneNumber { Number = "555-4321", Type = Person.Types.PhoneType.Home } }
		};

		var data = john.ToByteArray ();


		var john2 = DescriptorProto.Parser.ParseFrom (data);

		var nanana = john2.Name;
//		using (var output = File.Create("john.dat"))
//		{
//			john.WriteTo(output);
//		}	
		//var s = john.
//
		//Person john2;
//		using (var input = File.OpenRead("john.dat"))
//		{
//			byte[] b = new byte[1024];
//			UTF8Encoding temp = new UTF8Encoding(true);
//
//			while (input.Read(b,0,b.Length) > 0) 
//			{
//				Debug.Log(temp.GetString(b));
//			}
//			//john2 = Person.Parser.ParseFrom(input);
//		}
		//var a = john2.Uid;

		//DescriptorProto


		Debug.Log ("aaa");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
