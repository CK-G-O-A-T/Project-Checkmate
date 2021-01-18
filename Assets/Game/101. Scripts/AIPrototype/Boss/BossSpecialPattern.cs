using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class pattern
{
    [Tooltip("초로 계산됩니다.")]
    public float time;
    [Tooltip("HP가 일정 퍼센트일 때 작동합니다.")]
    public float activeHealthPoint;
    public bool isUse = false;
    public bool isReady = true;
    public float activeHealthPointToFloat;
}

public class BossSpecialPattern : MonoBehaviour
{
    public List<pattern> patterns;
    private AIMaster aiMaster;

    public pattern temp = null;

    private int index;

    private void Awake()
    {
        //patterns = patterns.OrderBy(x => x.activeHealthPoint).ToList();
        aiMaster = GetComponent<AIMaster>();
        temp = null;
    }

    private void Start()
    {
        StartCoroutine(DeadlyPatternCooldown(1.5f));
        for (int i = 0; i < patterns.Count; i++)
        {
            patterns[i].activeHealthPointToFloat = aiMaster.healthPoint * (patterns[i].activeHealthPoint / 100);
        }
        index = patterns.Count - 1;
    }

    private void Update()
    {
        CheckDeadlyPattern();
    }

    public void CheckDeadlyPattern()
    {
        if (patterns[index].activeHealthPointToFloat >= aiMaster.healthPoint)
        {
            if (index - 1 >= 0 && (patterns[index - 1].activeHealthPointToFloat >= aiMaster.healthPoint))
            {
                patterns[index].isReady = false;
                index--;
                patterns[index].isReady = true;
            }
            temp = patterns[index];
        }
    }

    public IEnumerator DeadlyPatternCooldown(float initTime)
    {
        float time;

        for (time = 0; time <= initTime; time += Time.deltaTime)
        {
            //Debug.Log(time);
            yield return null;
        }

        Debug.Log("Ready for Skill");
    }
}