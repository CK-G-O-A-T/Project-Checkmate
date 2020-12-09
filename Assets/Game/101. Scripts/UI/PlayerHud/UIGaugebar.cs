using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIGaugebar : MonoBehaviour
{
    public Image mainGaugeBar;
    public Image subGaugeBar;
    public float decreaseSpeed;

    protected double currentValue;
    protected double maxValue;

    // Start is called before the first frame update
    protected abstract void Start();

    public void UpdateGauge()
    {
        SetCurrentGaugeSetting();
        mainGaugeBar.fillAmount = (float)(currentValue / maxValue);
        StartCoroutine(LerpSubGauge());
    }

    IEnumerator LerpSubGauge()
    {
        while (Mathf.Abs(mainGaugeBar.fillAmount - subGaugeBar.fillAmount) > 0.001f)
        {
            subGaugeBar.fillAmount = Mathf.Lerp(subGaugeBar.fillAmount, mainGaugeBar.fillAmount, Time.deltaTime * decreaseSpeed);
            yield return null;
        }
    }

    protected abstract void SetCurrentGaugeSetting();
}
