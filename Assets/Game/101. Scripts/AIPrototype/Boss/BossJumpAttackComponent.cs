using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpAttackComponent : MonoBehaviour
{
    private AIMaster aiMaster;

    public Vector3 jumpTarget;
    public float speed = 50f;
    public bool checkArrive = false;

    private void Start()
    {
        aiMaster = GetComponent<AIMaster>();
    }

    public void StartJumpToTarget()
    {
        jumpTarget = aiMaster.getPlayer.transform.position;
        jumpTarget.y = 0f;
        speed = 50f;
        checkArrive = false;
    }

    public void UpdataJumpToTarget()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.y = 0f;
        if (Vector3.Distance(pos, jumpTarget) <= 15f)
        {
            checkArrive = true;
        }
        if (checkArrive)
        {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime * 4f);
            if (speed <= 10f)
            {
                aiMaster.anim.SetTrigger("timerTrigger");
            }
        }
    }
}
