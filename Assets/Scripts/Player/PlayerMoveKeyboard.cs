using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveKeyboard : MonoBehaviour
{
    private InputAction _moveAction;

    private void Awake()
    {
        _moveAction = new InputAction("Keyboard Move", InputActionType.Value, expectedControlType: "Vector2");
        _moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        _moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");
        _moveAction.AddBinding("<Gamepad>/leftStick");
    }

    private void OnEnable()
    {
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction?.Disable();
    }

    private void OnDestroy()
    {
        _moveAction?.Dispose();
    }

    public Vector2 ReadMoveInput()
    {
        return Vector2.ClampMagnitude(_moveAction.ReadValue<Vector2>(), 1f);
    }
}
