using UnityEngine;
using System.Collections;

/// <summary>
/// 微信sdk抽象类.
/// </summary>
/// 
public abstract class WeChatSDKInterface {

	public delegate void LoginSucHandler(string result);
	public delegate void ShareReponseHandler(string result);

	public delegate void LogoutHandler();

	private static WeChatSDKInterface _instance;

	public LoginSucHandler OnLoginSuc;


	public ShareReponseHandler OnShareShareReponse;

	public static WeChatSDKInterface Instance{
		
		get
		{
			if (_instance == null) {
				
				#if UNITY_EDITOR || UNITY_STANDLONE
				_instance = new WeChatSDKInterfaceDefault();
				#elif UNITY_ANDROID
				_instance = new WeChatSDKInterfaceAndroid();
				#elif UNITY_IOS
				_instance = new WeChatSDKInterfaceIOS();
				#endif

				}

				return _instance;
		}
	}

	//登录
	public abstract void Login();


	//分享，微信邀请好友
	public abstract void ShareToFriends(int type ,string title,string description,string urlorImageFile);

}


