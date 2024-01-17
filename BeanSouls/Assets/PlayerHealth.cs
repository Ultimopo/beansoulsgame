using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float HP;
    public float maximumHP;
    public HealthBar healthbar;

    void Start()
    {
        HP = maximumHP;
        healthbar.SetHealth(maximumHP);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            HP--;
            healthbar.SetHealth(HP);
            print("Hit!");
        }
    }
}
