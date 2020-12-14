
using UnityEngine;
using UnityEngine.Events;

class PlayerCharacterDamageHandler : DamageHandler
{
    [SerializeField] PlayerCharacterBehaviour behaviour;
    [SerializeField] PcvfxManager effectManager;
    [SerializeField] AudioSource hurtSound;
    [SerializeField] PlayerCameraManager cameraManager;
    [SerializeField] UnityEvent OnDie = new UnityEvent();

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
            cameraManager.CameraShake(0.2f, 0.1f, 10f, 0.5f);
            //hurtSound.Play();


            if (behaviour.Status.Hp <= 0 && !behaviour.IsDied)
            {
                behaviour.IsDied = true;
                OnDie.Invoke();
            }
        }
        else
        {
            Debug.Log($"플레이어 무적, 데미지 수신: {damageData}");
        }
    }
}