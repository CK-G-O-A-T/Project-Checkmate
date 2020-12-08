using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActionPoint : UIHPBaseClass
{
    public PlayerCharacterStatus player;

    protected override void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterStatus>();
        }
        maxHp = player.Stamina;
        currentHp = maxHp;
    }

    protected override void SetCurrentHpBarSetting()
    {
        currentHp = player.Stamina;
    }
}
