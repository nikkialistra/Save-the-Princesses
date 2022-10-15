using System;
using UnityEngine;

namespace Surrounding
{
    public class ZoneTrigger : MonoBehaviour
    {
        public event Action Enter;
        public event Action Exit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Enter?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Exit?.Invoke();
        }
    }
}
