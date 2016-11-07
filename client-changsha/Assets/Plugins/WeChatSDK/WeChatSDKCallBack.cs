using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

/// <summary>
/// 微信sdk回调. code by wangxinggang
/// </summary>
public class WeChatSDKCallBack : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void OnLoginSuc(string jsonData){

		Debuger.Log("OnLoginSuc"+jsonData);

		if( WeChatSDKInterface.Instance.OnLoginSuc != null){
			
			WeChatSDKInterface.Instance.OnLoginSuc(jsonData);
		}
		else{


		}
	}


	public void OnShareReponse(string result){

		if(WeChatSDKInterface.Instance.OnShareShareReponse !=null){

			WeChatSDKInterface.Instance.OnShareShareReponse(result);
		}
	}


	// iOS callback OnWeChatSuccessfulLogin
	public void OnWeChatSuccessfulLogin(string jsonData) {
		object obj = Json.Deserialize(jsonData);
		if (obj is IDictionary) {
			IDictionary<string, object> json = (IDictionary<string, object>)obj;
			Dictionary<string, object> newJson = new Dictionary<string, object> ();
			newJson.Add ("result", json ["errCode"]);
			newJson.Add ("token", json ["code"]);
			this.OnLoginSuc (Json.Serialize (newJson));
		} else {
			this.OnLoginSuc (null);
		}
	}

	// iOS callback OnWeChatSuccessfulShared
	public void OnWeChatSuccessfulShared(string jsonData) {

		object obj = Json.Deserialize(jsonData);
		if (obj is IDictionary) {
			IDictionary<string, object> json = (IDictionary<string, object>)obj;
			Dictionary<string, object> newJson = new Dictionary<string, object> ();
			newJson.Add ("errCode", json ["errCode"]);
			this.OnShareReponse(Json.Serialize (newJson));
		} else {
			this.OnShareReponse (null);
		}
	}

}

