using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHP : UIGaugebar
{
    public PlayerCharacterStatus player;

    protected override void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterStatus>();
        }
        maxValue = player.Hp;
        currentValue = maxValue;
    }

    protected override void SetCurrentGaugeSetting()
    {
        currentValue = player.Hp;
    }
}
