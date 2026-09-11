using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
        }
    }
}
