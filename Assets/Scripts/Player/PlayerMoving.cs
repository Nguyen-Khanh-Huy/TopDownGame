using UnityEngine;

public class PlayerMoving : PISMonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private PlayerMoveKeyboard _keyboardInput;
    [SerializeField] private PlayerMoveJoystick _joystickInput;

    private Vector2 _moveInput;
    private float _saveRotationY;
    private bool IsIdle => _moveInput.sqrMagnitude <= 0.0001f;

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    private void Update()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;

        ReadMoveInput();
        LookAtTarget();
        Move();
    }

    private void ReadMoveInput()
    {
        Vector2 keyboardInput = ReadKeyboardInput();
        Vector2 joystickInput = ReadJoystickInput();

        _moveInput = joystickInput.sqrMagnitude > 0.0001f ? joystickInput : keyboardInput;
    }

    private Vector2 ReadKeyboardInput()
    {
        if (_keyboardInput == null || !_keyboardInput.isActiveAndEnabled) return Vector2.zero;
        return _keyboardInput.ReadMoveInput();
    }

    private Vector2 ReadJoystickInput()
    {
        if (_joystickInput == null || !_joystickInput.isActiveAndEnabled) return Vector2.zero;
        return _joystickInput.ReadMoveInput();
    }

    private void ChangeState(PlayerState newState)
    {
        if (PlayerCtrl.Ins.Anim.GetInteger("State") != (int)newState)
            PlayerCtrl.Ins.Anim.SetInteger("State", (int)newState);
    }

    private void Move()
    {
        Rigidbody rb = PlayerCtrl.Ins.Rb;

        if (IsIdle)
        {
            ChangeState(PlayerState.Idle);
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        Vector3 move = new Vector3(_moveInput.x, 0f, _moveInput.y) * _moveSpeed;
        ChangeState(PlayerState.Walk);
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }

    private void LookAtTarget()
    {
        PlayerCtrl player = PlayerCtrl.Ins;

        if (player.PlayerTarget.Target != null)
        {
            Vector3 targetPosition = player.PlayerTarget.Target.transform.position;
            targetPosition.y = player.transform.position.y;
            player.transform.LookAt(targetPosition);
            return;
        }

        if (IsIdle)
        {
            player.transform.rotation = Quaternion.Euler(0f, _saveRotationY, 0f);
            return;
        }

        Vector3 moveDirection = new(_moveInput.x, 0f, _moveInput.y);
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, targetRotation, 540f * Time.deltaTime);
        _saveRotationY = player.transform.eulerAngles.y;
    }

    protected override void LoadComponents()
    {
        if (_keyboardInput != null && _joystickInput != null) return;
        _keyboardInput = GetComponent<PlayerMoveKeyboard>();
        _joystickInput = GetComponent<PlayerMoveJoystick>();
    }
}
