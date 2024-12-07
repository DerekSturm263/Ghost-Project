using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NetworkTransform), typeof(NetworkRigidbody), typeof(NetworkAnimator))]
public class PlayerController : NetworkBehaviour
{
    private LocalPlayer<Controls> _player;
    public ref LocalPlayer<Controls> Player => ref _player;

    private Rigidbody _rb;
    private Animator _anim;
    private Camera _head;

    [SerializeField] private MovementSettings _movementSettings;
    private Vector2 _movementAmount;
    private MovementSettings.PhysicsState _physicsState;
    private float _movementLerp;
    private float _movementTime;

    [SerializeField] private TurnSettings _turnSettings;
    private Vector2 _turnRot;

    [SerializeField] private SpeedSettings _speedSettings;
    private bool _isRunning;

    [SerializeField] private HeightSettings _heightSettings;
    private bool _isCrouching;

    [SerializeField] private JumpSettings _jumpSettings;
    private float _originalY;
    private float _jumpTime;

    [SerializeField] private FightSettings _fightSettings;
    [SerializeField] private CaptureSettings _captureSettings;

    [SerializeField] private CastHelper _groundCheck;
    private bool _isGrounded;

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
        //_player.Controls.Player.Move.started += StartMove;
        //_player.Controls.Player.Move.canceled += EndMove;
        Move(_player.Controls.Player.Move.ReadValue<Vector2>());
        Turn(_player.Controls.Player.Turn.ReadValue<Vector2>());

        _isGrounded = _groundCheck.GetHitInfo(transform).HasValue;

        if (_jumpTime > 0)
            UpdateJump();
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

        Debug.Log($"Moving by {amount}");

        Vector3 adjustedAmount = _head.transform.forward * amount.y + _head.transform.right * amount.x;
        adjustedAmount.y = 0;

        _rb.velocity = adjustedAmount * _speedSettings.Evaluate(_isRunning, _isCrouching);
    }

    public void Turn(InputAction.CallbackContext ctx) => Turn(ctx.ReadValue<Vector2>());

    public void Turn(Vector2 amount)
    {
        Debug.Log($"Turning by {amount}");

        _turnRot = _turnSettings.Evaluate(amount, _turnRot);

        _head.transform.rotation = Quaternion.Euler(-_turnRot.x, _turnRot.y, 0);
        _head.transform.localPosition = new(0, _heightSettings.Evaluate(_isCrouching, _movementTime, _rb.velocity.magnitude), 0);
    }

    public void StartRun(InputAction.CallbackContext ctx)
    {
        Debug.Log("Starting run!");

        _isRunning = true;
    }

    public void EndRun(InputAction.CallbackContext ctx)
    {
        Debug.Log("Ending run!");

        _isRunning = false;
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
        Debug.Log("Starting jump!");

        _jumpTime = 0.01f;
        _originalY = transform.position.y;
    }

    public void UpdateJump()
    {
        Debug.Log("Updating jump!");

        _jumpTime += Time.deltaTime;
        _rb.position.Set(_rb.position.x, _jumpSettings.Evaluate(_jumpTime), _rb.position.z);
    }

    public void EndJump(InputAction.CallbackContext ctx)
    {
        Debug.Log("Ending jump!");

        _jumpTime = 0;
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
