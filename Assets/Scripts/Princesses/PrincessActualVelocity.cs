using System.Collections.Generic;
using UnityEngine;

namespace Princesses
{
    public class PrincessActualVelocity : MonoBehaviour
    {
        public float Value => ((Vector2)transform.position - _lastPositions.Peek()).magnitude / _timeInterval;

        [SerializeField] private float _timeInterval = 0.06f;

        private Queue<Vector2> _lastPositions;
        private int _positionsCount;

        public void Initialize()
        {
            var updatesInSecond = 1f / Time.fixedDeltaTime;
            _positionsCount = (int)(updatesInSecond * _timeInterval);

            _lastPositions = new Queue<Vector2>(_positionsCount);
        }

        private void FixedUpdate()
        {
            _lastPositions.Enqueue(transform.position);

            if (_lastPositions.Count > _positionsCount)
                _lastPositions.Dequeue();
        }
    }
}
