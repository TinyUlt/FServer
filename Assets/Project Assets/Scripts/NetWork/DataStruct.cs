
using dword = System.UInt32;
using word = System.UInt16;
using tchar = System.Char;
using longlong = System.Int64;

public enum enConnectLogonType
{
    ConnectLogonType_User_Check,
    ConnectLogonType_User_Logon,
    ConnectLogonType_User_Reg,
    ConnectLogonType_CverifyPassWord,
    ConnectLogonType_Bank,
    ConnectLogonType_Bank_Take,
    ConnectLogonType_Bank_Save,
    ConnectLogonType_Bank_Transfer,
    ConnectLogonType_Lock,
    ConnectLogonType_Setting,
    ConnectLogonType_ChangePho,
    ConnectLogonType_ChangeFace,
    ConnectLogonType_User_Code,
    ConnectLogonType_CheckUpdate,
    ConnectLogonType_MoblieRegister,
    ConnectLogonType_INVALID,
    ConnectLogonType_RegisterType,
};


public class DBO_GP_LogonSuccess : NetPacket
{
    //属性资料
    public word wFaceID;                           //头像标识
    public dword dwUserID;                         //用户标识
    public dword dwGameID;                         //游戏标识
    public dword dwGroupID;                            //社团标识
    public dword dwCustomID;                           //自定索引
    public dword dwUserMedal;                      //用户奖牌
    public dword dwExperience;                     //经验数值
    public dword dwLoveLiness;                      //用户魅力
    public longlong lUserScore;                            //用户游戏币
    public longlong lUserInsure;                       //用户银行
                                                       //用户信息
    public byte cbGender;                          //用户性别
    public byte cbMoorMachine;                     //锁定机器
    public byte cbMaxStart;                          //工作线路
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ACCOUNTS)]
    public tchar[] szAccounts;//[LEN_ACCOUNTS];         //登录帐号
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ACCOUNTS)]
    public tchar[] szNickName;//[LEN_ACCOUNTS];         //用户昵称
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_GROUP_NAME)]
    public tchar[] szGroupName;//[LEN_GROUP_NAME];      //社团名字

    //附加信息
    public bool IsLockMobile;                      //手机绑定
    public byte cbMemberOrder;                     //会员等级
    public int RegisterFrom;                       //从哪里登陆过来 0华商 >0 其它地方
    public int IsChangePWD;                        //修改过密码   0没改，1修改过
    public int GameLogonTimes;                     //是否是第一次登陆 0第一次

    public int iError;
    [PacketFieldAttribute(ArraySizeConst = Define.Len_PASS_PORT_ID)]
    public tchar[] strPassPortID;//[Len_PASS_PORT_ID];
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MOBILE_PHONE)]
    public tchar[] strRegisterMobile;//[LEN_MOBILE_PHONE];

    //配置信息
    public byte cbShowServerStatus;                 //显示服务器状态
    public byte byServerLinkCount;
    public byte byDownLoadLinkCount;
    [PacketFieldAttribute(ArraySizeConst = 50, ArraySizeConst2 = 32)]
    public tchar[][] szDownServerLinkUrl;//[50][32];	 //登录帐号
    [PacketFieldAttribute(ArraySizeConst = 20, ArraySizeConst2 = 64)]
    public tchar[][] szDownLoadUrl;//[20][64];	 //登录帐号
};

public class CMD_GP_LogonAccounts : NetPacket
{
    //系统信息
    public dword dwPlazaVersion;                       //广场版本
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MACHINE_ID)]
    public tchar[] szMachineID;//[LEN_MACHINE_ID];      //机器序列
    public dword dwClientIP;                           //玩家IP

    //登录信息
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szPassword;//[LEN_MD5];              //登录密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ACCOUNTS)]
    public tchar[] szAccounts;//[LEN_ACCOUNTS];         //登录帐号
    public byte cbValidateFlags;                   //校验标识
    [PacketFieldAttribute(ArraySizeConst = 9)]
    public tchar[] szPhonePassword;//[9];
    public dword dwLogonType;
    public int dwClientVersion;
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szCdkey;//[LEN_MD5];
};

//大厅登陆失败
public class CMD_GP_LogonFailure : NetPacket
{
    public int lErrorCode;      //错误代码
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar[] szDescribeString;//[128];				//错误描述
};

// 注册帐号
public class CMD_GP_RegisterAccounts : NetPacket
{
    //系统信息
    public dword dwPlazaVersion;                       //广场版本
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MACHINE_ID)]
    public tchar[] szMachineID;//[LEN_MACHINE_ID];      //机器序列
    public dword dwClientIP;                           //玩家IP
                                                       //密码变量
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szLogonPass;//[LEN_MD5];             //登录密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szInsurePass;//[LEN_MD5];                //银行密码
                                //注册信息
    public word wFaceID;                           //头像标识
    public byte cbGender;                          //用户性别
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ACCOUNTS)]
    public tchar[] szAccounts;//[LEN_ACCOUNTS];         //登录帐号
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];         //用户昵称
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ACCOUNTS)]
    public tchar[] szSpreader;//[LEN_ACCOUNTS];         //推荐帐号
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASS_PORT_ID)]
    public tchar[] szPassPortID;//[LEN_PASS_PORT_ID];       //证件号码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_COMPELLATION)]
    public tchar[] szCompellation;//[LEN_COMPELLATION]; //真实名字
    public byte cbValidateFlags;                   //校验标识

    public dword dwValidateId;
    [PacketFieldAttribute(ArraySizeConst = 5)]
    public tchar[] szValidateCode;//[5];
    public int cbRegisterType;
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szCdkey;//[LEN_MD5];
};


