using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float knockBackForce = 10f;

    private CharacterController characterController;
    private Health health;
    private bool isKnockedBack;
    public GameObject player;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        health = GetComponent<Health>();
    }

    void OnEnable()
    {
        health.OnDamageTaken += TakeKnockback;
    }

    void OnDisable()
    {
        health.OnDamageTaken -= TakeKnockback;
    }

    void Update()
    {
        if (!isKnockedBack) FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (!player.transform) return;

        Vector3 targetDirection = (player.transform.position - transform.position).normalized;
        Vector3 followDirection = new Vector3(targetDirection.x, 0f, targetDirection.z);

        characterController.Move(followDirection * Time.deltaTime * speed);
    }

    private void TakeKnockback()
    {
        isKnockedBack = true;
        StartCoroutine(Knockback());
    }

    private IEnumerator Knockback()
    {
        Vector3 targetDirection = (transform.position - player.transform.position).normalized;
        targetDirection.y = 0;

        float duration = 0.15f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            characterController.Move(targetDirection * Time.deltaTime * knockBackForce);

            elapsed += Time.deltaTime;

            yield return null;
        }

        isKnockedBack = false;
    }
}
