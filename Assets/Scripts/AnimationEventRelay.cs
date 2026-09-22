using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    CombatController combatController;

    void Awake()
    {
        combatController = GetComponentInParent<CombatController>();
    }

    public void AttackOpen()
    {
        combatController.ToggleSwordHitbox(true);
    }

    public void AttackClose()
    {
        combatController.ToggleSwordHitbox(false);
    }

    public void ComboWindow()
    {
        combatController.TryChainCombo();
    }
}
