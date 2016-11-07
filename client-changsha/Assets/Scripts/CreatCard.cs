using UnityEngine;
using System.Collections;
using KWX_From;
using KWX;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using MiniJSON;

enum Suit
{
    Suit_Wan = 1,//万
    Suit_Tiao,//条
    Suit_Tong,//筒
    NOSuit = 0
};

enum Face
{
    Face_One = 1,
    Face_Two,
    Face_Three,
    Face_Four,
    Face_Five,
    Face_Six,
    Face_Seven,
    Face_Eight,
    Face_Nine,
    NOFace = 0
};
public enum TeXiao
{
    CHI,
    PENG,
    GANG,
    HU,
    HU_CSMJ_SiXi,//四喜
    HU_CSMJ_BanBanHu,//板板胡
    HU_CSMJ_QueYiSe,//缺一色
    HU_CSMJ_LiuLiuShun,//六六顺
}

enum PlayerStatus
{
    Player_Status_Ready = 1,//玩家准备
    Player_Status_Wait,//玩家等待状态
    Player_Status_Deal_Card,//玩家出牌状态
    Player_Status_Chi,//玩家可以吃状态
    Player_Status_Peng,//玩家可以碰状态
    Player_Status_Gang,//玩家可以杠状态
    Player_Status_AnGang,//玩家有暗杠状态
    Player_Status_Kai_Gang,//玩家可以开杠状态
    Player_Status_Bu_Zhang,//玩家可以补张状态
    Player_Status_Gang_Ting,//玩家开杠之后听牌状态
    Player_Status_Fang_Pao,//放炮状态
    Player_Status_Zi_Mo,//自摸胡
    Player_Status_Hu,//玩家可以胡状态
    Player_Status_Bill,//玩家结算状态
    Player_Status_Have_Mo,//玩家已经摸了牌的状态
    Player_Status_HDP,//玩家摸到了海底牌
    Player_Status_Start_Win//玩家起手胡
};



enum ErrorType
{
    Error_System_Run = 100,//游戏运行错误
    Error_System_Out_Range,//数据超出范围
    Error_Protocol_Out_Range,//协议长度超出范围
    Error_Room_Status,//房间状态错误
    Error_Room_Full,//房间已满
    Error_Seat_Full,//位子上已经有人了
    Error_Card_Out_Range,//牌超出范围
    Error_Card_Not_Found,//找不到牌
    Error_Card_Not_Chi,//吃的牌不对
    Error_Card_Not_Peng,//碰的牌不对
    Error_Card_Not_Gang,//杠的牌不对
    Error_Card_Not_Ting,//还没有听牌
    Error_Last_Card_Number,//底牌数量不对
    Error_Plyaer_Not_Found,//还没轮到出牌
    Error_Player_Deal_Card,//没轮到你出牌
    Error_Player_Status,//玩家状态错误
    Error_Player_Gang_Ting,//开杠之后只能胡牌和抓什么打什么
    Error_Player_No_Ready//玩家没有准备
};

enum HuCSMJType
{
    HU_CSMJ_NORMAL = 1,//普通胡
    HU_CSMJ_QiXiaoDui,//七小对
    HU_CSMJ_HaoHuaQXD,//豪华七小对
    HU_CSMJ_PengPengHu,//碰碰胡
    HU_CSMJ_JiangJiangHu,//将将胡
    HU_CSMJ_QingYiSe,//清一色
    HU_CSMJ_GangShangKaiHua,//杠上开花
    HU_CSMJ_GangShangPao,//杠上炮
    HU_CSMJ_QiangGangHu,//抢杠胡
    HU_CSMJ_TianHu,//天胡
    HU_CSMJ_DiHu,//地胡
    HU_CSMJ_HaiDiLaoYue,//海底捞月
    HU_CSMJ_QuanQiuRen,//全求人
    HU_CSMJ_SiXi,//四喜
    HU_CSMJ_BanBanHu,//板板胡
    HU_CSMJ_QueYiSe,//缺一色
    HU_CSMJ_LiuLiuShun,//六六顺
    NoHuCSMJType = 0
};

public class CreatCard : MonoBehaviour {

    int trunPai(Suit style)
    {
        switch (style)
        {
            case Suit.Suit_Wan:
                return 1;
            case Suit.Suit_Tiao:
                return 0;
            case Suit.Suit_Tong:
                return 2;
            case Suit.NOSuit:
                return -1;
            default:
                return -1;
        }
    }

