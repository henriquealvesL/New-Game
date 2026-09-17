using UnityEngine;

public class InputController : MonoBehaviour
{
    public InputSystem_Actions Controls { get; private set; }

    void Awake()
    {
        GameManager.OnGameOver += HandleGameOver;

        Controls = new InputSystem_Actions();
        Controls.Player.Enable();
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver() => Controls.Player.Disable();
}
