using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    //DESTYTOJAU JEI ZIURI CIA ZIAURIAI WIP

    //AI
    public Transform target;

    public float viewDistance = 10;

    //Health and DMG
    public float health;

    //sounds
    public AudioSource DeathSound;
    public AudioClip Dead;

    //misc variables
    Rigidbody rb;
    public bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        if (distance < viewDistance)
        {
            transform.LookAt(target);
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

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            health--;
        }
    }

}
