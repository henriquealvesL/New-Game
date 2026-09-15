using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Slider slider;

    void OnEnable()
    {
        health.OnHealthChanged += HealthChanged;
    }

    void OnDisable()
    {
        health.OnHealthChanged -= HealthChanged;
    }

    private void HealthChanged(int currentHealth, int maxHealth)
    {
        slider.value = (float)currentHealth / maxHealth;
    }
}
