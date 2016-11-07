using UnityEngine;
using System.Collections;

public class desk_scr : MonoBehaviour {

    public void PaiCaoGuanBi()
    {
        
    }
    public void ShengPai()
    {
        for (int s = 0; s < CreatCard.GetInstance_.IDX_Fapai; s++)
        {
            CreatCard.GetInstance_.AllMaJiang[s].GetComponent<Animator>().enabled = false;
            CreatCard.GetInstance_.AllMaJiang[s].GetComponent<card_info>().Isfapai = true;
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
