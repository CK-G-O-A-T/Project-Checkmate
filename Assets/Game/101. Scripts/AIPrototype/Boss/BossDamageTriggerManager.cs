﻿// 마지막 Element는 무조건 Growl
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageTriggerManager : MonoBehaviour
{
    public BossWeaponDamageTrigger[] damageTriggers;
    public BossGrowlDamageTrigger growlTrigger;
    public BossWeaponDamageTrigger wideAreaTrigger;
    public double setAttackDamage;

    public void DamageTrigger_StartTrigger()
    {
        for (int i = 0; i < damageTriggers.Length; i++)
        {
            damageTriggers[i].damage = setAttackDamage;
            damageTriggers[i].StartTrigger();
        }
    }

    public void DamageTrigger_StartIndexTrigger(int index)
    {
        damageTriggers[index].damage = setAttackDamage;
        damageTriggers[index].StartTrigger();
    }

    public void DamageTrigger_EndTrigger()
    {
        for (int i = 0; i < damageTriggers.Length; i++)
        {
            damageTriggers[i].EndTrigger();
        }
    }

    public void GrowlTrigger_StartTrigger()
    {
        growlTrigger.damage = setAttackDamage;
        growlTrigger.StartTrigger();
    }

    public void GrowlTrigger_EndTrigger()
    {
        growlTrigger.EndTrigger();
    }

    public void WideAreaTrigger_StartTrigger()
    {
        wideAreaTrigger.damage = setAttackDamage;
        wideAreaTrigger.StartTrigger();
    }

    public void WideAreaTrigger_EndTrigger()
    {
        wideAreaTrigger.EndTrigger();
    }
}
