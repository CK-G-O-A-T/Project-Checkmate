using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using FMODUnity;

/// <summary>
/// 애니메이션에서 FMOD에 대한 재생 이벤트를 수신하고 FMOD에 전달합니다.
/// </summary>
public class FmodAnimationEvent : MonoBehaviour
{
    [Serializable]
    struct FmodAnimationEventKeyData
    {
        public string keyName;
        public StudioEventEmitter fmodEvent;
    }
    [SerializeField] FmodAnimationEventKeyData[] eventKeyDatas = Array.Empty<FmodAnimationEventKeyData>();

    Dictionary<string, StudioEventEmitter> eventDictionary = new Dictionary<string, StudioEventEmitter>();

    private void Awake()
    {
        foreach(var eventKeyData in eventKeyDatas)
        {
            eventDictionary.Add(eventKeyData.keyName, eventKeyData.fmodEvent);
        }
    }

    void FMOD_Play(AnimationEvent eventParams)
    {
        var keyName = eventParams.stringParameter;
        if (eventDictionary.TryGetValue(keyName, out var fmodEvent))
        {
            fmodEvent.Play();
        }
    }
}