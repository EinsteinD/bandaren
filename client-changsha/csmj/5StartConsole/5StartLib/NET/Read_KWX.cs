using System;
using ProtoBuf;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
namespace KWX
{
    public class BufferData
    {
        public int Cmd;
        public int Length;
        public byte[] Buf;
        public BufferData Bufdt;
    }
    public partial class NET_KWX
    {
        public void StartReadBody(BufferData Msg)//开始读身体
        {
            if (Msg.Length - BufOffset == 0)
            {
                Debuger.Log("包体长度为0 是否服务器没有返回包体 直接读包体");
                ReadDATA(Msg);
            }
            else
            {
                Ns_Read.BeginRead(Buf, BufOffset, Msg.Length - BufOffset, ReadBody, Msg);
            }
        }
        public void ReadDATA(BufferData Msg)
        {
            BufOffset = 0;
            byte[] Buffer_ = new byte[Msg.Length];
            Array.Copy(Buf, 0, Buffer_, 0, Msg.Length);
            Msg.Buf = Buffer_;
            switch (CurTCP)//将数据抛给框架层处理
            {
                case CurSocket.Login_:
                    if (m_LoginFrom != null)
                        m_LoginFrom(Msg);
                    else
                        Debuger.Log("登陆框架不存在");
                    break;
                case CurSocket.Game_:
                    if (m_GameFrom != null)
                        m_GameFrom(Msg);
                    else
                        Debuger.Log("游戏框架不存在");
                    break;
                case CurSocket.NONO_:
                    break;
                default:
                    break;
            }
            StartRead();//继续读网络流
        }
        public void ReadBody(IAsyncResult ansy)//读身体
        {
            try
            {
                if (!mtcpClient.Connected || !Ns_Read.CanRead)
                {
                    Debuger.Log("链接已经被释放");
                    return;
                }
                BufferData Msg = (BufferData)ansy.AsyncState;
                int len = Ns_Read.EndRead(ansy);
                BufOffset += len;
                Debuger.Log("#####################!分割线!#####################");
                Debuger.Log("Body读到字节数：" + len);
                Debuger.Log("Body总共读的字节数：" + BufOffset);
                if (len == 0)
                {
                    Debuger.Log("读到0 服务器是否已经断开客户端连接");
                }
                else if (BufOffset == Msg.Length)
                {
                    ReadDATA(Msg);
                }
                else
                {
                    Debuger.Log("还没读完 继续读body");
                    StartReadBody(Msg);//数据还没读完继续读身体
                }
            }
            catch (Exception ex)
            {
                Debuger.LogRed("网络层读取异常 " + ex.Message + " " + ex.StackTrace);
                return;
            }
        }
        public void StartRead()//开始读头
        {
            if (Ns_Read.CanRead)
            {
                Ns_Read.BeginRead(Buf, BufOffset, 8 - BufOffset, ReadHead, Ns_Read);
            }
        }
        int ReadPageCount = 0;
        public void ReadHead(IAsyncResult ansy)//读头
        {
            int len;
            try
            {
                if (!mtcpClient.Connected || !Ns_Read.CanRead)
                {
                    Debuger.Log("链接已经被释放");
                    return;
                }
                len = Ns_Read.EndRead(ansy);
                Debuger.Log("#####################!分割线!#####################");
                BufOffset += len;
                Debuger.Log("Head读到字节数：" + len);
                Debuger.Log("Head总共读的字节数：" + BufOffset);
                if (len == 0)
                {
                    Debuger.Log("读到0 服务器是否已经断开客户端连接");
                }
                else if (BufOffset == 8)
                {
                    ReadPageCount++;
                    BufOffset = 0;
                    int MsgId = BitConverter.ToInt32(Buf, 0);
                    Debuger.Log("MsgID是：" + MsgId);
                    int MsgLength = BitConverter.ToInt32(Buf, 4);
                    Debuger.Log("Msg包体长度：" + MsgLength);
                    Debuger.Log("已收到成功消息包的数量为：" + ReadPageCount);
                    BufferData Data_Game = new BufferData();
                    Data_Game.Cmd = MsgId;
                    Data_Game.Length = MsgLength;
                    StartReadBody(Data_Game);
                }
                else
                {
                    Debuger.Log("还没读完 继续读head");
                    StartRead();
                }
            }
            catch (Exception ex)
            {
                Debuger.Log(ex.Message);
                NET_KWX.Instance_.mtcpClient.Close();
                return;
            }
        }
    }
}