	public GameObject BtnChi;
	public GameObject BtnPeng;
	public GameObject BtnGang;
	public GameObject BtnHu;
	public GameObject BtnGuo;
    public GameObject BtnQSH;
    public GameObject BtnKaiGang;
    public GameObject BtnBuZhang;
    public GameObject CPGH_BG;
	public Text TxtRoomID;
    public Text Time_Tex;
    public GameObject _2dCardPrifabe;
    public Sprite[] _2DMaJiangImage;
    public GameObject SeleChiCardView;
    public GameObject SeleChiCardVector;
    public GameObject SeleChiCardPrifabes;
    public GameObject SeleGangCardPrifabes;
    static CreatCard ins = null;
    public GameObject[] DNXB_BLIK;
    public UserInfo[] UserInfoS;
    public GameObject[] ChiPengGangeHuTeXiaoPrifabe;
    public GameObject LiangCang;
    public GameObject LiangPrifabe;
    public GameObject LiangView;
    public GameObject TalkView_BQK;
    public GameObject TalkView_LTK;
    public GameObject setSoundsView;
    public Text shengYuPaiShu;
    public int ZhuangJia;
    public GameObject allResult_view=null;//总结算的界面
    public GameObject singleResult_view = null;//单局结算的界面
    public GameObject jieSan_view;//解散的界面
    public GameObject HDP_view;//海底牌的界面
    void Awake()
    {
        GameManage.GetInstan.GameStart = SetStat;
        GameManage.GetInstan.jieSanLe = JieSuanLe;
        ins = this;
        SoundManage.GetInstan.playBGM();
        CurJuShu.text ="0/" + GameManage.GetInstan.JuShu;
    }
    void JieSuanLe(NTF_RoomDismissed result)
    {
        CreatCard.GetInstance_.jieSan_view.SetActive(false);
        if (singleResult_view==null)
        {
            allResult_view.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    void SelfInfoStart()
    {
        string TimeChuo = GameManage.GetInstan.GetTimeChuo().ToString();
        string Token = GameManage.GetInstan.Getmd5JiaMi(TimeChuo, GameManage.GetInstan.LoginUserID.ToString());
        Debuger.Log(TimeChuo + "  " + Token);
        StartCoroutine(DownUserInfo(Token, TimeChuo, GameManage.GetInstan.LoginUserID.ToString()));
    }
    //public Text Nick2D;
    IEnumerator DownUserInfo(string Token, string TimeChuo, string userid)
    {
        int TempViewSeatID = (int)GameManage.GetInstan.GetViewSeatID((uint)GameManage.GetInstan.SeatID);
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
            UserInfoS[TempViewSeatID].Nick.text = GameManage.GetInstan.Nick;
            //Nick2D.text = GameManage.GetInstan.Nick;
        }
        if (data.ContainsKey("gender"))
        {
            int sx = (int)data["gender"];
            GameManage.GetInstan.Sex = sx == 2 ? 0 : 1;
            UserInfoS[TempViewSeatID].sex = GameManage.GetInstan.Sex;
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
            UserInfoS[TempViewSeatID].TX.sprite = GameManage.GetInstan.DicTX[strHttpImag];
        }
        else
        {
            WWW downLoadHead = new WWW(strHttpImag);
            yield return downLoadHead;
            GameManage.GetInstan.DicTX.Add(strHttpImag, Sprite.Create(downLoadHead.texture, new Rect(0, 0, downLoadHead.texture.width, downLoadHead.texture.height), new Vector2(0.5f, 0.5f)));
            GameManage.GetInstan.TouXiang = GameManage.GetInstan.DicTX[strHttpImag];
            UserInfoS[TempViewSeatID].TX.sprite = GameManage.GetInstan.DicTX[strHttpImag];
        }
    }

    public bool keyiChuPai = false;
    public PB_Server_Room_Info m_Room_Info;
    //public List<uint> m_turn_player;
    int moPai;
    List<int> lastPai=new List<int>();
    List<int> liangPai = new List<int>();
    List<PB_Server_Room_Info> Room_Info_list = new List<PB_Server_Room_Info>();
    public bool hasDongHua = false;
    public void SetStat(PB_Server_Room_Info Room_Info)
    {
        Room_Info_list.Add(Room_Info);
        
    }

    void zhuMingLing(PB_Server_Room_Info Room_Info)
    {

		if (Room_Info.nReturnCode != 0) {
			switch ((ErrorType)Room_Info.nReturnCode) {
			case ErrorType.Error_System_Run:
				UIMessageBox.ShowMessageBox ("提示", "游戏运行错误");
				break;
			case ErrorType.Error_System_Out_Range:
				UIMessageBox.ShowMessageBox ("提示", "数据超出范围");
				break;
			case ErrorType.Error_Protocol_Out_Range:
				UIMessageBox.ShowMessageBox ("提示", "协议长度超出范围");
				break;
			case ErrorType.Error_Room_Status:
				UIMessageBox.ShowMessageBox ("提示", "房间状态错误");
				break;
			case ErrorType.Error_Room_Full:
				UIMessageBox.ShowMessageBox ("提示", "房间已满");
				break;
			case ErrorType.Error_Seat_Full:
				UIMessageBox.ShowMessageBox ("提示", "位子上已经有人了");
				break;
			case ErrorType.Error_Card_Out_Range:
				UIMessageBox.ShowMessageBox ("提示", "牌超出范围");
				break;
			case ErrorType.Error_Card_Not_Found:
				UIMessageBox.ShowMessageBox ("提示", "找不到牌");
				break;
			case ErrorType.Error_Card_Not_Chi:
				UIMessageBox.ShowMessageBox ("提示", "吃的牌不对");
				break;
			case ErrorType.Error_Card_Not_Peng:
				UIMessageBox.ShowMessageBox ("提示", "碰的牌不对");
				break;
			case ErrorType.Error_Card_Not_Gang:
				UIMessageBox.ShowMessageBox ("提示", "杠的牌不对");
				break;
			case ErrorType.Error_Card_Not_Ting:
				UIMessageBox.ShowMessageBox ("提示", "还没有听牌");
				break;
			case ErrorType.Error_Last_Card_Number:
				UIMessageBox.ShowMessageBox ("提示", "底牌数量不对");
				break;
			case ErrorType.Error_Plyaer_Not_Found:
				UIMessageBox.ShowMessageBox ("提示", "还没轮到出牌");
				break;
			case ErrorType.Error_Player_Deal_Card:
				UIMessageBox.ShowMessageBox ("提示", "没轮到你出牌");
				break;
			case ErrorType.Error_Player_Status:
				UIMessageBox.ShowMessageBox ("提示", "玩家状态错误");
				break;
			case ErrorType.Error_Player_Gang_Ting:
				UIMessageBox.ShowMessageBox ("提示", "开杠之后只能胡牌和抓什么打什么");
				break;
			case ErrorType.Error_Player_No_Ready:
				UIMessageBox.ShowMessageBox ("提示", "玩家没有准备");
				break;
			default:
				break;
			}

		} 
		else 
		{
			m_Room_Info = Room_Info;
			init();
		}	        
        for (int i = 0; i < m_Room_Info.turn_player.Count; i++)
        {
            if (m_Room_Info.turn_player[i] == GameManage.GetInstan.SeatID)//轮到我出牌
            {
                keyiChuPai = true;
                break;
            }
        }

        CurJuShu.text = m_Room_Info.round_num + "/" + m_Room_Info.max_round;//局数
        
        shengYuPaiShu.text = m_Room_Info.last_cards_size.ToString();//剩余牌数
        UserInfoS[GameManage.GetInstan.GetViewSeatID(m_Room_Info.banker)].Zhuang.SetActive(true);//显示庄家
        ZhuangJia = (int)m_Room_Info.banker;
        int geShu = 0;
        int outPaiShu = 0;
        for (int k = 0; k < m_Room_Info.last_deal_card.Count; k++)
        {
            if (m_Room_Info.last_deal_card[k] != 0)
            {
                outPaiShu++;
            }
        }

        for (int i = 0; i < m_Room_Info.player_info.Count; i++)
        {
            UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)i)].Score_.text = m_Room_Info.player_info[i].player_score.ToString();
            if (BIT_ENABLED(m_Room_Info.player_info[i].player_status, 1 << (int)PlayerStatus.Player_Status_Ready))
            {
                UserInfoS[i].JuShou.SetActive(true);
            }
            else
            {
                UserInfoS[i].JuShou.SetActive(false);
            }
            UserInfoS[i].shouPaiCang.transform.GetComponent<GridLayoutGroup>().enabled = false;
            UserInfoS[i].outPaiCang.transform.GetComponent<GridLayoutGroup>().enabled = false;
            UserInfoS[i].CPG_PaiCang.transform.GetComponent<GridLayoutGroup>().enabled = false;
            for (int j = 0; j < m_Room_Info.player_info[i].last_cards.Count; j++)//手牌
            {
                for (int k = 0; k < m_Room_Info.turn_player.Count; k++)
                {
                    if ((BIT_ENABLED(m_Room_Info.player_info[i].player_status, 1 << (int)PlayerStatus.Player_Status_Deal_Card)
                        || BIT_ENABLED(m_Room_Info.player_info[i].player_status, 1 << (int)PlayerStatus.Player_Status_AnGang))
                    && m_Room_Info.mo_card != 0
                    && i == m_Room_Info.turn_player[k]
                    && j == m_Room_Info.player_info[i].last_cards.Count - 1)
                    {
                        AllMaJiang[geShu].transform.GetChild(0).localPosition = new Vector3(0.025f, 0f, -0.427f);
                        moPai = geShu;
                        break;
                    }
                }

                MoveCardMJ((int)(m_Room_Info.player_info[i].seat_id), m_Room_Info.player_info[i].last_cards[j], AllMaJiang[geShu], 0);
                if (i != GameManage.GetInstan.SeatID
                    && m_Room_Info.player_info[i].last_cards[j] != 0)
                {
                    AllMaJiang[geShu].transform.GetChild(0).GetChild(0).localPosition = new Vector3(0f, -0.00137f, -0.00445f);
                    AllMaJiang[geShu].transform.GetChild(0).GetChild(0).Rotate(-90f, 0f, 0f);
                    liangPai.Add(geShu);
                }
                geShu++;

            }

