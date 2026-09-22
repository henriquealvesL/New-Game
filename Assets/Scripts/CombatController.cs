using UnityEngine;

public class CombatController : MonoBehaviour
{
    InputController inputController;
    Animator animator;

    [SerializeField] GameObject swordHitbox;

    private bool comboQueued;
    private bool isAttacking;
    private int comboStep;

    void Awake()
    {
        inputController = GetComponent<InputController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && isAttacking && !animator.IsInTransition(0))
        {
            ResetAttack();
        }

        if (inputController.Controls.Player.Attack.triggered)
        {
            HandleAttack();
        }
    }

    private void HandleAttack()
    {
        if (isAttacking)
        {
            comboQueued = true;
            return;
        }

        isAttacking = true;
        animator.SetTrigger("Attack");
    }

    public void ToggleSwordHitbox(bool isActive)
    {
        swordHitbox.SetActive(isActive);
    }

    public void TryChainCombo()
    {
        if (!comboQueued) return;

        comboStep++;
        animator.SetInteger("ComboStep", comboStep);
        animator.SetTrigger("Attack");
        comboQueued = false;
    }

    private void ResetAttack()
    {
        comboStep = 0;
        comboQueued = false;
        isAttacking = false;
        animator.ResetTrigger("Attack");
        ToggleSwordHitbox(false);
    }
}
