﻿using System.Collections;
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
        aiMaster.healthPoint -= (float)damageData.Damage;
        aiMaster.groggyComponent.groggy += (float)damageData.GroggyPoint;
        DPSFunction(damageData.Damage);
        if (!aiMaster.isDead && aiMaster.healthPoint <= 0)
        {
            SetDeadTrigger();
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
