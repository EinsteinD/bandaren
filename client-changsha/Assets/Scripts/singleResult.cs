using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using KWX;
using KWX_From;
public class singleResult : MonoBehaviour {
    public Text mTextTime;//时间
    public Text mTextTableId;//桌子号
    public Text mTextRound;//局数
    public Image huStyle;//胡的类型
    public Sprite[] winStyle;//胡牌类型的图片
    public userInfo_Result[] player_Result;//玩家结算结果    
    public GameObject pai_prifabes;//结算牌预制体
    public Sprite[] paiMian;
    public GameObject zhongNiaoC;//扎鸟的牌仓
    public Image chaKanZongScore_button;//查看总成绩的button
	// Use this for initialization


	const string FILE_NAME = "Screenshot.png";
    void Awake()
    {
        GameManage.GetInstan.SingleJieSuan = setSingleJieSuan;
    }
    public void setSingleJieSuan(PB_Server_Single_Bill result)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        mTextTableId.text = GameManage.GetInstan.RoomID.ToString();//房间号
        mTextRound.text = result.round_num + "/" + result.max_round;//局数
        mTextTime.text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//时间
        huStyle.sprite = winStyle[result.title];//自摸放炮流局
        player_Result[CreatCard.GetInstance_.ZhuangJia].zhuangMark.SetActive(true);
        if (result.zhong_niao_pai.Count != 0)
        {           
            for (int i = 0; i < result.zhong_niao_pai.Count; i++)
            {
                if (result.zhong_niao_pai[i] == 0)
                {
                    zhongNiaoC.transform.parent.gameObject.SetActive(false);
                    continue;
                }
                else
                {
                    GameObject pai = Instantiate(pai_prifabes);
                    pai.transform.SetParent(zhongNiaoC.transform);
                    pai.transform.localScale = new Vector2(0.8f, 0.8f);
                    pai.transform.localPosition = new Vector2(-100 + 38 * zhongNiaoC.transform.childCount, 50);

                    pai.transform.GetChild(1).transform.GetComponent<Image>().sprite = CreatCard.GetInstance_._2DMaJiangImage[result.zhong_niao_pai[i] - 16];

                    pai.transform.GetChild(0).transform.GetComponent<Image>().color = Color.yellow;
                    pai.transform.GetChild(1).transform.GetComponent<Image>().color = Color.yellow;
                    zhongNiaoC.transform.parent.gameObject.SetActive(true);
                }
            }
                
        }
        else
        {
            zhongNiaoC.transform.parent.gameObject.SetActive(false);
        }

