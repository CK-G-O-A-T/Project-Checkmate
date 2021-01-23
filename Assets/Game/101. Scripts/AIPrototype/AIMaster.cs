using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

public class AIMaster : MonoBehaviour
{
    [Header("Basic Setting")]
    public string bossName;
    public float currentHealthPoint;
    public int souls;
    public float setRotationSpeed;
    public bool isFirstStrike = false;
    public Transform rayCastTransform;
    public float guidanceDistance = 1f;
    [SerializeField] private BossDamageTriggerManager bossDamageTriggerManager;
    public int changePhase2HealthPoint;
    public StudioEventEmitter hitSound;

    [Header("Groggy")]
    public BossGroggyComponent groggyComponent;

    [Header("Jump Attack")]
    public BossJumpAttackComponent jumpAttackComponent;

    [Header("Special Pattern")]
    public BossSpecialPattern specialPatter;

    [Header("Distance Setting")]
    public float closeRangeAttackDistance;
    public float longRangeAttackDistance;
    public float trackingDistance;

    [Header("Debug")]
    [SerializeField] private bool DebugOn = true;
    [SerializeField] private Vector3 AgentNextPostiion;

    private NavMeshAgent agent;
    private GameObject player;
    [HideInInspector] public Animator anim;
    private float speedSave;

    [HideInInspector] public bool isEvade = false;
    [HideInInspector] public bool isMove = true;
    [HideInInspector] public bool isGroggy = false;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public float maxHealthPoint;

    public int animationHash;

    public Vector3 setAgentDestination
    {
        set
        {
            agent.destination = value;
        }
    }

    public GameObject getPlayer
    {
        get
        {
            return player;
        }
    }

    private void Awake()
    {
        if (bossDamageTriggerManager == null)
        {
            bossDamageTriggerManager = GetComponent<BossDamageTriggerManager>();
        }
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        maxHealthPoint = currentHealthPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;

        speedSave = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        AttackDistance();
        AgentNextPostiion = agent.nextPosition;
        UpdateHealthData();
    }

    private void FixedUpdate()
    {
        if (isFirstStrike && !isDead)
        {
            if (anim.GetInteger("attackCode") == 0 && isMove)
            {
                SwitchingRootMotion();
            }
            if (isEvade == false)
            {
                NavMeshAgentGuidance();
            }
        }
    }

    private void AttackDistance()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        anim.SetFloat("playerDistance", playerDistance);

        // 근접공격
        if (playerDistance <= closeRangeAttackDistance)
        {
            anim.SetBool("closeRangeAttack", true);
            anim.SetBool("longRangeAttack", false);
            anim.SetBool("trackingDistance", false);
        }

        // 원거리 공격
        else if (playerDistance > closeRangeAttackDistance && playerDistance <= longRangeAttackDistance)
        {
            anim.SetBool("closeRangeAttack", false);
            anim.SetBool("longRangeAttack", true);
            anim.SetBool("trackingDistance", false);
        }

        // 추적
        else if (playerDistance > longRangeAttackDistance && playerDistance <= trackingDistance)
        {
            anim.SetBool("closeRangeAttack", false);
            anim.SetBool("longRangeAttack", false);
            anim.SetBool("trackingDistance", true);
        }

