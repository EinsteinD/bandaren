
using UnityEngine;
using System.Collections;

/// <summary>
/// 微信SDK默认类.
/// </summary>
/// 
public  class WeChatSDKInterfaceDefault: WeChatSDKInterface {
	
	//登录
	public  override void Login(){

		Debuger.Log("weChat login");
	}

	//分享，微信邀请好友
	public override void ShareToFriends(int type ,string title,string description,string urlOrImageFile){

		Debuger.Log("weChat Share:"+ "type:" + type.ToString() + "description:"+ description + "urlOrImageFile:" +urlOrImageFile);
	}


}


