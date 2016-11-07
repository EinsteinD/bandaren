using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using KWX;
using KWX_From;

public class ShuRuFangJianHao : MonoBehaviour {
    public Text[] Texs;
    public Button[] Button_s;
	// Use this for initialization
	void Start () {
        ClearAllText();
        for (int i = 0; i < Button_s.Length; i++)
        {
            int Temp = i;
            Button_s[i].onClick.AddListener(() =>
            {
                //Button_s[Temp].gameObject.GetComponent<Animator>().Play("AanNiuTan");//暂时去掉动画。
            });
        }
	}
    public void ClearAllText()
    {
        for (int i = 0; i < Texs.Length; i++)
        {
            Texs[i].text = "";
        }
    }
    public void TuiGe()
    {
        for (int i = Texs.Length - 1; i > -1; i--)
        {
            if (Texs[i].text != "")
            {
                Texs[i].text = "";
                return;
            }
        }
    }
	public void ButtonOnclike(int num)
    {
        for (int i = 0; i < Texs.Length; i++)
        {
            if (Texs[i].text=="")
            {
                Texs[i].text = num.ToString();
                if (i==Texs.Length-1)
                {
                    string fanghao = "";
                    for (int j = 0; j < Texs.Length; j++)
                    {
                        fanghao += Texs[j].text;
                    }
                    Debuger.Log("申请加入的房间号为：" + fanghao);
                    CMD_EnterRoom entroom = new CMD_EnterRoom() { RoomID = uint.Parse(fanghao) };
                    GameManage.GetInstan.RoomID = entroom.RoomID;
                    KWXFrom.GetIns.LoginCmd.SendEntRoom(entroom);
                }
                return;
            }
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
