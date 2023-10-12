using System.Collections.Generic;
using UnityEngine;

namespace Hephaestus
{
    public abstract class AxisAction : MonoBehaviour, IInputAction
    {
        [SerializeField] protected string[] _axisesToGet;
        protected Dictionary<string, float> _inputs = new();


        void Awake()
        {
            foreach (var name in _axisesToGet)
            {
                _inputs.Add(name, 0);
            }
        }


        protected virtual void Update()
        {
            GetInputs();
        }


        private void GetInputs()
        {
            foreach (var name in _axisesToGet)
            {
                _inputs[name] = Input.GetAxisRaw(name);
            }
        }
    }
}