//用户信息
public class tagGlobalUserData : NetPacket
{
    //基本资料
    public dword dwUserID;                         //用户 I D
    public dword dwGameID;                         //游戏 I D
    public dword dwExperience;                     //用户经验
    public dword dwLoveLiness;                     //用户魅力
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ACCOUNTS)]
    public tchar[] szAccounts;//[LEN_ACCOUNTS];         //登录帐号
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];         //用户昵称
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szPassword;//[LEN_PASSWORD];         //登录密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szBankPassWord;//[LEN_PASSWORD];      //银行密码
                                  //用户成绩
    public longlong lUserScore;                            //用户游戏币
    public longlong lUserInsure;                       //用户银行
    public dword dwUserMedal;                      //用户奖牌

    //扩展资料
    public byte cbGender;                          //用户性别
    public byte cbMoorMachine;                     //锁定机器
    public byte cbMaxStart;
    //tchar							szUnderWrite[LEN_UNDER_WRITE];		//个性签名

    //社团资料
    public dword dwGroupID;                            //社团索引
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_GROUP_NAME)]
    public tchar[] szGroupName;//[LEN_GROUP_NAME];      //社团名字

    //会员资料
    public byte cbMemberOrder;                     //会员等级
                                                   //SYSTEMTIME						MemberOverDate;						//到期时间
                                                   //头像信息
    public word wFaceID;                           //头像索引
    public dword dwCustomID;                           //自定标识
                                                       //tagCustomFaceInfo				CustomFaceInfo;						//自定头像
                                                       //是否绑定手机
    public bool IsLockMB;
    public int iError;
    //TCHAR							strPassPortID[20];
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MOBILE_PHONE)]
    public tchar[] strRegisterMobile;//[LEN_MOBILE_PHONE];

    public int RegisterFrom;                       //从哪里登陆过来 0华商 >0 其它地方
    public int IsChangePWD;                        //修改过密码   0没改，1修改过
    public int GameLogonTimes;                     //是否是第一次登陆 0第一次

    public dword dwCdkey;

};

// 注册码

public class CMD_GP_GetValidateCode : NetPacket
{
    public dword dwValidateId;
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar szValidateCode;//[128];
};

// 游戏类型
public class tagGameTypes : NetPacket
{
    public tagGameType[] datas;

    public override void Deserialize(byte[] bytes)
    {
        tagGameType data = new tagGameType();
        int count = bytes.Length / NetPacketSize(typeof(tagGameType));
        datas = new tagGameType[count];
        ByteBuffer bb = new ByteBuffer(bytes);
        for (int i = 0; i < count; i++)
        {
            tagGameType value = new tagGameType();
            value.Deserialize(bb);
            datas[i] = value;
        }
    }

    public override byte[] Serialize()
    {
        ByteBuffer buffer = new ByteBuffer();
        for (int i = 0; i < datas.Length; i++)
        {
            datas[i].Serialize(buffer);
        }
        return buffer.ToArray();
    }
}

public class tagGameType : NetPacket
{
    public word wJoinID;                           //挂接索引
    public word wSortID;                           //排序索引
    public word wTypeID;                           //类型索引
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_TYPE)]
    public tchar[] szTypeName;//[LEN_TYPE];				//种类名字
};

// 游戏种类
public class tagGameKinds : NetPacket
{
    public tagGameKind[] datas;

    public override void Deserialize(byte[] bytes)
    {
        tagGameKind data = new tagGameKind();
        int count = bytes.Length / NetPacketSize(typeof(tagGameKind));
        datas = new tagGameKind[count];
        ByteBuffer bb = new ByteBuffer(bytes);
        for (int i = 0; i < count; i++)
        {
            tagGameKind value = new tagGameKind();
            value.Deserialize(bb);
            datas[i] = value;
        }
    }

    public override byte[] Serialize()
    {
        ByteBuffer buffer = new ByteBuffer();
        for (int i = 0; i < datas.Length; i++)
        {
            datas[i].Serialize(buffer);
        }
        return buffer.ToArray();
    }
}

public class tagGameKind : NetPacket
{
    public word wTypeID;                           //类型索引
    public word wJoinID;                           //挂接索引
    public word wSortID;                           //排序索引
    public word wKindID;                           //类型索引
    public word wGameID;                           //模块索引
    public word wPlazaType;
    public word wDiff;                               //安卓更新方式                             
    public dword dwOnLineCount;                        //在线人数
    public dword dwFullCount;                      //满员人数
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_KIND)]
    public tchar[] szKindName;//[LEN_KIND];             //游戏名字
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PROCESS)]
    public tchar[] szProcessName;//[LEN_PROCESS];           //进程名字

    [PacketFieldAttribute(ArraySizeConst = Define.LEN_DWELLING_PLACE)]
    public tchar[] szMBDownloadUrl;//[LEN_DWELLING_PLACE]; //下载地址LEN_DWELLING_PLACE
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_DWELLING_PLACE)]
    public tchar[] szAppStoreUrl;//[LEN_DWELLING_PLACE];   //App Store
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_DWELLING_PLACE)]
    public tchar[] szOpenUrl;//[LEN_DWELLING_PLACE];       //打开地址
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szGameVerison;//[LEN_MD5];               //版本验证

    [PacketFieldAttribute(ArraySizeConst = Define.LEN_DWELLING_PLACE)]
    public tchar[] szUpdatgeUrl;//[LEN_DWELLING_PLACE];       //打开地址
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_DWELLING_PLACE)]
    public tchar[] szCheckCode;//[LEN_DWELLING_PLACE];       //打开地址
};

// 游戏房间

public class tagGameServers : NetPacket
{
    public tagGameServer[] datas;

    public override void Deserialize(byte[] bytes)
    {
        tagGameServer data = new tagGameServer();
        int count = bytes.Length / NetPacketSize(typeof(tagGameServer));
        datas = new tagGameServer[count];
        ByteBuffer bb = new ByteBuffer(bytes);
        for (int i = 0; i < count; i++)
        {
            tagGameServer value = new tagGameServer();
            value.Deserialize(bb);
            datas[i] = value;
        }
    }

    public override byte[] Serialize()
    {
        ByteBuffer buffer = new ByteBuffer();
        for (int i = 0; i < datas.Length; i++)
        {
            datas[i].Serialize(buffer);
        }
        return buffer.ToArray();
    }
}

public class tagGameServer : NetPacket
{
    public word wKindID;                           //名称索引
    public word wNodeID;                           //节点索引
    public word wSortID;                           //排序索引
    public word wServerID;                         //房间索引
    public word wServerPort;                       //房间端口
    public dword dwOnLineCount;                        //在线人数
    public dword dwFullCount;                      //满员人数
    [PacketFieldAttribute(ArraySizeConst = 32)]
    public tchar[] szServerAddr;//[32];                 //房间名称
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_SERVER)]
    public tchar[] szServerName;//[LEN_SERVER];         //房间名称
    public longlong lMaxEnterScore;                        //最大进入分数
    public longlong lMinEnterScore;                     //最小进入分数
};


public class CMD_GP_SysemMessage : NetPacket
{
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_USER_CHAT)]
    public tchar[] szSystemMessage;//[LEN_USER_CHAT];       //系统消息
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_USER_CHAT)]
    public tchar[] szMessageUrl;//[LEN_USER_CHAT];      //系统消息
    public longlong tConcludeTime;                      //结束时间
};

