using UnityEngine;

public class CombatController : MonoBehaviour
{
    InputController inputController;

    [SerializeField] GameObject swordHitbox;

    void Awake()
    {
        inputController = GetComponent<InputController>();
    }

    void Update()
    {
        if (inputController.Controls.Player.Attack.triggered)
        {
            Atack();
        }
    }

    private void Atack()
    {
        Debug.Log("Atacking!!");
        swordHitbox.SetActive(true);
        Invoke(nameof(DisableSwordHitbox), 0.2f);
    }

    private void DisableSwordHitbox()
    {
        swordHitbox.SetActive(false);
    }


}
