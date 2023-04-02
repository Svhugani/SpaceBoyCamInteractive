using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private CharacterController charController;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float acceleration = .2f;
    [SerializeField] private float speedMultiplier = 1.3f;
    [SerializeField] private float rotationMultiplier = 100;
    [SerializeField] private float movementThreshold = .01f;
    [SerializeField] private AnimationCurve velocityResponse = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private InputAction _moveAction;
    private Vector3 _moveSpeed;
    private Vector2 _inputDir;
    private float _gravity;
    private void Awake()
    {
        _moveAction = playerInput.actions.FindAction("Move");
        
    }

    private void OnEnable()
    {
        //_moveAction.performed += MoveCharacter;
        //_moveAction.performed += MoveCharacter;
        

    }

    private void OnDisable()
    {
        //_moveAction.performed -= MoveCharacter;
        //_moveAction.performed -= MoveCharacter;
    }

    private void MoveCharacter(InputAction.CallbackContext context)
    {
        _inputDir = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        _inputDir =  _moveAction.ReadValue<Vector2>();
        _moveSpeed = Vector3.Lerp(_moveSpeed, new Vector3(_inputDir.x, 0, _inputDir.y), acceleration * Time.deltaTime);

        if (!charController.isGrounded) _gravity = -9.81f;
        else _gravity = 0;
        
        if (_moveSpeed.magnitude < movementThreshold || !_moveAction.inProgress)
        {
            animator.SetFloat("Velocity", 0);
            //animator.SetTime
        }

        else
        {
            this.transform.forward = Vector3.Slerp(this.transform.forward, -_moveSpeed, Time.deltaTime * rotationMultiplier);
            animator.SetFloat("Velocity", velocityResponse.Evaluate(_moveSpeed.magnitude));
        }
        



    }
    private void LateUpdate()
    {
        if (!_moveAction.inProgress) _inputDir = Vector2.zero;
    }

    private void OnAnimatorMove()
    {
        charController.Move(speedMultiplier * new Vector3(animator.deltaPosition.x, _gravity * Time.deltaTime, animator.deltaPosition.z));
    }
}