// 发送喇叭

public class CMD_GR_S_SendTrumpet : NetPacket
{
    public word wPropertyIndex;                      //道具索引 
    public dword dwSendUserID;                         //用户 I D
    public dword TrumpetColor;                        //喇叭颜色
    [PacketFieldAttribute(ArraySizeConst = 32)]
    public tchar[] szSendNickName;//[32];                   //玩家昵称
    [PacketFieldAttribute(ArraySizeConst = 32)]
    public tchar[] szSendUnderWrite;//[32];         //发送用户签名
    [PacketFieldAttribute(ArraySizeConst = Define.TRUMPET_MAX_CHAR)]
    public tchar[] szTrumpetContent;//[TRUMPET_MAX_CHAR];  //喇叭内容
};

//返回喇叭个数

public class CMD_GetLaBaCostOrNum : NetPacket
{
    public int mScore_xlb;
    public int mScore_dlb;
    public int mxlb_Number;
    public int mdlb_Number;
};

//获取喇叭个数

public class CMD_GR_GetUserProp : NetPacket
{
    public dword mUserID;
};

// 房间登录失败

public class CMD_GR_LogonFailure : NetPacket
{
    public word wLockServerid;
    public int lErrorCode;                         //错误代码
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar[] szDescribeString;//[128];				//描述消息
};


public class CMD_GP_Send_Efficacy : NetPacket
{
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MOBILE_PHONE)]
    public tchar[] szMobileNumber;//[LEN_MOBILE_PHONE];
    public dword dwUserID;
};


public class CMD_GP_Start_Efficacy : NetPacket
{
    public int nEfficacy;
    public dword dwUserID;
    public dword dwPlazaVersion;
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];            //用户昵称
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szPassword;//[LEN_MD5];                //密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MOBILE_PHONE)]
    public tchar[] szMbPhone;//[LEN_MOBILE_PHONE];        //手机
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MACHINE_ID)]
    public tchar[] szMachineID;//[LEN_MACHINE_ID];      //机器序列
    public dword dwClientIP;                           //玩家IP
    public int cbRegisterType;
};


public class NetMessageHead : NetPacket  //73
{
    public dword uMessageBeginFlag;                    //包头标志   4
    public dword dwCmdFlag;                            //命令标识      4
    public dword dwClientAddr;                     //连接地址          4
    public int mRandCnt;                           //随机种子               4
    public dword extend;                               //扩展字段           4
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public byte[] md5key;//[LEN_MD5];                     //md5key  33
    public dword dwPort;                               //连接端口          4
    public word dwPacketSize;                      //数据大小            2
    public word dwEncodeSize;                      //数据大小           2
    public dword uMessageEndFlag;                  //包尾标志        4
    public longlong ulCmdNo;                          //                      8
}

// 查询信息

public class CMD_GP_QueryIndividual : NetPacket
{
    public dword dwPlazaVersion;
    public dword dwUserID;                         //用户 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szPassword;//[LEN_PASSWORD];			//用户密码
};

// 修改密码

public class CMD_GP_ModifyLogonPass : NetPacket
{
    public dword dwUserID;                         //用户 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szDesPassword;//[LEN_PASSWORD];      //用户密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szScrPassword;//[LEN_PASSWORD];		//用户密码
};

// 修改密码

public class CMD_GP_ModifyInsurePass : NetPacket
{
    public dword dwUserID;                         //用户 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szDesPassword;//[LEN_PASSWORD];      //用户密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szScrPassword;//[LEN_PASSWORD];		//用户密码
};

// 修改资料

public class CMD_GP_ModifyIndividual : NetPacket
{
    //验证资料
    public byte cbGender;                          //用户性别
    public dword dwUserID;                         //用户 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szPassword;//[LEN_PASSWORD];			//用户密码
};

// 个人资料

public class CMD_GP_UserIndividual : NetPacket
{
    public dword dwUserID;                         //用户 I D
    public byte cbGender;
};

// 绑定机器

public class CMD_GP_ModifyMachine : NetPacket
{
    public byte cbBind;                                //绑定标志
    public dword dwUserID;                         //用户标识
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szPassword;//[LEN_PASSWORD];         //用户密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MACHINE_ID)]
    public tchar[] szMachineID;//[LEN_MACHINE_ID];		//机器序列
};

// 扩展资料

public class tagIndividualUserData : NetPacket
{
    //用户信息
    public dword dwUserID;                         //用户 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_USER_NOTE)]
    public tchar[] szUserNote;//[LEN_USER_NOTE];            //用户说明
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_USER_NOTE)]
    public tchar[] szUserWrite;//[LEN_USER_NOTE];
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_COMPELLATION)]
    public tchar[] szCompellation;//[LEN_COMPELLATION]; //真实名字

    //电话号码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_SEAT_PHONE)]
    public tchar[] szSeatPhone;//[LEN_SEAT_PHONE];      //固定电话
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MOBILE_PHONE)]
    public tchar[] szMobilePhone;//[LEN_MOBILE_PHONE];  //移动电话

    //联系资料
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_QQ)]
    public tchar[] szQQ;//[LEN_QQ];                     //Q Q 号码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_EMAIL)]
    public tchar[] szEMail;//[LEN_EMAIL];                   //电子邮件
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_DWELLING_PLACE)]
    public tchar[] szDwellingPlace;//[LEN_DWELLING_PLACE];//联系地址
};


public class CMD_GP_GameNewNum : NetPacket
{
    public word mNewSum;
};


public class CMD_GP_RecoidCnts : NetPacket
{
    public int m_RecoidCounts;
};


public class CMD_GP_Speaker_Info : NetPacket
{
    public word mMessagId;
    [PacketFieldAttribute(ArraySizeConst = 255)]
    public tchar[] szMessageInfo;//[255];
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ACCOUNTS)]
    public tchar[] szNickName;//[LEN_ACCOUNTS];
};


public class CMD_GP_SysemMessageHttp : NetPacket
{
    string sMessageTitle;      //系统消息
    string szMessageUrl;       //系统消息
    string tConcludeTime;     //结束时间
};


public class CMD_GP_QueryAppCheck : NetPacket
{
    public byte byPlazaType;                  //平台分类
    public word wKindid;                     //游戏id
    public word wAppversion;                  //app当前版本
};


