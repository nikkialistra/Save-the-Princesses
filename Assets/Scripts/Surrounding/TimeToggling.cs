using System;
using UnityEngine;

namespace Surrounding
{
    public class TimeToggling : MonoBehaviour
    {
        public event Action<bool> PauseChange;

        private bool _paused;

        public void Pause()
        {
            if (_paused) return;

            _paused = true;
            Time.timeScale = 0;

            PauseChange?.Invoke(_paused);
        }

        public void Resume()
        {
            if (!_paused) return;

            _paused = false;
            Time.timeScale = 1;

            PauseChange?.Invoke(_paused);
        }
    }
}
