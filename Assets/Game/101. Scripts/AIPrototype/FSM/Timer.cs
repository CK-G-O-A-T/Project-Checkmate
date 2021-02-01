﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : StateMachineBehaviour
{
    public float timer;
    private float saveTimer;
    private bool triggerCheckout;
    private AIMaster aiMaster;
    [Header("옵션")]
    public bool isPauseRecoveryHealthPoint = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.speed = 0;
        saveTimer = 0;
        triggerCheckout = false;
        if (aiMaster == null)
        {
            aiMaster = animator.GetComponent<AIMaster>();
        }
        if (isPauseRecoveryHealthPoint)
        {
            aiMaster.bossRecoveryHPComponent.StopRecovery();
        }
        aiMaster.AttackSequence();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        saveTimer += Time.deltaTime;
        //aiMaster.SetAngleToPlayer(2);
        if (saveTimer >= timer && !triggerCheckout)
        {
            animator.SetTrigger("timerTrigger");
            triggerCheckout = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isPauseRecoveryHealthPoint)
        {
            aiMaster.bossRecoveryHPComponent.StartRecovery();
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
