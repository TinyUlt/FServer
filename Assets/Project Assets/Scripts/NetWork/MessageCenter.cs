using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MessageCenter
{
    public const int MAIN_ID_MULT = 100000;

    public delegate void LoginMessageResult(DBO_GP_LogonSuccess loginType);
    public delegate void LoginMessage();

    public event LoginMessage loginMessage;
    public event LoginMessageResult loginMessageResult;
    private static readonly object syncObject = new object();
    private static MessageCenter instance;

    private Dictionary<Type, Action<object>> m_MessageCallBackMapper = new Dictionary<Type, Action<object>>();

	private Dictionary<enSocketType,Dictionary<int, Type>> m_MessageID2Type = new Dictionary<enSocketType, Dictionary<int, Type>>();
	private Dictionary<enSocketType,Dictionary<Type, int>> m_MessageType2ID = new Dictionary<enSocketType, Dictionary<Type, int>>();

    public static MessageCenter GetInstance()
    {
        lock (syncObject)
        {
            if (instance == null)
            {
                instance = new MessageCenter();
            }
            return instance;
        }
    }
    private MessageCenter()
    {
        InitMessageStructMapper();
    }


    private void InitMessageStructMapper()
    {
		//登陆socket
		AddMessageStructMapper<CMD_GP_NoStruct>(Messages.MDM_GP_HEAT, enSocketType.SocketType_Login);
		//登录
//		AddMessageStructMapper<CMD_GP_LogonFailure>(Messages.MDM_GP_LOGON, Messages.SUB_GP_LOGON_FAILURE,enSocketType.SocketType_Login);
//		AddMessageStructMapper<DBO_GP_LogonSuccess>(Messages.MDM_GP_LOGON, Messages.SUB_GP_LOGON_SUCCESS,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_LogonAccounts>(Messages.MDM_GP_LOGON, Messages.SUB_GP_LOGON_ACCOUNTS,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_RegisterAccounts>(Messages.MDM_GP_LOGON, Messages.SUB_GP_REGISTER_ACCOUNTS,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_NoStruct>(Messages.MDM_GP_LOGON, Messages.SUB_GP_LOGON_FINISH,enSocketType.SocketType_Login);
//		//列表信息
//		AddMessageStructMapper<tagGameKinds>(Messages.MDM_GP_SERVER_LIST, Messages.SUB_GP_LIST_KIND,enSocketType.SocketType_Login);
//		AddMessageStructMapper<tagGameTypes>(Messages.MDM_GP_SERVER_LIST, Messages.SUB_GP_LIST_TYPE,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_GameNewNum>(Messages.MDM_GP_SERVER_LIST, Messages.SUB_GP_GET_NEWSUM,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_SysemMessage>(Messages.MDM_GP_SERVER_LIST, Messages.SUB_GP_GET_SYSTEM_MESSAGE,enSocketType.SocketType_Login);
//		AddMessageStructMapper<tagGameServers>(Messages.MDM_GP_SERVER_LIST, Messages.SUB_GP_LIST_SERVER,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_NoStruct>(Messages.MDM_GP_SERVER_LIST, Messages.SUB_GP_LIST_FINISH,enSocketType.SocketType_Login);
//
//		//喇叭
//		AddMessageStructMapper<CMD_GR_GetUserProp>(Messages.MDM_GP_HORN_LOGON, Messages.SUB_GP_GetUSERPROP,enSocketType.SocketType_Speak);
//		AddMessageStructMapper<CMD_GP_NoStruct>(Messages.MDM_GP_HORN_LOGON, Messages.SUB_GR_SEND_SUCCESS,enSocketType.SocketType_Speak);
//		AddMessageStructMapper<CMD_GetLaBaCostOrNum>(Messages.MDM_GP_HORN_LOGON, Messages.SUB_GR_PROP_COST,enSocketType.SocketType_Speak);
//		AddMessageStructMapper<CMD_GR_S_SendTrumpet>(Messages.MDM_GP_HORN_LOGON, Messages.SUB_GR_HORN_BIG,enSocketType.SocketType_Speak);
//		AddMessageStructMapper<CMD_GP_UserHronFailure>(Messages.MDM_GP_HORN_LOGON, Messages.SUB_GR_SEND_Failure,enSocketType.SocketType_Speak);
//
////		AddMessageStructMapper<CMD_GR_Send_SendTrumpet>(Messages.MDM_GP_HORN_LOGON, Messages.SUB_GR_HORN_BIG,enSocketType.SocketType_Speak);
////		AddMessageStructMapper<CMD_GR_S_SendTrumpet>(Messages.MDM_GR_USER, Messages.SUB_GR_PROPERTY_TRUMPET_NEW,enSocketType.SocketType_Speak);
////		AddMessageStructMapper<CMD_GetLaBaCostOrNum>(Messages.MDM_GR_USER, Messages.SUB_GR_REQUEST_PROPERTY_COST,enSocketType.SocketType_Speak);
////		AddMessageStructMapper<CMD_GP_RecoidCnts>(Messages.MDM_GP_LOGON, Messages.SUB_GP_QUERY_MESSAGE,enSocketType.SocketType_Speak);
//		//银行 个人中心
//		AddMessageStructMapper<CMD_GP_UserFaceInfo>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_USER_FACE_INFO,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_UserIndividual>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_USER_INDIVIDUAL,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_LogonFailure>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_OPERATE_FAILURE,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_LogonFailure>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_OPERATE_SUCCESS,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_UserLogonBank>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_USER_LOGONBANK,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_NoStruct>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_LOGONBANK_FAILURE,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_UserLogonBankSuccess>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_LOGONBANK_SUCCESS,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_BankLogonFailure>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_LOGONBANK_FAILURE,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_UserInsureInfo>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_USER_INSURE_INFO, enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_UserInsureSuccess>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_USER_INSURE_SUCCESS,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_UserInsureFailure>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_USER_INSURE_FAILURE,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_UserTransferUserInfo>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_QUERY_USER_INFO_RESULT,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_BankDetailResults>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_BANK_DETAIL_RESULT,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GP_ReturnYSSocre>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GR_QUERY_RETURN_SCORE,enSocketType.SocketType_Login);
//		AddMessageStructMapper<CMD_GR_ChangeReturnInfo>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_CHANGE_RETURN_INFO,enSocketType.SocketType_Login);
//
//        AddMessageStructMapper<CMD_GP_QueryInsureInfo>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_QUERY_INSURE_INFO, enSocketType.SocketType_Login);
//        AddMessageStructMapper<CMD_GP_GetUserReturnScore>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GR_GETUSER_SCORE, enSocketType.SocketType_Login);
//        AddMessageStructMapper<CMD_GP_UserTakeScore>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_USER_TAKE_SCORE, enSocketType.SocketType_Login);
//        AddMessageStructMapper<CMD_GP_UserSaveScore>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_USER_SAVE_SCORE, enSocketType.SocketType_Login);
//        AddMessageStructMapper<CMD_GP_QueryUserInfoRequest>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_QUERY_USER_INFO_REQUEST, enSocketType.SocketType_Login);
//        AddMessageStructMapper<CMD_GP_QueryReturnSocre>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GR_QUERY_RETURN, enSocketType.SocketType_Login);
//        AddMessageStructMapper<CMD_GP_QueryBankDetail>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_QUERY_BANK_DETAIL, enSocketType.SocketType_Login);
//        AddMessageStructMapper<CMD_GP_UserTransferScore>(Messages.MDM_GP_USER_SERVICE, Messages.SUB_GP_USER_TRANSFER_SCORE, enSocketType.SocketType_Login);
//        //选位置坐下
//        AddMessageStructMapper<CMD_GR_UserSitDown>(Messages.MDM_GR_USER, Messages.SUB_GR_USER_SITDOWN,enSocketType.SocketType_Room);
//		//登陆游戏房间
//		AddMessageStructMapper<CMD_GR_LogonUserID>(Messages.MDM_GP_LOGON, Messages.SUB_GR_LOGON_USERID,enSocketType.SocketType_Room);
//		//游戏状态
//		AddMessageStructMapper<CMD_GF_GameStatus>(Messages.MDM_GF_FRAME, Messages.SUB_GF_GAME_STATUS,enSocketType.SocketType_Room);
//		//...
//		AddMessageStructMapper<DBO_GP_LogonSuccess>(Messages.MDM_GR_LOGON, Messages.SUB_GR_LOGON_SUCCESS,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GP_NoStruct>(Messages.MDM_GR_LOGON, Messages.SUB_GR_LOGON_FINISH,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GR_LogonFailure>(Messages.MDM_GR_LOGON, Messages.SUB_GR_LOGON_FAILURE,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GP_LogonFailure>(Messages.MDM_GR_LOGON, Messages.SUB_GR_LOGON_IPLIST,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GR_PcConfigServer>(Messages.MDM_GR_CONFIG, Messages.SUB_GR_CONFIG_SERVER,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GR_ConfigColumn>(Messages.MDM_GR_CONFIG, Messages.SUB_GR_CONFIG_COLUMN,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GP_NoStruct>(Messages.MDM_GR_CONFIG, Messages.SUB_GR_CONFIG_PROPERTY,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GP_NoStruct>(Messages.MDM_GR_CONFIG, Messages.SUB_GR_CONFIG_FINISH,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GR_TableInfo>(Messages.MDM_GR_STATUS, Messages.SUB_GR_TABLE_INFO,enSocketType.SocketType_Room);
//
//		AddMessageStructMapper<CMD_GR_TableStatus>(Messages.MDM_GR_STATUS, Messages.SUB_GR_TABLE_STATUS,enSocketType.SocketType_Room);
//		//...
//		AddMessageStructMapper<CMD_GF_GameScene>(Messages.MDM_GF_FRAME, Messages.SUB_GF_GAME_SCENE,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GR_TableInfo>(Messages.MDM_GR_STATUS, Messages.SUB_GR_TABLE_INFO,enSocketType.SocketType_Room);
//		//用户信息
//		AddMessageStructMapper<tagUserInfo>(Messages.MDM_GR_USER, Messages.SUB_GR_USER_ENTER,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GP_NoStruct>(Messages.MDM_GR_USER, Messages.SUB_GR_USER_PACKAGE,enSocketType.SocketType_Room);
//
//		AddMessageStructMapper<CMD_GR_UserScore>(Messages.MDM_GR_USER, Messages.SUB_GR_USER_SCORE,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GR_UserStatus>(Messages.MDM_GR_USER, Messages.SUB_GR_USER_STATUS,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_GR_RequestFailure>(Messages.MDM_GR_USER, Messages.SUB_GR_REQUEST_FAILURE,enSocketType.SocketType_Room);
//		//系统消息
//		AddMessageStructMapper<CMD_CM_SystemMessage>(Messages.MDM_CM_SYSTEM, Messages.SUB_CM_SYSTEM_MESSAGE,enSocketType.SocketType_Room);
//
//		AddMessageStructMapper<CMD_GF_GameOption>(Messages.MDM_GF_FRAME, Messages.SUB_GF_GAME_OPTION,enSocketType.SocketType_Room);
//		AddMessageStructMapper<CMD_CM_SystemMessage>(Messages.MDM_GF_FRAME, Messages.SUB_GF_SYSTEM_MESSAGE,enSocketType.SocketType_Room);//滚动消息
    }

	public void AddMessageStructMapper<T>(int MainCmdId, enSocketType socketType)
    { 
		if (!m_MessageID2Type.ContainsKey (socketType)) {
			m_MessageID2Type [socketType] = new Dictionary<int, Type> ();
			m_MessageType2ID [socketType] = new Dictionary<Type, int> ();
		}
        int id = MainCmdId * MAIN_ID_MULT ;
        Type t = typeof(T);
		m_MessageID2Type[socketType][id] = t;
		m_MessageType2ID[socketType][t] = id;
    }

	public void RemoveMessageStructMapper (int mainCmd,  enSocketType socketType){
		if (m_MessageID2Type.ContainsKey (socketType)) {
			int id = mainCmd * MAIN_ID_MULT ;
			Type t = m_MessageID2Type [socketType][id];
			m_MessageID2Type [socketType].Remove (id);
			m_MessageType2ID [socketType].Remove(t);
		}
	}

	public Type GetMessageStruct(int MainCmdId, enSocketType socketType)
    {
        int id = MainCmdId * MAIN_ID_MULT ;
		if (m_MessageID2Type.ContainsKey(socketType) && m_MessageID2Type[socketType].ContainsKey(id))
        {
			return m_MessageID2Type[socketType][id];
        }
        return null;
    }

	public bool GetMessageID(NetPacket packet, ref int mainCmdID, enSocketType socketType)
    {
        Type t = packet.GetType();
		if (m_MessageType2ID.ContainsKey(socketType) && m_MessageType2ID[socketType].ContainsKey(t))
        {
			int id = m_MessageType2ID[socketType][t];
            mainCmdID = id / MAIN_ID_MULT;
            
            return true;
        }
        return false;
    }

	public void RegistMessageListener<T>(Action<object> messageCallBack)
    {
        Type type = typeof(T);
		RegistMessageListener (type,messageCallBack);
    }

	public void RegistMessageListener(Type t,Action<object> callBack){
		if (m_MessageCallBackMapper.ContainsKey(t))
			m_MessageCallBackMapper[t] += callBack;
		else
			m_MessageCallBackMapper[t] = callBack;
	}

	public void UnRegistMessageListener<T>(Action<object> messageCallBack)
    {
        Type type = typeof(T);
		UnRegistMessageListener (type,messageCallBack);
    }

	public void UnRegistMessageListener(Type t,Action<object> callBack){
		if (m_MessageCallBackMapper.ContainsKey(t))
			m_MessageCallBackMapper[t] -= callBack;
	}

    public Action<object> GetMessageListener<T>()
    {
        Type type = typeof(T);
        if (m_MessageCallBackMapper.ContainsKey(type))
            return m_MessageCallBackMapper[type];
        return null;
    }

    public void CallMessageListener(object data)
    {
        if (data == null)
            return;
        Type type = data.GetType();
		if (m_MessageCallBackMapper.ContainsKey (type)) {
			if (m_MessageCallBackMapper [type] != null) {
				m_MessageCallBackMapper [type] (data);
			} else {
				
			}

		}
            
    }

    void Start()
    {
      
    }

	public void ConnectServer(enSocketType loginType, string ip,int port, LoginMessage message)
    {
        loginMessage = message;
		SocketServer.Instance.ConnetServer(loginType, ip, port);
    }

    public void CloseServer(enSocketType scocketType)
    {
        SocketServer.Instance.CloseServer(scocketType);
    }

    public void ConnetResult()
    {
        if (loginMessage != null)
        {
            loginMessage();
        }
    }

    public void LoginMessageResultRegister(LoginMessageResult result)
    {
        loginMessageResult = result;
    }

    public void LogonFinish(DBO_GP_LogonSuccess logonSuccess)
    {
        LoginMessagePost(logonSuccess);
    }
    public void LogonFailure(CMD_GP_LogonFailure logonFailure)
    {
        //		LoginMessagePost (logonSuccess);
    }
    public void RegisterValidateCode(CMD_GP_GetValidateCode validateCode)
    {
        //		LoginMessagePost (validateCode);
    }

    private void LoginMessagePost(DBO_GP_LogonSuccess logonSuccess)
    {
        if (loginMessageResult != null)
        {
            loginMessageResult(logonSuccess);
        }
    }




}