        for (int i = 0; i < result.player_info.Count; i++)
        {
            player_Result[i].Head.sprite = CreatCard.GetInstance_.UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)i)].TX.sprite;
            player_Result[i].Nick.text = CreatCard.GetInstance_.UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)i)].Nick.text;//昵称
            if (result.player_info[i].hu_pai_style==1)
            {
                player_Result[i].huMark.SetActive(true);
            }
            else
            {
                player_Result[i].huMark.SetActive(false);
            }
            if (result.player_info[i].hu_pai_style == 2)
            {
                player_Result[i].paoMark.SetActive(true);
            }
            else
            {
                player_Result[i].paoMark.SetActive(false);
            }

            player_Result[i].allScore.text = result.player_info[i].score.ToString();//分数

            for (int j = 0; j < result.player_info[i].have_chi_card.Count; j+=3)
            {
                createPai(i, result.player_info[i].have_chi_card[j]);
                createPai(i, result.player_info[i].have_chi_card[j+1]);
                createPai(i, result.player_info[i].have_chi_card[j+2]);
                createPai(i, 127);
            }
            for (int j = 0; j < result.player_info[i].have_peng_card.Count; j += 3)
            {
                createPai(i, result.player_info[i].have_peng_card[j]);
                createPai(i, result.player_info[i].have_peng_card[j + 1]);
                createPai(i, result.player_info[i].have_peng_card[j + 2]);
                createPai(i, 127);
            }
            for (int j = 0; j < result.player_info[i].have_gang_card.Count; j += 4)
            {
                createPai(i, result.player_info[i].have_gang_card[j]);
                createPai(i, result.player_info[i].have_gang_card[j + 1]);
                createPai(i, result.player_info[i].have_gang_card[j + 2]);
                createPai(i, result.player_info[i].have_gang_card[j + 3]);
                createPai(i, 127);
            }
            for (int j = 0; j < result.player_info[i].last_cards.Count; j ++)
            {
                createPai(i, result.player_info[i].last_cards[j]);                          
            }
            createPai(i, 127);

            if (result.player_info[i].zhong_niao_count > 0)
            {
                player_Result[i].zhongniaoNum.gameObject.SetActive(true);
                player_Result[i].zhongniaoNum.text += "中鸟 X ";
                player_Result[i].zhongniaoNum.text += result.player_info[i].zhong_niao_count.ToString();//中鸟数目
                player_Result[i].zhongniaoNum.text += "  ";
            }


            for (int j = 0; j < 18; j++)
            {
                if (CreatCard.GetInstance_.BIT_ENABLED(result.player_info[i].hu_pai_type, 1 << j))
                {
                    player_Result[i].zhongniaoNum.gameObject.SetActive(true);
                    player_Result[i].zhongniaoNum.text += hustyle[j];
                    player_Result[i].zhongniaoNum.text += "  ";
                }
                if (CreatCard.GetInstance_.BIT_ENABLED(result.player_info[i].hu_pai_type1, 1 << j))
                {
                    player_Result[i].zhongniaoNum.gameObject.SetActive(true);
                    player_Result[i].zhongniaoNum.text += hustyle[j];
                    player_Result[i].zhongniaoNum.text += "  ";
                }
            }            
        }
        for (int j = 0; j < result.hu_pai_id.Count; j++)
        {
            if (result.player_info[(int)result.hu_pai_id[j]].hu_pai!=0)
            {
                createPai((int)result.hu_pai_id[j], result.player_info[(int)result.hu_pai_id[j]].hu_pai);
                createPai((int)result.hu_pai_id[j], 127);
            }
            if (result.player_info[(int)result.hu_pai_id[j]].hu_pai1 != 0)
            {
                createPai((int)result.hu_pai_id[j], result.player_info[(int)result.hu_pai_id[j]].hu_pai1);
                createPai((int)result.hu_pai_id[j], 127);
            }
        }

        CreatCard.GetInstance_.singleResult_view = this.gameObject;
    }

    string[] hustyle = { "", "普通胡", "七小对", "豪华七小对", "碰碰胡", "将将胡", "清一色", "杠上开花", "杠上炮", "抢杠胡", "天胡", "地胡", "海底捞月", "全求人", "四喜", "板板胡", "缺一色", "六六顺" };
	string FullFileName { get { return Application.persistentDataPath + "/" + FILE_NAME; } }

	void TakeScreenshot(string fullFileName)
	{
		System.IO.File.Delete (fullFileName);

		//Create a texture to pass to encoding
		Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

		//Put buffer into texture
		texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0); //Unity complains about this line's call being made "while not inside drawing frame", but it works just fine.*
		byte[] bytes = texture.EncodeToPNG();

		//Save our test image (could also upload to WWW)
		System.IO.File.WriteAllBytes(fullFileName, bytes);

		DestroyObject(texture);

		WeChatSDKInterface.Instance.ShareToFriends(2,"","",fullFileName);
	}

    /// <summary>
    /// 分享button
    /// </summary>
    public void share()
    {
		Debuger.Log("share fullFileName:"+FullFileName);

        TakeScreenshot(FullFileName);
    }



	void Start () 
    {
        
 
	}
    //public void setResult(params object[] obj)
    //{
    //    RoundResult roundResult = (RoundResult)obj[0];
    //    if (roundResult.nCurRoundNum== roundResult.nAllRoundNum)
    //    {
    //        chaKanZongScore_button.gameObject.SetActive(false);//当第八局的时候单局结算开始游戏的按钮变成查看总成绩
    //        chaKanZongScore_button.transform.parent.GetChild(0).gameObject.SetActive(true);//当第八局的时候单局结算开始游戏的按钮变成查看总成绩
    //    }
    //    mTextTime.text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//时间
    //    mTextTableId.text = (RoomService.Instance.mTableID+800000).ToString();//房间
    //    mTextRound.text = roundResult.nCurRoundNum.ToString() + "/" + roundResult.nAllRoundNum.ToString();//局数
    //    if (roundResult.chFlag<3)
    //    {
    //        huStyle.sprite = winStyle[roundResult.chFlag];//自摸点炮流局
    //    }
        
    //    setPlayerResult(roundResult);
    //    createZN(RoomData.Instance.NotifyZaNiao);
    //}


    bool tiHuanle = false;
	// Update is called once per frame
    void Update()
    {
        if (CreatCard.GetInstance_.allResult_view && !tiHuanle)
        {
            chaKanZongScore_button.gameObject.SetActive(false);
            chaKanZongScore_button.transform.parent.GetChild(0).gameObject.SetActive(true);
            tiHuanle = true;
        }
	}

    /// <summary>
    /// 生成牌
    /// </summary>
    /// <param name="chirlid">座位号</param>
    /// <param name="value">牌的值</param>
    public void createPai(int chirlid, uint value) 
    {
        byte a = 0;
        for (int i = 0; i < player_Result[chirlid].paiC.transform.childCount; i++)
        {
            if (!player_Result[chirlid].paiC.transform.GetChild(i).GetChild(0).gameObject.activeSelf)
            {
                a++;
            }
        }
        GameObject pai = Instantiate(pai_prifabes);
        pai.transform.SetParent(player_Result[chirlid].paiC.transform);
        pai.transform.localScale = new Vector2(0.8f,0.8f);        
        pai.transform.localPosition = new Vector2(-352 + 38 * (player_Result[chirlid].paiC.transform.childCount - 1)-a*25, -6);
        
        if (value==127)
        {
            pai.transform.GetChild(1).transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            pai.transform.GetChild(0).transform.gameObject.SetActive(false);
        }
        else
        {
            if (value!=0)
            {
                pai.transform.GetChild(1).transform.GetComponent<Image>().sprite = CreatCard.GetInstance_._2DMaJiangImage[value - 16];
            }
           
        }
    }


    /// <summary>
    /// 设置头像和昵称
    /// </summary>
    /// <param name="seat">座位号</param>
    //public void setHeadNick(int seat) 
    //{
    //    UIPlayerReadyInfo player= UIRoom.Inst.getPlayer(seat);
    //    player_Result[seat].Head.sprite = player.imageHead.sprite;
    //    player_Result[seat].Nick.text = player.textName.text;        
    //}

   

    //public void setPlayerResult(RoundResult roundResult)
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        if (i==MJGameManage.GetInstance.myChirlID)
    //        {
    //            SetChiPengGangViewData(RoomData.Instance.Players.mSelfPlayInfo.PlayerSet(), i);
    //        }
    //        else
    //        {
    //            SetChiPengGangViewData(RoomData.Instance.Players.GetPlayerInfoBySeatId(i).PlayerSet(), i);
    //        }
             
            
    //    }
       
    //    //生成玩家手牌信息
    //    for (int i = 0; i < 4; i++)
    //    {
    //        setHeadNick(i);//设置头像和昵称        
    //        player_Result[i].allScore.text = roundResult.astFinishInfo[i].nWinScore.ToString();//积分
    //        QiShouHu(i, roundResult);//设置起手胡
    //        //DZJ.getInst.updateScore((byte)i, roundResult.astFinishInfo[i].nWinScore);//更新玩家的面板分数
    //        if (roundResult.astFinishInfo[i].nZaNiaoNum>0)
    //        {
    //            player_Result[i].zhongniaoNum.gameObject.SetActive(true);
    //            player_Result[i].zhongniaoNum.text += "中鸟 X ";
    //            player_Result[i].zhongniaoNum.text += roundResult.astFinishInfo[i].nZaNiaoNum.ToString();//中鸟数目
    //            player_Result[i].zhongniaoNum.text += "  ";
    //        }
            

    //        if (i!=MJGameManage.GetInstance.myChirlID)
    //        {
    //            JSONObject obj = new JSONObject();
    //            //获取I的手牌
    //            THandTiles iHandTiles = roundResult.otherPlayersHandles.Find((g) => g.nSeat == i);
                
    //            if (iHandTiles != null)
    //            {
    //                TileSort(iHandTiles);
    //                List<sbyte> iTiles = new List<sbyte>(iHandTiles.szTiles);
    //                iTiles.RemoveRange(iHandTiles.nHandTilesNum, iTiles.Count - iHandTiles.nHandTilesNum);
    //                //如果是自摸 而且指向当前玩家
    //                if (roundResult.chFlag == (byte)SO_FINISH_TYPE.FINISH_SELFDRAW && roundResult.astWinCardInfo[0].chWhoWin == i)
    //                {
    //                    iTiles.Remove(roundResult.astWinCardInfo[0].szWinTiles[0]);
    //                }

    //                iTiles.Sort();
    //                for (int k = 0; k < iTiles.Count; k++)
    //                {
    //                    createPai(i, iTiles[k]);//生成其他三家人的手牌
    //                }
    //                createPai(i, 127);
    //            }              
    //        }
    //        else
    //        {


    //            MJPlayTile TileS = new MJPlayTile();
    //            TileS.AddTiles(RoomData.Instance.Players.mSelfPlayInfo.PlayerTiles());
    //            //判断自摸牌   如果是别人自摸呢
    //            if (roundResult.chFlag == (byte)SO_FINISH_TYPE.FINISH_SELFDRAW && roundResult.astWinCardInfo[0].chWhoWin == i)
    //            {
    //                TileS.DelTile((byte)roundResult.astWinCardInfo[0].szWinTiles[0]);
    //                TileS.Sort();
    //            }
    //            for (int j = 0; j < TileS.m_nCurrentLength; j++)
    //            {
    //                createPai(i, (sbyte)TileS.m_tiles[j]);//生成我的手牌
    //            }
    //            createPai(i, 127);

    //        }

            
    //    }

    //    player_Result[roundResult.chWhoGun].paoMark.SetActive(true);//炮标记
    //    for (int i = 0; i < roundResult.nWinPlayerNum; i++)
    //    {

    //        player_Result[roundResult.astWinCardInfo[i].chWhoWin].huMark.SetActive(true);//胡牌标记
    //        huPaiStyle(roundResult, roundResult.astWinCardInfo[i].chWhoWin);//设置胡的那些类型
            
            

    //        for (int j = 0; j < roundResult.astWinCardInfo[i].nWinNum; j++) 
    //        {
    //            createPai(roundResult.astWinCardInfo[i].chWhoWin, (sbyte)roundResult.astWinCardInfo[i].szWinTiles[j]);//生成胡的牌
    //        }
    //        //player_Result[roundResult.astWinCardInfo[i].chWhoWin].fanScore.text = roundResult.astWinCardInfo[i].nFanNum.ToString();//番数
    //    }
        
    //    player_Result[roundResult.chDealer].zhuangMark.SetActive(true);//庄家标记
       

    //}

    /// <summary>
    /// 设置胡的那些类型
    /// </summary>
    /// <param name="roundResult">数据</param>
    /// <param name="seat">坐位号</param>
    //public void huPaiStyle(RoundResult roundResult, int seat)
    //{
    //    bool hasqixiaodui = false;
    //    bool hasSqixiaodui = false;
    //    bool hasputonghu = false;
    //    bool hasdahu = false;

  
    //    string[] hustyle = { "普通胡", "碰碰胡", "七小对", "豪华七小对", "将将胡", "清一色", "全求人", "海底胡", "天胡", "开杠胡", "开杠胡两张", "抢杠胡" };
    //    foreach (int i in Enum.GetValues(typeof(FAN_WIN_TYPE)))
    //    {

    //        if (null != cardInfo && ((1 << i) & cardInfo.nWinType) > 0)
    //        {
    //            player_Result[seat].zhongniaoNum.gameObject.SetActive(true);
    //            if ((FAN_WIN_TYPE)i == FAN_WIN_TYPE.FAN_KAIGANGHU)
    //            {
    //                player_Result[seat].zhongniaoNum.text += (SO_FINISH_TYPE)roundResult.chFlag == SO_FINISH_TYPE.FINISH_SELFDRAW ? "开杠胡" : "杠上炮";
    //            }
    //            else
    //            {
    //                player_Result[seat].zhongniaoNum.text += hustyle[i];
    //            }

    //            player_Result[seat].zhongniaoNum.text += "  ";
    //            if (i == 0)
    //            {
    //                hasputonghu = true;
    //            }
    //            if (i > 0)
    //            {
    //                hasdahu = true;
    //            }
    //            if (i == 2)
    //            {
    //                hasqixiaodui = true;
    //            }
    //            if (i == 3)
    //            {
    //                hasSqixiaodui = true;
    //            }
    //        }
    //    }
    //    if (hasqixiaodui && hasSqixiaodui)
    //    {
    //        int i = player_Result[seat].zhongniaoNum.text.IndexOf(hustyle[2]);
    //        player_Result[seat].zhongniaoNum.text = player_Result[seat].zhongniaoNum.text.Remove(i, hustyle[2].Length + "  ".Length);
    //    }
    //    if (hasputonghu && hasdahu)
    //    {
    //        int i = player_Result[seat].zhongniaoNum.text.IndexOf(hustyle[0]);
    //        player_Result[seat].zhongniaoNum.text = player_Result[seat].zhongniaoNum.text.Remove(i, hustyle[0].Length + "  ".Length);
    //        hasputonghu = false;
    //    }
    //}

    /// <summary>
    /// 设置起手胡
    /// </summary>
    /// <param name="seat">座位号</param>
    //public void QiShouHu(int seat, RoundResult roundResult)
    //{
    //    string[] QiShouhustyle = { "板板胡", "缺一色", "六六顺", "四喜" };
    //    foreach (int i in Enum.GetValues(typeof(START_WIN_TYPE)))
    //    {

    //        if (0 != roundResult.astFinishInfo[seat].nStartWinType
    //            && ((1 << i) & roundResult.astFinishInfo[seat].nStartWinType) > 0)
    //        {
    //            player_Result[seat].zhongniaoNum.gameObject.SetActive(true);

    //            player_Result[seat].zhongniaoNum.text += QiShouhustyle[i-1];


    //            player_Result[seat].zhongniaoNum.text += "  ";
    //        }
    //    }
    //}

   

    /// <summary>
    /// 再来一局
    /// </summary>
    public void replay() 
    {
        if (tiHuanle)
        {
            CreatCard.GetInstance_.allResult_view.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            KWXFrom.GetIns.GameCmd.SendGameMessgae.SendACTION_SendReady(new PB_Client_Player_Ready());
            for (int i = 0; i < player_Result.Length; i++)
            {
                for (int j = 0; j < player_Result[i].paiC.transform.childCount; j++)
                {
                    Destroy(player_Result[i].paiC.transform.GetChild(j).gameObject);
                }
                player_Result[i].zhongniaoNum.text = "";
                player_Result[i].zhuangMark.SetActive(false);
            }
            for (int i = 0; i < zhongNiaoC.transform.childCount; i++)
            {
                Destroy(zhongNiaoC.transform.GetChild(i).gameObject);
            }
            CreatCard.GetInstance_.init();
            for (int i = 0; i < 4; i++)
            {
                CreatCard.GetInstance_.DNXB_BLIK[i].SetActive(false);
            }
            CreatCard.GetInstance_.shengYuPaiShu.text = "";
        }       
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        CreatCard.GetInstance_.singleResult_view = null;
    }

 

    


}
