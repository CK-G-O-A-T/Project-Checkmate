﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadePlayer : StateMachineBehaviour
{
    AIMaster aiMaster;
    public bool isWalk = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiMaster = animator.GetComponent<AIMaster>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isWalk)
        {
            //bool isEvade;
            //aiMaster.SetEvadePosition(out isEvade);
            //if (isEvade == false)
            //{
            //    animator.SetTrigger("isEvade");
            //}
        }
        else
        {
            aiMaster.SetAngleToPlayer();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isWalk)
        {
            //animator.ResetTrigger("isEvade");
            //aiMaster.isEvade = false;
        }
        animator.SetBool("isEvade", false);
    }
}
