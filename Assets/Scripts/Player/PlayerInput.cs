using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, ICharacterInputProvider
{
    [SerializeField]
    private InputActionReference _moveInputAction;

    private void Awake()
    {
        _moveInputAction.action.Enable();
    }

    public Vector2 GetMoveInput()
    {
        return _moveInputAction.action.ReadValue<Vector2>();
    }
}
