using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using KWX_From;
using KWX;

    public class UIMessageBox : MonoBehaviour
    {
        public delegate void CallBack(UIMessageBox sender);

        public Text titleText;
        public Text msgText;
        public Button yesButton;
        public Text yesButtonText;
        public Button noButton;
        public Text noButtonText;
        public Button cancelButton;

        /// <summary>
        /// 管理器
        /// </summary>
        public UIMessageBoxManager manager { get; set; }

        /// <summary>
        /// message box 风格定义
        /// </summary>
        public enum Style
        {
            // 只有一个确定按钮
            Yes,    

            // 有两个按钮，yes或者no
            YesNo,

            // 确定和cancel
            YesCancel,

            // 有三个按钮,yes\no\cancel
            YesNoCancel,
        }

        public enum ButtonId
        {
            Yes,
            No,
        }

        public enum Result
        {
            Pending,
            Yes,
            No,
            Cancel,
        }

        /// <summary>
        /// 结果
        /// </summary>
        Result mResult = Result.Pending;

        public Result result {get {return mResult;}}

        public static UIMessageBox Show(Style style, CallBack callBack)
        {
            return null;
        }

        /// <summary>
        /// 风格
        /// </summary>
        public Style style
        {
            get
            {
                return mStyle;
            }

            set
            {
                mStyle = value;
                //RefreshLayout();
            }
        }
        Style mStyle = Style.YesNo;


        /// <summary>
        /// 按钮标题
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        public void SetButtonText(ButtonId id, string text)
        {
            switch (id)
            {
                case ButtonId.Yes:
                    if (null != yesButtonText && null != text)
                    {
                        yesButtonText.text = text;
                    }
                    break;
                case ButtonId.No:
                    if (null != noButtonText && null != text)
                    {
                        noButtonText.text = text;
                    }
                    break;
                default:
                    Debuger.LogWarning("SetButtonText -- button_id = " + id + " is illegal.");
                    break;
            }
        }

        /// <summary>
        /// caption
        /// </summary>
        public string caption
        {
            get
            {
                if (null != titleText)
                {
                    return titleText.text;
                }

                return string.Empty;
            }

            set
            {
                if (null != titleText)
                {
                    titleText.text = value;
                }
            }
        }

        public string msg
        {
            get
            {
                if (null != msgText)
                {
                    return msgText.text;
                }

                return string.Empty;
            }

            set
            {
                if (null != msgText)
                {
                    msgText.text = value;
                }
            }
        }

        public CallBack callBack
        {
            set
            {
                mCallBack = value;
            }
        }

        CallBack mCallBack;

        /// <summary>
        /// 更新界面布局
        /// </summary>
        public void RefreshLayout()
        {
            bool showYesButton = false;
            bool showNoButton = false;
            bool showCancelButton = false;

            switch (style)
            {
                case Style.Yes:
                    showYesButton = true;
                    break;
                case Style.YesNo:
                    showYesButton = true;
                    showNoButton = true;
                    break;
                case Style.YesCancel:
                    showYesButton = true;
                    showCancelButton = true;
                    break;
                case Style.YesNoCancel:
                    showYesButton = true;
                    showNoButton = true;
                    showCancelButton = true;
                    break;
                default:
                    break;
            }

            if (null != yesButton)
            {
                yesButton.gameObject.SetActive(showYesButton);
            }

            if (null != noButton)
            {
                noButton.gameObject.SetActive(showNoButton);
            }

            if (null != cancelButton)
            {
                cancelButton.gameObject.SetActive(showCancelButton);
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnYes()
        {
            mResult = Result.Yes;

            if (null != mCallBack)
            {
                mCallBack(this);
            }

            Close();
        }

        public void OnNo()
        {
            mResult = Result.No;

            if (null != mCallBack)
            {
                mCallBack(this);
            }

            Close();
        }

        public void OnCancel()
        {
            mResult = Result.Cancel;

            if (null != mCallBack)
            {
                mCallBack(this);
            }

            Close();
        }

        void Close()
        {
            manager.DestroyMessageBox(this);
            //gameObject.SetActive(false);
        }

        public void Show()
        {
            RefreshLayout();
            gameObject.SetActive(true);
        }

        #region 便利的创建接口
        
        /// <summary>
        ///     只提供信息显示功能和一个关闭按钮.
        /// </summary>
        public static UIMessageBox ShowMessageBox(string caption, string msg, string yesText = null)
        {
            UIMessageBox msgBox = UIMessageBoxManager.Inst.CreateMessageBox();

            if (null != msgBox)
            {
                msgBox.caption = caption;
                msgBox.msg = msg;
                msgBox.style = Style.Yes;

                if (null != yesText)
                {
                    msgBox.SetButtonText(ButtonId.Yes, yesText);
                }

                msgBox.Show();

                return msgBox;
            }

            return null;
        }

        /// <summary>
        ///     只提供信息显示功能和yes/no选项
        /// </summary>
        public static UIMessageBox ShowMessageBox(string caption, string msg, 
            string yesText, string noText, CallBack callBack)
        {
            UIMessageBox msgBox = UIMessageBoxManager.Inst.CreateMessageBox();

            if (null != msgBox)
            {
                msgBox.caption = caption;
                msgBox.msg = msg;
                msgBox.style = Style.YesNo;
                msgBox.SetButtonText(ButtonId.Yes, yesText);
                msgBox.SetButtonText(ButtonId.No, noText);
                msgBox.callBack = callBack;

                msgBox.Show();

                return msgBox;
            }

            return null;
        }

        #endregion
    }

