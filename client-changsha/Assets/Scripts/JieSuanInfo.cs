using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using KWX;
using KWX_From;
public class JieSuanInfo : MonoBehaviour
{
    public GameObject JieSuanDATA;
    public Text Time_;
    public Text JuShu_FH;
    public GameObject[] CardVector;
    public GameObject[] Hu_Pao;
    public Sprite[] Sprite_Hu_Pao;
    public Text[] PaiXing;
    public Text[] DeFen;
    public Text[] GangFen;
    public Text[] BeiLv;
    public int DiFen = 1;//底分
    public ZongJieSuanDATA ZJSDATA;
    // Use this for initialization
    void Awake()
    {
       
        //GameManage.GetInstan.ZongJieSuan += setZongJieSuan;
    }
    public void ClearCard()
    {
        for (int i = 0; i < Hu_Pao.Length; i++)
        {
            Hu_Pao[i].SetActive(false);
        }
        for (int i = 0; i < PaiXing.Length; i++)
        {
            PaiXing[i].text = "";
        }
        for (int i = 0; i < CardVector.Length; i++)
        {
            for (int j = 0; j < CardVector[i].transform.childCount; j++)
            {
                Destroy(CardVector[i].transform.GetChild(j).gameObject);
            }
        }
    }

   

    //public void setZongJieSuan(PB_Server_Bill_Info result)
    //{
        
    //}
}
