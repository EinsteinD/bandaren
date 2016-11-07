using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetSoundView : MonoBehaviour {
    public Slider BGM;
    public Slider Sounds;

    public Image BGM_button;
    public Image Sounds_button;

    public Sprite BGM_button_normal;
    public Sprite Sounds_button_normal;
    public Sprite BGM_button_hui;
    public Sprite Sounds_button_hui;
	// Use this for initialization
	void Start () {
	
	}
    void Awake()
    {
        BGM.value=SoundManage.GetInstan.BGM.volume;
        Sounds.value = SoundManage.GetInstan.Sounds[0].volume;
        onBGM_ValueChange();
        onSound_ValueChange();
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void onBGM_ValueChange()
    {
        if (BGM.value==0)
        {
            BGM_button.sprite = BGM_button_hui;
        }
        else
        {
            BGM_button.sprite = BGM_button_normal;
        }
        SoundManage.GetInstan.BGM.volume = BGM.value;
        PlayerPrefs.SetFloat("BGMVolume", SoundManage.GetInstan.BGM.volume);        
    }

    public void onSound_ValueChange()
    {
        if (Sounds.value==0)
        {
            Sounds_button.sprite = Sounds_button_hui;
        }
        else
        {
            Sounds_button.sprite = Sounds_button_normal;
        }
        for (int i = 0; i < SoundManage.GetInstan.Sounds.Length; i++)
        {
            SoundManage.GetInstan.Sounds[i].volume = Sounds.value;
            PlayerPrefs.SetFloat("SoundsVolume", SoundManage.GetInstan.Sounds[i].volume);
        }
    }

    public void BGM_Button()
    {
        if (BGM.value==0)
        {
            BGM.value = 1;
        }
        else
        {           
            BGM.value = 0;           
        }
        onBGM_ValueChange();
    }

    public void Sound_Button()
    {
        if (Sounds.value == 0)
        {
            Sounds.value = 1;
        }
        else
        {
            Sounds.value = 0;        
        }
        onSound_ValueChange();
    }

    public void close_button()
    {
        gameObject.SetActive(false);
    }
}
