using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.25F);
    }
}
