using UnityEngine;
using System.Collections;
using KWX_From;
using KWX;

    public class UIMessageBoxManager : MonoBehaviour
    {
        public GameObject messageBoxPrefab;
        public GameObject canvasObject;

        /// <summary>
        /// 单例
        /// </summary>
        static UIMessageBoxManager mInst;
        public static UIMessageBoxManager Inst { get { return mInst; } }

        UIMessageBoxManager()
        {
            mInst = this;
        }

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public UIMessageBox CreateMessageBox()
        {
            if (null != messageBoxPrefab && null != canvasObject)
            {
                GameObject messageBoxObject = Object.Instantiate<GameObject>(messageBoxPrefab);

                if (null != messageBoxObject)
                {
                    messageBoxObject.SetActive(false);

                    messageBoxObject.transform.position = Vector3.zero;
                    messageBoxObject.transform.SetParent(canvasObject.transform, false);

                    UIMessageBox msgBox = messageBoxObject.GetComponent<UIMessageBox>();

                    if (null != msgBox)
                    {
                        msgBox.manager = this;
                    }

                    return msgBox;
                }
            }

            return null;
        }

        public void DestroyMessageBox(UIMessageBox messageBox)
        {
            if (null != messageBox)
            {
                GameObject.Destroy(messageBox.gameObject);
            }
        }
    }


