using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStamina : UIGaugebar
{
    public PlayerCharacterStatus player;

    protected override void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterStatus>();
        }
        maxValue = player.Data.MaxStamina;
        currentValue = player.Stamina;
    }

    protected override void SetCurrentGaugeSetting()
    {
        currentValue = player.Stamina;
    }
}
