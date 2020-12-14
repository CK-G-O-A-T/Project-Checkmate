using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// 데미지가 제대로 전달되지 않을 때 Ready 상태에 넣어줄 것
/// </summary>
public class SetAttackDamage : StateMachineBehaviour
{
    private AIMaster aiMaster;
    public double setAttackDamage;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiMaster = animator.GetComponent<AIMaster>();
        aiMaster.SetAttackDamage(setAttackDamage);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        aiMaster.SetAttackDamage(setAttackDamage);
    }
}
