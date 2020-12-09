using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBossHP : UIGaugebar
{
    public AIMaster aiMaster;
    private BossDamageHandler damageEvent;

    protected override void Start()
    {
        maxValue = aiMaster.healthPoint;
        currentValue = maxValue;

        damageEvent = aiMaster.GetComponent<BossDamageHandler>();
        damageEvent.damageEvent.AddListener(UpdateGauge);
    }

    protected override void SetCurrentGaugeSetting()
    {
        currentValue = aiMaster.healthPoint;
    }
}
