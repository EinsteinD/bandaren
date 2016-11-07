using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KWX_From;
using KWX;
using UnityEngine;
using System.Threading;
namespace _5STARTCONSOLES
{
    class TEST
    {
        static void Main(string[] args)
        {
            Debuger.EnableLog = 1;
            PB_Client_DealCard act = new PB_Client_DealCard();

            act.card_value = 4294967295;
            byte[] temp = ProtoBufSerialize<PB_Client_DealCard>.Serialize(act);
            Debuger.Log(temp.Length);
            PB_Client_DealCard result = ProtoBufSerialize<PB_Client_DealCard>.DeSerialize(temp);
            Debuger.Log(result.card_value);
            //TestFrom test = new TestFrom();
            Console.ReadLine();
        }
    }
    #region 单元测试
    public class TestFrom : LoginInterface, GameInterface
    {
        public TestFrom()
        {
            Debuger.EnableLog = 1;//开启日志打印 0关闭 1控制台下日志 2unity3d日志 日志打印请使用Debuger.Log
            //链接登录服务器
            KWXFrom.GetIns.Login_ = this;
            KWXFrom.GetIns.InitLoginFrom("172.16.10.153", 4000);
            Random RANG = new Random();
            Console.ReadLine();
            //测试登陆
            CMD_Login LOGINitem = new CMD_Login() { UserID = (ulong)RANG.Next(1,99999999), Password = "liuyu", WeiXinCode = "12345" };
            KWXFrom.GetIns.LoginCmd.SendLoginGame(LOGINitem, null);
            Console.ReadLine();
            Debuger.Log("回车代表创建房间 其他任意键+回车 代表进入房间");
            if (Console.ReadLine()=="")
            {
                //测试创建房间
                CMD_CreateRoom creatroom = new CMD_CreateRoom() { GameName = "CSMJ", Rule = "哈哈" ,RoundCount=8};
                KWXFrom.GetIns.LoginCmd.SendCreatRoom(creatroom);
                Console.ReadLine();
            }
            else
            {
                Debuger.Log("请输入 房间号");
                //测试进入房间
                uint RoomIDD=uint.Parse(Console.ReadLine());
                CMD_EnterRoom entroom = new CMD_EnterRoom() { RoomID = RoomIDD };
                KWXFrom.GetIns.LoginCmd.SendEntRoom(entroom);
                m_result = new RSP_CreateRoom();
                m_result.RoomID = RoomIDD;
                m_result.Password = m_Loginresult.Password;
                Console.ReadLine();
            }

            int BaoLiCeShi = 0;
            while (BaoLiCeShi < 200)
            {
                //心跳测试
                CMD_HeartBeat HeartBeat_ = new CMD_HeartBeat();
                KWXFrom.GetIns.GameCmd.SendHeartBeat(HeartBeat_);
                BaoLiCeShi++;
                Debuger.Log("心跳暴力测试 次数：" + BaoLiCeShi);
                //Console.ReadLine();
            }
        }
        int BaoLiXinTiao = 0;
        public void HearBeat(RSP_HeartBeat result)
        {
            BaoLiXinTiao++;
            Debuger.Log("心跳回包 暴力测试回包次数：" + BaoLiXinTiao);
        }

        RSP_CreateRoom m_result = null;
        public void EntCartRoom(KWX.RSP_CreateRoom result)
        {
            Debuger.Log("创建房间返回" + result.RoomID);
            m_result = result;
            //链接游戏服务器
            KWXFrom.GetIns.Game_ = this;
            KWXFrom.GetIns.InitGameFrom(m_result.ServerHost, m_result.ServerPort);
            Console.ReadLine();
        }
        RSP_Login m_Loginresult;
        public void LoginOK(KWX.RSP_Login result)
        {
            Debuger.Log("登陆返回");
            m_Loginresult = result;
        }


        public void EntServer(RSP_LoginServer result)
        {
            Debuger.Log("登陆游戏服务器返回" + result.Error);
        }

        RSP_EnterRoom m_EnterRoomresult = null;
        public void EntRoom(RSP_EnterRoom result)
        {
            Debuger.Log("申请进入房间" + result.Error);
            m_EnterRoomresult = result;
            m_result.Password = m_EnterRoomresult.Password;
            m_result.SeatID = m_EnterRoomresult.SeatID;
            //链接游戏服务器
            KWXFrom.GetIns.Game_ = this;
            KWXFrom.GetIns.InitGameFrom(m_EnterRoomresult.ServerHost, m_EnterRoomresult.ServerPort);
        }


        public void ExitRoom(RSP_ExitRoom result)
        {
            Debuger.Log("推出房间" + result.Error);
        }

        public void OnMessage(NTF_Message result)
        {
            Debuger.Log("106，聊天信息" + result);
        }

        public void OnRoomDismissed(NTF_RoomDismissed result)
        {
            Debuger.Log("105，房间已解散" + result);
        }

        public void OnRoomUserInfo(NTF_RoomUserInfo result)
        {
            Debuger.Log("用户信息" + result.SeatID);
        }

        public void OnRoomUserOffline(NTF_RoomUserOffline result)
        {
            Debuger.Log("用户离线" + result.SeatID);
        }

        public void OnRoomUserOnline(NTF_RoomUserOnline result)
        {
            Debuger.Log("用户在线" + result.SeatID);
        }

        public void OnSameUserLogin(NTF_SameUserLogin result)
        {
          
        }


        public void OnGAME_STATE(PB_Server_Room_Info result)
        {
            foreach (var item in result.player_info)
            {
                string SPstr = "";

                SPstr += item.ToString() + ",";
                
                Debuger.Log("游戏开始 座位号" + SPstr);
            }
        }


        public void LinkLoginServerOK()
        {
            Debuger.Log("链接登陆服务器成功");
        }


        public void LinkGameServerOK()
        {
            Debuger.Log("链接游戏服务器成功");
            //登陆游戏服务器
            CMD_LoginServer LoginServer_ = new CMD_LoginServer()
            {
                Password = m_result.Password,
                RoomID = m_result.RoomID,
                SeatID = m_result.SeatID
            };
            KWXFrom.GetIns.GameCmd.SendLoginGameServer(LoginServer_);
            Console.ReadLine();
        }


        public void OnAllResult(PB_Server_ALL_Bill_Info result)
        {
            
        }

        public void OnSingleResult(PB_Server_Single_Bill result)
        {
            
        }

        public void DismisseMessage(NTF_Dismiss result)
        {
            
        }
    }
    #endregion
}
