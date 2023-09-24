using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject cinecamera;
    public GameObject character;
    public Transform target;

    [HideInInspector]public Animator animator;
    public bool isRolling = false;
    public bool isAttacking = false;
    public bool isAim = false;

    private CharacterController _controller;
    private CameraController _cameraController;
    
    private Vector3 _playerVelocity;
    private Vector3 _moveDirection;
    private Vector3 _cameraForward;

    private bool _groundedPlayer;
    private float _playerSpeed = 10.0f;
    private float _rollSpeed = 6f;
    private float _jumpHeight = 2.0f;
    private float _gravityValue = -20f;

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _cameraController = cinecamera.GetComponent<CameraController>();
        animator = character.GetComponent<Animator>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_groundedPlayer)
            {
                _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
                animator.SetBool("isJump", true);
            }
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        print("¿õÅ©¸®±â");
    }
    public void OnRolling(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRolling = true;
            animator.SetTrigger("isRoll");
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        _cameraController.OnLook(context);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        _cameraController.OnAiming(context);
        if (context.started)
        {
            animator.SetBool("isAiming", true);
            isAim = true;
        }
        if (context.canceled)
        {
            animator.SetBool("isAiming", false);
            isAim = false;
        }
    }

    public void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
            animator.SetBool("isJump", false);
        }
        if (!isRolling)
        {
            Vector3 move = target.transform.forward * _moveDirection.z + target.transform.right * _moveDirection.x;
            move.y = 0;
            if (_moveDirection != Vector3.zero && !isAim)
            {
                animator.SetBool("isMove", true);
                _cameraForward = move.normalized;
                character.transform.forward = _cameraForward;
            }
            else if (isAim)
            {
                _cameraForward = target.transform.forward;
                character.transform.forward = _cameraForward;
            }
            else
            {
                animator.SetBool("isMove", false);
            }


            _controller.Move(move * Time.deltaTime * _playerSpeed);
        }
        else
        {
            _controller.Move(character.transform.forward * Time.deltaTime * _rollSpeed);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}
