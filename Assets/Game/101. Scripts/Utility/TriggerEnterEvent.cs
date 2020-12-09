using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour
{
    public UnityEvent enterEvent;
    public bool isOneshot = false;
    public string[] targetTag;

    private void OnTriggerEnter(Collider other)
    {
        if (enabled && other.CompareTags(targetTag))
        {
            enterEvent.Invoke();

            if (isOneshot)
            {
                this.enabled = false;
            }
        }
    }
}
