using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPatternContext : StateMachineBehaviour
{
    private AIMaster aiMaster;
    [Tooltip("랜덤전이를 위한 특수 기술 파라미터 이름이 필요합니다.")]
    public List<string> specialPatternParameters;
    public int index;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        index = Random.Range(0, specialPatternParameters.Count);
        Debug.Log("(Before)Selected : " + index);
        int checkCount = 0;

        if (aiMaster == null)
        {
            aiMaster = animator.GetComponent<AIMaster>();
        }

        for (int i = 0; i < specialPatternParameters.Count; i++)
        {
            if (!animator.GetBool(specialPatternParameters[i]))
            {
                checkCount++;
            }
        }
        if (checkCount == specialPatternParameters.Count)
        {
            animator.SetInteger("attackCode", -1);
        }

        if (animator.GetInteger("attackCode") != -1)
        {
            do
            {
                if (!animator.GetBool(specialPatternParameters[index]))
                {
                    index++;
                    if (index >= specialPatternParameters.Count)
                    {
                        index = 0;
                    }
                }
                else
                {
                    break;
                }
            } while (true);

            Debug.Log("(After)Selected : " + index);
            animator.SetInteger("attackCode", index + 1);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("attackCode", 0);
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
