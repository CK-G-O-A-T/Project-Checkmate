using UnityEngine;
using System.Collections;

public class TargetFollow : MonoBehaviour
{
    Transform thisTransform;
    [SerializeField] Transform targetTransform;
    [SerializeField] float smoothTime = 1;
    Vector3 velocity;
    Vector3 moveTargetPosition;

    private void Awake()
    {
        thisTransform = transform;
        thisTransform.position = targetTransform.position;
        moveTargetPosition = targetTransform.position;
    }

    private void FixedUpdate()
    {
        var currentPosition = thisTransform.position;
        var targetPosition = targetTransform.position;

        var distanceVector = currentPosition - targetPosition;

        var sqrDistance = distanceVector.sqrMagnitude;

        moveTargetPosition = targetPosition + distanceVector / 2;

        currentPosition = Vector3.SmoothDamp(currentPosition, moveTargetPosition, ref velocity, smoothTime);
        thisTransform.position = currentPosition;
    }
}