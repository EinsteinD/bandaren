using UnityEngine;
using System.Collections;
using KWX;
using KWX_From;
using UnityEngine.UI;

public class TalkSendMsg : MonoBehaviour {
    public GameObject[] biaoQing_Buttons;
    public GameObject[] duiHua_Buttons;

    public void TalkSendBiaoQing_ChangYongYu(string msg_)
    {
        CMD_Message msg = new CMD_Message() { Message = msg_ };
        KWXFrom.GetIns.GameCmd.SendTalkMessage(msg);
    }

	// Use this for initialization
	void Start () 
    {
        GameManage.GetInstan.TalkMsg_Action += TalkMessage;
        for (int i = 0; i < biaoQing_Buttons.Length; i++)
        {
            int Tempi = i;
            biaoQing_Buttons[i].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => 
            {
                SoundManage.GetInstan.onclick_Sound();
                TalkSendBiaoQing_ChangYongYu(biaoQing_Buttons[Tempi].name);
                gameObject.SetActive(false);
            });
        }

        for (int i = 0; i < duiHua_Buttons.Length; i++)
        {
            int Tempi = i;
            duiHua_Buttons[i].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
            {
                SoundManage.GetInstan.onclick_Sound();
                TalkSendBiaoQing_ChangYongYu(duiHua_Buttons[Tempi].name);
                gameObject.SetActive(false);
            });
        }
	}
	public void TalkMessage(uint seatID,string msg)
    {
        Loom.QueueOnMainThread(() => 
        {
            if (CreatCard.GetInstance_.UserInfoS[GameManage.GetInstan.GetViewSeatID(seatID)].BQDH_Vector.transform.childCount != 0)
            {
                Destroy(CreatCard.GetInstance_.UserInfoS[GameManage.GetInstan.GetViewSeatID(seatID)].BQDH_Vector.transform.GetChild(0).gameObject);
            }
            for (int i = 0; i < duiHua_Buttons.Length; i++)
            {
                if (duiHua_Buttons[i].name==msg)
                {
                    SoundManage.GetInstan.playDuiHua_Sound(CreatCard.GetInstance_.UserInfoS[GameManage.GetInstan.GetViewSeatID(seatID)].sex, i);                    
                    
                    GameObject bq = Instantiate(duiHua_Buttons[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().gameObject);
                    bq.transform.SetParent(CreatCard.GetInstance_.UserInfoS[GameManage.GetInstan.GetViewSeatID(seatID)].BQDH_Vector.transform);
                    bq.transform.GetComponent<RectTransform>().anchorMax =new Vector2(0.5f, 0.5f);
                    bq.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                    bq.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 80f);
                    bq.transform.localPosition = Vector3.zero;
                    bq.transform.localScale = Vector3.one;
                    Destroy(bq, 3F);
                    break;
                }
            }
            for (int i = 0; i < biaoQing_Buttons.Length; i++)
			{
                if (biaoQing_Buttons[i].name==msg)
                {                   
                    GameObject bq = Instantiate(biaoQing_Buttons[i].transform.GetChild(0).GetComponent<Image>().gameObject);
                    bq.transform.SetParent(CreatCard.GetInstance_.UserInfoS[GameManage.GetInstan.GetViewSeatID(seatID)].BQDH_Vector.transform);
                    bq.transform.localPosition = Vector3.zero;
                    bq.transform.localScale = Vector3.one;
                    Destroy(bq, 3F);
                    break;
                }
			}
            
        
            
        });
    }
	// Update is called once per frame
	void Update () {
	
	}
   
}
