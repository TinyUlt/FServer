using UnityEngine;
using System.Collections;
using System.Reflection;

using dword = System.UInt32;
using word = System.UInt16;
using tchar = System.Char;
using longlong = System.Int64;
using System;
using System.Linq;
using System.Collections.Generic;

public class NetPacket
{
	
    public int mainCmd { get { return m_mainCmd; } set { m_mainCmd = value; } }
    public int subCmd { get { return m_subCmd; } set { m_subCmd = value; } }
	public int socketType{ get { return m_socketType; } set { m_socketType = value; } }

	private int m_mainCmd; //主命令ID
	private int m_subCmd; //从命令ID
	private int m_socketType; //消息对应哪个socket

	private static Dictionary<Type,int> m_cachePacketSize = new Dictionary<Type, int>();

	public virtual byte[] Serialize()
    {
        ByteBuffer buffer = new ByteBuffer();
        this.Serialize(buffer);
        return buffer.ToBytes();
    }

	public void Serialize(ByteBuffer buffer)
    {
        FieldInfo[] fields = this.GetType().GetFields();
        foreach (FieldInfo field in fields)
        {
			Serialize (buffer, field, field.GetValue (this));
        }
    }

	void Serialize(ByteBuffer buffer,FieldInfo field,object fieldValue){
		//object fieldValue = field.GetValue(this);
		Type fieldType = field.FieldType;

		if (fieldType.BaseType == typeof(Array)) {
			PacketFieldAttribute attr = (PacketFieldAttribute)field.GetCustomAttributes (typeof(PacketFieldAttribute), false).FirstOrDefault ();
			if (attr == null) {
				Debug.LogError ("只处理定长数组！出错 " + this + " " + field);
				return;
			}

			if (attr.ArraySizeConst2 != 0) {//二维数组
				Array arrayField = fieldValue == null ? null : (Array)fieldValue;
				int arrayLength = arrayField == null ? 0 : arrayField.Length;
				Type elementType = fieldType.GetElementType ().GetElementType ();
				for (int i = 0; i < attr.ArraySizeConst; i++) {
					Array arrayField2 = null;
					if (i < arrayLength) {
						arrayField2 = (Array)(arrayField.GetValue (i));
					}
					int arrayLength2 = arrayField2 == null ? 0 : arrayField2.Length;
					for (int j = 0; j < attr.ArraySizeConst2; j++) {
						if (j < arrayLength2)
							SerializeBase (buffer, elementType, arrayField2.GetValue (j));
						else {
							object elementValue = Activator.CreateInstance (elementType);
							SerializeBase (buffer, elementType, elementValue);
						}
					}
				}
			} else {//一维数组
				Array arrayField = fieldValue == null ? null : (Array)fieldValue;
				int arrayLength = arrayField == null ? 0 : arrayField.Length;
				Type elementType = fieldType.GetElementType ();
				for (int i = 0; i < attr.ArraySizeConst; i++) {
					if (i < arrayLength)
						SerializeBase (buffer, elementType, arrayField.GetValue (i));
					else {
						object elementValue = Activator.CreateInstance (elementType);
						SerializeBase (buffer, elementType, elementValue);
					}
				}
			}
		} else {
			SerializeBase (buffer, fieldType, fieldValue);
		}
	}

	void  SerializeBase(ByteBuffer buffer,Type fieldType,object fieldValue){
		
		//Type fieldType = field.FieldType;
		if (fieldType == typeof(int)) {
			buffer.Write ((int)fieldValue);
		} else if (fieldType == typeof(bool)) {
			buffer.Write ((bool)fieldValue);
		} else if (fieldType == typeof(uint)) {
			buffer.Write ((uint)fieldValue);
		} else if (fieldType == typeof(short)) {
			buffer.Write ((short)fieldValue);
		} else if (fieldType == typeof(ushort)) {
			buffer.Write ((ushort)fieldValue);
		} else if (fieldType == typeof(long)) {
			buffer.Write ((long)fieldValue);
		} else if (fieldType == typeof(ulong)) {
			buffer.Write ((ulong)fieldValue);
		} else if (fieldType == typeof(byte)) {
			buffer.Write ((byte)fieldValue);
		}else if (fieldType == typeof(char)) {
			buffer.Write ((char)fieldValue);
		}else if (fieldType == typeof(float)) {
			buffer.Write ((float)fieldValue);
		} else if (fieldType.IsSubclassOf (typeof(NetPacket))) {
			NetPacket packetField = ((NetPacket)fieldValue);
			if (packetField == null) {
				packetField = (NetPacket)Activator.CreateInstance (fieldType);
			}
			packetField.Serialize (buffer);
		}else
		{
			Debug.LogError("Serialize 未处理类型  " + fieldType);
		}
	}

