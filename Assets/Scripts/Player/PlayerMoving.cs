using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : PISMonoBehaviour
{
    //public Joystick Joystick;
    //public bool IsOnMobile;

    private bool _canMoveLeft;
    private bool _canMoveRight;
    private bool _canMoveUp;
    private bool _canMoveDown;

    [SerializeField] private PlayerController _playerController;

    private float _hozMove, _vertMove;
    private float _yRotation;
    private bool IsIdle
    {
        get => !_canMoveLeft && !_canMoveRight && !_canMoveUp && !_canMoveDown;
    }

    //private void Start()
    //{
    //    //if (IsOnMobile)
    //    //{ UIGamePlayManager.Ins.UIMobileGamepad.SetActive(true); }
    //    //else
    //    //{ UIGamePlayManager.Ins.UIMobileGamepad.SetActive(false); }
    //}

    private void Update()
    {
        SetUpIsOnMobile();
        LookAtTarget();
        Moving();
    }
    protected override void LoadComponents()
    {
        if (_playerController != null) return;
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void SetUpIsOnMobile()
    {
        //if (!IsOnMobile)
        //{
            _hozMove = Input.GetAxisRaw("Horizontal");
            _vertMove = Input.GetAxisRaw("Vertical");

            _canMoveLeft = _hozMove < 0;
            _canMoveRight = _hozMove > 0;
            _canMoveUp = _vertMove > 0;
            _canMoveDown = _vertMove < 0;
        //}
        //else
        //{
            //if (Joystick == null) return;
            //_canMoveLeft = Joystick.xValue < 0 ? true : false;
            //_canMoveRight = Joystick.xValue > 0 ? true : false;
            //_canMoveUp = Joystick.yValue > 0 ? true : false;
            //_canMoveDown = Joystick.yValue < 0 ? true : false;
        //}
    }

    private void ChangeState(PlayerState State)
    {
        _playerController.Anim.SetInteger("State", (int)State);
    }

    private void Moving()
    {
        Vector3 move = new Vector3(_hozMove, 0f, _vertMove).normalized * _playerController.MoveSpeed;
        if (_canMoveLeft || _canMoveRight || _canMoveUp || _canMoveDown)
        {
            ChangeState(PlayerState.Walk);
            _playerController.Rb.velocity = new Vector3(move.x, _playerController.Rb.velocity.y, move.z);
        }

        if (IsIdle)
        {
            ChangeState(PlayerState.Idle);
            //_playerController.Rb.velocity = Vector3.zero;
            _playerController.Rb.velocity = new Vector3(0, _playerController.Rb.velocity.y, 0);
        }
    }

    private void LookAtTarget()
    {
        if (_playerController.PlayerTarget.Target == null)
        {
            if (IsIdle)
            {
                _playerController.transform.rotation = Quaternion.Euler(0f, _yRotation, 0f);
                return;
            }
            Vector3 moveDirection = new Vector3(_hozMove, 0, _vertMove).normalized;
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);

            Quaternion targetRotation = Quaternion.RotateTowards(_playerController.transform.rotation, toRotation, 540f * Time.deltaTime);
            targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

            _yRotation = targetRotation.eulerAngles.y;

            _playerController.transform.rotation = targetRotation;
        }
        else
        {
            Vector3 targetPosition = _playerController.PlayerTarget.Target.transform.position;
            //float playerZ = _playerController.transform.position.z;
            //float targetZ = _playerController.PlayerTarget.Target.transform.position.z;

            //float playerX = _playerController.transform.position.x;
            //float targetX = _playerController.PlayerTarget.Target.transform.position.x;

            //if (playerZ > targetZ)
            //{
            //    targetPosition.x += 0.12f;
            //}
            //else if (playerZ < targetZ)
            //{
            //    targetPosition.x -= 0.12f;
            //}

            //if(playerX > targetX)
            //{
            //    targetPosition.z -= 0.2f;
            //}
            //else if (playerX < targetX)
            //{
            //    targetPosition.z += 0.2f;
            //}

            targetPosition.y = _playerController.transform.position.y;
            _playerController.transform.LookAt(targetPosition);
        }
    }
}
