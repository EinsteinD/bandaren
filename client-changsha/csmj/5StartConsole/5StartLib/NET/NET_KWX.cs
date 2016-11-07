using System;
using ProtoBuf;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
namespace KWX
{
    public partial class NET_KWX
    {
        public static NET_KWX Instance_
        {
            get
            {
                if (Ins==null)
                {
                    Ins = new NET_KWX();
                }
                return Ins;
            }
        }
        static NET_KWX Ins = null;
        public TcpClient mtcpClient;//TCP客户端连接
        NetworkStream Ns_Read;
        CurSocket CurTCP=CurSocket.NONO_;
        public LoginGameFrom m_LoginFrom = null;//游戏框架回掉
        public GameRoomFrom m_GameFrom = null;//游戏房间回掉
        byte[] Buf = new byte[10240];//缓冲区
        int BufOffset = 0;
        public string IP_;
        public int Port_;
        public CurSocket GetCurSTATE
        {
            get
            {
                return CurTCP;
            }
        }
        void ConnectOK(IAsyncResult ansy)
        {
            mtcpClient.EndConnect(ansy);
            Debuger.Log("链接返回：" + mtcpClient.Connected);
            if (!mtcpClient.Connected)
            {
            }

            Ns_Read = null;
            Ns_Read = mtcpClient.GetStream();
            StartRead();
            if (CurTCP == CurSocket.Login_)
            {
                if (m_LoginFrom != null)
                {
                    m_LoginFrom(new BufferData() { Cmd = -1000 });
                }
                BufferData LoginSend = ansy.AsyncState as BufferData;
                if (LoginSend.Bufdt != null && LoginSend.Bufdt.Cmd != LoginSend.Cmd)
                {
                    SendDATA(LoginSend.Bufdt);//如果主命令不相同表示此时发起的不仅是登陆请求还有其他请求
                }
                SendDATA(LoginSend);
            }
            else
            {
                if (m_GameFrom != null)
                {
                    m_GameFrom(new BufferData() { Cmd = -1000 });
                }
            }
        }
        public void StartConnect(string Ip, int Port, CurSocket cur, int CMD = 0, byte[] temp = null, BufferData bufdt = null)
        {
            CurTCP = cur;
            mtcpClient = new TcpClient();
            BufferData BUF = new BufferData();
            if (cur==CurSocket.Game_)
            {
                Debuger.Log("当前链接的服务器是游戏服务器" + Ip + " " + Port);
                mtcpClient.BeginConnect(Ip, Port, ConnectOK, null);
            }
            else
            {
                Debuger.Log("当前链接的服务器是登陆服务器");
                IP_ = Ip;
                Port_ = Port;
                if (bufdt != null)
                {
                    BUF.Cmd = CMD;
                    BUF.Buf = temp;
                    BUF.Length = temp.Length;
                    BUF.Bufdt = bufdt;
                }
                mtcpClient.BeginConnect(Ip, Port, ConnectOK, BUF);
            }
        }
    }
}