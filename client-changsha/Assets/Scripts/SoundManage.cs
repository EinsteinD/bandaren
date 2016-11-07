using UnityEngine;
using System.Collections;

public class SoundManage : MonoBehaviour 
{
    public AudioSource BGM;//背景声源
    public AudioSource[] Sounds; //效果音音源


    public AudioClip[] BGM_Clip;//背景音乐

    public AudioClip[] pai_Clip_nan;    //叫牌的声音
    public AudioClip[] pai_Clip_nv;     //叫牌的声音


    public AudioClip[] CPGHL_Clip_nan;      //吃碰杠胡亮自摸的声音
    public AudioClip[] CPGHL_Clip_nv;       //吃碰杠胡亮自摸的声音


    public AudioClip win_Clip;      //输赢的声音
    public AudioClip lose_Clip;     //输赢的声音
    

    public AudioClip[] duiHua_Clip_man;   //对话的声音
    public AudioClip[] duiHua_Clip_nv;    //对话的声音


    public AudioClip button_Clip;//点击button的声音
    public AudioClip warm_Clip;//警告的声音


    private static SoundManage inst;
    public static SoundManage GetInstan
    {
        get
        {
            return inst;
        }
    }
    void Awake()
    {
        inst = this;
        DontDestroyOnLoad(this);
        INIT();
        
    }

    /// <summary>
    /// 初始化音源
    /// </summary>
    void INIT()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            BGM.volume = PlayerPrefs.GetFloat("BGMVolume");

        }
        else
        {
            BGM.volume = 1;
            
        }
        if (PlayerPrefs.HasKey("SoundsVolume"))
        {
            for (int i = 0; i < Sounds.Length; i++)
            {
                Sounds[i].volume = PlayerPrefs.GetFloat("SoundsVolume");
            }
        }
        else
        {
            for (int i = 0; i < Sounds.Length; i++)
            {
                Sounds[i].volume = 1;
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 打开或关闭BGM
    /// </summary>
    /// <param name="openOrClose">true为打开</param>
    public void openOrCloseBGM(bool openOrClose)
    {
        BGM.mute = !openOrClose;
    }

    /// <summary>
    /// 打开或关闭音效
    /// </summary>
    /// <param name="openOrClose">true为打开</param>
    public void openOrCloseSounds(bool openOrClose)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].mute = !openOrClose;
        }        
    }

    /// <summary>
    /// 设置背景音乐的大小
    /// </summary>
    /// <param name="value">取值范围0到1</param>
    public void setBGM_volume(float value)
    {
        BGM.volume = value;
    }

    /// <summary>
    /// 设置背景音乐的大小
    /// </summary>
    /// <param name="value">取值范围0到1</param>
    public void setSounds_volume(float value)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].volume = value;
        }       
    }

    /// <summary>
    /// 寻找空闲的音源
    /// </summary>
    /// <returns></returns>
    AudioSource findFreeAudioSource() 
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (!Sounds[i].isPlaying)
            {
                return Sounds[i];
            }
        }
        return Sounds[0];
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">播放的片段</param>
    public void playSound(AudioClip clip) 
    {
        findFreeAudioSource().PlayOneShot(clip);
    }

    /// <summary>
    /// button点击的声音
    /// </summary>
    public void onclick_Sound()
    {
        playSound(button_Clip);
    }

    /// <summary>
    /// 播放吃碰杠胡亮自摸的声音
    /// </summary>
    /// <param name="sex">性别</param>
    /// <param name="TX_">吃碰杠胡亮的枚举</param>
    public void playCPGHL_Sound(int sex, TeXiao TX_)
    {
        if (sex==0)
        {
            playSound(CPGHL_Clip_nv[(int)TX_]);
        }
        else
        {
            playSound(CPGHL_Clip_nan[(int)TX_]);
        }
    }

    ///// <summary>
    ///// 播放自摸的声音
    ///// </summary>
    ///// <param name="sex">性别</param>
    //public void playZiMo_Sound(int sex) 
    //{
    //    if (sex == 0)
    //    {
    //        playSound(ziMo_Clip_nv);
    //    }
    //    else
    //    {
    //        playSound(ziMo_Clip_nan);
    //    }
    //}

    /// <summary>
    /// 播放叫牌的声音
    /// </summary>
    /// <param name="sex">性别</param>
    /// <param name="value">牌的值</param>
    public void playPAI_Sound(int sex, int value)
    {
        if (sex==0)
        {
            playSound(pai_Clip_nv[value]);
        }
        else
        {
            playSound(pai_Clip_nan[value]);
        }
    }

    /// <summary>
    /// 播放输赢的音效
    /// </summary>
    /// <param name="WinLose">true为赢，反之输</param>
    public void playWinLose_Sound( bool WinLose)
    {
        pauseBGM();
        if (WinLose)
        {
            playSound(win_Clip);
        }
        else
        {
            playSound(lose_Clip);
        }
                 
    }

    /// <summary>
    /// 播放对话的声音
    /// </summary>
    /// <param name="sex">性别</param>
    /// <param name="index">索引</param>
    public void playDuiHua_Sound(int sex, int index)
    {
        if (sex == 0)
        {
            playSound(duiHua_Clip_nv[index]);
        }
        else
        {
            playSound(duiHua_Clip_man[index]);
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public void playBGM() 
    {
        BGM.clip = BGM_Clip[Random.Range(0,2)];
        BGM.Play();
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void pauseBGM()
    {
        if (BGM != null)
        {
            BGM.Pause();
        }
    }
}
