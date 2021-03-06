﻿using System;
using System.Linq;
using UnityEngine;

public class PlayerCharacterEquipment : MonoBehaviour
{
    const int BaseLayerIndex = 0;
    const int WeaponType1LayerIndex = 1;
    const int WeaponType2LayerIndex = 2;
    const int OverrideLayerIndex = 3;
    const int WeaponType1OverrideLayerIndex = 4;
    const int WeaponType2OverrideLayerIndex = 5;
    const int UpperLayerIndex = 6;

    [Header("오브젝트 설정")]
    [SerializeField] Transform weaponJoint;
    [SerializeField] Transform currentWeaponTransform;
    [SerializeField] PlayerCharacterBehaviour playerCharacterBehaviour;

    [Header("데이터")]
    [SerializeField] WeaponData weaponData;

    DamageTrigger[] damageTriggers = Array.Empty<DamageTrigger>();

    private void Awake()
    {
        if (playerCharacterBehaviour == null)
            playerCharacterBehaviour = GetComponent<PlayerCharacterBehaviour>();
    }

    private void Start()
    {
        WeaponData = WeaponData;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Correctness", "UNT0008:Null propagation on Unity objects", Justification = "<보류 중>")]
    public WeaponData WeaponData
    {
        get => weaponData;
        set
        {
            //var weaponData = this.weaponData.ToReferenceNull();
            value = value.ToReferenceNull();

            weaponData = value;

            // 기존 무기 제거
            if (currentWeaponTransform != null)
            {
                var currentWeaponObject = currentWeaponTransform.gameObject;

                damageTriggers = null;
                Destroy(currentWeaponObject);
            }

            // 무기 오브젝트 생성
            if (weaponData != null)
            {
                var currentWeaponObject = Instantiate(weaponData.WeaponPrefab);

                // 부모 설정
                currentWeaponTransform = currentWeaponObject.transform;
                currentWeaponTransform.parent = weaponJoint;
                currentWeaponTransform.Initialize();

                damageTriggers = currentWeaponObject.GetComponents<DamageTrigger>();

                foreach (var damageTrigger in damageTriggers)
                {
                    if (damageTrigger is WeaponDamageTrigger weaponDamageTrigger)
                    {
                        weaponDamageTrigger.PlayerCharacterBehaviour = playerCharacterBehaviour;
                    }
                }

                // 애니메이터 상태 설정
                SetWeaponTypeForAnimator(WeaponData.Type);
                //playerCharacterBehaviour.Animator.SetInteger("weaponType", (int)weaponData.Type);
            }
        } // end of setter
    } // end of WeaponData

    void SetWeaponTypeForAnimator(WeaponType type)
    {
        var animator = playerCharacterBehaviour.Animator;
        switch (type)
        {
            default:
                animator.SetLayerWeight(WeaponType1LayerIndex, 0);
                animator.SetLayerWeight(WeaponType2LayerIndex, 0);

                animator.SetLayerWeight(WeaponType1OverrideLayerIndex, 0);
                animator.SetLayerWeight(WeaponType2OverrideLayerIndex, 0);
                break;
            case WeaponType.OneHanded:
                animator.SetLayerWeight(WeaponType1LayerIndex, 1);
                animator.SetLayerWeight(WeaponType2LayerIndex, 0);

                animator.SetLayerWeight(WeaponType1OverrideLayerIndex, 1);
                animator.SetLayerWeight(WeaponType2OverrideLayerIndex, 0);
                //5회 공격
                playerCharacterBehaviour.MaxAttackCombo = 5;
                break;
            case WeaponType.Rapier:
                animator.SetLayerWeight(WeaponType1LayerIndex, 0);
                animator.SetLayerWeight(WeaponType2LayerIndex, 1);

                animator.SetLayerWeight(WeaponType1OverrideLayerIndex, 0);
                animator.SetLayerWeight(WeaponType2OverrideLayerIndex, 1);
                //4회 공격
                playerCharacterBehaviour.MaxAttackCombo = 4;
                break;
        }
    }

    void PlaySwingAudio()
    {
        switch (WeaponData.Type)
        {
            case WeaponType.OneHanded:
                playerCharacterBehaviour.CharacterAudio.weaponType1AttackAudio.Play();
                break;
            case WeaponType.Rapier:
                break;
        }
    }

    void CancelSwingAudio()
    {
        switch (WeaponData.Type)
        {
            case WeaponType.OneHanded:
                playerCharacterBehaviour.CharacterAudio.weaponType1AttackAudio.Stop();
                break;
            case WeaponType.Rapier:
                break;
        }
    }

    public void StartTrigger()
    {
        foreach (var damageTrigger in damageTriggers)
        {
            damageTrigger.StartTrigger();
        }
    }

    public void EndTrigger()
    {
        foreach (var damageTrigger in damageTriggers)
        {
            damageTrigger.EndTrigger();
        }
    }

    void DamageTrigger_StartTrigger(int layerIndex)
    {
        if (LayerIndexMatched(layerIndex))
        {
            StartTrigger();
            PlaySwingAudio();
        }
    }

    void DamageTrigger_EndTrigger(int layerIndex)
    {
        if (LayerIndexMatched(layerIndex))
        {
            EndTrigger();
        }
    }

    void DamageTrigger_CancelTrigger()
    {
        EndTrigger();
        CancelSwingAudio();
    }

    public bool LayerIndexMatched(int layerIndex)
    {
        switch (WeaponData.Type)
        {
            case WeaponType.OneHanded:
                if (layerIndex == WeaponType1LayerIndex || layerIndex == WeaponType1OverrideLayerIndex)
                {
                    return true;
                }
                break;
            case WeaponType.Rapier:
                if (layerIndex == WeaponType2LayerIndex || layerIndex == WeaponType2OverrideLayerIndex)
                {
                    return true;
                }
                break;
        }
        return false;
    }
}