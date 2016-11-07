
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 微信SDK Android.
/// </summary>
/// 

public  class WeChatSDKInterfaceAndroid: WeChatSDKInterface {
	
	private AndroidJavaObject jo;

	public WeChatSDKInterfaceAndroid()
	{
		using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		}
	}

	private T SDKCall<T>(string method, params object[] param)
	{
		try
		{
			return jo.Call<T>(method, param);
		}
		catch (Exception e)
		{
			Debuger.LogError(e);
		}
		return default(T);
	}


	private void SDKCall(string method, params object[] param)
	{
		try
		{
			jo.Call(method, param);
		}
		catch (Exception e)
		{
			Debuger.LogError(e);
		}
	}


	public override void Login()
	{
		Debuger.Log("wxg: text");

        SDKCall("login");
	}
		
	//分享，微信邀请好友
	public override void ShareToFriends( int type ,string title="",string description = "",string urlorImageFile = ""){

		Debuger.Log("weChat Share"+urlorImageFile);

		SDKCall("ShareToFriends",type,title,description,urlorImageFile);

	}
}


