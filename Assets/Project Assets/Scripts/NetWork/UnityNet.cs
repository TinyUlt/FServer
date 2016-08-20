using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public enum enSocketType
{
    SocketType_Login,
    SocketType_Room,
    SocketType_Speak,
    SocketType_Mission,
    SocketType_Count,
}

public enum NetLogLevel
{

}

//[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
public delegate void PostToNetWorkMessageCCallback(IntPtr This, IntPtr custom, enSocketType SocketType, int MainCmdId, int SubCmdId, IntPtr Buffer, UInt16 Length, Int64 CmdNo, UInt16 SocketID);
//[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
public delegate void PostToNetWorkConnectedCCallback(IntPtr This, IntPtr custom, enSocketType SocketType, int ErrorCode, string ErrorDesc, UInt16 SocketID);
//[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
public delegate void PostToNetWorkClosedCCallback(IntPtr This, IntPtr custom, enSocketType SocketType, bool CloseByServer, byte cbShutReason, UInt16 wSocketID);

public class UnityNet
{

//    [DllImport("TinyNetwork")]
//    private static extern void sendCmd(IntPtr This, int SocketType, int wMainCmd, int wSubCmd);
//
//    [DllImport("TinyNetwork")]
//    private static extern void sendData(IntPtr This, int SocketType, int wMainCmd, int wSubCmd, byte[] address, int memorySize);
//
//    [DllImport("TinyNetwork")]
//    private static extern void update(IntPtr This, float dt);
//
//    [DllImport("TinyNetwork")]
//    private static extern void setServerList(IntPtr This, ref UInt32 plist, int length);
//
//    [DllImport("TinyNetwork")]
//    private static extern void connectServer(IntPtr This, int SocketType, int wPort, int wNum, string szAccounts = "");
//
//    [DllImport("TinyNetwork")]
//    private static extern void closeServer(IntPtr This, int SocketType);
//
//    [DllImport("TinyNetwork")]
//    private static extern void setReceiveMessageCallback(IntPtr This, PostToNetWorkMessageCCallback postToNetWorkConnectedCCallback, IntPtr custom);
//
//    [DllImport("TinyNetwork")]
//    private static extern void setConnectResultCallback(IntPtr This, PostToNetWorkConnectedCCallback postToNetWorkConnectedCCallback, IntPtr custom);
//
//    [DllImport("TinyNetwork")]
//    private static extern void setClosedCallback(IntPtr This, PostToNetWorkClosedCCallback postToNetWorkClosedCCallback, IntPtr custom);
//
//    [DllImport("TinyNetwork")]
//    private static extern void sendLog(IntPtr This, NetLogLevel level, string text);
//
//    [DllImport("TinyNetwork")]
//    private static extern IntPtr CreateTinyNetwork(string writePath);
//    [DllImport("TinyNetwork")]
//    private static extern void deleteTinyNetwork(IntPtr This);
//    [DllImport("TinyNetwork")]
//    private static extern void AddUserID(IntPtr This, int SocketType, uint userID);

	//---------------------------
	private static void sendCmd(IntPtr This, int SocketType, int wMainCmd, int wSubCmd){}

	private static void sendData(IntPtr This, int SocketType, int wMainCmd, int wSubCmd, byte[] address, int memorySize){}

	private static void update(IntPtr This, float dt){}

	private static void setServerList(IntPtr This, ref UInt32 plist, int length){}

	private static void connectServer(IntPtr This, int SocketType, int wPort){}

	private static void closeServer(IntPtr This, int SocketType){}

	private static void setReceiveMessageCallback(IntPtr This, PostToNetWorkMessageCCallback postToNetWorkConnectedCCallback, IntPtr custom){}

	private static void setConnectResultCallback(IntPtr This, PostToNetWorkConnectedCCallback postToNetWorkConnectedCCallback, IntPtr custom){}

	private static void setClosedCallback(IntPtr This, PostToNetWorkClosedCCallback postToNetWorkClosedCCallback, IntPtr custom){}

	private static void sendLog(IntPtr This, NetLogLevel level, string text){}

	private static IntPtr CreateTinyNetwork(string writePath){return IntPtr.Zero;}

	private static void deleteTinyNetwork(IntPtr This){}

	private static void AddUserID(IntPtr This, int SocketType, uint userID){}

    public virtual void sendCmd(int SocketType, int wMainCmd, int wSubCmd)
    {
        sendCmd(This, SocketType, wMainCmd, wSubCmd);
    }
    public virtual void sendData(int SocketType, int wMainCmd, int wSubCmd, byte[] address, int memorySize)
    {
        sendData(This, SocketType, wMainCmd, wSubCmd, address, memorySize);
    }

    public virtual void sendData(int SocketType, NetPacket packet)
    {

    }

    public virtual void AddUserID(int socketType, uint userID)
    {
        AddUserID(This, socketType, userID);
    }

    public virtual void update(float dt)
    {
        update(This, dt);
    }

    public virtual void setServerList(ref UInt32 plist, int length)
    {
        setServerList(This, ref plist, length);
    }

    public virtual void setServerList(UInt32[] list)
    {
        setServerList(This, ref list[0], list.Length);
    }

	public virtual void connectServer(int SocketType, string ip,int wPort)
    {
        closeServer(SocketType);
		//AddUserID(SocketType, UserData.GetInstance().GetGlobalUserData().dwUserID);
        AddUserID(SocketType, 1);
        connectServer(This, SocketType, wPort);
    }

    public virtual void closeServer(int SocketType)
    {
        closeServer(This, SocketType);
    }

    public virtual void CloseAll()
    {

    }

    public virtual void setReceiveMessageCallback(PostToNetWorkMessageCCallback postToNetWorkConnectedCCallback, IntPtr custom)
    {
        setReceiveMessageCallback(This, postToNetWorkConnectedCCallback, custom);
    }

    public virtual void setConnectResultCallback(PostToNetWorkConnectedCCallback postToNetWorkConnectedCCallback, IntPtr custom)
    {
        setConnectResultCallback(This, postToNetWorkConnectedCCallback, custom);
    }

    public virtual void setClosedCallback(PostToNetWorkClosedCCallback postToNetWorkClosedCCallback, IntPtr custom)
    {
        setClosedCallback(This, postToNetWorkClosedCCallback, custom);
    }

    public virtual void sendLog(NetLogLevel level, string text)
    {
        sendLog(This, level, text);
    }

    private IntPtr This;

    public UnityNet()
    {
        This = CreateTinyNetwork("tset.log");
    }
    ~UnityNet()
    {
        deleteTinyNetwork(This);
    }
}
