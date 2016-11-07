using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using KWX_From;
using KWX;
using UnityEngine.UI;
public class login : MonoBehaviour 
{
	public InputField zhanghao;
	public InputField mima;


	// Use this for initialization
	void Start () 
    {
#if (UNITY_ANDROID || UNITY_IPHONE) && (!UNITY_EDITOR)
        zhanghao.gameObject.SetActive(false);
#endif
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void denglu()
    {
        try
        {
            //GameManage.GetInstan.SetZhuanZhuan(GameObject.Find("Canvas").transform, "正在登陆游戏中...");

            SoundManage.GetInstan.onclick_Sound();
#if (UNITY_ANDROID || UNITY_IPHONE) && (!UNITY_EDITOR)
                
				WeChatSDKInterface.Instance.Login();
#else
            //测试登陆
            CMD_Login LOGINitem = new CMD_Login() { UserID = ulong.Parse(zhanghao.text), Password = "liuyu", WeiXinCode = "" };
            GameManage.GetInstan.LoginUserID = ulong.Parse(zhanghao.text);
            GameManage.GetInstan.LoginPassWord = mima.text;
            KWXFrom.GetIns.LoginCmd.SendLoginGame(LOGINitem, null);
            zhanghao.gameObject.SetActive(false);
#endif


        }
        catch (System.Exception ex)
        {
            Debuger.Log(ex.Message);
            GameManage.GetInstan.DisZhuanZhuan();
        }
    }
    
}
