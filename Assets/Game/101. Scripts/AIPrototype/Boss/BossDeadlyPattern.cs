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
    public bool isReady = false;
    public float activeHealthPointToFloat;
}

public class BossDeadlyPattern : MonoBehaviour
{
    public List<pattern> patterns;
    private AIMaster aiMaster;

    public pattern test;

    private void Awake()
    {
        //patterns = patterns.OrderBy(x => x.activeHealthPoint).ToList();
        aiMaster = GetComponent<AIMaster>();
    }

    private void Start()
    {
        StartCoroutine(DeadlyPatternCooldown(1.5f));
        for (int i = 0; i < patterns.Count; i++)
        {
            patterns[i].activeHealthPointToFloat = aiMaster.healthPoint * (patterns[i].activeHealthPoint / 100);
        }
    }

    private void Update()
    {
        CheckDeadlyPattern();
    }

    public void CheckDeadlyPattern()
    {
        float priorityPattern = patterns[patterns.Count - 1].activeHealthPoint;

        for (int i = patterns.Count - 1; i >= 0; i--)
        {
            // ㅅㅂ
            float percentage = patterns[i].activeHealthPoint / 100;
            if (aiMaster.maxHealthPoint * percentage >= aiMaster.healthPoint)
            {
                test = patterns[i];
            }
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