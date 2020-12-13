//윤표꺼의 보스이펙트소환을 똑같이 적용함

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PceffectStruct
{
    public ParticleSystem particleObject;
    [HideInInspector]
    public GameObject particleGameObject;
}



public class PcvfxManager : MonoBehaviour
{
    public List<PceffectStruct> Pceffects;
    public Transform effectPosition;
    public float effectDuration;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Pceffects.Count; i++)
        {
            Pceffects[i].particleGameObject = Pceffects[i].particleObject.gameObject;
            Pceffects[i].particleGameObject.SetActive(false);
        }
    }

    public void PlayEffect(int index)
    {
        Pceffects[index].particleGameObject.SetActive(true);
        Pceffects[index].particleObject.Play();
        //effects[index].particleGameObject.transform.position = effectPosition.position;
        //effects[index].particleGameObject.transform.rotation = Quaternion.identity;
        StartCoroutine(DestroyParticle(Pceffects[index].particleGameObject, Pceffects[index].particleObject.duration));
        Debug.Log("Effect Index : " + index);
    }

    // 애니메이션 이벤트용 함수
    void Effect_Start(int index)
    {
        PlayEffect(index);
    }

    IEnumerator DestroyParticle(GameObject gb, float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);
        gb.SetActive(false);
    }
}