public class CMD_GP_AppCheckSuccess : NetPacket
{
    bool IsShowLogonPng;
    public word wStatus;                     //是否更新版本
    public word wAppcheck;                     //游戏id
    [PacketFieldAttribute(ArraySizeConst = 256)]
    public tchar[] szUpdateUrl;//[256];             //更新地址
    public dword dwAzSize;
    [PacketFieldAttribute(ArraySizeConst = 256)]
    public tchar[] szAzUpdateUrl;//[256];             //更新地址
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szAzMd5;//[LEN_MD5];
    public int bQudaoShowMore;                  //安卓渠道是否显示更多
    //win32&andriod
    //public int                              bConstraint;                    //是否启动强制升级
};


public class CMD_PC_UPDATEVN : NetPacket
{
    public int m_PcVesion;
};


public class CMD_NEXT_UPDATE : NetPacket
{
    public dword dwNextTimer;
};


public class CMD_GP_REGISTER_TYPE : NetPacket
{
    public int mType;
};


public class CMD_GP_Constraint : NetPacket		      //登陆的时候强制更新大厅 优先于后台推送
{
    public int iDownLoadSize;        //文件大小
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar[] szDownLoadName;//[128];    //更新包
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar[] szDescribeString;//[128];	 //描述消息
};


public class CMD_GP_PC_CHECK_UPDATE : NetPacket
{
    public dword dwSercwerVN;
    public dword dwNextTimer;
    public dword dwAzSize;
    [PacketFieldAttribute(ArraySizeConst = 256)]
    public tchar[] szAzUpdateUrl;//[256];             //更新地址
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szAzMd5;//[LEN_MD5];
};


//登录银行

public class CMD_GP_UserLogonBank : NetPacket
{
    public dword dwUserID;                         //用户 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szPassword;//[LEN_MD5];              //银行密码
    public dword dwCdkey;
};

//登录银行成功

public class CMD_GP_UserLogonBankSuccess : NetPacket
{
    public dword dwUserID;                         //用户 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szPassword;//[LEN_MD5];              //银行密码
    public dword dwCdkey;
};

//查询银行

public class CMD_GP_QueryInsureInfo : NetPacket
{
    public dword dwUserID;                         //用户 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szPassword;//[LEN_MD5];				//银行密码
};

//存入金币

public class CMD_GP_UserSaveScore : NetPacket
{
    public dword dwPlazaVersion;                       //广场版本
    public dword dwUserID;                         //用户 I D
    public dword dwIP;
    //用户成绩
    public longlong lSaveScore;                            //用户游戏币
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MACHINE_ID)]
    public tchar[] szMachineID;//[LEN_MACHINE_ID];		//机器序列
};

//提取金币

public class CMD_GP_UserTakeScore : NetPacket
{
    public dword dwPlazaVersion;                       //广场版本
    public dword dwUserID;                         //用户 I D
    public dword dwIP;
    public longlong lTakeScore;                            //用户游戏币
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szPassword;//[LEN_MD5];              //银行密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MACHINE_ID)]
    public tchar[] szMachineID;//[LEN_MACHINE_ID];		//机器序列
};

// 银行资料

public class CMD_GP_UserInsureInfo : NetPacket
{
    public word wRevenueTake;                      //税收比例
    public word wRevenueTransfer;                  //税收比例
    public word wServerID;                         //房间标识
    public longlong lUserScore;                            //用户游戏币
    public longlong lUserInsure;                       //用户银行
    public longlong lTransferPrerequisite;             //转账条件
    [PacketFieldAttribute(ArraySizeConst = 64)]
    public tchar[] szLastLoginTime;//[64];
    public byte cbAccountsProtect;
};

// 
// 银行成功

public class CMD_GP_UserInsureSuccess : NetPacket
{
    public dword dwUserID;                         //用户 I D
    public longlong lUserScore;                            //用户游戏币
    public longlong lUserInsure;                       //用户银行
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar[] szDescribeString;//[128];				//描述消息
};
// 
// 银行失败

public class CMD_GP_UserInsureFailure : NetPacket
{
    public int lResultCode;                        //错误代码
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar[] szDescribeString;//[128];				//描述消息
};

// 银行失败

public class CMD_GP_BankLogonFailure : NetPacket
{

};

// 
// 查询用户

public class CMD_GP_QueryUserInfoRequest : NetPacket
{
    public byte cbByNickName;                       //昵称赠送
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];			//目标用户
};
// 
// 用户信息

public class CMD_GP_UserTransferUserInfo : NetPacket
{
    public dword dwTargetGameID;                       //目标用户
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];			//目标用户
};
// 
// 转账金币

public class CMD_GP_UserTransferScore : NetPacket
{
    public dword dwPlazaVersion;
    public dword dwUserID;                         //用户 I D
    public dword dwIP;
    public dword dwGameID;
    public longlong lTransferScore;                            //转账金币
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szPassword;//[LEN_MD5];              //银行密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];         //目标用户
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MACHINE_ID)]
    public tchar[] szMachineID;//[LEN_MACHINE_ID];      //机器序列
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szCdkey;//[LEN_MD5];
};
// 
// 查询明细

public class CMD_GP_QueryBankDetail : NetPacket
{
    public dword dwPlazaVersion;                       //广场版本
    public dword dwUserID;                         //用户 I D
    public byte cbTransferIn;
};


public class CMD_GP_QueryReturnSocre : NetPacket
{
    public dword dwUserID;
};

// 获取数目

public class CMD_GP_GetUserReturnScore : NetPacket
{
    public dword dwUserID;  //用户ID
    public dword dwCount;	 //获取数目
};


public class CMD_GP_ReturnYSSocre : NetPacket
{
    public dword dwUserID;
    public longlong lScore;
};


public class CMD_GR_ChangeReturnInfo : NetPacket
{
    bool bFinsh;                            //兑换结果
    public longlong llReturncount;
};


// 明细结果

public class CMD_GP_BankDetailResults : NetPacket
{
    public CMD_GP_BankDetailResult[] datas;

    public override void Deserialize(byte[] bytes)
    {
        tagGameServer data = new tagGameServer();
        int count = bytes.Length / NetPacketSize(typeof(tagGameServer));
        datas = new CMD_GP_BankDetailResult[count];
        ByteBuffer bb = new ByteBuffer(bytes);
        for (int i = 0; i < count; i++)
        {
            CMD_GP_BankDetailResult value = new CMD_GP_BankDetailResult();
            value.Deserialize(bb);
            datas[i] = value;
        }
    }

    public override byte[] Serialize()
    {
        ByteBuffer buffer = new ByteBuffer();
        for (int i = 0; i < datas.Length; i++)
        {
            datas[i].Serialize(buffer);
        }
        return buffer.ToArray();
    }
};

