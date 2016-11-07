using KWX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KWX_From
{
    public class LoginDISPATH
    {
        public static void DispathMssage(BufferData Msg)//消息分发
        {
            Debuger.Log("收到大厅主命令：" + Msg.Cmd);//-1000代表链接服务器成功
            switch (Msg.Cmd)
            {
                case -1000:
                    {
                        if (KWXFrom.GetIns.Login_ != null)
                        {
                            KWXFrom.GetIns.Login_.LinkLoginServerOK();
                        }
                    }
                    break;
                case 1:
                    {
                        RSP_Login result = ProtoBufSerialize<RSP_Login>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Login_ != null)
                        {
                            KWXFrom.GetIns.Login_.LoginOK(result);
                        }
                    }
                    break;
                case 2:
                    {
                        KWXFrom.GetIns.DisConnet();
                        RSP_CreateRoom result = ProtoBufSerialize<RSP_CreateRoom>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Login_ != null)
                        {
                            KWXFrom.GetIns.Login_.EntCartRoom(result);
                        }
                    }
                    break;
                case 3:
                    {
                        KWXFrom.GetIns.DisConnet();
                        RSP_EnterRoom result = ProtoBufSerialize<RSP_EnterRoom>.DeSerialize(Msg.Buf);
                        if (KWXFrom.GetIns.Login_ != null)
                        {
                            KWXFrom.GetIns.Login_.EntRoom(result);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
