using UnityEngine;
using System.Collections;
using System.Threading;
using System.Net;
using System.Net.Sockets;

public class BytePacket
{
	public byte [] Bytes;
	public BytePacket()
	{
		Clear();
	}
	public void Clear()
	{
		Bytes=new byte[0];
	}
	public void Add(int V)
	{
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)+4];
		System.Array.Copy(b2,Bytes,b2.GetLength(0));
		System.Array.Copy(System.BitConverter.GetBytes(V),0,Bytes,b2.GetLength(0),4);
	}
	public void Add(uint V)
	{
		Add ((int)V);
	}
	public void Add(double V)
	{
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)+8];
		System.Array.Copy(b2,Bytes,b2.GetLength(0));
		System.Array.Copy(System.BitConverter.GetBytes(V),0,Bytes,b2.GetLength(0),8);
	}
	public void Add(float V)
	{
		Add((double)V);
	}
	public void Add(string V)
	{
		byte [] b2=Bytes;
		byte [] b3=System.Text.Encoding.Default.GetBytes(V);
		Bytes=new byte[b2.GetLength(0)+b3.GetLength(0)+1];
		System.Array.Copy(b2,Bytes,b2.GetLength(0));
		System.Array.Copy(b3,0,Bytes,b2.GetLength(0),b3.GetLength(0));
		Bytes[Bytes.GetLength(0)-1]=0;
	}
	public void Add(byte [] V)
	{
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)+4+V.GetLength(0)];
		System.Array.Copy(b2,Bytes,b2.GetLength(0));
		System.Array.Copy(System.BitConverter.GetBytes(V.GetLength(0)),0,Bytes,b2.GetLength(0),4);
		System.Array.Copy(V,0,Bytes,b2.GetLength(0)+4,V.GetLength(0));
	}
	public void Get(out int V)
	{
		V=System.BitConverter.ToInt32(Bytes,0);
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)-4];
		System.Array.Copy (b2,4,Bytes,0,Bytes.GetLength (0));
	}
	public void Get(out uint V)
	{
		int v2;
		Get (out v2);
		V=(uint)v2;
	}
	public void Get(out double V)
	{
		V=System.BitConverter.ToDouble(Bytes,0);
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)-8];
		System.Array.Copy (b2,8,Bytes,0,Bytes.GetLength (0));
	}
	public void Get(out float V)
	{
		double v2;
		Get (out v2);
		V=(float)v2;
	}
	public void Get(out string V)
	{
		uint len=0;
		while(Bytes[len]!=0)len++;
		byte [] b3=new byte[len];
		System.Array.Copy (Bytes,b3,len);
		V=System.Text.Encoding.Default.GetString(b3);
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)-(len+1)];
		System.Array.Copy (b2,len+1,Bytes,0,Bytes.GetLength(0));
	}
	public void Get(out byte [] V)
	{
		uint len=System.BitConverter.ToUInt32(Bytes,0);
		V=new byte[len];
		System.Array.Copy (Bytes,4,V,0,len);
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)-(4+len)];
		System.Array.Copy (b2,4+len,Bytes,0,Bytes.GetLength(0));
	}
	
	public float GetFloat()
	{
		float v=System.BitConverter.ToSingle(Bytes,0);
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)-4];
		System.Array.Copy (b2,4,Bytes,0,Bytes.GetLength (0));
		return v;
	}
	public string GetString(uint MaxLen)
	{
		uint i;
		for(i=0;i<MaxLen;i++)
		{
			if(0==Bytes[i])break;
		}
		string v;
		byte [] b3=new byte[i];
		System.Array.Copy (Bytes,b3,i);
		v=System.Text.Encoding.Default.GetString(b3);
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)-MaxLen];
		System.Array.Copy (b2,MaxLen,Bytes,0,Bytes.GetLength(0));
		return v;
	}
	public bool GetBool()
	{
		bool v=Bytes[0]!=0;
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)-1];
		System.Array.Copy (b2,1,Bytes,0,Bytes.GetLength (0));
		return v;
	}
	public void Skip(uint Len)
	{
		byte [] b2=Bytes;
		Bytes=new byte[b2.GetLength(0)-Len];
		System.Array.Copy (b2,Len,Bytes,0,Bytes.GetLength (0));
	}
}

class NetThread
{
	public AutoResetEvent ReturnEvent;
	public Mutex CallMutex;
	public BytePacket NameAndParams;
	
	private Mutex SendMutex;
	private byte [] ToSendData;
	
