using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class WeChatSDKInterfaceIOS:WeChatSDKInterface
{
	public enum WXScene:int {
		WXSceneSession,        /**< 聊天界面 */
		WXSceneTimeline,       /**< 朋友圈 */
		WXSceneFavorite,       /**< 收藏 */
	};

	#if UNITY_IPHONE

	[DllImport ("__Internal")]
	private static extern void _WXLogin();

	[DllImport ("__Internal")]
	private static extern void _WXShareTextToFriends(int scene, string text);

	[DllImport ("__Internal")]
	private static extern void _WXShareWebpageMessageToFriends(int scene, string title, string desc, string url);

	[DllImport ("__Internal")]
	private static extern void _WXShareImageMessageToFriends(int scene, string title, string desc, string url);

	#endif

	public WeChatSDKInterfaceIOS ()
	{
			
	}

	public override void Login() {
		#if UNITY_IPHONE
		_WXLogin();
		#endif
	}

	public override void ShareToFriends(int type ,string title, string description,string urlOrImageFile) 
	{
		#if UNITY_IPHONE
		// WXSceneSession = 0
		//发送的目标场景，表示发送到会话
		if(type == 1){

			_WXShareWebpageMessageToFriends(0,title,description,urlOrImageFile);
		}

		if(type == 2){

			_WXShareImageMessageToFriends(0,title,description,urlOrImageFile);
		}

		//_WXShareTextToFriends((int)(WXScene.WXSceneSession), "多米棋牌房间$$1234567$$，复制此段文字后进入多米棋牌开始游戏，下载地址：http://baidu.com");
		#endif
	}
}


