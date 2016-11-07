using KWX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KWX_From
{
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
        Error_Card_Not_Kang,//杠的牌不对
        Error_Last_Card_Number,//底牌数量不对
        Error_Player_Deal_Card,//没轮到你出牌
        Error_Player_Status,//玩家状态错误
        Error_Player_Kang_Ting,//开杠之后只能胡牌和抓什么打什么
        Error_Player_No_Ready//玩家没有准备
    };

    //客户端请求协议
    enum ClientProtocol
    {
        Client_Protocol_Player_Join_Room = 1000,//玩家加入房间
        Client_Protocol_Player_Quit_Room,//玩家退出房间
        Client_Protocol_Player_Dissmis_Room,//玩家解散房间
        Client_Protocol_Player_Agree_Dissmis,//玩家解散房间反馈
        Client_Protocol_Player_Ready,//玩家准备
        Client_Protocol_Player_Deal_Card,//玩家出牌
        Client_Protocol_Player_Zi_Mo,//玩家自摸
        Client_Protocol_Player_Chi_Card,//玩家吃牌
        Client_Protocol_Player_Peng_Card,//玩家碰牌
        Client_Protocol_Player_Gang_Card,//玩家杠牌
        Client_Protocol_Player_Bu_Zhang,//玩家补张
        Client_Protocol_Player_Kai_Gang,//玩家开杠
        Client_Protocol_Player_Hu_Pai,//玩家胡牌
        Client_Protocol_Player_Pass_Card,//玩家过牌
        Client_Protocol_Player_HDP,//玩家海底牌
        Client_Protocol_Player_Reconnection//玩家断线重连
    };

    //服务器主动下发协议
    enum ServerProtocol
    {
        Server_Protocol_Room_Info = 10000,//桌面信息
        Server_Protocol_Single_Bill,//单局结算
        Server_Protocol_All_Bill//总结算
    };

    /// <summary>
    /// 游戏命令 T为游戏扩展命令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GameCMD
    {
        public GameCMD()
        {
            GameMessage = new KWXGameMessgae();
        }
        public void SendHeartBeat(CMD_HeartBeat item)//命令ID：1 心跳包
        {
            if (NET_KWX.Instance_.mtcpClient.Connected && NET_KWX.Instance_.GetCurSTATE==CurSocket.Game_)
            {
                byte[] temp = ProtoBufSerialize<CMD_HeartBeat>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = 1, Buf = temp });
            }
        }
        public void SendLoginGameServer(CMD_LoginServer item)//命令ID：2，登录到游戏服务器
        {
            byte[] temp = ProtoBufSerialize<CMD_LoginServer>.Serialize(item);
            NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = 2, Buf = temp });
        }
        public void SendExitRoom()//命令ID：3，请求退出房间
        {
            CMD_ExitRoom t = new CMD_ExitRoom();
            byte[] temp = ProtoBufSerialize<CMD_ExitRoom>.Serialize(t);
            NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = 3, Buf = temp });
        }
        public void SendTalkMessage(CMD_Message item)//命令ID：4，聊天信息（包括文字、表情、语音等）
        {
            byte[] temp = ProtoBufSerialize<CMD_Message>.Serialize(item);
            NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = 4, Buf = temp });
        }
        public void SendACTION_SendDismiss(CMD_Dismiss item)//命令ID: 6，申请解散房间
        {
            byte[] temp = ProtoBufSerialize<CMD_Dismiss>.Serialize(item);
            NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = 6, Buf = temp });
        }
        /// <summary>
        /// 游戏内部消息
        /// </summary>
        KWXGameMessgae GameMessage;
        public KWXGameMessgae SendGameMessgae
        {
            get
            {
                return GameMessage;
            }
        }

        public class KWXGameMessgae
        {
            public void SendACTION_CHUPAI(PB_Client_DealCard item)//命令ID: ，出牌
            {
                byte[] temp = ProtoBufSerialize<PB_Client_DealCard>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = (int)ClientProtocol.Client_Protocol_Player_Deal_Card, Buf = temp });
            }

            public void SendACTION_CHIPAI(PB_Client_ChiCard item)//命令ID: ，吃牌
            {
                byte[] temp = ProtoBufSerialize<PB_Client_ChiCard>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = (int)ClientProtocol.Client_Protocol_Player_Chi_Card, Buf = temp });
            }
            public void SendACTION_PENGPAI(PB_Client_PengCard item)//命令ID: ，碰牌
            {
                byte[] temp = ProtoBufSerialize<PB_Client_PengCard>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = (int)ClientProtocol.Client_Protocol_Player_Peng_Card, Buf = temp });
            }
            public void SendACTION_GANG(PB_Client_GangCard item)//命令ID: ，杠牌
            {
                byte[] temp = ProtoBufSerialize<PB_Client_GangCard>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = (int)ClientProtocol.Client_Protocol_Player_Gang_Card, Buf = temp });
            }
            public void SendACTION_KAIGANG(PB_Client_KaiGang item)//命令ID: ，开杠
            {
                byte[] temp = ProtoBufSerialize<PB_Client_KaiGang>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = (int)ClientProtocol.Client_Protocol_Player_Kai_Gang, Buf = temp });
            }
            public void SendACTION_BUZHANG(PB_Client_BuZhang item)//命令ID: ，补张
            {
                byte[] temp = ProtoBufSerialize<PB_Client_BuZhang>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = (int)ClientProtocol.Client_Protocol_Player_Bu_Zhang, Buf = temp });
            }
            public void SendACTION_HUPAI(PB_Client_HuPai item)//命令ID: ，胡牌
            {
                byte[] temp = ProtoBufSerialize<PB_Client_HuPai>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = (int)ClientProtocol.Client_Protocol_Player_Hu_Pai, Buf = temp });
            }
            public void SendACTION_GUOPAI(PB_Client_Pass item)//命令ID: ，过牌
            {
                byte[] temp = ProtoBufSerialize<PB_Client_Pass>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = (int)ClientProtocol.Client_Protocol_Player_Pass_Card, Buf = temp });
            }
            public void SendACTION_SendReady(PB_Client_Player_Ready item)//命令ID: ，准备
            {
                byte[] temp = ProtoBufSerialize<PB_Client_Player_Ready>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = (int)ClientProtocol.Client_Protocol_Player_Ready, Buf = temp });
            }
            public void SendACTION_HDP(PB_Client_HDP item)//命令ID: ，海底牌
            {
                byte[] temp = ProtoBufSerialize<PB_Client_HDP>.Serialize(item);
                NET_KWX.Instance_.SendDATA(new BufferData() { Cmd = (int)ClientProtocol.Client_Protocol_Player_HDP, Buf = temp });
            }
        }
    }
}