	public NetThread()
	{
		CallMutex=new Mutex(false);
		ReturnEvent=new AutoResetEvent(false);
		SendMutex=new Mutex(false);
		ToSendData=new byte [0];
	}
	private void CallProc(byte [] NPData)
	{
		CallMutex.WaitOne ();
		NameAndParams=new BytePacket();
		NameAndParams.Bytes=NPData;
		CallMutex.ReleaseMutex();
		ReturnEvent.WaitOne();
	}
	private void CallProc(BytePacket NP)
	{
		CallMutex.WaitOne ();
		NameAndParams=NP;
		CallMutex.ReleaseMutex();
		ReturnEvent.WaitOne();
	}
	public void SendData(BytePacket Data)
	{
		SendMutex.WaitOne ();
		byte [] b2=ToSendData;
		ToSendData=new byte[b2.GetLength(0)+4+Data.Bytes.GetLength(0)];
		System.Array.Copy (b2,ToSendData,b2.GetLength (0));
		System.Array.Copy (System.BitConverter.GetBytes(Data.Bytes.GetLength(0)),0,ToSendData,b2.GetLength(0),3);
		ToSendData[b2.GetLength(0)+3]=1;
		System.Array.Copy (Data.Bytes,0,ToSendData,b2.GetLength(0)+4,Data.Bytes.GetLength (0));
		SendMutex.ReleaseMutex();
	}
	public void ThreadProc()
	{
		BytePacket np=new BytePacket();
		Socket sock;
		try
		{
			sock=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			sock.Connect("www.nebulastormsoft.com",8888);
			//sock.Connect("127.0.0.1",8888);
		}
		catch(SocketException)
		{
			np.Add("OnError");
			np.Add("不能连接到服务器");
			CallProc(np);
			return;
		}
		byte [] LenB=new byte[4];
		byte [] dat;
		byte [] heart=new byte[4];
		heart[0]=0;
		heart[1]=0;
		heart[2]=0;
		heart[3]=0;
		uint LastSendTime=(uint)System.Environment.TickCount;
		while(true)
		{
			SendMutex.WaitOne();
			if(0==ToSendData.GetLength(0))
			{
				SendMutex.ReleaseMutex();
			}
			else
			{
				dat=ToSendData;
				ToSendData=new byte[0];
				SendMutex.ReleaseMutex();
				sock.Send (dat);
				LastSendTime=(uint)System.Environment.TickCount;
			}
			
			if((uint)System.Environment.TickCount-LastSendTime>30*1000)
			{//发送心跳包
				sock.Send(heart);
				LastSendTime=(uint)System.Environment.TickCount;
			}
			
			int res=RecvBytes(sock,LenB,0,4,100);
			if(0==res)continue;
			if(-1==res)break;
			LenB[3]=0;
			uint len=System.BitConverter.ToUInt32(LenB,0);
			if(0==len)continue;//心跳回应包直接丢弃
			dat=new byte[len];
			res=RecvBytes(sock,dat,0,len,-1);
			if(-1==res)break;
			CallProc(dat);
		}
		
		np.Add("OnError");
		np.Add("与服务器的连接已断开");
		CallProc(np);
	}
	private int RecvBytes(Socket Sock,byte [] Buf,uint Index,uint Len,int TimeOut)
	{//返回值：0:超时无数据 1:成功 -1:失败
		if(0==Len)return 1;
		Sock.ReceiveTimeout=TimeOut;
		int n;
		try
		{
			n=Sock.Receive(Buf,(int)Index,(int)Len,SocketFlags.None);
		}
		catch(SocketException e)
		{
			if(e.SocketErrorCode==SocketError.TimedOut)return 0;
			return -1;
		}
		if(0==n)return -1;
		Len-=(uint)n;
		Index+=(uint)n;
		Sock.ReceiveTimeout=-1;
		while(Len>0)
		{
			try
			{
				n=Sock.Receive(Buf,(int)Index,(int)Len,SocketFlags.None);
			}
			catch(SocketException)
			{
				return -1;
			}
			if(0==n)return -1;
			Len-=(uint)n;
			Index+=(uint)n;
		}
		return 1;
	}
}

public class NetIO : MonoBehaviour
{
	private NetThread netThread;
	void Start()
	{
		netThread=new NetThread();
		Thread th=new Thread(netThread.ThreadProc);
		th.Start();
		MSKVersionReport(1);
	}
	void Update()
	{
		netThread.CallMutex.WaitOne();
		if(null==netThread.NameAndParams)
		{
			netThread.CallMutex.ReleaseMutex();
			return;
		}
		netThread.CallMutex.ReleaseMutex();
		string Name;
		netThread.NameAndParams.Get (out Name);
		
		if("OnError"==Name)
		{
			string Desc;
			netThread.NameAndParams.Get(out Desc);
//			gameObject.GetComponent<Main>().OnError(Desc);
		}
		else if("OnCharInfo"==Name)
		{
			byte [] Effect;
			byte [] Char;
			netThread.NameAndParams.Get (out Effect);
			netThread.NameAndParams.Get (out Char);
//			gameObject.GetComponent<Main>().OnCharInfo(Effect,Char);
		}
		
		netThread.NameAndParams=null;
		netThread.ReturnEvent.Set ();
	}
	void MSKVersionReport(uint Version)
	{
		BytePacket bp=new BytePacket();
		bp.Add("MSKVersionReport");
		bp.Add (Version);
		netThread.SendData(bp);
	}
};
