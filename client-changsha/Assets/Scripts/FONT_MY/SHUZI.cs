using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SHUZI : MonoBehaviour {
    public Sprite[] ZERO_NINE;
    public GameObject ShuZiYuZhiTi;
    public List<shuziZhi> WenBenMeiShu;
    int TimeNum=15;
    bool B_TimeOff = true;
    public void SetTime_(int Num,bool TimeOff=true)
    {
        TimeNum = Num;
        B_TimeOff = TimeOff;
    }
    IEnumerator ie_;
    public void Awake()
    {
        ie_ = AsyInwoke();
    }
	// Use this for initialization
    public void StartTIME() 
    {
        StopCoroutine(ie_);
        TimeNum = 15;
        CreatMeiShuFontS(TimeNum.ToString());
        StartCoroutine(ie_);
	}
    IEnumerator AsyInwoke()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (TimeNum<=0)
            {
                TimeNum = 0;
            }
            if (TimeNum<10)
            {
                CreatMeiShuFontS("0"+TimeNum.ToString());
            }
            else
            {
                CreatMeiShuFontS(TimeNum.ToString());
            }
            TimeNum--;
        }
    }
	public void CreatMeiShuFontS(string str_nub)
    {
        for (int i = 0; i < WenBenMeiShu.Count; i++)
        {
            Destroy(WenBenMeiShu[i].gameObject);
        }
        WenBenMeiShu.Clear();

        char[] charArray = str_nub.ToCharArray();
        int Temp = 0;
        for (int i = 0; i < charArray.Length; i++)
        {
            Temp = int.Parse(charArray[i].ToString());

            shuziZhi Value_m = Instantiate(ShuZiYuZhiTi).GetComponent<shuziZhi>();
            Value_m.transform.SetParent(transform);
            Value_m.transform.localPosition = Vector3.zero;
            Value_m.transform.localScale = Vector3.one;
            Value_m.transform.localEulerAngles = Vector3.zero;
            Value_m.sp.sprite = ZERO_NINE[Temp];
            Value_m.gameObject.SetActive(true);
            WenBenMeiShu.Add(Value_m);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