public class CMD_GP_BankDetailResult : NetPacket
{
    public byte cbState;
    public byte cbTransferIn;
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];
    public dword dwGameID;
    public longlong lSwapScore;
    [PacketFieldAttribute(ArraySizeConst = 64)]
    public tchar[] szDateTime;//[64];
};
// 
// 
// ///////////答题结构/////////////////////////////////////////////////////////////
// 

public class CMD_GP_Question : NetPacket
{
    public dword dwUserID;
};


public class CMD_GP_Answer : NetPacket
{
    public dword dwUserID;
    public int mcorrect;
};
// 
// //////////////////////////房间结构//////////////////////////////////////////////
//I D 登录


public class CMD_GR_LogonUserID : NetPacket
{
    //版本信息
    public dword dwPlazaVersion;                       //广场版本
    public dword dwFrameVersion;                       //框架版本
    public dword dwProcessVersion;                 //进程版本

    //登录信息
    public dword dwUserID;                         //用户 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MD5)]
    public tchar[] szPassword;//[LEN_MD5];              //登录密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MACHINE_ID)]
    public tchar[] szMachineID;//[LEN_MACHINE_ID];      //机器序列
    public word wKindID;                           //类型索引
    public dword dwClientIP;                           //玩家IP
    public int dwVersion;
    public int dwLoginFrom;
};
// 
// 列表子项

public class tagColumnItem : NetPacket
{
    public byte cbColumnWidth;                     //列表宽度
    public byte cbDataDescribe;                        //字段类型
    [PacketFieldAttribute(ArraySizeConst = 16)]
    public tchar[] szColumnName;//[16];					//列表名字
};


//列表配置

public class CMD_GR_ConfigColumn : NetPacket
{
    public byte cbColumnCount;                     //列表数目
    [PacketFieldAttribute(ArraySizeConst = 18 * Define.MAX_COLUMN)]
    tagColumnItem[] ColumnItem;//[MAX_COLUMN];				//列表描述
};
// 
// 房间配置

public class CMD_GR_PcConfigServer : NetPacket
{
    //房间属性
    public word wTableCount;                       //桌子数目
    public word wChairCount;                       //椅子数目

    //房间配置
    public word wServerType;                       //房间类型
    public dword dwServerRule;                     //房间规则
    public longlong lGoldLeast;
    public longlong lGoldMost;
};
// 房间配置

public class CMD_GR_ConfigServer : NetPacket
{
    //房间属性
    public word wTableCount;                       //桌子数目
    public word wChairCount;                       //椅子数目

    //房间配置
    public word wServerType;                       //房间类型
    public dword dwServerRule;						//房间规则
};
// 
// 
// //道具配置
// public class CMD_GR_ConfigProperty
// {
// 	public byte							cbPropertyCount;					//道具数目
// 	tagPropertyInfo					PropertyInfo[MAX_PROPERTY];			//道具描述
// };

//玩家权限

public class CMD_GR_ConfigUserRight : NetPacket
{
    public dword dwUserRight;						//玩家权限
};
// 
// 桌子状态

public class tagTableStatus : NetPacket
{
    public byte cbTableLock;                       //锁定标志
    public byte cbPlayStatus;						//游戏标志
};

//用户状态

public class tagUserStatus : NetPacket
{
    public word wTableID;                          //桌子索引
    public word wChairID;                          //椅子位置
    public byte cbUserStatus;						//用户状态
};
// 
// 桌子信息

public class CMD_GR_TableInfo : NetPacket
{
    public word wTableCount;                       //桌子数目
    [PacketFieldAttribute(ArraySizeConst =  512)]
	public tagTableStatus[] TableStatusArray;//[512];				//桌子状态
};

//桌子状态'

public class CMD_GR_TableStatus : NetPacket
{
    public word wTableID;                          //桌子号码
	public tagTableStatus TableStatus;						//桌子状态
};
// 
//时间信息

public class tagTimeInfo
{
    public dword dwEnterTableTimer;                        //进出桌子时间
    public dword dwLeaveTableTimer;                        //离开桌子时间
    public dword dwStartGameTimer;                     //开始游戏时间
    public dword dwEndGameTimer;							//离开游戏时间
};
// 
//用户信息

