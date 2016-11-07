using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using KWX;
using KWX_From;
using UnityEngine.SceneManagement;
public class ALLResult : MonoBehaviour {
    public Text mTextTime;//时间
    public Text mTextTableId;//桌子号
    public Text mTextRound;//局数
    public ALLResult_Player[] player_Result;//玩家结算结果   
	// Use this for initialization


	const string FILE_NAME = "Screenshot.png";


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

		WeChatSDKInterface.Instance.ShareToFriends(2,"","",FullFileName);
	}
    void Awake()
    {
        GameManage.GetInstan.ZongJieSuan = setZongJieSuan;
    }

	void Start () {
	
	}
    public void setZongJieSuan(PB_Server_ALL_Bill_Info result)
    {
        mTextTime.text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//时间
        mTextTableId.text = GameManage.GetInstan.RoomID.ToString();//房间号
        mTextRound.text = CreatCard.GetInstance_.CurJuShu.text;
        for (int i = 0; i < result.best_pao.Count; i++)
        {
            player_Result[result.best_pao[i]].paoShou.SetActive(true);//炮手图标
        }
        for (int i = 0; i < result.best_win.Count; i++)
        {
            player_Result[result.best_win[i]].win.SetActive(true);//炮手图标
        }

        for (int i = 0; i < player_Result.Length; i++)
        {
            player_Result[i].TouXiang.sprite = CreatCard.GetInstance_.UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)i)].TX.sprite;
            player_Result[i].Nick.text = CreatCard.GetInstance_.UserInfoS[GameManage.GetInstan.GetViewSeatID((uint)i)].Nick.text;//昵称
            player_Result[i].ziMoTimes.text = result.player_info[i].zi_mo.ToString();//自摸次数
            player_Result[i].jiePaoTimes.text = result.player_info[i].get_pao.ToString();//接炮次数
            player_Result[i].dianPaoTimes.text = result.player_info[i].fang_pao.ToString();//点炮次数
            player_Result[i].zhongNiaoTimes.text = result.player_info[i].zha_niao.ToString();//中鸟次数
            player_Result[i].kaiGangTimes.text = result.player_info[i].kai_gang.ToString();//开杠次数
            player_Result[i].ALLScore.text = result.player_info[i].all_score.ToString();//总分
        }
        CreatCard.GetInstance_.allResult_view = this.gameObject;
    }
	// Update is called once per frame
	void Update () {
        
	}

    //public void setResult(params object[] obj)
    //{
    //    CNotifyEndFinish allResult = (CNotifyEndFinish)obj[0];
    //    mTextTime.text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//时间
    //    mTextTableId.text = (RoomService.Instance.mTableID+800000).ToString();//房间号
    //    mTextRound.text = (allResult.nCurRoundNum-1).ToString() + "/" + allResult.nAllRoundNum.ToString();//局数
    //    setALLPlayerResult(allResult);//设置各玩家的数据

    //}


    

    /// <summary>
    /// 设置头像和昵称
    /// </summary>
    /// <param name="seat">座位号</param>
    //public void setHeadNick(int seat)
    //{

    //    UIPlayerReadyInfo player= UIRoom.Inst.getPlayer(seat);

    //    if(player !=null)
    //    {
		
    //        player_Result[seat].TouXiang.sprite = player.imageHead.sprite;
    //        player_Result[seat].Nick.text = player.textName.text;
    //    }  
	
    //}


    //public void setALLPlayerResult(CNotifyEndFinish allResult)
    //{
    //    for (int i = 0; i < player_Result.Length; i++)
    //    {
    //        setHeadNick(i);//设置头像和昵称        
    //        player_Result[i].playerID.text = RoomData.Instance.Players.GetPlayerInfoBySeatId((short)i).PlayerUin.ToString();//ID
    //        if (i==allResult.nBestPaoshouSeatid)
    //        {
    //            player_Result[i].paoShou.SetActive(true);//炮手图标
    //        }
    //        if (i==allResult.nWinnerSeatid)
    //        {
    //            player_Result[i].win.SetActive(true);          //winner

    //        }

    //        player_Result[i].ziMoTimes.text = allResult.astEndFinishInfo[i].nZiMoNum.ToString();//自摸次数
    //        player_Result[i].jiePaoTimes.text = allResult.astEndFinishInfo[i].nGetPaoNum.ToString();//接炮次数
    //        player_Result[i].dianPaoTimes.text = allResult.astEndFinishInfo[i].nFanPaoNum.ToString();//点炮次数
    //        player_Result[i].zhongNiaoTimes.text = allResult.astEndFinishInfo[i].nZaNiaoNum.ToString();//中鸟次数
    //        player_Result[i].kaiGangTimes.text = allResult.astEndFinishInfo[i].nKaiGangNum.ToString();//开杠次数
    //        player_Result[i].ALLScore.text = allResult.astEndFinishInfo[i].nWinScore.ToString();//总分
    //    }
    //}


    public short get_number(short[] a)
    {
        short i = 0;
        short max = a[0];

        for (i = 0; i < a.Length; i++)
        {
            if (max < a[i])
                max = a[i];

        }

        return max;
    }

    public void close() 
    {
        SceneManager.LoadScene("daTing");        
    }


    

    public void share() 
    {
		TakeScreenshot(FullFileName);
    }
}
