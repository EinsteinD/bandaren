using UnityEngine;
using System.Collections;

public class card_info : MonoBehaviour {
    public MeshRenderer meshr;
	public int cardValu;
    public GameObject ZhiShiQi;
    public int YiDingYaoLiang = 0;//一定要亮

    public bool Isfapai = false;
    float Speed = 0.001f;
    float Time_ = 0;
	// Use this for initialization
	void Start () {
	
	}
	public void SetZhiShiQiTrue()
    {
        ZhiShiQi.SetActive(true);
    }
    public void ShengPaiJieShu()
    {
    }
	// Update is called once per frame
	void Update () {
        if (Isfapai)
        {
            if ((Time_ += Time.deltaTime) > Speed)
            {
                Time_ = 0;
                transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, Vector3.zero, 3 * Time.deltaTime);
            }
            if (Vector3.Distance(transform.GetChild(0).localPosition, Vector3.zero) < 0.0002f)
            {
                transform.GetChild(0).localPosition = Vector3.zero;
                GetComponent<Animator>().enabled = false;
                Isfapai = false;
                if (CreatCard.GetInstance_.IsFaPai)
                {
                    CreatCard.GetInstance_.IsFaPai = false;
                    CreatCard.GetInstance_.MaJiangZhuo.GetComponent<Animator>().Play("MaJiangZhuo_GuanBi");
                }
            }
        }
	}
}
