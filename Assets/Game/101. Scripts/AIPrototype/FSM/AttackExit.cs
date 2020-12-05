﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackExit : StateMachineBehaviour
{
    private AIMaster aiMaster;
    public bool continueAngleToPlayer = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiMaster = animator.GetComponent<AIMaster>();
        aiMaster.SetDestinationToPlayer();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("attackCode", 0);
        if (continueAngleToPlayer)
        {
            aiMaster.isMove = true;
        }
    }
}
