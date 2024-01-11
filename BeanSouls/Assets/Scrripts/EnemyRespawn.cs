using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public Transform enemyrespawnpoint1;
    public Transform enemyrespawnpoint2;
    public Transform enemyrespawnpoint3;
    public Transform enemyrespawnpoint4;
    public Transform enemyrespawnpoint5;
    public Transform enemyrespawnpoint6;
    public Transform enemyrespawnpoint7;

    public GameObject enemy;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("EventActivator"))
        {
            Instantiate(enemy, enemyrespawnpoint1.position, enemyrespawnpoint1.rotation);
        }
    }

}
