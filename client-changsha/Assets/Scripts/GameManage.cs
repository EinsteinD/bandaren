using UnityEngine;
using System.Collections;
using KWX_From;
using KWX;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;
using System.Security.Cryptography;
using System.Text;

namespace UnityEngine
{
    [Obsolete("少年郎请使用Debuger类 这个类已经放弃", true)]
    partial class Debug { };
}
public class GameManage : MonoBehaviour,LoginInterface,GameInterface {
    #region GameInterface implementation
	public void LinkGameServerOK ()
	{
        IsJianCeNet = true;//当链接游戏服务器成功时开启断线检测
		//登陆游戏服务器
		CMD_LoginServer LoginServer_ = new CMD_LoginServer()
		{
			Password = Password,
			RoomID = RoomID,
			SeatID = SeatID
		};
		KWXFrom.GetIns.GameCmd.SendLoginGameServer(LoginServer_);
	}
	public void EntServer (RSP_LoginServer result)
	{
		Loom.QueueOnMainThread (() => {
			SceneManager.LoadScene("Room");
		});
	}

	public void HearBeat (RSP_HeartBeat result)
	{
        Loom.QueueOnMainThread(() =>
        {
            Debuger.Log("收到心跳包");
        });
	}

	public void ExitRoom (RSP_ExitRoom result)
    {
        Loom.QueueOnMainThread(() =>
        {
            Debuger.Log("离开房间了");
            SceneManager.LoadScene("daTing");
        });
	}

