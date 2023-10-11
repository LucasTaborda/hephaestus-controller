using UnityEngine;

public class Movement : InputAction
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private GameObject _body;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationTime;

    private float _diagonalSpeed;
    private Vector3 _direction;
    private Vector3 _lastDirection;
    private bool _isRotating;

    private string _horizontal => _inputsNames[0];
    private string _vertical => _inputsNames[1];

    public float rotationSpeed; 

    private void Start()
    {
        _diagonalSpeed = _movementSpeed * 0.7f;

    }

    protected override void Update()
    {
        base.Update();

        SetDirection();
        Move();
        Rotate();
    }


    private void Move()
    {
        var currentSpeed = (_direction.x != 0 && _direction.z != 0) ? _diagonalSpeed : _movementSpeed; 
        _controller.Move(_direction * currentSpeed * Time.deltaTime);
    }


    private void SetDirection()
    {
        _direction = new Vector3(_inputs[_horizontal], 0, _inputs[_vertical]).normalized;

        if (_direction.magnitude != 0)
        {
            _lastDirection = _direction;
            _isRotating = true;
        }
    }


    private void Rotate()
    {
        if (!_isRotating) return;

        var targetRotation = Quaternion.LookRotation(_lastDirection);
        var currentRotation = Quaternion.Lerp(_body.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        _body.transform.rotation = currentRotation;

        var progress = Mathf.InverseLerp(0, 1, Quaternion.Dot(_body.transform.rotation, targetRotation));
        if (progress >= 1) _isRotating = false;
    }
}
