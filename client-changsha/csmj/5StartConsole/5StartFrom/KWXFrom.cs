using KWX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KWX_From
{
    public partial class KWXFrom
    {
        private KWXFrom() { }
        private static KWXFrom ins = null;
        public static KWXFrom GetIns
        {
            get
            {
                if (ins == null)
                {
                    ins = new KWXFrom();
                }
                return ins;
            }
        }
        public LoginInterface Login_ = null;
        public LoginCMD LoginCmd;
        public GameInterface Game_ = null;
        public GameCMD GameCmd;
        public void InitLoginFrom(string Ip, int Port)
        {
            NET_KWX.Instance_.m_LoginFrom = LoginDISPATH.DispathMssage;
            NET_KWX.Instance_.IP_ = Ip;
            NET_KWX.Instance_.Port_ = Port;
            LoginCmd = new LoginCMD();
        }
        public void InitGameFrom(string Ip, int Port)
        {
            NET_KWX.Instance_.m_GameFrom = GameDISPATH.DispathMssage;
            NET_KWX.Instance_.StartConnect(Ip, Port, CurSocket.Game_);
            GameCmd = new GameCMD();
        }
        public void DisConnet()
        {
            NET_KWX.Instance_.mtcpClient.GetStream().Flush();
            NET_KWX.Instance_.mtcpClient.GetStream().Close();
            NET_KWX.Instance_.mtcpClient.Close();
            NET_KWX.Instance_.mtcpClient = null;
        }
    }
}
