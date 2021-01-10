using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrying : StateMachineBehaviour
{
    private AIMaster aiMaster;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiMaster = animator.GetComponent<AIMaster>();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(aiMaster.animationHash);
        animator.Play(aiMaster.animationHash);
    }
}
