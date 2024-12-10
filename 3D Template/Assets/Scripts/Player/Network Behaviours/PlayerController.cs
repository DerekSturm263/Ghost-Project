using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NetworkTransform), typeof(NetworkRigidbody), typeof(NetworkAnimator))]
public class PlayerController : NetworkBehaviour
{
    private LocalPlayer<Controls> _player;
    public ref LocalPlayer<Controls> Player => ref _player;

    private Rigidbody _rb;
    private Animator _anim;
    private Camera _head;

    private float _movementTime;
    private bool _isRunning;
    private bool _isCrouching;
    private Vector2 _turnRot;

    [SerializeField] private SpeedSettings _speedSettings;
    [SerializeField] private HeightSettings _heightSettings;
    [SerializeField] private FOVSettings _fovSettings;
    [SerializeField] private TurnSettings _turnSettings;
    [SerializeField] private JumpSettings _jumpSettings;
    [SerializeField] private FightSettings _fightSettings;
    [SerializeField] private CaptureSettings _captureSettings;

    [SerializeField] private CastHelper _groundCheck;
    private bool _isGrounded;

    [SerializeField] private UnityEvent _onStartRunning;
    [SerializeField] private UnityEvent _onEndRunning;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _head = GetComponentInChildren<Camera>();

        _player = new(null);

        if (true || IsOwner)
        {
            _player.Controls.Player.Run.started += StartRun;
            _player.Controls.Player.Run.canceled += EndRun;
            _player.Controls.Player.Crouch.started += StartCrouch;
            _player.Controls.Player.Crouch.canceled += EndCrouch;
            _player.Controls.Player.Jump.started += StartJump;
            _player.Controls.Player.Jump.canceled += EndJump;
            _player.Controls.Player.Fight.performed += Fight;
            _player.Controls.Player.Capture.started += StartCapture;
            _player.Controls.Player.Capture.canceled += EndCapture;
        }
    }

    private void Update()
    {
        Move(_player.Controls.Player.Move.ReadValue<Vector2>());
        Turn(_player.Controls.Player.Turn.ReadValue<Vector2>());

        _isGrounded = _groundCheck.GetHitInfo(transform).HasValue;

        _head.fieldOfView = Mathf.Lerp(_head.fieldOfView, _fovSettings.Evaluate(_isCrouching, _isRunning), Time.deltaTime * _fovSettings.FOVChangeSpeed);
    }

    private void OnDrawGizmos()
    {
        _groundCheck.Draw(transform);
    }

    public void Move(InputAction.CallbackContext ctx) => Move(ctx.ReadValue<Vector2>());

    public void Move(Vector2 amount)
    {
        if (amount == Vector2.zero)
            _movementTime = 0;
        else
            _movementTime += Time.deltaTime;

        Vector3 adjustedAmount;
        Vector3 velocity;

        //if (_isGrounded)
        //{
            adjustedAmount = _head.transform.forward * amount.y + _head.transform.right * amount.x;
        //}
        //else
        //{
            //adjustedAmount = Vector3.Lerp(_rb.velocity, _head.transform.forward * amount.y + _head.transform.right * amount.x, Time.deltaTime * 5);
        //}

        velocity = adjustedAmount * _speedSettings.Evaluate(_isRunning, _isCrouching);
        velocity.y = _rb.velocity.y;

        _rb.velocity = velocity;
    }

    public void Turn(InputAction.CallbackContext ctx) => Turn(ctx.ReadValue<Vector2>());

    public void Turn(Vector2 amount)
    {
        _turnRot = _turnSettings.Evaluate(amount, _turnRot);

        _head.transform.rotation = Quaternion.Euler(-_turnRot.x, _turnRot.y, 0);
        _head.transform.localPosition = new(0, _heightSettings.Evaluate(_isCrouching, _movementTime, _rb.velocity.magnitude), 0);
    }

    public void StartRun(InputAction.CallbackContext ctx)
    {
        Debug.Log("Starting run!");

        _isRunning = true;
        _onStartRunning.Invoke();
    }

    public void EndRun(InputAction.CallbackContext ctx)
    {
        Debug.Log("Ending run!");

        _isRunning = false;
        _onEndRunning.Invoke();
    }

    public void StartCrouch(InputAction.CallbackContext ctx)
    {
        Debug.Log("Starting crouch!");

        _isCrouching = true;
    }

    public void EndCrouch(InputAction.CallbackContext ctx)
    {
        Debug.Log("Ending crouch!");

        _isCrouching = false;
    }

    public void StartJump(InputAction.CallbackContext ctx)
    {
        if (!_isGrounded)
            return;

        Debug.Log("Starting jump!");

        _rb.AddForce(Vector3.up * _jumpSettings.Evaluate(), ForceMode.Impulse);
    }

    public void EndJump(InputAction.CallbackContext ctx)
    {
        Debug.Log("Ending jump!");
    }

    public void Fight(InputAction.CallbackContext ctx)
    {
        Debug.Log("Fighting!");

        _fightSettings.Evaluate(_anim);
    }

    public void StartCapture(InputAction.CallbackContext ctx)
    {
        Debug.Log("Starting capture!");

        _captureSettings.Evaluate(_anim, true);
    }

    public void EndCapture(InputAction.CallbackContext ctx)
    {
        Debug.Log("Ending capture!");

        _captureSettings.Evaluate(_anim, false);
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _player.Enable();
    }

    private void OnDisable()
    {
        _player.Disable();
        Cursor.lockState = CursorLockMode.None;
    }
}