            for (int j = 0; j < m_Room_Info.player_info[i].play_cards.Count; j++)//桌牌
            {
                for (int k = 0; k < m_Room_Info.last_deal_card.Count; k++)
                {
                    if (m_Room_Info.last_deal_card[k] != 0
                        && i == m_Room_Info.last_player
                        && j >= m_Room_Info.player_info[i].play_cards.Count - outPaiShu)
                    {
						if (outPaiShu==2) {
							AllMaJiang[geShu].transform.GetChild(0).GetComponent<card_info>().SetZhiShiQiTrue();
						}

                        lastPai.Add(geShu);
                    }
                }

                MoveCardMJ((int)(m_Room_Info.player_info[i].seat_id), m_Room_Info.player_info[i].play_cards[j], AllMaJiang[geShu], 1);
                geShu++;
            }
            int jianGE = 0;
            for (int j = 0; j < m_Room_Info.player_info[i].have_chi_card.Count; j++)//吃牌
            {
                if (j != 0 && j % 3 == 0)
                {
                    jianGE++;
                }
                MoveCardMJ((int)(m_Room_Info.player_info[i].seat_id), m_Room_Info.player_info[i].have_chi_card[j], AllMaJiang[geShu], 2);
                AllMaJiang[geShu].transform.GetChild(0).localPosition = new Vector3(-0.025f*jianGE, 0f, -0.427f);
                
                geShu++;
            }
            for (int j = 0; j < m_Room_Info.player_info[i].have_peng_card.Count; j++)//碰牌
            {
                if (j % 3 == 0)
                {
                    jianGE++;
                }
                MoveCardMJ((int)(m_Room_Info.player_info[i].seat_id), m_Room_Info.player_info[i].have_peng_card[j], AllMaJiang[geShu], 2);
                AllMaJiang[geShu].transform.GetChild(0).localPosition = new Vector3(-0.025f * jianGE, 0f, -0.427f);
                geShu++;
            }
            for (int j = 0; j < m_Room_Info.player_info[i].have_gang_card.Count; j++)//杠牌
            {
                if (j % 4 == 0)
                {
                    jianGE++;
                }
                MoveCardMJ((int)(m_Room_Info.player_info[i].seat_id), m_Room_Info.player_info[i].have_gang_card[j], AllMaJiang[geShu], 2);
                AllMaJiang[geShu].transform.GetChild(0).localPosition = new Vector3(-0.025f * jianGE, 0f, -0.427f);
                geShu++;
            }




            UserInfoS[i].shouPaiCang.transform.GetComponent<GridLayoutGroup>().enabled = true;
            UserInfoS[i].outPaiCang.transform.GetComponent<GridLayoutGroup>().enabled = true;
            UserInfoS[i].CPG_PaiCang.transform.GetComponent<GridLayoutGroup>().enabled = true;