public class tagUserInfo : NetPacket
{
    //基本属性
    public dword dwUserID;                         //用户 I D
    public dword dwGameID;                         //游戏 I D
    public dword dwGroupID;                            //社团 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];         //用户昵称
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_GROUP_NAME)]
    public tchar[] szGroupName;//[LEN_GROUP_NAME];      //社团名字
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_UNDER_WRITE)]
    public tchar[] szUnderWrite;//[LEN_UNDER_WRITE];        //个性签名
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_QQ)]
    public tchar[] szUserQQ;//[LEN_QQ];
    //头像信息
    public word wFaceID;                           //头像索引
    public dword dwCustomID;                           //自定标识

    //用户资料
    public byte cbGender;                          //用户性别
    public byte cbMemberOrder;                     //会员等级
    public byte cbMasterOrder;                     //管理等级

    //用户状态
    public word wTableID;                          //桌子索引
    public word wChairID;                          //椅子索引
    public byte cbUserStatus;                      //用户状态

    //积分信息
    public longlong lScore;                                //用户分数
    public longlong lGrade;                                //用户成绩
    public longlong lInsure;                           //用户银行

    //游戏信息
    public dword dwWinCount;                           //胜利盘数
    public dword dwLostCount;                      //失败盘数
    public dword dwDrawCount;                      //和局盘数
    public dword dwFleeCount;                      //逃跑盘数
    public dword dwUserMedal;                      //用户奖牌
    public dword dwExperience;                     //用户经验
    public int lLoveLiness;                        //用户魅力

    //时间信息
    tagTimeInfo TimerInfo;                          //时间信息

    //比赛信息
    public byte cbEnlistStatus;                        //报名状态

    //扩展标识
    public int lExpand;
    public dword dwExpand;

	public bool bIsAndroidUser;
	public bool bIsMobileUser;

    public override void Deserialize(byte[] bytes)
    {
        tagUserInfoHead head = new tagUserInfoHead();
        ByteBuffer buffer = new ByteBuffer(bytes);
        head.Deserialize(buffer);

        //用户属性
        wFaceID = head.wFaceID;
        dwGameID = head.dwGameID;
        dwUserID = head.dwUserID;
        dwGroupID = head.dwGroupID;
        dwCustomID = head.dwCustomID;
        bIsAndroidUser = head.bIsAndroidUser;
        //用户状态
        wTableID = head.wTableID;
        wChairID = head.wChairID;
        cbUserStatus = head.cbUserStatus;

        //用户属性
        cbGender = head.cbGender;
        cbMemberOrder = head.cbMemberOrder;
        cbMasterOrder = head.cbMasterOrder;
        //用户积分
        lScore = head.lScore;
        lGrade = head.lGrade;
        lInsure = head.lInsure;
        dwWinCount = head.dwWinCount;
        dwLostCount = head.dwLostCount;
        dwDrawCount = head.dwDrawCount;
        dwFleeCount = head.dwFleeCount;
        dwUserMedal = head.dwUserMedal;
        dwExperience = head.dwExperience;
        lLoveLiness = head.lLoveLiness;
        bIsMobileUser = head.bIsMobileUser;
        lExpand = head.dwLogForm;
        while (true)
        {
            if (buffer.Length <= buffer.Position)
                break;

            word datasize = buffer.ReadUShort();
            word datades = buffer.ReadUShort();
            if (datades == 0)
                break;
            switch (datades)
            {
                case Define.DTP_GR_NICK_NAME: //用户昵称
                    char[] nickNamevalue = buffer.ReadChars(datasize);
                    szNickName = nickNamevalue;
                    break;
                case Define.DTP_GR_GROUP_NAME: //用户社团
                    char[] groupNamevalue = buffer.ReadChars(datasize);
                    szGroupName = groupNamevalue;
                    break;
                case Define.DTP_GR_UNDER_WRITE: //个性签名
                    char[] underWritevalue = buffer.ReadChars(datasize);
                    szUnderWrite = underWritevalue;
                    break;
                case Define.DTP_GR_USER_QQ: //QQ
                    char[] qqvalue = buffer.ReadChars(datasize);
                    szUserQQ = qqvalue;
                    break;
            }

        }
    }

    public override byte[] Serialize()
    {
        tagUserInfoHead head = new tagUserInfoHead();
        ByteBuffer buffer = new ByteBuffer();


        //用户属性
        head.wFaceID = wFaceID;
        head.dwGameID = dwGameID;
        head.dwUserID = dwUserID;
        head.dwGroupID = dwGroupID;
        head.dwCustomID = dwCustomID;
        head.bIsAndroidUser = bIsAndroidUser;
        //用户状态
        head.wTableID = wTableID;
        head.wChairID = wChairID;
        head.cbUserStatus = cbUserStatus;

        //用户属性
        head.cbGender = cbGender;
        head.cbMemberOrder = cbMemberOrder;
        head.cbMasterOrder = cbMasterOrder;
        //用户积分
        head.lScore = lScore;
        head.lGrade = lGrade;
        head.lInsure = lInsure;
        head.dwWinCount = dwWinCount;
        head.dwLostCount = dwLostCount;
        head.dwDrawCount = dwDrawCount;
        head.dwFleeCount = dwFleeCount;
        head.dwUserMedal = dwUserMedal;
        head.dwExperience = dwExperience;
        head.lLoveLiness = lLoveLiness;
        head.bIsMobileUser = bIsMobileUser;
        head.dwLogForm = (ushort)lExpand;
        head.Serialize(buffer);
        if (szNickName != null)
        {
            buffer.Write((word)szNickName.Length);
            buffer.Write(Define.DTP_GR_NICK_NAME);
            buffer.Write(szNickName);
        }
        if (szGroupName != null)
        {
            buffer.Write((word)szGroupName.Length);
            buffer.Write(Define.DTP_GR_GROUP_NAME);
            buffer.Write(szGroupName);
        }
        if (szUnderWrite != null)
        {
            buffer.Write((word)szUnderWrite.Length);
            buffer.Write(Define.DTP_GR_UNDER_WRITE);
            buffer.Write(szUnderWrite);
        }
        if (szUserQQ != null)
        {
            buffer.Write((word)szUserQQ.Length);
            buffer.Write(Define.DTP_GR_USER_QQ);
            buffer.Write(szUserQQ);
        }
        return buffer.ToArray();
    }
};
// 
// 头像信息

public class tagCustomFaceInfo : NetPacket
{
    public dword dwDataSize;                           //数据大小
    [PacketFieldAttribute(ArraySizeConst = Define.FACE_CX * Define.FACE_CY)]
    public dword[] dwCustomFace;//[Define.FACE_CX * Define.FACE_CY];		//图片信息
};
// 
// 

public class tagUserInfoHead : NetPacket
{
    //用户属性
    public dword dwGameID;                         //游戏 I D
    public dword dwUserID;                         //用户 I D
    public dword dwGroupID;                            //社团 I D

    //头像信息
    public word wFaceID;                           //头像索引
    public dword dwCustomID;                           //自定标识

    //用户属性
    public byte cbGender;                          //用户性别
    public byte cbMemberOrder;                     //会员等级
    public byte cbMasterOrder;                     //管理等级

    //用户状态
    public word wTableID;                          //桌子索引
    public word wChairID;                          //椅子索引
    public byte cbUserStatus;                      //用户状态

    //积分信息
    public longlong lScore;                                //用户分数
    public longlong lGrade;                                //用户成绩
    public longlong lInsure;                           //用户银行

    //游戏信息
    public dword dwWinCount;                           //胜利盘数
    public dword dwLostCount;                      //失败盘数
    public dword dwDrawCount;                      //和局盘数
    public dword dwFleeCount;                      //逃跑盘数
    public dword dwUserMedal;                      //用户奖牌
    public dword dwExperience;                     //用户经验
    public int lLoveLiness;                        //用户魅力

    public bool bIsAndroidUser;                        //是否机器人
    public bool bIsMobileUser;

    public word dwLogForm;                          //平台类型
                                                    //TCHAR                           szUserQQ[LEN_QQ];
                                                    //
                                                    //DWORD                           dwMasterRight;
};
// 
// ////////////////////////////////////////////////////////////////////////////////
// 
// 系统消息

public class CMD_CM_SystemMessage : NetPacket
{
    public word wType;                             //消息类型
    public word wLength;                           //消息长度
    [PacketFieldAttribute(ArraySizeConst = 1024)]
    public tchar[] szString;//[1024];						//消息内容
};


//动作信息

public class tagActionHead : NetPacket
{
    public int uResponseID;                        //响应标识
    public word wAppendSize;                       //附加大小
    public byte cbActionType;						//动作类型
};
// 
// 浏览动作

public class tagActionBrowse : NetPacket
{
    public byte cbBrowseType;                      //浏览类型
    [PacketFieldAttribute(ArraySizeConst = 256)]
    public tchar[] szBrowseUrl;//[256];					//浏览地址
};

