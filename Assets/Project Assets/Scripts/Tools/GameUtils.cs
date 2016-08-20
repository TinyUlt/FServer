using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;

using dword = System.UInt32;
using word = System.UInt16;
using tchar = System.Char;
using longlong = System.Int64;
using System.Runtime.InteropServices;

public class GameUtils
{

    private static GameUtils _instance = null;
    private GameUtils() { }
	private Action GameQuit;
    public static GameUtils GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameUtils();
        }
        return _instance;
    }

    //获取平台类型
    public UInt32 GetLogonType()
    {
#if UNITY_ANDROID
			return 2;
#elif UNITY_IPHONE
		return 1;
#elif UNITY_STANDALONE_WIN
        if ((Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major == 6))
            return 4;
        else
            return 3;
#else
			return 0;
#endif
    }

    //获取本地IP
    public UInt32 GetIP()
    {
        IPAddress ipAddr = null;
        IPAddress[] arrIP = Dns.GetHostAddresses(Dns.GetHostName());
        foreach (IPAddress ip in arrIP)
        {
            if (System.Net.Sockets.AddressFamily.InterNetwork.Equals(ip.AddressFamily))
            {
                ipAddr = ip;
            }
            else if (System.Net.Sockets.AddressFamily.InterNetworkV6.Equals(ip.AddressFamily))
            {
                ipAddr = ip;
            }
        }
        return BitConverter.ToUInt32(ipAddr.GetAddressBytes(), 0);
    }

    //获取大厅版本，目前写死
    public dword GetPlazaVersion()
    {
        return GetProcessVersion(13, 0, 3);

        // return 101646339;//Convert.ToUInt32(((PRODUCT_VER) << 24) + (((cbMainVer)) << 16) + ((cbSubVer) << 8) + (cbBuildVer));
    }

    //获取框架版本
    public dword GetFrameVersion()
    {
        return GetProcessVersion(6, 0, 3);
    }

    //模块版本
    public dword GetProcessVersion(byte cbMainVer, byte cbSubVer, byte cbBuildVer)
    {
        return (dword)(
            (((byte)(Define.PRODUCT_VER)) << 24) +
            (((byte)(cbMainVer)) << 16) +
            ((byte)(cbSubVer) << 8)) +
            ((byte)(cbBuildVer));
    }

    //获取标示
    public byte GetValidateFlags()
    {
        return Messages.MB_VALIDATE_FLAGS | Messages.LOW_VER_VALIDATE_FLAGS;
    }

    //字符串转MD5
    public tchar[] GetMD5String(string strText)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(strText));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < encryptedBytes.Length; i++)
        {
            sb.AppendFormat("{0:x2}", encryptedBytes[i]);
        }
        return sb.ToString().ToUpper().ToCharArray();
    }

    //获取char数组String
    public string GetCharString(tchar[] aString)
    {
        return System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Unicode.GetBytes(aString));
    }

    //截取字符串，按照中文两字符处理
    public string SubString(string aString, int lenth)
    {
        string _outString = "";
        int _len = 0;
        for (int i = 0; i < aString.Length; i++)
        {
            if (Char.ConvertToUtf32(aString, i) >= Convert.ToInt32("4e00", 16) && Char.ConvertToUtf32(aString, i) <= Convert.ToInt32("9fff", 16))
            {
                _len += 2;
                if (_len > lenth)//截取的长度若是最后一个占两个字节，则不截取
                {
                    break;
                }
            }
            else
            {
                _len++;
            }


            try
            {
                _outString += aString.Substring(i, 1);
            }
            catch
            {
                break;
            }
            if (_len >= lenth)
            {
                break;
            }
        }
        if (aString != _outString)//判断是否添加省略号
        {
            _outString += "...";
        }
        return _outString;
    }

    //字符串是否含有中文
    public bool IsHasChineseWords(string aString)
    {
        string text = aString;
        for (int i = 0; i < text.Length; i++)
        {
            if ((int)text[i] > 127)
                return true;
        }
        return false;
    }

    //整形转string，4位插入逗号
    public string ToMoneyString(longlong score)
    {
        if (score == 0) return "0";
        string theStr = score.ToString();
        int startScan = 0;
        if (score < 0)
        {
            startScan = 1;
        }

        int j = 0;
        for (int i = (int)(theStr.Length) - 4; i > startScan; i -= 4)
        {
            theStr = theStr.Insert(j + i, ",");
            j = 0;
        }
        return theStr;
    }

    public UInt32 IPToUInt32(string address)
    {
        var adds = address.Split(new char[] { '.' });
        if (adds.Length < 4)
        {
            UnityEngine.Debug.Log("!!!!!!");
        }
        long i = long.Parse(adds[3]) << 24 | long.Parse(adds[2]) << 16 | long.Parse(adds[1]) << 8 | long.Parse(adds[0]);

        return Convert.ToUInt32(i);
    }

    public void ShowMessageBox(string message, string title = "友情提示", UnityEngine.Events.UnityAction callback1 = null)
    {
//        var dialog = GameManager.GetInstance().CreateUIByObject(GameManager.GetInstance().dialog);
//        dialog.GetComponent<UIDialogListener>().UIDialogInit(message, title, callback1);
    }

    public void ShowMessageBox(tchar[] message, string title = "友情提示", UnityEngine.Events.UnityAction callback1 = null)
    {
//        var dialog = GameManager.GetInstance().CreateUIByObject(GameManager.GetInstance().dialog);
//        dialog.GetComponent<UIDialogListener>().UIDialogInit(new string(message), title, callback1);
    }

    public string SwitchScoreString(char[] aString)
    {
        string tempStr = new string(aString);
        RemoveComma(ref tempStr);
        aString = tempStr.ToCharArray();

        if (aString.Length == 0)
            return "";

        List<string> SzVtChinaString = new List<string>();
        string[] WEI = { " ", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "万亿", "拾", "佰", "仟" };
        string[] NUM = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖", "拾" };
        int n = 1;
        int k = 1;
        int i = 1;

        n = aString.Length;
        if (aString[0] != '0')
        {
            for (k = n, i = 0; i < k; i++, n--)
            {
                if (aString[i] == '0')
                {
                    if ((n - 1) % 4 == 0)
                    {
                        if (n == 9)
                        {
                            if (aString[i - 1] == '0' && aString[i - 2] == '0' && aString[i - 3] == '0')
                            {
                                if (aString[i + 1] != '0')
                                {
                                    SzVtChinaString.Add("零");
                                    continue;
                                }
                                else if (aString[i + 2] != '0'
                                    || aString[i + 3] != '0' || aString[i + 4] != '0')
                                    continue;
                                else if ((aString[i + 1] == '0' && aString[i + 2] == '0')
                                    || (aString[i + 3] == '0' && aString[i + 4] == '0'))
                                    continue;
                            }
                        }
                        if (n == 5)
                        {
                            if (aString[i - 1] == '0' && aString[i - 2] == '0' && aString[i - 3] == '0')
                            {
                                if (aString[i + 1] != '0')
                                {
                                    SzVtChinaString.Add("零");
                                    continue;
                                }
                                else if (aString[i + 2] != '0'
                                    || aString[i + 3] != '0' || aString[i + 4] != '0')
                                    continue;
                                else if ((aString[i + 1] == '0' && aString[i + 2] == '0')
                                    || (aString[i + 3] == '0' && aString[i + 4] == '0'))
                                    continue;
                            }
                        }
                        SzVtChinaString.Add(WEI[n - 1]);
                        continue;
                    }
                    if (aString[i + 1] == '0')
                    {
                        continue;
                    }
                    SzVtChinaString.Add(NUM[(int)(aString[i] - '0')]);
                    continue;
                }
                SzVtChinaString.Add(NUM[(int)(aString[i] - '0')]);
                SzVtChinaString.Add(WEI[n - 1]);
            }
        }
        string RetString = "";
        for (int u = 0; u < SzVtChinaString.Count; u++)
        {
            RetString += SzVtChinaString[u];
        }
        return RetString;
    }
    public string CharToString(char[] value)
    {
        var end = GetCharEnd(value);
        if (end != 0)
        {
            char[] result = new char[end];
            for (int i = 0; i < end; i++)
            {
                result[i] = value[i];
            }
            return new string(result);
        }
        return null;
    }

    public int GetCharEnd(char[] value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] == '\0')
            {
                return i;
            }
        }
        return value.Length;
    }
    longlong[] lLevelScore = new long[]
        {
        0L,99999,499999,999999,1499999,1999999,2999999,3999999,6999999,10999999,
        29999999,99999999,
        };
    public int SelectRoomUserFaceID(tagUserInfo pUserInfo)
    {
        int wLevel = 1;
        if (pUserInfo == null) return wLevel;

        for (int i = 0; i < lLevelScore.Length; i++)
        {
            if (pUserInfo.lScore > lLevelScore[i])
                wLevel = (i + 1);
        }

        if (pUserInfo.cbGender == 0)
        {
            return wLevel;
        }
        else
        {
            return wLevel + 12;
        }
    }
    public string RemoveComma(ref string aString)
    {
        if (aString.Length == 0) return "";
        aString = aString.Replace(",", "");

        return aString;
    }

    public string ExceptMoneyString(string tempStr)
    {
        string aString = tempStr;
        for (int i = 0; i < aString.Length; i++)
        {
            if (aString[i] < 48 || aString[i] > 57)
            {
                if (aString[i] != 44)
                    aString = aString.Remove(i, 1);
            }
        }

        aString = RemoveComma(ref aString);
        if (!string.IsNullOrEmpty(aString))
            aString = ToMoneyString(Int64.Parse(aString));

        return aString;
    }

    public char[] GetMachineID()
    {
        return "b1a6afedf9cbc767ac8ff04fe997655a".ToCharArray();
    }
	public void QuitCurrentGame()
	{
		if(GameQuit != null){
			GameQuit ();
		}
	}
	public void RegisterGameQuit(Action callBack){
		GameQuit = callBack;
	}
}