	public void OnRoomUserInfo (NTF_RoomUserInfo result)
    {
        Loom.QueueOnMainThread(() =>
        {
            int TempViewSeatID = (int)GameManage.GetInstan.GetViewSeatID(result.SeatID);

            CreatCard.GetInstance_.UserInfoS[GetViewSeatID(result.SeatID)].ID = result.UserID;

            CreatCard.GetInstance_.UserInfoS[TempViewSeatID].gameObject.SetActive(true);
            string TimeChuo = GameManage.GetInstan.GetTimeChuo().ToString();
            string Token = GameManage.GetInstan.Getmd5JiaMi(TimeChuo, result.UserID.ToString());
            Debuger.Log(TimeChuo + "  " + Token);
            CreatCard.GetInstance_.UserInfoS[TempViewSeatID].Nick.text = result.UserID.ToString();
           
            StartCoroutine(DownUserInfo(Token, TimeChuo, result.UserID.ToString(), CreatCard.GetInstance_.UserInfoS[TempViewSeatID]));
        });
	}
    public long GetTimeChuo()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds);
    }
    public MD5 md5;
    string SALT_A = "SbtATxjoo989000x*29lyp";
    string SALT_B = "SbtB009XVVWDVLXX89#S)X";
    public string Getmd5JiaMi(string TimeChuo, string UserID)
    {
        md5 = new MD5CryptoServiceProvider();
        string s1 = TimeChuo, st2 = UserID;
        byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(SALT_A + st2));
        string s3 = BitConverter.ToString(output).Replace("-", "").ToLower();
        byte[] output2 = md5.ComputeHash(Encoding.Default.GetBytes(s1 + s3 + SALT_B));
        string s4 = BitConverter.ToString(output2).Replace("-", "").ToLower();
        Debuger.Log(s4);
        return s4;
    }
    IEnumerator DownUserInfo(string Token, string TimeChuo, string userid, UserInfo us)
    {
        WWW www = new WWW("http://" + GameManage.GetInstan.IPAdress + ":8080/csmj/user/info?uid=" + userid +
            "&timestamp=" + TimeChuo +
            "&token=" + Token);
        yield return www;

        if (www.error != null)
        {
            Debuger.Log("获取个人信息失败" + www.error);
            yield break;
        }
        Debuger.Log(www.text);
        Dictionary<string, object> Dic = Json.Deserialize(www.text) as Dictionary<string, object>;
        if (((int)Dic["ret"]) != 0)
        {
            Debuger.Log("获取个人信息出错");
            yield break;
        }
        if (!Dic.ContainsKey("data"))
        {
            yield break;
        }
        Dictionary<string, object> data = Dic["data"] as Dictionary<string, object>;
        if (data.ContainsKey("nickname"))
        {
            us.Nick.text = data["nickname"] as string;
        }
        if (data.ContainsKey("gender"))
        {
            int sx = (int)data["gender"];
            us.sex = sx == 2 ? 0 : 1;
            Debuger.Log("男女：" + us.sex);
        }
        string strHttpImag = "";
        if (data.ContainsKey("headImg"))
        {
            strHttpImag = data["headImg"] as string;
        }
        if (strHttpImag == "")
        {
            Debuger.Log("没头像");
            yield break;
        }

        if (GameManage.GetInstan.DicTX.ContainsKey(strHttpImag))
        {
            us.TX.sprite = GameManage.GetInstan.DicTX[strHttpImag];
        }
        else
        {
            WWW downLoadHead = new WWW(strHttpImag);
            yield return downLoadHead;
            GameManage.GetInstan.DicTX.Add(strHttpImag, Sprite.Create(downLoadHead.texture, new Rect(0, 0, downLoadHead.texture.width, downLoadHead.texture.height), new Vector2(0.5f, 0.5f)));
            us.TX.sprite = GameManage.GetInstan.DicTX[strHttpImag];
        }
    }
	public void OnSameUserLogin (NTF_SameUserLogin result)
	{
		
	}

	public void OnRoomUserOnline (NTF_RoomUserOnline result)
	{
        Loom.QueueOnMainThread(() =>
        {
            CreatCard.GetInstance_.UserInfoS[GetViewSeatID(result.SeatID)].LiXian.SetActive(false);
        });
	}

	public void OnRoomUserOffline (NTF_RoomUserOffline result)
	{
		Loom.QueueOnMainThread(() =>
        {
            CreatCard.GetInstance_.UserInfoS[GetViewSeatID(result.SeatID)].LiXian.SetActive(true);  
        });
	}
    public Action<NTF_RoomDismissed> jieSanLe=null;
	public void OnRoomDismissed (NTF_RoomDismissed result)
	{
        Loom.QueueOnMainThread(() =>
        {
            if (jieSanLe!=null)
            {
                jieSanLe(result);
            }
            else
            {

            }
        });
	}

    public Action<uint,string> TalkMsg_Action = null;
	public void OnMessage (NTF_Message result)
	{
        if (TalkMsg_Action!=null)
        {
            TalkMsg_Action(result.SeatID, result.Message);
        }
	}



	#endregion

	#region LoginInterface implementation

	public void LinkLoginServerOK ()
	{
		
	}
    public RSP_Login result_UserInfo;
	public void LoginOK (RSP_Login result)
	{
		Loom.QueueOnMainThread (() => {
            DisMessageBOX();
            result_UserInfo = result;
            Debuger.Log("DLHD" + result.UserID);
            GameManage.GetInstan.LoginUserID = result.UserID;
            if (result_UserInfo.RoomID != 0)//登陆时如果在房间内则不为0 代表断线重连
            {
                CMD_EnterRoom entroom = new CMD_EnterRoom() { RoomID = result_UserInfo.RoomID };
                GameManage.GetInstan.RoomID = result_UserInfo.RoomID;
                KWXFrom.GetIns.LoginCmd.SendEntRoom(entroom);
            }
            else
            {
                SceneManager.LoadScene("daTing");
            }
		});
	}
	public void EntCartRoom (RSP_CreateRoom result)
	{
		Loom.QueueOnMainThread (() => {
			Host=result.ServerHost;
			Port=result.ServerPort;
			Password=result.Password;
			SeatID=result.SeatID;
			RoomID=result.RoomID;
			Debuger.Log(result.ServerHost);
			KWXFrom.GetIns.InitGameFrom(result.ServerHost,result.ServerPort);
		});
	}
	public void EntRoom (RSP_EnterRoom result)
	{
		Loom.QueueOnMainThread (() => {
			Host=result.ServerHost;
			Port=result.ServerPort;
			Password=result.Password;
			SeatID=result.SeatID;
            JuShu = result.RoundCount;
			KWXFrom.GetIns.InitGameFrom(result.ServerHost,result.ServerPort);
		});
	}
	#endregion
	static GameManage inst;
	public string Host;
	public int Port;
	public string Password;
	public uint RoomID;
	public uint SeatID;
    public uint JuShu;
    public ulong LoginUserID;
    public string LoginPassWord;

    public string IPAdress = "";
    public Sprite TouXiang;
    public int Sex;
    public string Nick = "";
    public Dictionary<string, Sprite> DicTX = new Dictionary<string, Sprite>();
    public GameObject ZhuanZhuanPrifabe;
    GameObject ZhuanZhuanObject = null;
    public string WeiXinCodeToKen = "";
	public static GameManage GetInstan
	{
		get{ 
			return inst;
		}
	}
    public GameObject MessageBOXPrifabe;
    GameObject MessageBOXObject = null;
    public void DisMessageBOX()
    {
        Destroy(MessageBOXObject);
        MessageBOXObject = null;
    }
    public void SetMessageBOX(Transform trans, string BiaoTi, string NeiRong, Action actQuRen, Action actQuXiao)
    {
        if (MessageBOXObject != null)
        {
            DisMessageBOX();
        }
        MessageBOXObject = Instantiate(MessageBOXPrifabe);
        MessageBOXObject.transform.SetParent(trans);
        MessageBOXObject.transform.localPosition = new Vector3(0, 0, 0);
        MessageBOXObject.transform.localScale = Vector3.one;
        MessageBOXObject.transform.localEulerAngles = Vector3.zero;
        MessageBOXObject.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        MessageBOXObject.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        MessageBox msg = MessageBOXObject.GetComponent<MessageBox>();
        msg.BiaoTi.text = BiaoTi;
        msg.NeiRong.text = NeiRong;
        msg.QR.onClick.AddListener(
        () =>
        {
            actQuRen();
        });
        msg.QX.onClick.AddListener(
        () =>
        {
            actQuXiao();
        });
    }
    public void SetZhuanZhuan(Transform trans, string str)
    {
        if (ZhuanZhuanObject != null)
        {
            DisZhuanZhuan();
        }
        ZhuanZhuanObject = Instantiate(ZhuanZhuanPrifabe);
        ZhuanZhuanObject.transform.SetParent(trans);
        ZhuanZhuanObject.transform.localPosition = new Vector3(0, 0, 0);
        ZhuanZhuanObject.transform.localScale = Vector3.one;
        ZhuanZhuanObject.transform.localEulerAngles = Vector3.zero;
        ZhuanZhuanObject.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        ZhuanZhuanObject.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        ZhuanZhuanObject.transform.GetChild(1).GetComponent<Text>().text = str;
    }
    public void DisZhuanZhuan()
    {
        Debuger.Log("DisZhuanZhuan");
        Destroy(ZhuanZhuanObject);
        ZhuanZhuanObject = null;
    }
    public void OnLoginSuc(string jsonData)
    {
        Debuger.Log(jsonData);
    }
    public void OnLoginSuc_(string jsonData)
    {
        Debuger.Log(jsonData);

        Dictionary<string, object> Dic = Json.Deserialize(jsonData) as Dictionary<string, object>;
        int result = (int)Dic["result"];
        if (result != 0)
        {
            Debuger.Log("微信登陆失败");
            SetMessageBOX(GameObject.Find("Canvas").transform, "微信登陆失败", "微信登陆失败，请重新登陆", () =>
            {
                DisMessageBOX();
                DisZhuanZhuan();
            },
            () =>
            {
                DisMessageBOX();
                DisZhuanZhuan();
            });
            return;
        }
        string token = (string)Dic["token"];
        WeiXinCodeToKen = token;
        //测试登陆
        CMD_Login LOGINitem = new CMD_Login() { UserID = ulong.Parse("0"), Password = "liuyu", WeiXinCode = WeiXinCodeToKen };
        LoginPassWord = "liuyu";
        KWXFrom.GetIns.LoginCmd.SendLoginGame(LOGINitem, null);
        //CMD_Login LOGINitem;
        //LOGINitem = new CMD_Login() { UserID = 0, Password = "liuyu", WeiXinCode = GameManage.GetInstan.WeiXinCodeToKen };
        Debuger.Log("微信登陆 TOKEN:" + GameManage.GetInstan.WeiXinCodeToKen);

        //KWXFrom.GetIns.LoginCmd.SendLoginGame(LOGINitem, null);
    }
	// Use this for initialization
	void Awake () {
		inst = this;
		Debuger.EnableLog = 2;//开启日志打印 0关闭 1控制台下日志 2unity3d日志 日志打印请使用Debuger.Log
		KWXFrom.GetIns.Login_ = this;
		KWXFrom.GetIns.Game_ = this;
        KWXFrom.GetIns.InitLoginFrom("112.74.12.198", 4000);//172.16.9.234"112.74.12.198"
        IPAdress = "112.74.12.198";
		if (Loom.Current) {
			Debuger.Log ("初始化线程队列");
		}
        
#if (UNITY_ANDROID || UNITY_IPHONE) && (!UNITY_EDITOR)
        WeChatSDKInterface.Instance.OnLoginSuc = OnLoginSuc_;
#endif
        DontDestroyOnLoad(this);
	}
    public uint GetViewSeatID(uint i)
    {
        return (uint)(i + 4 - GameManage.GetInstan.SeatID) % 4;
    }
	void Start()
	{
		SceneManager.LoadScene("login");
	}
    float Time_XinTiao = 0;
    float Time_XinTiao_JianGe = 10;
    bool IsJianCeNet = false;
	// Update is called once per frame
	void Update () {
        if ((Time_XinTiao+=Time.deltaTime)>Time_XinTiao_JianGe)
        {
            Time_XinTiao = 0;
            if (KWXFrom.GetIns.GameCmd!=null)
            {
                Debuger.LogRed("发送心跳包");
                KWXFrom.GetIns.GameCmd.SendHeartBeat(new CMD_HeartBeat() { });
            }
        }
        if (IsJianCeNet)
        {
            if (Application.loadedLevelName == "Room" && NET_KWX.Instance_.mtcpClient != null && NET_KWX.Instance_.GetCurSTATE == CurSocket.Game_ && NET_KWX.Instance_.mtcpClient.Connected == false)//当在游戏中断线时
            {
                if (Application.internetReachability != NetworkReachability.NotReachable)//当网络可用时
                {
//                     Debuger.LogRed("断线重连");
//                     IsJianCeNet = false;
//                     GameStart = null;
//                     CMD_Login LOGINitem = new CMD_Login() { UserID = GameManage.GetInstan.LoginUserID, Password = GameManage.GetInstan.LoginPassWord, WeiXinCode = 0 };
//                     KWXFrom.GetIns.LoginCmd.SendLoginGame(LOGINitem, null);
                }
                else
                {
                    Debuger.LogRed("网络不可用");
                }
            }
        }
	}

    public Action<PB_Server_Room_Info> GameStart = null;

    public void OnGAME_STATE(PB_Server_Room_Info result)
    {
        Debuger.Log("OnGAME_STATE");
        Loom.QueueOnMainThread(() =>
        {
            if (GameStart != null)
            {
                GameStart(result);
            }
            else
            {
                Debuger.Log("GameStart没初始化成功");
            }
        });       
    }



    public Action<PB_Server_ALL_Bill_Info> ZongJieSuan = null;
    public void OnAllResult(PB_Server_ALL_Bill_Info result)
    {
        Loom.QueueOnMainThread(() =>
        {
            if (ZongJieSuan != null)
            {
                ZongJieSuan(result);
            }
            else
            {
                Debuger.Log("ZongJieSuan没初始化成功");
            }
        });       
    }
    public Action<PB_Server_Single_Bill> SingleJieSuan = null;
    public void OnSingleResult(PB_Server_Single_Bill result)
    {
        Loom.QueueOnMainThread(() =>
        {
            if (SingleJieSuan != null)
            {
                SingleJieSuan(result);
            }
            else
            {
                Debuger.Log("SingleJieSuan没初始化成功");
            }
        });       
    }
   
    public void DismisseMessage(NTF_Dismiss result)
    {
        Loom.QueueOnMainThread(() =>
        {
            CreatCard.GetInstance_.jieSan_view.SetActive(true);
            for (int i = 0; i < result.Action.Count; i++)
            {
                UIDismissRoom.Inst.player[i].textName.text = CreatCard.GetInstance_.UserInfoS[GetViewSeatID((uint)i)].Nick.text;
                switch (result.Action[i])
                {
                    case 0:
                        UIDismissRoom.Inst.player[i].textStatus.text = "等待确认";
                        if (i==SeatID)
                        {
                            UIDismissRoom.Inst.btnAgree.interactable = true;
                            UIDismissRoom.Inst.btnReject.interactable = true;
                        }
                        break;
                    case 1:
                        UIDismissRoom.Inst.player[i].textStatus.text = "申请解散";
                        if (i == SeatID)
                        {
                            UIDismissRoom.Inst.btnAgree.interactable = false;
                            UIDismissRoom.Inst.btnReject.interactable = false;
                        }
                        break;
                    case 2:
                        UIDismissRoom.Inst.player[i].textStatus.text = "同意解散";
                        if (i == SeatID)
                        {
                            UIDismissRoom.Inst.btnAgree.interactable = false;
                            UIDismissRoom.Inst.btnReject.interactable = false;
                        }
                        break;
                    case 3:
                        UIDismissRoom.Inst.player[i].textStatus.text = "拒绝解散";
                        if (i == SeatID)
                        {
                            UIDismissRoom.Inst.btnAgree.interactable = false;
                            UIDismissRoom.Inst.btnReject.interactable = false;
                        }
                        break;
                    default:
                        break;
                }
            }
        });       
    }
}
