﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPatternDelay : StateMachineBehaviour
{
    private AIMaster aiMaster;
    public string skillParameterName;
    public float delayTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool(skillParameterName))
        {
            Debug.LogError("애니메이터 파라미터를 찾을 수 없습니다.");
        }

        if (aiMaster == null)
        {
            aiMaster = animator.GetComponent<AIMaster>();
        }
        aiMaster.StartCoroutine(Delay(delayTime, animator));
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    IEnumerator Delay(float time, Animator animator)
    {
        animator.SetBool(skillParameterName, false);
        yield return new WaitForSeconds(time);
        animator.SetBool(skillParameterName, true);
    }
}