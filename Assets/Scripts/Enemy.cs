using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController characterController;
    public GameObject player;
    [SerializeField] private float speed = 4f;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (!player.transform) return;

        Vector3 targetDirection = (player.transform.position - transform.position).normalized;
        Vector3 followDirection = new Vector3(targetDirection.x, 0f, targetDirection.z);

        characterController.Move(followDirection * Time.deltaTime * speed);
    }
}