	public static int NetPacketSize(Type t){
		if (m_cachePacketSize.ContainsKey (t)) {
			return m_cachePacketSize [t];
		}
		int size = 0;
		FieldInfo[] fields = t.GetFields();
		foreach (FieldInfo field in fields)
		{
			Type fieldType = field.FieldType;
			if (fieldType.BaseType == typeof(Array)) {
				PacketFieldAttribute attr = (PacketFieldAttribute)field.GetCustomAttributes (typeof(PacketFieldAttribute), false).FirstOrDefault ();
				if (attr == null) {
					Debug.LogError ("NetPacketSize 只处理定长数组！出错 " + t + " " + field);
					return 0;
				}

				if (attr.ArraySizeConst2 != 0) {//二维数组
					Type elementType = fieldType.GetElementType ().GetElementType ();
					size += NetPacketSizeBase (elementType) * attr.ArraySizeConst * attr.ArraySizeConst2;
				} else {//一维数组
					Type elementType = fieldType.GetElementType ();
					size += NetPacketSizeBase (elementType) * attr.ArraySizeConst;
				}
			} else {
				size += NetPacketSizeBase (fieldType);
			}
		}
		m_cachePacketSize [t] = size;
		return size;
	}

	static int  NetPacketSizeBase(Type fieldType){
		if (fieldType == typeof(int)) {
			return 4;
		} else if (fieldType == typeof(bool)) {
			return 1;
		} else if (fieldType == typeof(uint)) {
			return 4;
		} else if (fieldType == typeof(short)) {
			return 2;
		} else if (fieldType == typeof(ushort)) {
			return 2;
		} else if (fieldType == typeof(long)) {
			return 8;
		} else if (fieldType == typeof(ulong)) {
			return 8;
		} else if (fieldType == typeof(byte)) {
			return 1;
		}else if (fieldType == typeof(char)) {
			return 2;
		}else if (fieldType == typeof(float)) {
			return 4;
		} else if (fieldType.IsSubclassOf (typeof(NetPacket))) {
			return NetPacket.NetPacketSize(fieldType);
		}else
		{
			Debug.LogError("NetPacketSizeBase 未处理类型  " + fieldType);
			return 0;
		}
	}

    public virtual void Deserialize(byte[] bytes)
    {
        ByteBuffer buffer = new ByteBuffer(bytes);
        Deserialize(buffer);
    }

	public void Deserialize(ByteBuffer buffer)
    {
		var tt = this.GetType ();
        FieldInfo[] fields = this.GetType().GetFields();
        foreach (FieldInfo field in fields)
        {
			Deserialize (buffer, field);
        }
    }