//下载动作

public class tagActionDownLoad : NetPacket
{
    public byte cbDownLoadMode;                        //下载方式
    [PacketFieldAttribute(ArraySizeConst = 256)]
    public tchar[] szDownLoadUrl;//[256];					//下载地址
};

//动作消息

public class CMD_CM_ActionMessage : NetPacket
{
    public word wType;                             //消息类型
    public word wLength;                           //消息长度
    public int nButtonType;                        //按钮类型
    [PacketFieldAttribute(ArraySizeConst = 1024)]
    public tchar[] szString;//[1024];						//消息内容
};
// 
// ////////////////////////////////////////////////////////////////////////////////
// 
// 下载信息

public class CMD_CM_DownLoadModule : NetPacket
{
    public byte cbShowUI;                          //显示界面
    public byte cbAutoInstall;                     //自动安装
    public word wFileNameSize;                     //名字长度
    public word wDescribeSize;                     //描述长度
    public word wDownLoadUrlSize;					//地址长度
};

/////////////////房间银行消息///////////////////////////////////////////////////////

//查询银行

public class CMD_GR_C_QueryInsureInfoRequest : NetPacket
{
    public byte cbActivityGame;                     //游戏动作
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szInsurePass;//[LEN_PASSWORD];			//银行密码
};
// 
// 存款请求

public class CMD_GR_C_SaveScoreRequest : NetPacket
{
    public byte cbActivityGame;                     //游戏动作
    public longlong lSaveScore;							//存款数目
};

//取款请求

public class CMD_GR_C_TakeScoreRequest : NetPacket
{
    public byte cbActivityGame;                     //游戏动作
    public longlong lTakeScore;                            //取款数目
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szInsurePass;//[LEN_PASSWORD];			//银行密码
};


//转账金币

public class CMD_GP_C_TransferScoreRequest : NetPacket
{
    public byte cbActivityGame;                     //游戏动作
    public byte cbByNickName;                       //昵称赠送
    public longlong lTransferScore;                        //转账金币
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];         //目标用户
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szInsurePass;//[LEN_PASSWORD];			//银行密码
};
// 
// 查询明细

public class CMD_GR_QueryBankDetail : NetPacket
{
    public dword dwUserID;                         //用户 I D
    public byte cbTransferIn;
};


//查询用户

public class CMD_GR_C_QueryUserInfoRequest : NetPacket
{
    public byte cbActivityGame;                     //游戏动作
    public byte cbByNickName;                       //昵称赠送
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];			//目标用户
};

//查询密码

public class CMD_GR_C_CheckInsurePassword : NetPacket
{
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szInsurePass;//[LEN_PASSWORD];			//银行密码
};


//银行资料

public class CMD_GR_S_UserInsureInfo : NetPacket
{
    public byte cbActivityGame;                     //游戏动作
    public word wRevenueTake;                      //税收比例
    public word wRevenueTransfer;                  //税收比例
    public word wServerID;                         //房间标识
    public longlong lUserScore;                            //用户金币
    public longlong lUserInsure;                       //银行金币
    public longlong lTransferPrerequisite;				//转账条件
};
// 
// 银行成功

public class CMD_GR_S_UserInsureSuccess : NetPacket
{
    public byte cbActivityGame;                     //游戏动作
    public longlong lUserScore;                            //身上金币
    public longlong lUserInsure;                       //银行金币
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar[] szDescribeString;//[128];				//描述消息
};

//银行失败

public class CMD_GR_S_UserInsureFailure : NetPacket
{
    public byte cbActivityGame;                     //游戏动作
    public int lErrorCode;                         //错误代码
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar[] szDescribeString;//[128];				//描述消息
};

//用户信息

public class CMD_GR_S_UserTransferUserInfo : NetPacket
{
    public byte cbActivityGame;                     //游戏动作
    public dword dwTargetGameID;                       //目标用户
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];			//目标用户
};

//明细结果

public class CMD_GR_BankDetailResult : NetPacket
{
    public byte cbState;
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_NICKNAME)]
    public tchar[] szNickName;//[LEN_NICKNAME];
    public dword dwGameID;
    public longlong lSwapScore;
    [PacketFieldAttribute(ArraySizeConst = 64)]
    public tchar[] szDateTime;//[64];
};
// 
// 修改头像

public class CMD_GP_SystemFaceInfo : NetPacket
{
    public word wFaceID;                           //头像标识
    public dword dwUserID;                         //用户 I D
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szPassword;//[LEN_PASSWORD];         //用户密码
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_MACHINE_ID)]
    public tchar[] szMachineID;//[LEN_MACHINE_ID];		//机器序列
};
//用户头像

public class CMD_GP_UserFaceInfo : NetPacket
{
    public word wFaceID;                           //头像标识
    public dword dwCustomID;							//自定标识
};

//用户聊天

public class CMD_GR_C_UserChat : NetPacket
{
    public word wChatLength;                       //信息长度
    public dword dwChatColor;                      //信息颜色
    public dword dwTargetUserID;                       //目标用户	
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_USER_CHAT)]
    public tchar[] szChatString;//[LEN_USER_CHAT];		//聊天信息
};




//银行失败

public class CMD_GP_UserHronFailure : NetPacket
{
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar[] szDescribeString;//[128];				//描述消息
};

//道具失败

public class CMD_GR_PropertyFailure : NetPacket
{
    public word wRequestArea;                       //请求区域
    long lErrorCode;                            //错误代码
    [PacketFieldAttribute(ArraySizeConst = 256)]
    public tchar[] szDescribeString;//[256];				//描述信息
};
// 
// 
// 用户聊天

public class CMD_GR_S_UserChat : NetPacket
{
    public word wChatLength;                       //信息长度
    public dword dwChatColor;                      //信息颜色
    public dword dwSendUserID;                     //发送用户
    public dword dwTargetUserID;                       //目标用户
    [PacketFieldAttribute(ArraySizeConst = 32)]
    public tchar[] szSendUserNickName;//[32];               //发送用户昵称
    [PacketFieldAttribute(ArraySizeConst = 32)]
    public tchar[] szSendUserUnderWrite;//[32];         //发送用户签名
    [PacketFieldAttribute(ArraySizeConst = 32)]
    public tchar[] szTargetUserNickName;//[32];         //目标用户昵称
    [PacketFieldAttribute(ArraySizeConst = 32)]
    public tchar[] szTargetUserUnderWrite;//[32];           //目标用户签名
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_USER_CHAT)]
    public tchar[] szChatString;//[LEN_USER_CHAT];		//聊天信息
};
//发送喇叭

