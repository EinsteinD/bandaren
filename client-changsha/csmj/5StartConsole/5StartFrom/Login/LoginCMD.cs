using KWX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KWX_From
{
    public class LoginCMD
    {
        public BufferData LOGINitem;
        public void SendEntRoom(CMD_EnterRoom item)//进入房间
        {
            byte[] temp = ProtoBufSerialize<CMD_EnterRoom>.Serialize(item);
            NET_KWX.Instance_.LoginSendDate(3, temp, LOGINitem);
        }
        public void SendCreatRoom(CMD_CreateRoom item)//创建房间
        {
            byte[] temp = ProtoBufSerialize<CMD_CreateRoom>.Serialize(item);
            NET_KWX.Instance_.LoginSendDate(2, temp, LOGINitem);
        }
        public void SendLoginGame(CMD_Login item,Action Act)//登陆游戏
        {
            byte[] temp = ProtoBufSerialize<CMD_Login>.Serialize(item);
            LOGINitem = new BufferData();
            LOGINitem.Cmd = 1;
            LOGINitem.Buf = temp;
            NET_KWX.Instance_.LoginSendDate(1, temp, LOGINitem);
        }
    }
}
