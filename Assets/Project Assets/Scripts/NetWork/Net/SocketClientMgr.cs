using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

public class SocketClientMgr : UnityNet
{
    //	public static SocketClientMgr Instance{
    //		get{ 
    //			if (m_Instance == null) {
    //				m_Instance = new SocketClientMgr ();
    //			}
    //			return m_Instance;
    //		}
    //	}
    //
    //	static SocketClientMgr m_Instance;

    Dictionary<int, SocketClient> m_clients = new Dictionary<int, SocketClient>();

    PostToNetWorkConnectedCCallback m_connectedCallBack;
    PostToNetWorkMessageCCallback m_receiveMessageCallBack;
    PostToNetWorkClosedCCallback m_closeCallback;

    public override void sendCmd(int SocketType, int wMainCmd, int wSubCmd)
    {
        if (m_clients.ContainsKey(SocketType))
        {
            NetPacket packet = new NetPacket();
            packet.mainCmd = wMainCmd;
            packet.subCmd = wSubCmd;

            SocketClient client = m_clients[SocketType];
            client.SendPacket(packet);
        }

    }
    public override void sendData(int SocketType, int wMainCmd, int wSubCmd, byte[] address, int memorySize)
    {

    }

    public override void sendData(int SocketType, NetPacket packet)
    {
        if (m_clients.ContainsKey(SocketType))
        {
            SocketClient client = m_clients[SocketType];
            client.SendPacket(packet);
        }
    }

    public override void AddUserID(int socketType, uint userID)
    {

    }

    public override void update(float dt)
    {
        foreach (var client in m_clients)
        {
            if (client.Value != null)
                client.Value.OnRun();
        }
    }

    public override void setServerList(ref UInt32 plist, int length)
    {

    }

    public override void setServerList(UInt32[] list)
    {

    }

	public override void connectServer(int SocketType,string ip, int wPort)
    {
        if (m_clients.ContainsKey(SocketType) && m_clients[SocketType] != null)
        {
            m_clients[SocketType].Close();
        }
        m_clients[SocketType] = new SocketClient(SocketType);
        m_clients[SocketType].SetOnGetPacketCallback(OnSocketClientGetPacket);

        SocketClient client = m_clients[SocketType];
		client.Connect(ip, wPort, (connected) =>
        { //119.147.144.154
            OnSocketClientConnect(SocketType, connected);
        });
    }

    public override void closeServer(int SocketType)
    {
        if (m_clients.ContainsKey(SocketType))
        {
            m_clients[SocketType].Close();
        }
    }

    public override void CloseAll()
    {
        foreach (var client in m_clients)
        {
            client.Value.Close();
        }
    }

    public override void setReceiveMessageCallback(PostToNetWorkMessageCCallback postToNetWorkConnectedCCallback, IntPtr custom)
    {
        m_receiveMessageCallBack = postToNetWorkConnectedCCallback;
    }

    public override void setConnectResultCallback(PostToNetWorkConnectedCCallback postToNetWorkConnectedCCallback, IntPtr custom)
    {
        m_connectedCallBack = postToNetWorkConnectedCCallback;
    }

    public override void setClosedCallback(PostToNetWorkClosedCCallback postToNetWorkClosedCCallback, IntPtr custom)
    {
        m_closeCallback = postToNetWorkClosedCCallback;
    }

    public override void sendLog(NetLogLevel level, string text)
    {

    }

    //----------------------------------------------------------------

    private void OnSocketDisConnect(int socketType)
    {
        m_closeCallback(IntPtr.Zero, IntPtr.Zero, (enSocketType)socketType, false, 0, 0);
    }

    private void OnSocketClientConnect(int socketType, bool connected)
    {
        Debug.Log("OnSocketClientConnect " + socketType + " " + connected);
        m_connectedCallBack(IntPtr.Zero, IntPtr.Zero, (enSocketType)socketType, connected == true ? 0 : 1, connected ? "" : "连接服务器失败", 0);
    }

    private void OnSocketClientGetPacket(int socketType, NetPacket packet)
    {
        byte[] all = packet.Serialize();
        IntPtr sendPtr = Marshal.AllocHGlobal(all.Length);
        Marshal.Copy(all, 0, sendPtr, all.Length);
        m_receiveMessageCallBack(IntPtr.Zero, IntPtr.Zero, (enSocketType)socketType, packet.mainCmd, packet.subCmd, sendPtr, (ushort)all.Length, 0, (ushort)0);
        Marshal.FreeHGlobal(sendPtr);
    }

}
