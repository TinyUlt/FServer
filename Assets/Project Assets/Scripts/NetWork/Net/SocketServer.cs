using UnityEngine;
using System;

using dword = System.UInt32;
using word = System.UInt16;
using tchar = System.Char;
using longlong = System.Int64;
using System.Runtime.InteropServices;

public class SocketClientInfo
{
    public int connectPort;
    public enConnectLogonType connetType;
}

public class SocketServer : MonoBehaviour
{
    public static SocketServer Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject go = new GameObject("SocketServer");
                m_instance = go.AddComponent<SocketServer>();
                DontDestroyOnLoad(go);
            }
            return m_instance;
        }
    }

    protected static SocketServer m_instance;

    protected UnityNet mUnityNet = null;                                      //网络实例

    SocketServer()
    {
        InitServer();
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if (mUnityNet != null)
            mUnityNet.update(Time.deltaTime);
    }

    public virtual void InitServer()
    {
        //mUnityNet = new UnityNet();
        mUnityNet = new SocketClientMgr();
        //设置连接服务器列表
        mUnityNet.setServerList(IpConfig.ips);
        //网络回调==网络连接状态
        mUnityNet.setConnectResultCallback(ConnectResultCallback, IntPtr.Zero);
        //网络回调==服务器返回
        mUnityNet.setReceiveMessageCallback(ReceiveMessageCallback, IntPtr.Zero);
        //网络回调==关闭连接
        mUnityNet.setClosedCallback(ClosedCallback, IntPtr.Zero);
    }

	public virtual void ConnetServer(enSocketType connectType, string ip,int port)
    {
        //GameManager.GetInstance().StartLoading();

		mUnityNet.connectServer((int)connectType,ip, port);
    }

    public virtual void CloseServer(enSocketType connectType)
    {
        Debug.Log("CloseServer " + connectType);
        mUnityNet.closeServer((int)connectType);
    }

    public virtual IntPtr GetStructPtr(object curStruct, int lenth)
    {
        IntPtr structPtr = Marshal.AllocHGlobal(lenth);
        Marshal.Copy(new byte[lenth], 0, structPtr, lenth);
        Marshal.StructureToPtr(curStruct, structPtr, true);
        return structPtr;
    }

    public virtual void SendCmd(int mainCMD, int subCMD, enSocketType socketType)
    {
        Debug.Log("发送消息 " + " MainCmdId: " + mainCMD + " SubCmdId: " + subCMD);
        mUnityNet.sendCmd((int)socketType, mainCMD, subCMD);
    }

    //byte[] m_sendBytes;
    public virtual void SendMessage(NetPacket packet, enSocketType socketType)
    {
        int mainCmdId = 0, subCmdId = 0;
        if (MessageCenter.GetInstance().GetMessageID(packet, ref mainCmdId,  socketType) == false)
        {
			Debug.LogError("客户端不存在发送结构对应的消息号 " + packet);
            return;
        }
        Debug.Log("发送消息 " + packet + " MainCmdId: " + mainCmdId + " SubCmdId: " + subCmdId);
        packet.mainCmd = mainCmdId;
        packet.subCmd = subCmdId;
        mUnityNet.sendData((int)socketType, packet);
        //        try
        //        {
        //            byte[] m_sendBytes = packet.Serialize();
        //            //发送消息
        //            mUnityNet.sendData((int)socketType, mainCmdId, subCmdId, m_sendBytes, m_sendBytes.Length);
        //
        //        }
        //        catch (Exception e)
        //        {
        //            Debug.LogError("发送消息失败 " + e);
        //        }

    }

	public virtual void SendMessage(int mainCmdId,int subCmdId,NetPacket packet, enSocketType socketType)
	{
		Debug.Log("发送消息 " + packet + " MainCmdId: " + mainCmdId + " SubCmdId: " + subCmdId);
		packet.mainCmd = mainCmdId;
		packet.subCmd = subCmdId;
		mUnityNet.sendData ((int)socketType,packet);
	}



    protected virtual void ReceiveMessageCallback(IntPtr This, IntPtr custom, enSocketType SocketType, int MainCmdId, int SubCmdId, IntPtr Buffer, UInt16 Length, Int64 CmdNo, UInt16 SocketID)
    {
//        GameManager.GetInstance().EndLoading();
        Type packetType = MessageCenter.GetInstance().GetMessageStruct(MainCmdId,  SocketType);
        if (packetType == null)
        {
            Debug.LogError("客户端不存在消息对应的结构 " + MainCmdId + " " + SubCmdId);
            return;
        }
//        Debug.Log("收到消息结构: " + packetType + " MainCmdId: " + MainCmdId + " SubCmdId: " + SubCmdId);
        try
        {
            NetPacket _tempStruct = (NetPacket)Activator.CreateInstance(packetType);
            byte[] receiveBytes = new byte[Length];
            Marshal.Copy(Buffer, receiveBytes, 0, Length);
            _tempStruct.Deserialize(receiveBytes);
            _tempStruct.mainCmd = MainCmdId;
            _tempStruct.subCmd = SubCmdId;

            if (_tempStruct != null)
            {
                MessageCenter.GetInstance().CallMessageListener(_tempStruct);
            }


        }
        catch (Exception e)
        {
            Debug.LogError("解析服务器消息出错 " + e);
        }
    }

    protected virtual void ConnectResultCallback(IntPtr This, IntPtr custom, enSocketType SocketType, int ErrorCode, string ErrorDesc, UInt16 SocketID)
    {
        if (ErrorCode == 0)
        {
            Debug.Log("连接服务器成功 " + SocketType);
            MessageCenter.GetInstance().ConnetResult();
        }
        else
        {
            Debug.LogError("ErrorDesc:" + ErrorDesc);
            Debug.LogError("ErrorCode:" + ErrorCode);
        }
    }

    protected virtual void ClosedCallback(IntPtr This, IntPtr custom, enSocketType SocketType, bool CloseByServer, byte cbShutReason, UInt16 wSocketID)
    {
        Debug.Log("socket closed " + SocketType);
    }

    protected virtual void OnDestroy()
    {
        if (mUnityNet != null)
        {
            mUnityNet.CloseAll();
            mUnityNet = null;
        }
    }
}