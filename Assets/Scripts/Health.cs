using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDeath;
    public event Action<int, int> OnHealthChanged;
    private int maxHealth = 100;
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        OnHealthChanged.Invoke(currentHealth, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Morreu");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }


}
