using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, ICharacterInputProvider
{
    private PlayerInputActions _actions;

    private void Awake()
    {
        _actions = new PlayerInputActions();
        _actions.Player.Enable();
    }

    public Vector2 GetMoveInput()
    {
        return _actions.Player.Move.ReadValue<Vector2>();
    }
}
