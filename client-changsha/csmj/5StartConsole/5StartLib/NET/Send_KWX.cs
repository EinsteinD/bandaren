using System;
using ProtoBuf;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System.Collections;
namespace KWX
{
    public partial class NET_KWX
    {
        public void LoginSendDate(int CMD, byte[] temp, BufferData bufdt = null)
        {
            if (mtcpClient != null && mtcpClient.Connected)
            {
                BufferData LoginSend = new BufferData() { Cmd = CMD, Buf = temp };
                SendDATA(LoginSend);
                return;
            }
            StartConnect(IP_, Port_, CurSocket.Login_, CMD, temp, bufdt);
        }
        public void SendDATA(BufferData LoginSend)//发送数据
        {
            Debuger.Log("发送数据 CMD:"+LoginSend.Cmd);
            byte[] Data_ = new byte[8 + LoginSend.Buf.Length];
            Array.Copy(BitConverter.GetBytes(LoginSend.Cmd), 0, Data_, 0, 4);
            Array.Copy(BitConverter.GetBytes((int)LoginSend.Buf.Length), 0, Data_, 4, 4);
            Array.Copy(LoginSend.Buf, 0, Data_, 8, LoginSend.Buf.Length);
            Debuger.Log("发送数据 CMD  长度:" + Data_.Length);
            Send(Data_);
        }
        void Send(byte[] buf)
        {
            if (Ns_Read==null)
            {
                Debuger.Log("网络流未被初始化");
            }
            if (Ns_Read.CanWrite)
            {
                //Ns_Read.Write(buf, 0, buf.Length);
                lock (Ns_Read)
                {
                    Ns_Read.BeginWrite(buf, 0, buf.Length, SendOK, null);
                }
            }
            else
            {
                Debuger.Log("数据流已经被关闭 无法写入");
            }
        }
        int SendPageCount;
        void SendOK(IAsyncResult ansy)
        {
            Ns_Read.EndWrite(ansy);
            SendPageCount++;
            Debuger.Log("SEND OK 已发送成功消息包的数量为：" + SendPageCount);
        }
    }
}