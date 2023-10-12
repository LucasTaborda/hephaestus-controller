# Hephaestus Controller

Forma modular de crear un controller para unity.

La idea es que en lugar de tener un componente Controller que abarque todo, tener un componente para cada acción que pueda realizar el jugador. 

## Cómo se usa

### Si tu acción necesita de axis inputs (Por ejemplo movimientos)

1. Crear un componente que herede de AxisAction.
2. Asignar el componente que creaste a tu gameObject del player
3. En el campo Axises To Get  del componente mostrado en el inspector, asignar los nombres de los inputs que va a esperar recibir (Deben estar definidos en los axis del Input Manager).
4. Dentro del componente acceder a los axis de la siguiente manera: `_inputs["Nombre del axis"]`.

### Si tu acción necesita Eventos de botón presionado o levantado (Por ejemplo disparos)

1. Crear un componente que herede de ButtonAction.
2. Asignar el componente que creaste a tu gameObject del player
3. Suscribir un método al evento requerido de las siguientes maneras (dentro del componente que creaste):

**Button down:** `_onButtonDownEvents.Add("Fire1", MiMetodo);`

**Button up:** `_onButtonUpEvents.Add("Fire1", MiOtroMetodo);`

#### Aclaración

Si se necesita utilizar el método Update de Unity, se debe overridear utilizando `base.Update()` o Hephaestus no funcionará correctamente.


## Ejemplo de clase Movimiento utilizando Hephaestus:

```cs
using UnityEngine;
using Hephaestus;

public class Movement : AxisAction
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private GameObject _body;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationTime;

    private float _diagonalSpeed;
    private Vector3 _direction;
    private Vector3 _lastDirection;
    private bool _isRotating;

    private string _horizontal => _axisesToGet[0];
    private string _vertical => _axisesToGet[1];

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
        float currentSpeed;

        if(_direction.x != 0 && _direction.z != 0)
            currentSpeed = _diagonalSpeed;
        else
            currentSpeed = _movementSpeed; 

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

        var currentRotation = Quaternion.Lerp(
            _body.transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );

        _body.transform.rotation = currentRotation;

        var dot = Quaternion.Dot(_body.transform.rotation, targetRotation);
        var progress = Mathf.InverseLerp(0, 1, dot);
        if (progress >= 1) _isRotating = false;
    }
}
```
