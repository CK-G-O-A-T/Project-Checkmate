﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSwitchPoint : UIGaugebar
{
    public PlayerCharacterStatus player;

    protected override void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterStatus>();
        }
        maxValue = player.Data.MaxSwitchingPoint;
        currentValue = player.SwitchPoint;
    }

    protected override void SetCurrentGaugeSetting()
    {
        currentValue = player.SwitchPoint;
    }
}
