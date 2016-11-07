using KWX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KWX_From
{
    public class GameDISPATH
    {
        #region 1-1000命令解析 游戏通用命令
        static void Dis_1_1000(BufferData Msg)
        {
            switch (Msg.Cmd)
            {
                case -1000://链接服务器成功
                    {
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.LinkGameServerOK();
                        }
                    }
                    break;
                case 1:
                    {
                        RSP_HeartBeat result = ProtoBufSerialize<RSP_HeartBeat>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.HearBeat(result);
                        }
                    }
                    break;
                case 2:
                    {
                        RSP_LoginServer result = ProtoBufSerialize<RSP_LoginServer>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.EntServer(result);
                        }
                    }
                    break;
                case 3:
                    {
                        RSP_ExitRoom result = ProtoBufSerialize<RSP_ExitRoom>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.ExitRoom(result);
                        }
                    }
                    break;
                case 101://命令ID: 101，房间内的用户信息
                    {
                        NTF_RoomUserInfo result = ProtoBufSerialize<NTF_RoomUserInfo>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.OnRoomUserInfo(result);
                        }
                    }
                    break;
                case 102://命令ID：102，相同用户已在另一地点登录，当前用户已被迫下线
                    {
                        NTF_SameUserLogin result = ProtoBufSerialize<NTF_SameUserLogin>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.OnSameUserLogin(result);
                        }
                    }
                    break;
                case 103://命令ID: 103，房间内的用户上线
                    {
                        NTF_RoomUserOnline result = ProtoBufSerialize<NTF_RoomUserOnline>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.OnRoomUserOnline(result);
                        }
                    }
                    break;
                case 104://命令ID: 104，房间内的用户离线
                    {
                        NTF_RoomUserOffline result = ProtoBufSerialize<NTF_RoomUserOffline>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.OnRoomUserOffline(result);
                        }
                    }
                    break;
                case 105://命令ID: 105，房间已解散
                    {
                        NTF_RoomDismissed result = ProtoBufSerialize<NTF_RoomDismissed>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.OnRoomDismissed(result);
                        }
                    }
                    break;
                case 106://命令ID: 106，聊天信息
                    {
                        NTF_Message result = ProtoBufSerialize<NTF_Message>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.OnMessage(result);
                        }
                    }
                    break;
                case 107://命令ID: 107，解散信息
                    {
                        NTF_Dismiss result = ProtoBufSerialize<NTF_Dismiss>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.DismisseMessage(result);
                        }
                    }
                    break;
                default:
                    Debuger.Log("未解析的主命令" + Msg.Cmd);
                    break;
            }
        }
        #endregion
        #region 1000以上命令解析 单款游戏命令解析
        static void Dis_1000_MAX(BufferData Msg)
        {
            switch ((ServerProtocol)Msg.Cmd)
            {
                case ServerProtocol.Server_Protocol_Room_Info:
                    {
                        PB_Server_Room_Info result = ProtoBufSerialize<PB_Server_Room_Info>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.OnGAME_STATE(result);
                        }
                    }
                    break;
                case ServerProtocol.Server_Protocol_Single_Bill:
                    {
                        PB_Server_Single_Bill result = ProtoBufSerialize<PB_Server_Single_Bill>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.OnSingleResult(result);
                        }
                    }
                    break;
                case ServerProtocol.Server_Protocol_All_Bill:
                    {
                        PB_Server_ALL_Bill_Info result = ProtoBufSerialize<PB_Server_ALL_Bill_Info>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Game_ != null)
                        {
                            KWXFrom.GetIns.Game_.OnAllResult(result);
                        }
                    }
                    break;           
                default:
                    break;
            }
            
        }
        #endregion
        public static void DispathMssage(BufferData Msg)//消息分发
        {
            Debuger.Log("收到游戏主命令：" + Msg.Cmd);
            if (Msg.Cmd<1000)
            {
                Dis_1_1000(Msg);
            }
            else
            {
                Dis_1000_MAX(Msg);
            }
        }
    }
}
