using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    //public Joystick Joystick;
    //public bool IsOnMobile;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private bool _canMoveLeft;
    [SerializeField] private bool _canMoveRight;
    [SerializeField] private bool _canMoveUp;
    [SerializeField] private bool _canMoveDown;

    private float _hozMove, _vertMove;
    private float _saveRotationY;
    private bool IsIdle { get => !_canMoveLeft && !_canMoveRight && !_canMoveUp && !_canMoveDown; }

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    private void Update()
    {
        if (!UIGamePlayManager.Ins.CheckPlayTime) return;
        SetUpIsOnMobile();
        LookAtTarget();
        Moving();
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

    private void ChangeState(PlayerState newState)
    {
        //_playerController.Anim.SetInteger("State", (int)State);
        if (PlayerCtrl.Ins.Anim.GetInteger("State") != (int)newState)
            PlayerCtrl.Ins.Anim.SetInteger("State", (int)newState);
    }

    private void Moving()
    {
        Vector3 move = new Vector3(_hozMove, 0f, _vertMove).normalized * _moveSpeed;
        if (IsIdle)
        {
            ChangeState(PlayerState.Idle);
            PlayerCtrl.Ins.Rb.velocity = new Vector3(0, PlayerCtrl.Ins.Rb.velocity.y, 0);
        }

        //if (_canMoveLeft || _canMoveRight || _canMoveUp || _canMoveDown)
        else
        {
            ChangeState(PlayerState.Walk);
            PlayerCtrl.Ins.Rb.velocity = new Vector3(move.x, PlayerCtrl.Ins.Rb.velocity.y, move.z);
        }


    }
    private void LookAtTarget()
    {
        if (PlayerCtrl.Ins.PlayerTarget.Target != null)
        {
            Vector3 targetPosition = PlayerCtrl.Ins.PlayerTarget.Target.transform.position;
            targetPosition.y = PlayerCtrl.Ins.transform.position.y;
            PlayerCtrl.Ins.transform.LookAt(targetPosition);
            return;
        }

        if (IsIdle)
        {
            PlayerCtrl.Ins.transform.rotation = Quaternion.Euler(0f, _saveRotationY, 0f);
            return;
        }

        Vector3 moveDirection = new(_hozMove, 0, _vertMove);
        if (moveDirection.sqrMagnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection.normalized);
            PlayerCtrl.Ins.transform.rotation = Quaternion.RotateTowards(PlayerCtrl.Ins.transform.rotation, targetRotation, 540f * Time.deltaTime);
            _saveRotationY = PlayerCtrl.Ins.transform.eulerAngles.y;
        }
    }

    //private void LookAtTarget()
    //{
    //    if (_playerController.PlayerTarget.Target == null)
    //    {
    //        if (IsIdle)
    //        {
    //            _playerController.transform.rotation = Quaternion.Euler(0f, _yRotation, 0f);
    //            return;
    //        }
    //        Vector3 moveDirection = new Vector3(_hozMove, 0, _vertMove).normalized;
    //        Quaternion toRotation = Quaternion.LookRotation(moveDirection);

    //        Quaternion targetRotation = Quaternion.RotateTowards(_playerController.transform.rotation, toRotation, 540f * Time.deltaTime);
    //        targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

    //        _yRotation = targetRotation.eulerAngles.y;

    //        _playerController.transform.rotation = targetRotation;
    //    }
    //    else
    //    {
    //        Vector3 targetPosition = _playerController.PlayerTarget.Target.transform.position;
    //        targetPosition.y = _playerController.transform.position.y;
    //        _playerController.transform.LookAt(targetPosition);
    //    }
    //}
}
