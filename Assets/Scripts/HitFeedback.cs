using System.Collections;
using UnityEngine;

public class HitFeedback : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private MeshRenderer meshRenderer;
    private Color originalColor;


    void Awake()
    {
        originalColor = meshRenderer.material.color;
    }
    void OnEnable()
    {
        health.OnDamageTaken += DamageTaken;
    }

    void OnDisable()
    {
        health.OnDamageTaken -= DamageTaken;
    }

    private void DamageTaken()
    {
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        meshRenderer.material.color = Color.Lerp(
            originalColor,
            Color.white,
            0.3f
        );
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.color = originalColor;
    }
}
