using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// 애니메이션에서 FMOD에 대한 재생 이벤트를 수신하고 FMOD에 전달합니다.
/// </summary>
public class FmodAnimationEvent : MonoBehaviour
{
    struct FmodAnimationEventKeyData
    {
        public string keyName;
        [FMODUnity.EventRef]
        public string fmodEventName;
    }
    [SerializeField] FMODUnity.StudioEventEmitter fmodStudioEventEmiiter;
    [SerializeField] FmodAnimationEventKeyData[] eventKeyDatas = Array.Empty<FmodAnimationEventKeyData>();

    Dictionary<string, string> eventDictionary = new Dictionary<string, string>();

    private void Awake()
    {
        foreach(var eventKeyData in eventKeyDatas)
        {
            eventDictionary.Add(eventKeyData.keyName, eventKeyData.fmodEventName);
        }

        if (fmodStudioEventEmiiter == null)
        {
            this.enabled = false;
        }
    }

    void FMOD_Play(string keyName)
    {
        if (eventDictionary.TryGetValue(keyName, out var value))
        {
            fmodStudioEventEmiiter.Event = value;
            fmodStudioEventEmiiter.Play();
        }
    }
}