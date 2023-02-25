using System;
using UnityEngine;

namespace Internal.Scripts
{
    public class GoalAreaController : MonoBehaviour
    {
        private Action _callback;

        public void Initialize(Action callback)
        {
            _callback = callback;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                _callback.Invoke();
            }
        }
    }
}