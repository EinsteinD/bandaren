using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WeChatShare: MonoBehaviour
{
	
	public event System.Action OnDone;

	const string FILE_NAME = "Screenshot.png";

	void Start()
	{

	}

	// Update is called once per frame
	void Update(){

	}
	string FullFileName { get { return Application.persistentDataPath + "/" + FILE_NAME; } }


	public void shareImage(){
		
		StartCoroutine(StartScreenshotFlow());
	}

	IEnumerator StartScreenshotFlow(){

		yield return 0;
		yield return new WaitForEndOfFrame();
		
		TakeScreenshot(FullFileName);

		//WeChatSDKInterface.Instance.ShareImage(FullFileName);

		//DoCameraEffect();
	}
	
	void DoCameraEffect() {
		//Globals.MenuAudio.PlayTakeScreenshot ();
		
		// Do flash effect
		//UIOverlayController.Instance.ScreenFadeOverlay.SetColor(Color.white);
		//UIOverlayController.Instance.ScreenFadeOverlay.Fade(1f, 0f, 1f, OnDoneFading);
	}
	
	void OnDoneFading() {
		//		StartCoroutine(Waiter.DoWaitWithTimeout(
		//			() => !System.IO.File.Exists(FullFileName),
		//			onDone: () => {
		//			#if UNITY_IPHONE && !UNITY_EDITOR
		//			Globals.SocialManager.ShareImageorLink(FullFileName, "2",shareHighSocreImageCallBack);
		//			#else
		//			shareHighSocreImage();
		//			#endif
		//		}, 
		//		onTimeout: shareHighSocreImage,
		//		timeout: 10f
		//		));
	}


	void TakeScreenshot(string fullFileName){

		Debuger.Log("wxg*****:"+fullFileName);

		System.IO.File.Delete (fullFileName);
		
		//Create a texture to pass to encoding
		Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		
		//Put buffer into texture
		texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0); 

		byte[] bytes = texture.EncodeToPNG();
		
		//Save our test image (could also upload to WWW)
		System.IO.File.WriteAllBytes(fullFileName, bytes);
		
		DestroyObject(texture);
	}
}