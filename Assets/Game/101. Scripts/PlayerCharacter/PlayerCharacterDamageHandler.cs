
using UnityEngine;

class PlayerCharacterDamageHandler : DamageHandler
{
    [SerializeField] PlayerCharacterBehaviour behaviour;
    [SerializeField] PcvfxManager effectManager;
    [SerializeField] AudioSource hurtSound;
    [SerializeField] PlayerCameraManager cameraManager;

    private void Awake()
    {
        if (behaviour == null)
        {
            behaviour = GetComponent<PlayerCharacterBehaviour>();
        }
        if (effectManager == null)
        {
            effectManager = GetComponent<PcvfxManager>();
        }
    }

    public override void DamageHandle(DamageData damageData)
    {
        if (!behaviour.IsNoDamage)
        {
            Debug.Log($"플레이어 데미지 핸들: {damageData}");
            behaviour.Status.Hp -= damageData.Damage;
            behaviour.DoImpact();
            behaviour.CharacterAudio.hitAudio.Play();
            effectManager.PlayEffect(6);
            cameraManager.CameraShake(0.1f, 10f, 0.5f);
            //hurtSound.Play();
        }
        else
        {
            Debug.Log($"플레이어 무적, 데미지 수신: {damageData}");
        }
    }
}