public class CMD_GR_C_SendTrumpet : NetPacket
{
    public byte cbRequestArea;                        //请求范围 
    public word wPropertyIndex;                      //道具索引 
    public dword TrumpetColor;                        //喇叭颜色
    [PacketFieldAttribute(ArraySizeConst = Define.TRUMPET_MAX_CHAR)]
    public tchar[] szTrumpetContent;//[TRUMPET_MAX_CHAR];  //喇叭内容
};




//发送喇叭

public class CMD_GR_Send_SendTrumpet : NetPacket
{
    public word wPropertyIndex;                      //道具索引
    public dword dwSendUserID;                         //用户 I D
    public dword dwClientIP;							//玩家IP
    public dword TrumpetColor;                        //喇叭颜色
    [PacketFieldAttribute(ArraySizeConst = 32)]
    public tchar[] szSendNickName;//[32];                    //玩家昵称
    [PacketFieldAttribute(ArraySizeConst = 32)]
    public tchar[] szSendUnderWrite;//[32];          //发送用户签名
    [PacketFieldAttribute(ArraySizeConst = Define.TRUMPET_MAX_CHAR)]
    public tchar[] szTrumpetContent;//[TRUMPET_MAX_CHAR];  //喇叭内容
};
// 
// 用户积分

public class tagUserScore : NetPacket
{
    //积分信息
    public longlong lScore;                                //用户分数
    public longlong lGrade;                                //用户成绩
    public longlong lInsure;                           //用户银行

    //输赢信息
    public dword dwWinCount;                           //胜利盘数
    public dword dwLostCount;                      //失败盘数
    public dword dwDrawCount;                      //和局盘数
    public dword dwFleeCount;                      //逃跑盘数

    //全局信息
    public dword dwUserMedal;                      //用户奖牌
    public dword dwExperience;                     //用户经验
    public int lLoveLiness;						//用户魅力
};

//用户分数

public class CMD_GR_UserScore : NetPacket
{
    public dword dwUserID;                         //用户标识
    public byte cbReason;                           //改变类型
    public tagUserScore UserScore;							//积分信息
};

//用户状态

public class CMD_GR_UserStatus : NetPacket
{
    public dword dwUserID;                         //用户标识
    public tagUserStatus UserStatus;                           //用户状态
    public longlong lScore;
    public longlong lInsure;
};

//坐下请求

public class CMD_GR_UserSitDown : NetPacket
{
    public word wTableID;                          //桌子位置
    public word wChairID;                          //椅子位置
    public byte cbIphoneSit;                        //手机端坐下  
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_PASSWORD)]
    public tchar[] szPassword;//[LEN_PASSWORD];			//桌子密码
};
// 
// 旁观请求

public class CMD_GR_UserIphone : NetPacket
{
    public dword dwUserID;
    public word wTableID;                          //桌子位置
    public word wChairID;							//椅子位置
};

//旁观请求

public class CMD_GR_UserLookon : NetPacket
{
    public word wTableID;                          //桌子位置
    public word wChairID;							//椅子位置
};

//请求失败

public class CMD_GR_RequestFailure : NetPacket
{
    public int lErrorCode;                         //错误代码
    [PacketFieldAttribute(ArraySizeConst = 256)]
    public tchar[] szDescribeString;//[256];				//描述信息
};

//起立请求

public class CMD_GR_UserStandUp : NetPacket
{
    public word wTableID;                          //桌子位置
    public word wChairID;                          //椅子位置
    public byte cbForceLeave;						//强行离开
};



//网络发送
// 
// public class IPC_GF_SocketSend
// {
//     TCP_Command CommandInfo;                        //命令信息
//     [PacketFieldAttribute(ArraySizeConst = Define.)]
//     public byte cbBuffer[SOCKET_TCP_PACKET];		//数据缓冲
// };
// 
// //网络接收
// 
// public class IPC_GF_SocketRecv
// {
//     TCP_Command CommandInfo;                        //命令信息
//     public byte cbBuffer[SOCKET_TCP_PACKET];		//数据缓冲
// };



//用户规则

public class CMD_GR_UserRule : NetPacket
{
    public byte cbRuleMask;                            //规则掩码
    public word wMinWinRate;                       //最低胜率
    public word wMaxFleeRate;                      //最高逃率
    public int lMaxGameScore;                      //最高分数
    public int lMinGameScore;						//最低分数
};


public class CMD_GP_QuestionMessage : NetPacket
{
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_QUESTION)]
    public tchar[] szQuestion;//[LEN_QUESTION];
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ANSWER)]
    public tchar[] szanswerA;//[LEN_ANSWER];
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ANSWER)]
    public tchar[] szanswerB;//[LEN_ANSWER];
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ANSWER)]
    public tchar[] szanswerC;//[LEN_ANSWER];
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ANSWER)]
    public tchar[] szanswerD;//[LEN_ANSWER];
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_ANSWER)]
    public tchar[] szstandard;//[LEN_ANSWER];
};


public class CMD_GP_LeaveMessage : NetPacket
{
    public int mLeaveCount;
};


public class CMD_GP_Rewards : NetPacket
{
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_QUESTION)]
    public tchar[] szRewards;//[LEN_QUESTION];
};

//发送抽奖结果

public class CMD_GR_LuckyResult : NetPacket
{
    public dword wUserid;
    [PacketFieldAttribute(ArraySizeConst = Define.LEN_LUCKY_RESULT)]
    public tchar[] szRewards;//[LEN_LUCKY_RESULT];
};


public class CMD_GR_ResultReturn : NetPacket
{
    [PacketFieldAttribute(ArraySizeConst = 128)]
    public tchar[] szDescribeString;//[128];                //描述消息
    public int lReturn;
};


public class CMD_GP_MissionNum : NetPacket
{
    public dword dwUserID;
};


public class CMD_GR_MissionNum : NetPacket
{
    public int dwLevel1;
    public int dwLevel2;
    public int dwLevel3;
};

public class CMD_GP_NoStruct : NetPacket
{

};


public class CMD_GF_GameStatus : NetPacket
{
    public byte cbGameStatus;
    public byte cbAllowLookon;
};

public class CMD_GF_GameScene : NetPacket
{
    public byte[] data;

    public override void Deserialize(byte[] bytes)
    {
        data = bytes;
    }

    public override byte[] Serialize()
    {
        return data;
    }
};
