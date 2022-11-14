using System.Collections.Generic;
using GameData.Settings;
using UnityEngine;

namespace Princesses
{
    public class PrincessActualVelocity
    {
        public float Value => (_princess.Position - _lastPositions.Peek()).magnitude / GameSettings.Princess.TimeIntervalForStuckCheck;

        private readonly Queue<Vector2> _lastPositions;
        private readonly int _positionsCount;

        private readonly Princess _princess;

        public PrincessActualVelocity(Princess princess)
        {
            _princess = princess;

            var updatesInSecond = 1f / Time.fixedDeltaTime;
            _positionsCount = (int)(updatesInSecond * GameSettings.Princess.TimeIntervalForStuckCheck);

            _lastPositions = new Queue<Vector2>(_positionsCount);
        }

        public void FixedTick()
        {
            _lastPositions.Enqueue(_princess.Position);

            if (_lastPositions.Count > _positionsCount)
                _lastPositions.Dequeue();
        }
    }
}
