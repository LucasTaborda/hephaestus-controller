using System.Collections.Generic;
using UnityEngine;

namespace Hephaestus
{
    public class ButtonAction : MonoBehaviour, IInputAction
    {
        protected delegate void MethodSubscriptor();
        protected Dictionary<string, MethodSubscriptor> _onButtonDownEvents;
        protected Dictionary<string, MethodSubscriptor> _onButtonUpEvents;


        protected virtual void Update()
        {
            GetInputs();
        }


        private void GetInputs()
        {
            GetButtonsDown();
            GetButtonsUp();
        }


        private void GetButtonsDown()
        {
            foreach (var eventButton in _onButtonDownEvents)
            {
                if (Input.GetButtonDown(eventButton.Key))
                    eventButton.Value();
            }
        }


        private void GetButtonsUp()
        {
            foreach (var eventButton in _onButtonUpEvents)
            {
                if (Input.GetButtonUp(eventButton.Key))
                    eventButton.Value();
            }
        }
    }
}
