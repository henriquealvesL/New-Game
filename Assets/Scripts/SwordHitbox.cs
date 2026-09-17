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
            Health health = other.GetComponent<Health>();
            health.TakeDamage(10);
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Hitou o player");
            Health health = other.GetComponent<Health>();
            health.TakeDamage(5);
        }
    }
}
