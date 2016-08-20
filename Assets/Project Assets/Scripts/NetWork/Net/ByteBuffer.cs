using System.Collections;
using System.IO;
using System;
using System.Text;

public class ByteBuffer : MemoryStream
{
    public ByteBuffer()
    {
    }

    public ByteBuffer(byte[] bytes) : base(bytes)
    {
    }

    public byte[] ToBytes()
    {
        long length = this.Length;
        byte[] bytes = new byte[length];
        Array.Copy(this.GetBuffer(), 0, bytes, 0, length);
        return bytes;
    }

    public void Write(bool value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        this.Write(bytes, 0, bytes.Length);
    }

    public void Write(int value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        this.Write(bytes, 0, bytes.Length);
    }

	public void Write(float value){
		byte[] bytes = BitConverter.GetBytes (value);
		this.Write (bytes, 0, bytes.Length);
	}

	public void Write(int[] value){
		int length = value.Length;
		//this.Write (length);
		for (int i = 0; i < length; i++) {
			this.Write (value [i]);
		}
	}

    public void Write(long[] value)
    {
        int length = value.Length;
        //this.Write (length);
        for (int i = 0; i < length; i++)
        {
            this.Write(value[i]);
        }
    }

    public void Write(char[] value)
    {
        int length = value.Length;
        //this.Write (length);
        for (int i = 0; i < length; i++)
        {
            this.Write(value[i]);
        }
    }

    public void Write(uint value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        this.Write(bytes, 0, bytes.Length);
    }

    public void Write(short value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        this.Write(bytes, 0, bytes.Length);
    }

    public void Write(char value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        this.Write(bytes, 0, bytes.Length);
    }

    public void Write(ushort value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        this.Write(bytes, 0, bytes.Length);
    }

    public void Write(long value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        this.Write(bytes, 0, bytes.Length);
    }

    public void Write(ulong value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        this.Write(bytes, 0, bytes.Length);
    }

    public void Write(byte value)
    {
        this.WriteByte(value);
    }

    public void Write(byte[] bytes)
    {
        int length = bytes.Length;
        //this.Write (length);
        this.Write(bytes, 0, length);
    }

    public void Write(string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        this.Write(bytes.Length);
        this.Write(bytes, 0, bytes.Length);
    }

    public byte[] ReadBytes(int size)
    {
        byte[] bytes = new byte[size];
        this.Read(bytes, 0, size);
        return bytes;
    }

    public byte[] ReadBytes()
    {
        int size = this.ReadInt();
        byte[] bytes = new byte[size];
        this.Read(bytes, 0, size);
        return bytes;
    }

    new public byte ReadByte()
    {
        return ReadBytes(sizeof(byte))[0];
    }

    public char ReadChar()
    {
        byte[] bytes = ReadBytes(sizeof(char));
        return BitConverter.ToChar(bytes, 0);
    }

	public float ReadFloat(){
		byte[] bytes = ReadBytes (sizeof(float));
		return BitConverter.ToSingle (bytes, 0);
	}

	public char[] ReadChars(int length){
		char[] chars = new char[length];
		for (int i = 0; i < length; i++) {
			chars [i] = ReadChar ();
		}
		return chars;
	}

    public bool ReadBoolean()
    {
        byte[] bytes = ReadBytes(sizeof(bool));
        return BitConverter.ToBoolean(bytes, 0);
    }

    public int ReadInt()
    {
        byte[] bytes = ReadBytes(sizeof(int));
        return BitConverter.ToInt32(bytes, 0);
    }

    public int[] ReadInts(int length)
    {
        //int length = ReadInt ();
        int[] values = new int[length];
        for (int i = 0; i < length; i++)
        {
            values[i] = ReadInt();
        }
        return values;
    }

    public uint ReadUInt()
    {
        byte[] bytes = ReadBytes(sizeof(uint));
        return BitConverter.ToUInt32(bytes, 0);
    }

    public short ReadShort()
    {
        byte[] bytes = ReadBytes(sizeof(short));
        return BitConverter.ToInt16(bytes, 0);
    }

    public ushort ReadUShort()
    {
        byte[] bytes = ReadBytes(sizeof(ushort));
        return BitConverter.ToUInt16(bytes, 0);
    }

    public long ReadLong()
    {
        byte[] bytes = ReadBytes(sizeof(long));
        return BitConverter.ToInt64(bytes, 0);
    }

    public long[] ReadLongs(int length)
    {
        //int length = ReadInt ();
        long[] values = new long[length];
        for (int i = 0; i < length; i++)
        {
            values[i] = ReadLong();
        }
        return values;
    }

    public ulong ReadULong()
    {
        byte[] bytes = ReadBytes(sizeof(ulong));
        return BitConverter.ToUInt64(bytes, 0);
    }

    public string ReadString()
    {
        int length = ReadInt();
        byte[] bytes = ReadBytes(length);
        return Encoding.UTF8.GetString(bytes, 0, length);
    }

}
