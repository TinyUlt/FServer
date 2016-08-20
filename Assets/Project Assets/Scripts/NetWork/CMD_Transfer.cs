using dword = System.UInt32;
using word = System.UInt16;
using tchar = System.Char;
using longlong = System.Int64;

public class CMD_Transfer
{

    //登录命令

    //public const int MDM_TR_SYSTEM				10									//登录信息
    //
    //public const int SUB_TR_ROOM_LOGON           100      //房间中转连接
    //public const int SUB_TR_ROOM_FIHISH          101      //房间中转完成
    //public const int SUB_TR_GAME_LOGON           102      //游戏中转连接
    //public const int SUB_TR_GAME_FIHISH          103      //游戏中转完成
    //
    //
    //public const int  SUB_TR_GAME_CONNECT        104      //告诉大厅房间正常
    //public const int  SUB_TR_ROOM_CONNECT        105      //告诉游戏房间连接正常
    //
    //public const int SUB_TR_COLSE_FRAME          106       //大厅主动关闭游戏框架
    //public const int SUB_TR_TOPLAZA_CLOSEGAME    107        //游戏框架主动关闭通知大厅
    //
    //public const int IPC_CMD_GF_SOCKET			21									//网络消息

    public const int IPC_MAIN_SOCKET = 1;						    //网络消息

    public const int IPC_SUB_GF_SOCKET_SEND = 1;								//网络发送
    public const int IPC_SUB_GF_SOCKET_RECV = 2;							//网络接收


    public const int IPC_CMD_GF_CONTROL = 2;							//控制消息

    public const int IPC_SUB_GF_CLIENT_READY = 1;							//准备就绪
    public const int IPC_SUB_GF_CLIENT_CLOSE = 2;							//进程关闭

    public const int IPC_SUB_GF_CLOSE_PROCESS = 100;							//关闭进程
    public const int IPC_SUB_GF_ACTIVE_PROCESS = 101;							//激活进程

    public const int IPC_SUB_GF_BOSS_COME = 200;								//老板来了
    public const int IPC_SUB_GF_BOSS_LEFT = 201;							//老板走了

    //////////////////////////////////////////////////////////////////////////////////
    //配置消息

    public const int IPC_CMD_GF_CONFIG = 3;							//配置消息

    public const int IPC_SUB_GF_LEVEL_INFO = 100;						//等级信息
    public const int IPC_SUB_GF_COLUMN_INFO = 101;							//列表信息
    public const int IPC_SUB_GF_SERVER_INFO = 102;						//房间信息
    public const int IPC_SUB_GF_PROPERTY_INFO = 103;						//道具信息
    public const int IPC_SUB_GF_CONFIG_FINISH = 104;							//配置完成
    public const int IPC_SUB_GF_CONFIG_CLOSE = 105;
    public const int IPC_SUB_GF_SCOKET_CLOSE = 106;                              //网络关闭
    public const int IPC_SUB_GF_USER_RIGHT = 107;							//玩家权限
    public const int IPC_SUB_GF_LABA_MESSAGE = 108;                            //喇叭消息

    public const int IPC_CMD_GF_USER_INFO = 4;						//用户消息

    public const int IPC_SUB_GF_USER_ENTER = 100;							//用户进入
    public const int IPC_SUB_GF_USER_SCORE = 101;								//用户分数
    public const int IPC_SUB_GF_USER_STATUS = 102;							//用户状态
    public const int IPC_SUB_GF_USER_ATTRIB = 103;							//用户属性
    public const int IPC_SUB_GF_CUSTOM_FACE = 104;							//自定头像
    public const int IPC_SUB_GF_KICK_USER = 105;                           //用户踢出
    public const int IPC_SUB_GF_QUICK_TRANSPOS = 106;                               //用户换位
    public const int IPC_SUB_GAME_START = 107;						//游戏开始
    public const int IPC_SUB_GAME_FINISH = 108;						//游戏结束

    //内核命令
    public const int MDM_KN_COMMAND = 0;							//内核命令
    public const int SUB_KN_DETECT_SOCKET = 1;							//检测命令
    public const int SUB_KN_VALIDATE_SOCKET = 2;								//验证命令

    public const int MDM_KN_COMMAND_LAJI = 65534;						//内核命令
    public const int MDM_KN_COMMAND_LAJI2 = 65533;							//内核命令
    public const int MDM_KN_COMMAND_LAJI3 = 65532;						//内核命令
    public const int MDM_KN_COMMAND_LAJI4 = 65531;							//内核命令
    public const int MDM_KN_COMMAND_LAJI5 = 65530;							//内核命令
    public const int MDM_KN_COMMAND_LAJI6 = 65529;							//内核命令

    //////////////////////////////////////////////////////////////////////////////////
    //传输数据

    public const int IPC_VER = 1;		 					//版本标识
    public const int IPC_IDENTIFIER = 1; //标识号码
                                         // public const int IPC_PACKET					(10240-sizeof(IPC_Head))			//最大包长
                                         // public const int IPC_BUFFER					(sizeof(IPC_Head)+IPC_PACKET)		//缓冲长度
}

public class CMD_TR_RoomConnect : NetPacket
{
    public word wKindID;                           //名称索引
    public word wServerID;                         //房间索引
    public word wServerPort;                       //房间端口
    public dword dwUserID;                           //用户ID
};

//房间信息
public class IPC_GF_ServerInfo : NetPacket
{
    //用户信息
    public word wTableID;                          //桌子号码
    public word wChairID;                          //椅子号码
    public dword dwUserID;                         //用户 I D

    //用户权限
    public dword dwUserRight;                      //用户权限
    public dword dwMasterRight;                        //管理权限

    //房间信息
    public word wKindID;                           //类型标识
    public word wServerID;                         //房间标识
    public word wServerType;                       //房间类型
    public dword dwServerRule;                     //房间规则
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_SERVER)]
    public tchar[] szServerName;//[LEN_SERVER];         //房间名称

    //视频配置
    public word wAVServerPort;                     //服务端口
    public dword dwAVServerAddr;                       //服务地址

    public word wChairCount;                       //椅子数目
    public dword dwClientVersion;                  //游戏版本
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_SERVER)]
    public tchar[] szGameName;//[LEN_KIND];             //游戏名字
};

//用户信息
public class IPC_GF_UserInfo : NetPacket
{
    public byte cbCompanion;                       //用户关系
    public tagUserInfoHead UserInfoHead;                       //用户信息
};

//用户积分
public class IPC_GF_UserScore : NetPacket
{
    public dword dwUserID;                         //用户标识
    public byte cbReason;                           //改变类型
    public tagUserScore UserScore;                         //用户积分
};


//用户状态
public class IPC_GF_UserStatus : NetPacket
{
    public dword dwUserID;                         //用户标识
    public tagUserStatus UserStatus;                           //用户状态
};

//游戏配置
public class CMD_GF_GameOption : NetPacket
{
    public byte cbAllowLookon;                     //旁观标志
    public dword dwFrameVersion;                       //框架版本
    public dword dwClientVersion;                  //游戏版本
};

//数据包头
public class IPC_Head : NetPacket
{
    public word wVersion;                          //版本标识
    public word wPacketSize;                       //数据大小
    public word wMainCmdID;                            //主命令码
    public word wSubCmdID;                         //子命令码
};

//IPC 包结构
public class IPC_Buffer : NetPacket
{
    public IPC_Head Head;                              //数据包头
    [PacketFieldAttribute(ArraySizeConst = CMD_Transfer.IPC_VER)]
    public byte cbBuffer;//[CMD_Transfer.IPC_VER];              //数据缓冲
};