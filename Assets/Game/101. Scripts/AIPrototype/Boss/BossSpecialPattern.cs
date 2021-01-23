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
    public float activeHealthPercent;
    public bool isUse = false;
    public bool isActive = true;
    public float activeHealthPercentConvertToHealthPoint;
}

public class BossSpecialPattern : MonoBehaviour
{
    public List<pattern> patterns;
    private AIMaster aiMaster;

    public pattern currentActivePattern = null;

    private int index;

    private void Awake()
    {
        //patterns = patterns.OrderBy(x => x.activeHealthPoint).ToList();
        aiMaster = GetComponent<AIMaster>();
        currentActivePattern = null;

        for (int i = 0; i < patterns.Count; i++)
        {
            patterns[i].isActive = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DeadlyPatternCooldown(1.5f));
        for (int i = 0; i < patterns.Count; i++)
        {
            patterns[i].activeHealthPercentConvertToHealthPoint = aiMaster.currentHealthPoint * (patterns[i].activeHealthPercent / 100);
        }
        index = patterns.Count - 1;
    }

    private void Update()
    {
        // 이거 업데이트 돌리면 안됨!
        // 나중에 데미지 받을 때만 호출 하도록 수정할 것
        CheckDeadlyPattern();
    }

    public void CheckDeadlyPattern()
    {
        if (patterns[index].activeHealthPercentConvertToHealthPoint >= aiMaster.currentHealthPoint)
        {
            if (index - 1 >= 0 && (patterns[index - 1].activeHealthPercentConvertToHealthPoint >= aiMaster.currentHealthPoint))
            {
                patterns[index].isActive = false;
                index--;
            }
            patterns[index].isActive = true;
            currentActivePattern = patterns[index];
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