using UnityEngine;
using System.Collections;
using KWX;
using KWX_From;
using UnityEngine.UI;
public class HDP_view : MonoBehaviour {
    public GameObject maJiang;
    public Image dongTu;
    public Sprite[] zhen;
    bool yaole = false;
	// Use this for initialization
	void Start () {
	
	}
    float time0 = 0;
    float JG = 0.1f;
    int diJiZhen = 0;
	// Update is called once per frame
	void Update () {
        if (yaole)
        {
            if ((time0+=Time.deltaTime)>=JG)
            {
                if (diJiZhen < 4)
                {
                    time0 = 0;
                    dongTu.sprite = zhen[diJiZhen];
                    diJiZhen++;
                }
                else
                {
                    diJiZhen++;
                    if (diJiZhen==15)
                    {
                        dongTu.gameObject.SetActive(false);
                        maJiang.SetActive(true);
                        dongTu.sprite = zhen[0];
                        diJiZhen = 0;
                        yaole = false;
                        CreatCard.GetInstance_.hasDongHua = false;
                    }
                }
            }
        }
	}

    public void Yao()
    {
        CreatCard.GetInstance_.hasDongHua = true;

        yaole = true;
        maJiang.transform.GetChild(0).transform.GetComponent<Image>().sprite = CreatCard.GetInstance_._2DMaJiangImage[CreatCard.GetInstance_.m_Room_Info.mo_card - 16];
        PB_Client_HDP act = new PB_Client_HDP();
        act.want = 1;
        KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_HDP(act);
    }
    public void BuYao()
    {
        PB_Client_HDP act = new PB_Client_HDP();
        act.want = 0;
        KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_HDP(act);
    }
}
