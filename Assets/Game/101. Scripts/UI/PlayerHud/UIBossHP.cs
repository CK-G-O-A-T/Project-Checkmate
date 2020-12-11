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

        StartCoroutine(LerpMainGauge());
    }

    protected override void SetCurrentGaugeSetting()
    {
        currentValue = aiMaster.healthPoint;
    }

    IEnumerator LerpMainGauge()
    {
        while (mainGaugeBar.fillAmount < 0.999f)
        {
            //mainGaugeBar.fillAmount = Mathf.Lerp(mainGaugeBar.fillAmount, 1, Time.deltaTime * 0.7f);
            mainGaugeBar.fillAmount += 0.2f * Time.deltaTime;
            yield return null;
        }
    }
}
