using UnityEngine;

public class InputController : MonoBehaviour
{
    public InputSystem_Actions Controls { get; private set; }

    void Awake()
    {
        Controls = new InputSystem_Actions();
        Controls.Player.Enable();
    }

}
