using System.Collections.Generic;
using UnityEngine;

public class InputAction : MonoBehaviour
{
    [SerializeField] protected string[] _inputsNames;
    protected Dictionary<string, float> _inputs = new Dictionary<string, float>();


    protected virtual void Awake()
    {
        foreach (var name in _inputsNames)
        {
            _inputs.Add(name, 0);
        }
    }


    protected virtual void Update()
    {
        GetAxis();
    }


    private void GetAxis()
    {
        foreach (var name in _inputsNames)
        {
            _inputs[name] = Input.GetAxisRaw(name);
        }
    }
}
