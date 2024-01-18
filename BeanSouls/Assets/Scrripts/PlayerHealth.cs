using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float HP;
    public float maximumHP;
    public HealthBar healthbar;

    public bool Iframes;
    public float IframeCD = 1.5f;

    void Awake()
    {
        HP = maximumHP;
        healthbar.SetHealth(HP);
    }
    private void Update()
    {
        if (Iframes)
        {
            IframeCD -= Time.deltaTime;
            if (IframeCD < 0)
            {
                Iframes = false;
                IframeCD = 1.5f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!Iframes)
        {
            if (other.CompareTag("FodderEnemyAttack"))
            {
                HP -= 20;
                healthbar.SetHealth(HP);
                Iframes = true;
            }

        }
        
    }

}
