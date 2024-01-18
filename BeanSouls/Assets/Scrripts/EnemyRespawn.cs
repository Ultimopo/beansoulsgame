using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public List<Transform> spawner;


    public GameObject enemy;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("EventActivator"))
        {
            for (int i = 0; i < spawner.Count; i++)
            {
                Instantiate(enemy, spawner[i].position, spawner[i].rotation);
            }
        }
    }

}
