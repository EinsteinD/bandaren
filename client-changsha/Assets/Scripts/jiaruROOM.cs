using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using KWX;
using KWX_From;
using UnityEngine.UI;
using System.Collections.Generic;
using MiniJSON;
public class jiaruROOM : MonoBehaviour {
    public Text Nick;
    public Text ID;
    uint JuShu = 8;
    public Text KouJiZhang;
    public Text FangKaShu;
    public Image TouXiang;
    public void SeletJuShu(int jushu)
    {
        JuShu = (uint)jushu;
        KouJiZhang.text = (jushu / 8).ToString();
    }
	// Use this for initialization
    void Start()
    {
        ID.text = "ID:" + GameManage.GetInstan.result_UserInfo.UserID.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Awake(){
        string TimeChuo = GameManage.GetInstan.GetTimeChuo().ToString();
        string Token = GameManage.GetInstan.Getmd5JiaMi(TimeChuo, GameManage.GetInstan.LoginUserID.ToString());
        Debuger.Log("Token为" + Token + "UserID为" + GameManage.GetInstan.LoginUserID.ToString() + "TimeChuo为" + TimeChuo);
        StartCoroutine(DownUserInfo(Token,TimeChuo,GameManage.GetInstan.LoginUserID.ToString()));
    }
	public void CreateRoom()
	{
        CMD_CreateRoom creatroom = new CMD_CreateRoom() { GameName = "CSMJ", Rule = "哈哈", RoundCount = JuShu };
        GameManage.GetInstan.JuShu = creatroom.RoundCount;
		KWXFrom.GetIns.LoginCmd.SendCreatRoom(creatroom);
	}
	public void jiaRuRoom()
	{
//         CMD_EnterRoom entroom = new CMD_EnterRoom() { RoomID = uint.Parse(fanghao.text) };
// 		GameManage.GetInstan.RoomID = entroom.RoomID;
//         KWXFrom.GetIns.LoginCmd.SendEntRoom(entroom);
    }
    IEnumerator DownUserInfo(string Token, string TimeChuo, string userid)
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
            GameManage.GetInstan.Nick = data["nickname"] as string;
            Nick.text = GameManage.GetInstan.Nick;
        }
        if (data.ContainsKey("gender"))
        {
            int sx = (int)data["gender"];
            GameManage.GetInstan.Sex = sx == 2 ? 0 : 1;
        }
        string strHttpImag = "";
        if (data.ContainsKey("headImg"))
        {
            strHttpImag = data["headImg"] as string;
        }
        Debuger.Log("TOUXIANGDIZHI" + strHttpImag);
        if (strHttpImag == "")
        {
            Debuger.Log("没头像");
            yield break;
        }
        if (data.ContainsKey("cards"))
        {
            FangKaShu.text = ((int)data["cards"]).ToString();
        }

        if (GameManage.GetInstan.DicTX.ContainsKey(strHttpImag))
        {
            TouXiang.sprite = GameManage.GetInstan.DicTX[strHttpImag];
        }
        else
        {
            WWW downLoadHead = new WWW(strHttpImag);
            yield return downLoadHead;
            GameManage.GetInstan.DicTX.Add(strHttpImag, Sprite.Create(downLoadHead.texture, new Rect(0, 0, downLoadHead.texture.width, downLoadHead.texture.height), new Vector2(0.5f, 0.5f)));
            GameManage.GetInstan.TouXiang = GameManage.GetInstan.DicTX[strHttpImag];
            TouXiang.sprite = GameManage.GetInstan.DicTX[strHttpImag];
        }
    }
}
