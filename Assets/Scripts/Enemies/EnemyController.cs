using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float Health = 100f;
    public float DamageMultiplier = 1f;
    public float AttackCooldown = 2f;
    public float Speed = 0.8f;
    public float RunningSpeed = 8f;
    public float WalkingSpeed = 0.8f;
    public float smoothing = 1f;
    public float LookRadius = 10f;
    public float MaxRoamDistance = 30f;
    public float MinRoamDistance = 15f;
    public Vector3 RoamPos = new Vector3(0, 0, 0);

    public Mesh[] ZSkins;
    private SkinnedMeshRenderer animatedSkin;
    private SkinnedMeshRenderer ragdollSkin;
    private Animator animator;
    public Transform target;
    private NavMeshAgent agent;
    public RagdollController ragdoll;
    public Rigidbody rigid;
    public CapsuleCollider cCollider;

    
    //Animator Bools
    private bool IsIdle = true;
    private bool IsRoaming = false;
    private bool IsChasing = false;
    private bool AreWeAttacking = false;

    private void Awake()
    {
        animatedSkin = GetComponentsInChildren<SkinnedMeshRenderer>()[0];
        ragdollSkin = GetComponentsInChildren<SkinnedMeshRenderer>()[1];
    }

    // Start is called before the first frame update
    void Start()
    {

        animatedSkin.sharedMesh = ragdollSkin.sharedMesh = ZSkins[Random.Range(0,ZSkins.Length)];
        animator = GetComponentInChildren<Animator>();
        target = GameManager.Instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<RagdollController>();
        rigid = GetComponent<Rigidbody>();
        cCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(target.position, transform.position);

        if(agent.enabled && ragdoll.GetDead() == false)
        {
            // If Target Is Within Agro Radius
            if (Distance <= LookRadius)
            {
                //FaceTarget();
                //Are we at target to be able to attack?
                if (Distance <= agent.stoppingDistance)
                {
                    StartCoroutine(InitiateAttack());
                }
                // Continue to chase
                else if(AreWeAttacking == false)
                {
                    agent.speed = Speed = Mathf.Lerp(Speed, RunningSpeed, smoothing * Time.deltaTime);
                    agent.SetDestination(target.position);

                    animator.SetBool("InTargetRange", false);
                    animator.SetBool("IsChasing", true);
                    IsChasing = true;
                }
            }
            // Else If Target Is Greater Than Agro Radius
            else if(Distance >= LookRadius)
            {
                agent.speed = Speed = Mathf.Lerp(Speed, WalkingSpeed, smoothing * Time.deltaTime);
                if (IsChasing)
                {
                    StartCoroutine(ReturnToRoam());
                }
            
                if (IsIdle)
                {
                    StartCoroutine(CreateNewRoam());
                    IsIdle = false;
                }
                if (IsRoaming && agent.remainingDistance < agent.stoppingDistance)
                {
                    //Debug.Log("Reached Roam Destination");
                    IsIdle = true;
                    IsRoaming = false;
                    animator.SetBool("IsRoaming", false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var CarTag = other.tag;
        //If We Collided with the players vehicle
        if (CarTag == "CarCollider")
        {
            ragdoll.Dead();
            cCollider.enabled = false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    var CarTag = collision.collider.tag;
    //    Debug.Log("CarTag: " + CarTag);
    //    if (CarTag == "CarCollider")
    //    {
    //        ragdoll.ToggleDead();
    //    }
    //}

    IEnumerator CreateNewRoam()
    {
        // Gather new roam location
        RoamPos = new Vector3(
            transform.position.x + (Random.Range(0, 100) > 50 ? Random.Range(-MinRoamDistance, -MaxRoamDistance) : Random.Range(MinRoamDistance, MaxRoamDistance)),
            transform.position.y,
            transform.position.z + (Random.Range(0, 100) > 50 ? Random.Range(-MinRoamDistance, -MaxRoamDistance) : Random.Range(MinRoamDistance, MaxRoamDistance)));
        //Debug.Log("Roam Dest: X:" + RoamPos.x + "Y: " + RoamPos.y + "Z: " + RoamPos.z);

        yield return new WaitForSeconds(Random.Range(5f, 8f));

        agent.SetDestination(RoamPos);
        animator.SetBool("IsRoaming", true);
        IsRoaming = true;
        yield return null;
        
    }

    IEnumerator ReturnToRoam()
    {
        animator.SetBool("IsChasing", false);
        IsChasing = false;
        yield return new WaitForSeconds(5f);
        agent.ResetPath();
        yield return null;
    }

    IEnumerator InitiateAttack()
    {
        agent.ResetPath();
        animator.SetBool("InTargetRange", true);
        AreWeAttacking = true;
        yield return new WaitForSeconds(AttackCooldown);
        AreWeAttacking = false;
        yield return null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
        Gizmos.DrawWireCube(RoamPos, new Vector3(1, 1, 1));
    }
}
