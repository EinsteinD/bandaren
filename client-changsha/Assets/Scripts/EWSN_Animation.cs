using UnityEngine;
using System.Collections;

public class EWSN_Animation : MonoBehaviour {
    public Material AniMaterial;
    float Time_Jiange = 0.001f;
    float Time_ = 0;
    bool Xian_Ying = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ((Time_+=Time.deltaTime)>Time_Jiange)
        {
            Time_ = 0;
            if (AniMaterial.color.a >= 1)
            {
                Xian_Ying = false;
            }
            else if (AniMaterial.color.a <= 0.3f)
            {
                Xian_Ying = true;
            }
            if (Xian_Ying)
            {
                AniMaterial.color = new Color(AniMaterial.color.r, AniMaterial.color.g, AniMaterial.color.b, AniMaterial.color.a + 0.03f);
            }
            else
            {
                AniMaterial.color = new Color(AniMaterial.color.r, AniMaterial.color.g, AniMaterial.color.b, AniMaterial.color.a - 0.03f);
            }
        }
	}
}
