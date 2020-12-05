
using UnityEngine;

public class StaminaDecreasementByEvade : StateMachineBehaviour
{
    PlayerCharacterBehaviour playerCharacterBehaviour;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCharacterBehaviour = animator.GetComponent<PlayerCharacterBehaviour>();

        var playerCharacterStatus = playerCharacterBehaviour.Status;
        var playerCharacterData = playerCharacterStatus.Data;

        playerCharacterStatus.Stamina -= playerCharacterData.StamianDecreaseByEvasion;
        playerCharacterStatus.SetStaminaRecoveryDelay();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCharacterBehaviour.Status.SetStaminaRecoveryDelay();
    }
}