            if (UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)i)].shouPaiCang.transform.childCount % 3 == 2)
            {
                DNXB_BLIK[i].SetActive(true);
            }
            else
            {
                DNXB_BLIK[i].SetActive(false);
            }

            if (i == GameManage.GetInstan.SeatID
                && !BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_Wait))
            {
                if (BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_Chi)
                && m_Room_Info.player_info[i].can_chi_card.Count > 0)
                {
                    BtnChi.SetActive(true);

                    BtnGuo.SetActive(true);
                    CPGH_BG.SetActive(true);
                }

                if (BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_Peng)
                    && m_Room_Info.player_info[i].can_peng_card.Count > 0)
                {
                    BtnPeng.SetActive(true);
                    BtnGuo.SetActive(true);
                    CPGH_BG.SetActive(true);
                }

                if (BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_Gang)
                    || BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_AnGang))
                {
                    BtnGang.SetActive(true);
                    BtnGuo.SetActive(true);
                    CPGH_BG.SetActive(true);


                }
                if (BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_Kai_Gang))
                {
                    BtnBuZhang.SetActive(true);
                    BtnKaiGang.SetActive(true);
                    BtnKaiGang.transform.GetComponent<Button>().interactable = true;
                    BtnGuo.SetActive(true);
                    CPGH_BG.SetActive(true);
                    BtnGang.SetActive(false);
                    BtnChi.SetActive(false);
                    BtnPeng.SetActive(false);
                    BtnHu.SetActive(false);
                    BtnKaiGang.transform.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        HideSeletChiCard();
                        for (int j = 0; j < m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card.Count; j += 4)
                        {
                            GameObject TempMJ = Instantiate(SeleGangCardPrifabes);
                            TempMJ.transform.SetParent(SeleChiCardVector.transform);
                            TempMJ.transform.localPosition = Vector3.zero;
                            TempMJ.transform.localScale = Vector3.one;
                            TempMJ.transform.localEulerAngles = Vector3.zero;
                            int tempj = j;
                            TempMJ.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                            TempMJ.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                            TempMJ.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                            TempMJ.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                            TempMJ.GetComponent<Button>().onClick.AddListener(() =>
                            {
                                PB_Client_KaiGang act = new PB_Client_KaiGang();
                                act.gang_card = m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[tempj];
                                KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_KAIGANG(act);

                            });
                        }
                        SeleChiCardView.SetActive(true);
                    });
                }

                if (BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_Bu_Zhang))
                {
                    BtnKaiGang.SetActive(true);
                    BtnBuZhang.SetActive(true);
                    BtnBuZhang.transform.GetComponent<Button>().interactable = true;
                    BtnGuo.SetActive(true);
                    CPGH_BG.SetActive(true);
                    BtnGang.SetActive(false);
                    BtnChi.SetActive(false);
                    BtnPeng.SetActive(false);
                    BtnHu.SetActive(false);
                    BtnBuZhang.transform.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        HideSeletChiCard();
                        for (int j = 0; j < m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card.Count; j += 4)
                        {
                            GameObject TempMJ = Instantiate(SeleGangCardPrifabes);
                            TempMJ.transform.SetParent(SeleChiCardVector.transform);
                            TempMJ.transform.localPosition = Vector3.zero;
                            TempMJ.transform.localScale = Vector3.one;
                            TempMJ.transform.localEulerAngles = Vector3.zero;
                            int tempj = j;
                            TempMJ.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                            TempMJ.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                            TempMJ.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                            TempMJ.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                            TempMJ.GetComponent<Button>().onClick.AddListener(() =>
                            {
                                PB_Client_BuZhang act = new PB_Client_BuZhang();
                                act.gang_card = m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[tempj];
                                KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_BUZHANG(act);

                            });
                        }
                        SeleChiCardView.SetActive(true);
                    });
                }
                if (BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_Hu))
                {
                    BtnHu.SetActive(true);
                    BtnGuo.SetActive(true);
                    CPGH_BG.SetActive(true);
                }
                if (BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_HDP))
                {
                    HDP_view.transform.GetComponent<HDP_view>().dongTu.gameObject.SetActive(true);
                    HDP_view.transform.GetComponent<HDP_view>().maJiang.SetActive(false);
                    HDP_view.SetActive(true);
                }
                if (BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_Start_Win))
	            {
                    BtnQSH.SetActive(true);
                    BtnGuo.SetActive(true);
                    CPGH_BG.SetActive(true);
	            }
            }



        }
        for (int i = 0; i < m_Room_Info.player_info.Count; i++)
        {
            switch (m_Room_Info.player_info[i].action)
            {
                case 1:
                    SoundManage.GetInstan.playCPGHL_Sound(UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].sex, TeXiao.CHI);
                    CreatTeXiao(TeXiao.CHI, i);
                    break;
                case 2:
                    SoundManage.GetInstan.playCPGHL_Sound(UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].sex, TeXiao.PENG);
                    CreatTeXiao(TeXiao.PENG, i);
                    break;
                case 3:
                    SoundManage.GetInstan.playCPGHL_Sound(UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].sex, TeXiao.GANG);
                    CreatTeXiao(TeXiao.GANG, i);
                    break;
                case 4: 
                    SoundManage.GetInstan.playCPGHL_Sound(UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].sex, TeXiao.HU);
                    CreatTeXiao(TeXiao.HU, i);
                    break;
                case 5:
                    hasDongHua = true;
                    SoundManage.GetInstan.playPAI_Sound(UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].sex, (int)m_Room_Info.player_info[i].cards-16);
                    UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].outPaiCang.transform.GetChild(UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].outPaiCang.transform.childCount-1).GetChild(0).localPosition
                            =new Vector3(0.06f,-0.223f,-1.02f);
                    //UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].outPaiCang.transform.GetChild(UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].outPaiCang.transform.childCount - 1).GetChild(0).localRotation
                    //        = Quaternion.AngleAxis(0, Vector3.right);
                    StartCoroutine(outCard((uint)i, UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].outPaiCang.transform.GetChild(UserInfoS[(int)GameManage.GetInstan.GetViewSeatID((uint)i)].outPaiCang.transform.childCount - 1).gameObject));
                    break;
                case 6:
                    if (BIT_ENABLED((int)m_Room_Info.player_info[i].hu_pai_type, 1 << (int)HuCSMJType.HU_CSMJ_SiXi))
                    {
                        CreatTeXiao(TeXiao.HU_CSMJ_SiXi, i);
                    }
                    if (BIT_ENABLED((int)m_Room_Info.player_info[i].hu_pai_type, 1 << (int)HuCSMJType.HU_CSMJ_BanBanHu))
                    {
                        CreatTeXiao(TeXiao.HU_CSMJ_BanBanHu, i); 
                    }
                    if (BIT_ENABLED((int)m_Room_Info.player_info[i].hu_pai_type, 1 << (int)HuCSMJType.HU_CSMJ_QueYiSe))
                    {
                        CreatTeXiao(TeXiao.HU_CSMJ_QueYiSe, i);
                    }
                    if (BIT_ENABLED((int)m_Room_Info.player_info[i].hu_pai_type, 1 << (int)HuCSMJType.HU_CSMJ_LiuLiuShun))
                    {
                        CreatTeXiao(TeXiao.HU_CSMJ_LiuLiuShun, i);
                    }    
                    break;
                default:
                    break;
            }
        }
        
    }
    public bool BIT_ENABLED(int aword, int bit)
    {
        if ((aword & bit) != 0)
            return true;
        else
            return false;
    }

    public void init()
    {
        HideAllButton();//隐藏吃碰杠胡button
        HideSeletChiCard();//摧毁吃牌二级界面的牌
        SeleChiCardView.SetActive(false);//隐藏吃牌二级界面
        
        for (int i = 0; i < UserInfoS.Length; i++)
        {
            for (int j = 0; j < UserInfoS[i].shouPaiCang.transform.childCount; )
            {
                UserInfoS[i].shouPaiCang.transform.GetChild(0).gameObject.SetActive(false);
                UserInfoS[i].shouPaiCang.transform.GetChild(0).SetParent(AllMaJiang_cang);
            }
            for (int j = 0; j < UserInfoS[i].outPaiCang.transform.childCount; )
            {
                UserInfoS[i].outPaiCang.transform.GetChild(0).gameObject.SetActive(false);
                UserInfoS[i].outPaiCang.transform.GetChild(0).SetParent(AllMaJiang_cang);
            }
            for (int j = 0; j < UserInfoS[i].CPG_PaiCang.transform.childCount; )
            {
                UserInfoS[i].CPG_PaiCang.transform.GetChild(0).gameObject.SetActive(false);
                UserInfoS[i].CPG_PaiCang.transform.GetChild(0).SetParent(AllMaJiang_cang);
            }
        }
        for (int i = 0; i < AllMaJiang.Length; i++)
        {
            AllMaJiang[i].transform.GetChild(0).localPosition = new Vector3(0f, 0f, -0.427f);
        }
        for (int i = 0; i < lastPai.Count;)
        {
            AllMaJiang[lastPai[0]].transform.GetChild(0).GetComponent<card_info>().ZhiShiQi.SetActive(false);
            lastPai.Remove(lastPai[0]);
        }
        for (int i = 0; i < liangPai.Count; )
        {
            AllMaJiang[liangPai[0]].transform.GetChild(0).GetChild(0).localPosition = Vector3.zero;
            AllMaJiang[liangPai[0]].transform.GetChild(0).GetChild(0).Rotate(90f, 0f, 0f);
            liangPai.Remove(liangPai[0]);
        }
        for (int i = 0; i < 4; i++)
        {
            UserInfoS[i].Zhuang.SetActive(false);
        }
        BtnKaiGang.transform.GetComponent<Button>().interactable = false;
        BtnBuZhang.transform.GetComponent<Button>().interactable = false;
        if (upPai)
        {
            upPai.localPosition = Vector3.zero;
        }
        HDP_view.SetActive(false);
    }

    public static CreatCard GetInstance_
    {
        get
        {
            if (ins==null)
            {
                Debuger.Log("游戏尚未初始化成功");
            }
            return ins;
        }
    }
    public void SetBQK_LTK(bool isOn)
    {
        if (isOn)
        {
            TalkView_LTK.SetActive(true);
            TalkView_BQK.SetActive(false);
        }
        else
        {
            TalkView_BQK.SetActive(true);
            TalkView_LTK.SetActive(false);
        }
    }
	void HideAllButton()
	{
		BtnChi.SetActive (false);
		BtnPeng.SetActive (false);
		BtnGang.SetActive (false);
		BtnHu.SetActive (false);
        BtnGuo.SetActive(false);
        BtnQSH.SetActive(false);
        BtnBuZhang.SetActive(false);
        BtnKaiGang.SetActive(false);
        CPGH_BG.SetActive(false);
	}
    public void HideSeletChiCard()
    {
        for (int i = 0; i < SeleChiCardVector.transform.childCount; i++)
        {
            Destroy(SeleChiCardVector.transform.GetChild(i).gameObject);
        }
    }
    public Shader shader1;
    public Shader shader2;
    public GameObject XuanGaiPai;
    

   
	


	public GameObject CardMJPrifabe;
	public GameObject CardVector;
    public GameObject DNXB;
	public GameObject[] AllMaJiang;//所有的麻将
    public Transform AllMaJiang_cang;
	public float [][] UV;
	public const float MaJiangWidth=0.045f;//麻将宽度
	public const float MaJiangHeight=0.06f;//麻将高度
	int [] ShouZhang;
	int [] KeChi;
	int [] KeGang;
	uint AllCount;
    List<int> BuLiang;
    public void CreatTeXiao(TeXiao TX_,int SeatID)
    {
        if ((int)TX_<4)
        {
            SoundManage.GetInstan.playCPGHL_Sound(UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)SeatID)].sex, TX_);
        }   
        
        GameObject Temp = Instantiate(ChiPengGangeHuTeXiaoPrifabe[(int)TX_]);
        Temp.transform.SetParent(UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)SeatID)].ChiPengGangTXVector.transform);
        Temp.transform.localPosition = Vector3.zero;
        Temp.transform.localScale = Vector3.one;
        Temp.transform.localEulerAngles = Vector3.zero;
    }
	public void  CreatCardMJ(int seatID,uint pai)
	{
        GameObject mj = Instantiate(CardMJPrifabe);
        mj.transform.SetParent(UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)seatID)].shouPaiCang.transform);
        mj.transform.GetChild(0).GetComponent<card_info>().meshr.materials[1].mainTextureOffset = 
            new Vector2((getCardValue(pai) - 1f) / 10f, 0.25f - (getCardColor(pai) * 0.25f));
        mj.transform.localPosition = Vector3.zero;
        mj.transform.localRotation = new Quaternion(0,0,0,0);
        mj.transform.GetChild(0).GetComponent<card_info>().cardValu = (int)pai;

	}


    public void MoveCardMJ(int seatID, uint pai, GameObject maJiang,int to)
    {
        if (to==0)
        {
            maJiang.transform.SetParent(UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)seatID)].shouPaiCang.transform);
            maJiang.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        else if (to==1)
        {
            maJiang.transform.SetParent(UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)seatID)].outPaiCang.transform);
            maJiang.transform.localRotation = Quaternion.AngleAxis(-60, Vector3.right);
        }
        else if (to == 2)
        {
            maJiang.transform.SetParent(UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)seatID)].CPG_PaiCang.transform);
            maJiang.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        
        maJiang.transform.GetChild(0).GetComponent<card_info>().meshr.materials[1].mainTextureOffset =
            new Vector2((getCardValue(pai) - 1f) / 10f, 0.25f - (getCardColor(pai) * 0.25f));
        maJiang.transform.localPosition = Vector3.zero;
        maJiang.transform.GetChild(0).GetComponent<card_info>().cardValu = (int)pai;
        maJiang.SetActive(true);
    }

    public void chi_OnClick()
    {
        if (m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_chi_card.Count==3)           
        {
            PB_Client_ChiCard act = new PB_Client_ChiCard();
            act.chi_card.Add(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_chi_card[0]);
            act.chi_card.Add(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_chi_card[1]);
            act.chi_card.Add(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_chi_card[2]);
            KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_CHIPAI(act);
        }
        else
        {
            HideSeletChiCard();
            for (int j = 0; j < m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_chi_card.Count; j += 3)
            {
                GameObject TempMJ = Instantiate(SeleChiCardPrifabes);
                TempMJ.transform.SetParent(SeleChiCardVector.transform);
                TempMJ.transform.localPosition = Vector3.zero;
                TempMJ.transform.localScale = Vector3.one;
                TempMJ.transform.localEulerAngles = Vector3.zero;
                int tempi = (int)GameManage.GetInstan.SeatID;
                int tempj = j;
                TempMJ.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_chi_card[j] - 16];
                TempMJ.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_chi_card[j + 1] - 16];
                TempMJ.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_chi_card[j + 2] - 16];
                TempMJ.GetComponent<Button>().onClick.AddListener(() =>
                {
                    PB_Client_ChiCard act = new PB_Client_ChiCard();
                    act.chi_card.Add(m_Room_Info.player_info[tempi].can_chi_card[tempj]);
                    act.chi_card.Add(m_Room_Info.player_info[tempi].can_chi_card[tempj + 1]);
                    act.chi_card.Add(m_Room_Info.player_info[tempi].can_chi_card[tempj + 2]);
                    KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_CHIPAI(act);

                });

            }
            SeleChiCardView.SetActive(true);
        }
        
        
    }
    public void peng_OnClick()
    {

        PB_Client_PengCard act = new PB_Client_PengCard();
        act.peng_card = m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_peng_card[0];
        KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_PENGPAI(act);

        
    }
    public void gang_OnClick()
    {
        KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_GANG(new PB_Client_GangCard());
        //BtnGang.SetActive(false);
        //BtnBuZhang.SetActive(true);
        //BtnKaiGang.SetActive(true);
      
        /*if (BIT_ENABLED(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].player_status, 1 << (int)PlayerStatus.Player_Status_Kai_Kang))
        {
            BtnKaiGang.transform.GetComponent<Button>().interactable = true;
            BtnKaiGang.transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                
            });
        }
        else
        {
            BtnKaiGang.transform.GetComponent<Button>().interactable = false;
        }


        BtnBuZhang.transform.GetComponent<Button>().onClick.AddListener(() => 
        {
           
        });
        if (m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card.Count == 8)
        {
            for (int j = 0; j < m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card.Count; j += 4)
            {
                GameObject TempMJ = Instantiate(SeleChiCardPrifabes);
                TempMJ.transform.SetParent(SeleChiCardVector.transform);
                TempMJ.transform.localPosition = Vector3.zero;
                TempMJ.transform.localScale = Vector3.one;
                TempMJ.transform.localEulerAngles = Vector3.zero;
                int tempj = j;
                TempMJ.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                TempMJ.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                TempMJ.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                TempMJ.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[j] - 16];
                TempMJ.GetComponent<Button>().onClick.AddListener(() =>
                {
                    PB_Client_ChiCard act = new PB_Client_ChiCard();
                    act.chi_card.Add(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[tempj]);
                    act.chi_card.Add(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[tempj]);
                    act.chi_card.Add(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[tempj]);
                    act.chi_card.Add(m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[tempj]);
                    KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_CHIPAI(act);
                    HideAllButton();
                    HideSeletChiCard();
                    SeleChiCardView.SetActive(false);
                });

            }
        }*/

        //PB_Client_GangCard act = new PB_Client_GangCard();
        //act.gang_card = m_Room_Info.player_info[(int)GameManage.GetInstan.SeatID].can_gang_card[0];
        //KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_GANG(act);
        //for (int j = 0; j < Room_Info.player_info[i].can_gang_card.Count; j += 4)
        //{
        //    GameObject TempMJ = Instantiate(SeleChiCardPrifabes);
        //    TempMJ.transform.SetParent(SeleChiCardVector.transform);
        //    TempMJ.transform.localPosition = Vector3.zero;
        //    TempMJ.transform.localScale = Vector3.one;
        //    TempMJ.transform.localEulerAngles = Vector3.zero;
        //    int tempi = i;
        //    int tempj = j;
        //    TempMJ.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[Room_Info.player_info[i].can_gang_card[j] - 16];
        //    TempMJ.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[Room_Info.player_info[i].can_gang_card[j] - 16];
        //    TempMJ.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[Room_Info.player_info[i].can_gang_card[j] - 16];
        //    TempMJ.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = _2DMaJiangImage[Room_Info.player_info[i].can_gang_card[j] - 16];
        //    TempMJ.GetComponent<Button>().onClick.AddListener(() =>
        //    {
        //        PB_Client_ChiCard act = new PB_Client_ChiCard();
        //        act.chi_card.Add(Room_Info.player_info[tempi].can_gang_card[tempj]);
        //        act.chi_card.Add(Room_Info.player_info[tempi].can_gang_card[tempj]);
        //        act.chi_card.Add(Room_Info.player_info[tempi].can_gang_card[tempj]);
        //        act.chi_card.Add(Room_Info.player_info[tempi].can_gang_card[tempj]);
        //        KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_CHIPAI(act);
        //        HideAllButton();
        //        HideSeletChiCard();
        //        SeleChiCardView.SetActive(false);
        //    });

        //}
    }
    public void hu_OnClick()
    {
        KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_HUPAI(new PB_Client_HuPai());

    }
    public void guo_OnClick()
    {

        KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_GUOPAI(new PB_Client_Pass());

    }
    public int getCardValue(uint card)
    {
        return (int)(card & 0xF);
    }
    public int getCardColor(uint card)
    {
        switch ((Suit)(card >> 4))
        {
            case Suit.Suit_Wan:
                return 1;
            case Suit.Suit_Tiao:
                return 0;
            case Suit.Suit_Tong:
                return 2;
            case Suit.NOSuit:
                return -1;
            default:
                return -1;
        }
    }

    public void jieSan_OnClick()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!UserInfoS[i].gameObject.activeSelf)
            {
                KWXFrom.GetIns.GameCmd.SendExitRoom();
                return;
            }
        }
        
        setSoundsView.SetActive(false);
        CMD_Dismiss result = new CMD_Dismiss();
        result.Action = 1;
        KWXFrom.GetIns.GameCmd.SendACTION_SendDismiss(result);
    }
	// Use this for initialization
	void Start () {
		TxtRoomID.text = GameManage.GetInstan.RoomID.ToString();


        int TempViewSeatID = (int)GameManage.GetInstan.GetViewSeatID((uint)GameManage.GetInstan.SeatID);
        UserInfoS[TempViewSeatID].gameObject.SetActive(true);
        UserInfoS[TempViewSeatID].Nick.text = GameManage.GetInstan.result_UserInfo.UserID.ToString();

		
        DNXB.transform.localEulerAngles = new Vector3(0, 180+GameManage.GetInstan.SeatID*90, 0);
        SelfInfoStart();
	}
    public GameObject MaJiangZhuo;
    public int IDX_Fapai;
    public bool IsFaPai = false;
    PB_Server_Room_Info GameState;
    public GameObject ShaZiObj;
    public float[,] ShaZiUV = new float[,] { 
    { 0, 0 }, //1
    { 0.64f, 0 }, //2
    { 0.35F, 0 }, //3
    { 0.35F, 0.5f }, //4
    { 0F, 0.5f }, //5
    {0.64f, .5f  } };//6 
    public void SetShaiZiMater(int Dian1, int Dian2)
    {
        ShaZiObj.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(ShaZiUV[Dian1 - 1, 0], ShaZiUV[Dian1 - 1, 1]);
        ShaZiObj.transform.GetChild(1).GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(ShaZiUV[Dian2 - 1, 0], ShaZiUV[Dian2 - 1, 1]);
        ShaZiObj.SetActive(true);
        ShaZiObj.GetComponent<Animator>().Play(0);
    }
    public SHUZI TimeDaoJiShi;
   
  
    public Text CurJuShu;

	
    public GameObject Canmere;
    void GetDunPos(uint SeatID, out float sx, out float sz, out float ox, out float oz, out float ag, uint[] DunCnt)
	{
		const float dis = 0.352f;
		if (0 == SeatID) {
			sx = -MaJiangWidth * DunCnt [SeatID] / 2 + MaJiangWidth/2;
			sz = -dis;
			ox = MaJiangWidth;
			oz = 0;
			ag = 0;
		}
		else if(1==SeatID) {
			sx = dis;
			sz = -MaJiangWidth * DunCnt [SeatID] / 2 + MaJiangWidth/2;
			ox = 0;
			oz = MaJiangWidth;
			ag = 90;
		}
		else if (2 == SeatID) {
			sx = MaJiangWidth * DunCnt [SeatID] / 2 - MaJiangWidth/2;
			sz = dis;
			ox = -MaJiangWidth;
			oz = 0;
			ag = 180;
		}
		else//if(3==SeatID)
		{
			sx = -dis;
			sz = MaJiangWidth * DunCnt [SeatID] / 2 - MaJiangWidth/2;
			ox = 0;
			oz = -MaJiangWidth;
			ag = 270;
		}
	}
    //uint SetPaiDun(uint StartPos,uint Count,uint AllCount,uint Index)
    //{
    //    uint AllCnt = AllCount / 2;
    //    uint[] DunCnt = new uint[4];
    //    DunCnt[0] = DunCnt[1] = DunCnt[2] = DunCnt[3] = AllCnt / 4;
    //    uint[] simp = new uint[4];
    //    uint i;
    //    for (i = 0; i < 4; i++)
    //        simp[i] = (uint)(i + 4 - GameManage.GetInstan.SeatID) % 4;
    //    switch (AllCnt % 4)
    //    {
    //        case 1:
    //            DunCnt[simp[0]]++;
    //            break;
    //        case 2:
    //            DunCnt[simp[0]]++;
    //            DunCnt[simp[2]]++;
    //            break;
    //        case 3:
    //            DunCnt[simp[0]]++;
    //            DunCnt[simp[1]]++;
    //            DunCnt[simp[2]]++;
    //            break;
    //    }
        
    //    uint st;
    //    for (st = 4 - GameManage.GetInstan.SeatID;; st++) {
    //        if (4 == st)
    //            st = 0;
    //        if (DunCnt [st] * 2 > StartPos)
    //            break;
    //        StartPos -= DunCnt [st] * 2;
    //    }
    //    float sx, sz, ox, oz, ag;
    //    bool Up = false;
    //    GetDunPos (st, out sx, out sz, out ox, out oz, out ag,DunCnt);
    //    for (i = 0; i < Count; i++) {
    //        AllMaJiang[Index].transform.localPosition = new Vector3 (sx+ox*(StartPos/2), -0.0135f+(Up?0.03f:0), sz+oz*(StartPos/2));
    //        AllMaJiang[Index].transform.localEulerAngles = new Vector3(90, ag, 0);
    //        AllMaJiang[Index].SetActive(true);
    //        Index++;
    //        StartPos++;
    //        Up=!Up;
    //        if (StartPos == DunCnt [st]*2) {
    //            StartPos = 0;
    //            Up = false;
    //            st++;
    //            if (4 == st)
    //                st = 0;
    //            GetDunPos(st, out sx, out sz, out ox, out oz, out ag, DunCnt);
    //        }
    //    }
    //    return Index;
    //}

	
    //uint SetQiZhang(uint SeatID,List<int> Pai,bool ChuPai,bool ZhiShi,uint Index)
    //{//设置某个玩家的弃张		
    //    Vector3 Angles;
    //    const float posx=0.05f,posz=0.1224f;
    //    float sx,sz,ox1,oz1,ox2,oz2;
    //    float aox,aoz;
    //    if (0 == SeatID)
    //    {
    //        sx = -posx;
    //        sz = -posz;
    //        ox1 = MaJiangWidth;
    //        oz1 = 0;
    //        ox2 = 0;
    //        oz2 = -MaJiangHeight;
    //        aox = 0;
    //        aoz = -0.5f;
    //        Angles = new Vector3 (-90, 180, 0);
    //    } else if (1 == SeatID) {
    //        sx = posz;
    //        sz = -posx;
    //        ox1 = 0;
    //        oz1 = MaJiangWidth;
    //        ox2 = MaJiangHeight;
    //        oz2 = 0;
    //        aox = 0.5f;
    //        aoz = 0;
    //        Angles = new Vector3(-90, 90, 0);
    //    } else if (2 == SeatID) {
    //        sx = posx;
    //        sz = posz;
    //        ox1 = -MaJiangWidth;
    //        oz1 = 0;
    //        ox2 = 0;
    //        oz2 = MaJiangHeight;
    //        aox = 0;
    //        aoz = 0.5f;
    //        Angles = new Vector3(-90, 0, 0);
    //    } else {
    //        sx = -posz;
    //        sz = posx;
    //        ox1 = 0;
    //        oz1 = -MaJiangWidth;
    //        ox2 = -MaJiangHeight;
    //        oz2 = 0;
    //        aox = -0.5f;
    //        aoz = 0;
    //        Angles = new Vector3(-90, -90, 0);
    //    }
    //    int i;
    //    for (i = 0; i < Pai.Count; i++) {
    //        AllMaJiang[Index].transform.localPosition = new Vector3 (sx, -0.0135f, sz);
    //        sx += ox1;
    //        sz += oz1;
    //        if ((i+1)%7==0) {
    //            sx -= ox1 * 7;
    //            sz -= oz1 * 7;
    //            sx += ox2;
    //            sz += oz2;
    //        }
    //        AllMaJiang[Index].transform.GetComponent<card_info>().meshr.materials[1].mainTextureOffset = new Vector2(UV[Pai[i]][0], UV[Pai[i]][1]);
    //        if (0 == Pai [i])
    //            AllMaJiang [Index].transform.localEulerAngles = new Vector3 (Angles.x + 180, Angles.y, Angles.z);
    //        else
    //            AllMaJiang [Index].transform.localEulerAngles = Angles;
    //        AllMaJiang [Index].transform.GetComponent<card_info> ().cardValu = 0;
    //        AllMaJiang[Index].SetActive (true);
    //        Index++;
    //    }
    //    if (ChuPai)
    //    {
    //        AllMaJiang[Index - 1].transform.GetComponent<Animator>().enabled = true;
    //        AllMaJiang[Index - 1].transform.GetComponent<Animator>().Play("mj_fei");
    //        SoundManage.GetInstan.playPAI_Sound(UserInfoS[GameManage.GetInstan.GetViewSeatID(SeatID)].sex, Pai[Pai.Count - 1]); //出牌声音
    //    }
    //    else if(ZhiShi)
    //    {
    //        AllMaJiang[Index - 1].GetComponent<card_info>().SetZhiShiQiTrue();
    //    }
    //       //AllMaJiang[Index - 1].transform.GetComponent<card_info>().MoveTo_ = AllMaJiang[Index - 1].transform.localPosition;
    //       //AllMaJiang[Index - 1].transform.localPosition = new Vector3(AllMaJiang[Index - 1].transform.localPosition.x + aox, AllMaJiang[Index - 1].transform.localPosition.y + 0.5f, AllMaJiang[Index - 1].transform.localPosition.z + aoz);
    //       //AllMaJiang[Index - 1].transform.GetComponent<card_info>().IsOut = true;
    //    return Index;
    //}
    State_Ting IsTing = State_Ting.NONO;
    bool TQ = false;
    GameObject TQ_Card = null;
    Vector3 daole = new Vector3(0, 0, -0.427f);
    IEnumerator outCard(uint seatID, GameObject Card)
    {
        while (true)
        {
            Card.transform.GetChild(0).localPosition = Vector3.Slerp(Card.transform.GetChild(0).localPosition, daole, 10f * Time.deltaTime);
            yield return new WaitForSeconds(0.0001f);
            //Card.transform.GetChild(0).localRotation = Quaternion.Lerp(Card.transform.GetChild(0).localRotation, Quaternion.AngleAxis(-60f, Vector3.right), 0.5f);
            if (Vector3.Distance(Card.transform.GetChild(0).localPosition, daole) < 0.0001)
            {
                Card.transform.GetChild(0).localPosition = new Vector3(0, 0, -0.427f);
                AllMaJiang[lastPai[0]].transform.GetChild(0).GetComponent<card_info>().SetZhiShiQiTrue();
                //Card.transform.GetChild(0).localRotation = Quaternion.AngleAxis(-60f, Vector3.right);
                break;
            }
        }

        hasDongHua = false;

    }

    List<GameObject> List_XZ = new List<GameObject>();

    Vector3 up = new Vector3(0, 0.002f, 0);
    Transform upPai=null;
	// Update is called once per frame
	void Update ()
    {
        if (hasDongHua==false
            && Room_Info_list.Count != 0)
        {
            zhuMingLing(Room_Info_list[0]);
            Room_Info_list.Remove(Room_Info_list[0]);
        }
        Time_Tex.text = DateTime.Now.ToShortTimeString().ToString();
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == "MyMaJiang"
                    && hitInfo.collider.transform.parent.GetComponent<card_info>().cardValu != 0
                    && hitInfo.collider.transform.parent.parent.parent.parent.tag == "MyMaJiang"
                    && hitInfo.collider.transform.parent.parent.parent.tag == "MyMaJiang")
                {
                    if (hitInfo.collider.transform.localPosition == Vector3.zero)
                    {
                        for (int i = 0; i < UserInfoS[0].shouPaiCang.transform.childCount; i++)
                        {
                            UserInfoS[0].shouPaiCang.transform.GetChild(i).GetChild(0).GetChild(0).localPosition = Vector3.zero;
                        }
                        hitInfo.collider.transform.localPosition = up;
                        upPai = hitInfo.collider.transform;
                    }
                    else if (hitInfo.collider.transform.localPosition == up)
                    {
                        hitInfo.collider.transform.localPosition = Vector3.zero;
                        if (keyiChuPai)
                        {
                            keyiChuPai = false;
                            
                            //outCard(GameManage.GetInstan.SeatID, hitInfo.collider.transform.parent.parent.gameObject);
                            PB_Client_DealCard act = new PB_Client_DealCard();
                            act.card_value = (uint)(hitInfo.collider.transform.parent.GetComponent<card_info>().cardValu);
                            KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_CHUPAI(act);
                        }
                       
                        
                        
                    }
                }                
            }
        }
	}


    public void setSounds_button()
    {        
        setSoundsView.SetActive(true);        
    }

    public void button_sound()
    {
        SoundManage.GetInstan.onclick_Sound();
    }
}
public enum State_Ting
{
    NONO,
    LIANG,
    CHULIANG,
}