	void Deserialize(ByteBuffer buffer,FieldInfo field){
		//object fieldValue = field.GetValue(this);
		Type fieldType = field.FieldType;

		if (fieldType == typeof(int)) {
			field.SetValue (this, buffer.ReadInt ());
		} else if (fieldType == typeof(bool)) {
			field.SetValue (this, buffer.ReadBoolean ());
		} else if (fieldType == typeof(uint)) {
			field.SetValue (this, buffer.ReadUInt ());
		} else if (fieldType == typeof(short)) {
			field.SetValue (this, buffer.ReadShort ());
		} else if (fieldType == typeof(ushort)) {
			field.SetValue (this, buffer.ReadUShort ());
		} else if (fieldType == typeof(long)) {
			field.SetValue (this, buffer.ReadLong ());
		} else if (fieldType == typeof(ulong)) {
			field.SetValue (this, buffer.ReadULong ());
		} else if (fieldType == typeof(byte)) {
			field.SetValue (this, buffer.ReadByte ());
		} else if (fieldType == typeof(char)) {
			field.SetValue (this, buffer.ReadChar ());
		} else if (fieldType == typeof(float)) {
			field.SetValue (this, buffer.ReadFloat ());
		}else if (fieldType.IsSubclassOf (typeof(NetPacket))) {
			NetPacket packetField = (NetPacket)Activator.CreateInstance (fieldType);
			packetField.Deserialize (buffer);
			field.SetValue (this, packetField);
		}
		else if (fieldType.BaseType == typeof(Array)) {
			PacketFieldAttribute attr = (PacketFieldAttribute)field.GetCustomAttributes(typeof(PacketFieldAttribute), false).FirstOrDefault();
			if (attr == null) {
				Debug.LogError("只处理定长数组！出错 " + this +" "+field);
				return;
			}
			Type elementType = fieldType.GetElementType ();
			if (attr.ArraySizeConst2 != 0) {//二维数组
				Array arrayField = Array.CreateInstance(fieldType.GetElementType (),attr.ArraySizeConst);
				for (int i = 0; i < attr.ArraySizeConst; i++) {
					Array arrayField2 =  Array.CreateInstance(fieldType.GetElementType ().GetElementType(),attr.ArraySizeConst2);
					for (int j = 0; j < attr.ArraySizeConst2; j++) {
						object elementValue = DeserializeBase (buffer, fieldType.GetElementType ().GetElementType());
						arrayField2.SetValue (elementValue, j);
					}
					arrayField.SetValue (arrayField2,i);
				}
				field.SetValue (this, arrayField);
			} else {//一维数组
				Array arrayField = Array.CreateInstance(fieldType.GetElementType (),attr.ArraySizeConst);
				for (int i = 0; i < attr.ArraySizeConst; i++) {
					object elementValue = DeserializeBase (buffer, fieldType.GetElementType ());
					arrayField.SetValue (elementValue, i);
				}
				field.SetValue (this, arrayField);
			}
		}	
		else
		{
			Debug.LogError("Deserialize 未处理类型  " + field);
		}
	}

	object DeserializeBase(ByteBuffer buffer,Type fieldType){
		object outValue = null;

		if (fieldType == typeof(int)) {
			outValue = buffer.ReadInt ();
		} else if (fieldType == typeof(bool)) {
			outValue = buffer.ReadBoolean ();
		} else if (fieldType == typeof(uint)) {
			outValue = buffer.ReadUInt ();
		} else if (fieldType == typeof(short)) {
			outValue = buffer.ReadShort ();
		} else if (fieldType == typeof(ushort)) {
			outValue = buffer.ReadUShort ();
		} else if (fieldType == typeof(long)) {
			outValue = buffer.ReadLong ();
		} else if (fieldType == typeof(ulong)) {
			outValue = buffer.ReadULong ();
		} else if (fieldType == typeof(byte)) {
			outValue = buffer.ReadByte ();
		} else if (fieldType == typeof(char)) {
			outValue = buffer.ReadChar ();
		} else if (fieldType == typeof(float)) {
			outValue = buffer.ReadFloat ();
		} else if (fieldType.IsSubclassOf (typeof(NetPacket))) {
			NetPacket packetField = (NetPacket)Activator.CreateInstance (fieldType);
			packetField.Deserialize (buffer);
			outValue = packetField;
		} else {
			Debug.LogError("DeserializeBase 未处理类型  " + fieldType);
		}
		return outValue;
	}
}

[AttributeUsage(AttributeTargets.Field)]
public class PacketFieldAttribute : Attribute
{
    public int ArraySizeConst; //1维数组
    public int ArraySizeConst2; //用于2维数组
}

//网络内核
public class TCP_Info : NetPacket  //171
{
	public word                          wEnPacketSize;          //2
	[PacketFieldAttribute(ArraySizeConst = 84)]
	public byte[]                          dwClientPort; //[84]  //84
	public longlong					   ulCmdNo;                //8
	public NetMessageHead					netHead;    //73
	public byte							cbDataKind;							//数据类型  1
	public byte							cbCheckCode;						//效验字段  1
	public word							wPacketSize;						//数据大小       2
}

//网络命令
public class TCP_Command : NetPacket
{
	public word		wMainCmdID;						//主命令码
	public word		wSubCmdID;						//子命令码
} 

//网络包头
public class TCP_Head : NetPacket
{
	public TCP_Info	TCPInfo;						//基础结构
	public TCP_Command	CommandInfo;					//命令信息
}