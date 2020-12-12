using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivate : MonoBehaviour
{
    public AIMaster aiMaster;
    // Start is called before the first frame update
    void Start()
    {
        if (aiMaster == null)
        {
            aiMaster = GetComponent<AIMaster>();
        }
    }

    public void BossStart()
    {
        aiMaster.anim.SetTrigger("gameStart");
        aiMaster.isFirstStrike = true;
    }
}
