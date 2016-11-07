using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using KWX;
using KWX_From;


	public class UIDismissRoom : MonoBehaviour
	{

		public Text mainContent;


        public DismissRoomPlayer[] player;

		public Text textCountdownTime;
		private float leftTime = 10 * 60f;


		public Button btnAgree;


		public Button btnReject;

		private static UIDismissRoom mInst;

		public static UIDismissRoom Inst{ get { return mInst; } }

		void Awake ()
		{
			mInst = this;
		}

		// Use this for initialization
		void Start ()
		{
	
		}

		

	
		// Update is called once per frame
		void Update ()
		{
			setLeftTime ();

		}

		void setLeftTime ()
		{
            if (leftTime>0)
            {
                leftTime -= Time.deltaTime;
                int min = (int)(leftTime / 60);
                int second = (int)(leftTime % 60);
                string minStr = min >= 10 ? min.ToString() : "0" + min;
                string secondStr = second >= 10 ? second.ToString() : "0" + second;
                textCountdownTime.text = "(" + minStr + ":" + second + ")";
            }
            else
            {
                //dismissUI();
            }
			
		}


        public void agree()
        {
            CMD_Dismiss result = new CMD_Dismiss();
            result.Action = 2;
            KWXFrom.GetIns.GameCmd.SendACTION_SendDismiss(result);
        }

        public void disagree()
        {
            CMD_Dismiss result = new CMD_Dismiss();
            result.Action = 3;
            KWXFrom.GetIns.GameCmd.SendACTION_SendDismiss(result);
        }
	

		void setCountdownTime (float countdownTime)
		{
			leftTime = countdownTime;
		}

	
	}

