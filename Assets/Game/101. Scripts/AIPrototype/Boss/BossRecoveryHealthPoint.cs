﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossDamageHandler))]
public class BossRecoveryHealthPoint : MonoBehaviour
{
    private AIMaster aiMaster;
    private BossDamageHandler bossDamageHandler;

    [Tooltip("기다릴 시간을 설정합니다.(초 단위)")]
    public float standbyTime;
    [Tooltip("초당 회복할 체력을 설정합니다.")]
    public float recoveryHealthPoint;

    private float backupStandbyTime;
    private float oneSecond = 1f;
    private bool isActive = true;

    private void Awake()
    {
        if (aiMaster == null)
        {
            aiMaster = GetComponent<AIMaster>();
        }
        backupStandbyTime = standbyTime;
        bossDamageHandler = aiMaster.GetComponent<BossDamageHandler>();
        bossDamageHandler.damageEvent.AddListener(InitStandbyTime);
    }

    private void Update()
    {
        if (isActive)
        {
            if (backupStandbyTime > 0)
            {
                backupStandbyTime -= Time.deltaTime;
            }
            else
            {
                oneSecond -= Time.deltaTime;
                if (oneSecond <= 0 && aiMaster.currentHealthPoint < aiMaster.maxHealthPoint)
                {
                    aiMaster.currentHealthPoint += recoveryHealthPoint;
                    oneSecond = 1f;
                }
            }
        }
    }

    public void InitStandbyTime()
    {
        backupStandbyTime = standbyTime;
        oneSecond = 1f;
    }

    public void StopRecovery()
    {
        isActive = false;
        InitStandbyTime();
    }

    public void StartRecovery()
    {
        isActive = true;
    }
}
