using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossDamageHandler : DamageHandler
{
    [SerializeField]
    private AIMaster aiMaster;
    public UnityEvent damageEvent;
    public UnityEvent dieEvent;

    public double damagePerSecond = 0f;
    public bool isCheck = false;

    public float timer = 0f;

    private void Awake()
    {
        if (aiMaster == null)
        {
            aiMaster = GetComponent<AIMaster>();
        }
    }

    public override void DamageHandle(DamageData damageData)
    {
        Debug.Log($"Boss Damage Handle : {damageData}");
        aiMaster.currentHealthPoint -= (float)damageData.Damage;

        if (aiMaster.currentHealthPoint <= 0)
        {
            aiMaster.currentHealthPoint = 0f;
        }

        // aiMaster.groggyComponent.groggy += (float)damageData.GroggyPoint;
        DPSFunction(damageData.Damage);
        if (!aiMaster.isDead && aiMaster.currentHealthPoint <= 0)
        {
            if (aiMaster.anim.GetBool("deadReady"))
            {
                aiMaster.isDead = true;
                aiMaster.anim.SetBool("isDead", aiMaster.isDead);
            }

            // 나중에 기술 생기면 함수 실행 순서를 바꿔줘야 함
            if (aiMaster.isGroggy)
            {
                aiMaster.currentHealthPoint = aiMaster.maxHealthPoint;
                aiMaster.anim.SetTrigger("isCritical");
                aiMaster.isGroggy = false;
                return;
            }

            if (aiMaster.souls >= 0 && !aiMaster.isGroggy)
            {
                aiMaster.isGroggy = true;
                aiMaster.anim.SetTrigger("isGroggy");
            }
        }
        damageEvent.Invoke();
    }

    private void DPSFunction(double damage)
    {
        timer = 0f;

        damagePerSecond += damage;
        if (damagePerSecond >= 75f && aiMaster.anim.GetInteger("Phase") == 2)
        {
            aiMaster.SetBool("isEvade");
        }
        if (!isCheck)
        {
            StartCoroutine(DPSChecker(damage));
        }
    }

    IEnumerator DPSChecker(double damage)
    {
        isCheck = true;

        while (timer >= 0)
        {
            if (timer >= 1.5f)
            {
                timer = 0.0f;
                damagePerSecond = 0f;
                isCheck = false;
                break;
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void SetDeadTrigger()
    {
        aiMaster.anim.SetBool("isDead", true);
        aiMaster.isDead = true;
        dieEvent.Invoke();
    }
}
