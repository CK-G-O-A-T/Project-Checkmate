using UnityEngine;
using System.Collections;
using Cinemachine;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook freeLookCamera;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform targetTransform;
    [Range(0, 1)]
    [SerializeField] float lockonCameraHeight = 0.35f;
    [SerializeField] CinemachineImpulseSource cameraImpulseSource;

    //Transform freeLookCameraTransform;
    float playerAngleOrigin;
    float xspeedBackup;
    float yspeedBackup;
    public Transform TargetTransform
    {
        get => this.targetTransform;
        set
        {
            this.targetTransform = value;

            freeLookCamera.LookAt = value;


            if (value != null)
            {
                xspeedBackup = freeLookCamera.m_XAxis.m_MaxSpeed;
                yspeedBackup = freeLookCamera.m_YAxis.m_MaxSpeed;
                freeLookCamera.m_XAxis.m_MaxSpeed = 0;
                freeLookCamera.m_YAxis.m_MaxSpeed = 0;
            }
            else
            {
                freeLookCamera.m_XAxis.m_MaxSpeed = xspeedBackup;
                freeLookCamera.m_YAxis.m_MaxSpeed = yspeedBackup;
            }
        }
    }

    public void CameraShake(float time, float frequency, float amplitude)
    {
        var impulseDefine = cameraImpulseSource.m_ImpulseDefinition;
        impulseDefine.m_TimeEnvelope.m_DecayTime = time;
        impulseDefine.m_AmplitudeGain = amplitude;
        impulseDefine.m_FrequencyGain = frequency;
        cameraImpulseSource.GenerateImpulse(transform.position);
    }

    private void Awake()
    {
        xspeedBackup = freeLookCamera.m_XAxis.m_MaxSpeed;
        yspeedBackup = freeLookCamera.m_YAxis.m_MaxSpeed;
        //freeLookCameraTransform = freeLookCamera.transform;
        playerAngleOrigin = playerTransform.eulerAngles.y;
    }

    private void Start()
    {
        TargetTransform = TargetTransform;
    }

    private void LateUpdate()
    {
        if (freeLookCamera.LookAt == null)
        {
            targetTransform = null;
            freeLookCamera.LookAt = playerTransform;
        }
        else if (targetTransform != null)
        {
           // var freeLookForward = freeLookCameraTransform.forward.ToXZ();
            var angle = Vector2.SignedAngle(Vector2.up, (targetTransform.position - playerTransform.position).ToXZ());
            angle += playerAngleOrigin;

            freeLookCamera.m_XAxis.Value = -angle;
            freeLookCamera.m_YAxis.Value = lockonCameraHeight;
        }
    }
}