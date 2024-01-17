using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    //AI
    public Transform target;

    //distances 
    public float attackDistance = 7.5f;
    public float viewDistance = 20;
    public float desiredDistance;

    public GameObject player;
    public float speed;
    NavMeshAgent agent;
    public bool shouldMove;



    //Health and DMG
    public float health;
    public float DMG;
    public float timeBetweenAttacks;
    public bool isAttacking;
    public bool isStunned;
    public GameObject hitbox;
    public Transform hitboxArea;


    //sounds
    public AudioSource DeathSound;
    public AudioClip Dead;

    //misc variables
    Rigidbody rb;
    public bool isDead;
    public bool stopLooking;
    public GameObject Warning;
    public Transform warningFunctionArea;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (!isDead)
        {
            if (distance < viewDistance)
            {
                if (!stopLooking)
                {
                    transform.LookAt(target);
                }
            }
            if (distance < attackDistance)
            {
                agent.destination = target.position;
                stopLooking = true;
            }
            if (distance > attackDistance)
            {
                shouldMove = true;
                speed = 2.5f;
            }
            else
            {
                stopLooking = false;
            }
        }
        if (health <= 0)
        {
            if (!isDead)
            {
                DeathSound.PlayOneShot(Dead);
                isDead = true;
            }
            Destroy(gameObject, 5f);
            rb.constraints = RigidbodyConstraints.None;
        }
        if (!isDead)
        {
            if (shouldMove)
            {
                var offset = this.transform.position - target.position;
                if (offset.sqrMagnitude < desiredDistance * desiredDistance)
                {
                
                    this.transform.position = this.transform.position + offset.normalized * (desiredDistance - offset.magnitude);
                    shouldMove = false;
                }
            }
            
        }

        if (distance < 4.5 && isStunned == false && !isDead)
        {
            timeBetweenAttacks += -Time.deltaTime;

            if(timeBetweenAttacks < 1)
            {
                if (isAttacking == false)
                {
                    Instantiate(Warning, warningFunctionArea.position, warningFunctionArea.rotation);
                    isAttacking = true;
                    speed = 0f;
                }
            }
            if(timeBetweenAttacks < 0)
            {
                Instantiate(hitbox, hitboxArea.position, hitboxArea.rotation);
                timeBetweenAttacks = 3.5f;
                isAttacking = false;
            }
        }
        
        agent.speed = speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            health--;
        }
    }

}