        // 모두 안함 (공격, 추적 거리를 넘어섬)
        else
        {
            anim.SetBool("closeRangeAttack", false);
            anim.SetBool("longRangeAttack", false);
            anim.SetBool("trackingDistance", false);
        }
    }

    public void SwitchingRootMotion()
    {
        Vector3 newTransformPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 newAgentPosition = new Vector3(agent.nextPosition.x, transform.position.y, agent.nextPosition.z);

        if (Vector3.Distance(newTransformPosition, newAgentPosition) >= 0.3f)
        {
            CustomLookAt(newAgentPosition, setRotationSpeed);
        }
        else
        {

        }
    }

    /// <summary>
    /// 2021.1.5 함수의 기능이 비슷해 보이지만 역할과 로직이 다르다고 판단하여 수정하지 않아도 될 것 같음
    /// </summary>
    private void NavMeshAgentGuidance()
    {
        Vector3 direction;
        //float speed;
        if (agent.path.corners.Length >= 2)
        {
            direction = (agent.path.corners[1] - transform.position).normalized;
        }
        else
        {
            direction = (agent.path.corners[0] - transform.position).normalized;
        }

        agent.nextPosition = transform.position + (direction * guidanceDistance);
        agent.speed = Mathf.Lerp(agent.speed, 0, Time.deltaTime * 3f);
    }

    public void AttackSequence()
    {
        isMove = false;
    }

    public void SetAngleToPlayer(float rotationSpeed = 20)
    {
        CustomLookAt(player.transform.position, rotationSpeed);
    }

    public void TrackingPlayer()
    {
        SwitchingRootMotion();
        agent.destination = player.transform.position;
    }

    public bool CheckArriveDestination()
    {
        if (Vector3.Distance(transform.position, agent.destination) <= 0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetDestinationToPlayer()
    {
        agent.destination = player.transform.position;
    }

    public void SetAttackDamage(double value)
    {
        bossDamageTriggerManager.setAttackDamage = value;
    }

    /// <summary>
    /// 정식 사용중
    /// </summary>
    /// <param name="phase"></param>
    public void ChangePhase(int phase)
    {
        if (currentHealthPoint <= changePhase2HealthPoint)
        {
            anim.SetInteger("Phase", phase);
        }
    }

    public void SetBool(string name)
    {
        anim.SetBool(name, true);
    }

    public void PlayHitSound()
    {
        hitSound.Play();
    }

    private void UpdateHealthData()
    {
        anim.SetFloat("healthPoint", currentHealthPoint);
        anim.SetInteger("souls", souls);
    }

    #region Evade Function
    public void SetEvadePosition(out bool value)
    {
        SetEvadeDirection();

        isMove = true;

        value = isEvade;

        Vector3 evadeDirection = (transform.position - player.transform.position).normalized;

        RaycastHit hit;
        Ray ray = new Ray(rayCastTransform.position, rayCastTransform.forward);

        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red);

        //agent.destination = evadeDirection * 10f;
        if ((Physics.Raycast(ray, out hit, 3f) && !hit.collider.CompareTag("Player")) || Vector3.Distance(transform.position, player.transform.position) >= 20)
        {
            isEvade = false;
            value = isEvade;
        }
        else
        {
            isEvade = true;
            value = isEvade;
        }
    }

    public void SetEvadeDirection(bool isReverse = false)
    {
        Vector3 evadeDirection;
        if (isReverse)
        {
            evadeDirection = (player.transform.position - transform.position).normalized;
        }
        else
        {
            evadeDirection = (transform.position - player.transform.position).normalized;
        }
        agent.nextPosition = transform.position + (evadeDirection * guidanceDistance);
        agent.destination = agent.nextPosition + (transform.forward * 5f);
    }
    #endregion

    #region Utilities Function

    /// <summary>
    /// target과의 각도를 0~180도를 검사함
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private float GetTargetAngle(Vector3 target)
    {
        Vector3 targetDirection = target - transform.position;
        float result = Mathf.Acos(Vector3.Dot(targetDirection.normalized, transform.forward));

        return result;
    }

    [Range(1, 360)]
    public float angle;
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (DebugOn)
        {
            Handles.color = new Color(1, 0, 0, 0.3f);
            Handles.DrawSolidArc(transform.position, transform.up, transform.forward, angle / 2, closeRangeAttackDistance);
            Handles.DrawSolidArc(transform.position, transform.up, transform.forward, -angle / 2, closeRangeAttackDistance);

            Handles.color = new Color(0, 0, 1, 0.2f);
            Handles.DrawSolidDisc(transform.position, transform.up, longRangeAttackDistance);

            Handles.color = new Color(0, 0, 0, 0.2f);
            Handles.DrawSolidDisc(transform.position, transform.up, trackingDistance);
        }
    }
#endif

    private void DebugString(string str)
    {
        if (DebugOn)
        {
            Debug.Log(str);
        }
    }

    private void CustomLookAt(Vector3 target, float rotationSpeed = 20, bool isReverse = false)
    {
        Vector3 direction;
        if (isReverse)
        {
            direction = transform.position - target;
        }
        else
        {
            direction = target - transform.position;
        }
        direction.y = transform.position.y;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
    }
    #endregion
}
