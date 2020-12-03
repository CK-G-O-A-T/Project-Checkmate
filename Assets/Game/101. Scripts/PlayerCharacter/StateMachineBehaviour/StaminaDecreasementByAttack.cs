
using UnityEngine;

public class StaminaDecreasementByAttack : StateMachineBehaviour
{
    PlayerCharacterBehaviour playerCharacterBehaviour;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCharacterBehaviour = animator.GetComponent<PlayerCharacterBehaviour>();

        var currentWeaponData = playerCharacterBehaviour.Equipment.WeaponData;
        var playerCharacterStatus = playerCharacterBehaviour.Status;

        playerCharacterStatus.Stamina -= currentWeaponData.StaminaDecreaseByAttack;
        playerCharacterStatus.SetStaminaRecoveryDelay();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCharacterBehaviour.Status.SetStaminaRecoveryDelay();
    }
}