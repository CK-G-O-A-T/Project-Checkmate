using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour
{
    public UnityEvent enterEvent;

    private void OnTriggerEnter(Collider other)
    {
        enterEvent.Invoke();
    }
}
