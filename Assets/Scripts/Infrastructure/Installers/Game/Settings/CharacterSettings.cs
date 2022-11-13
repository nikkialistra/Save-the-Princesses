using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Infrastructure.Installers.Game.Settings
{
    [Serializable]
    public class CharacterSettings
    {
        public float AccelerationTime => _accelerationTime;
        public float DecelerationTime => _decelerationTime;

        public float VelocityDelta => _velocityDelta;
        public float DirectionChangeTime => _directionChangeTime;
        public float DirectionChangeTimeAlternative => _directionChangeTimeAlternative;

        public float TimeAtKnockback => _timeAtKnockback;
        public float InvulnerabilityTimeAfterHit => _invulnerabilityTimeAfterHit;

        public float DestinationDistanceDelta => _destinationDistanceDelta;
        public float DestinationDistanceDeltaPathfinding => _destinationDistanceDeltaPathfinding;
        public float RepathRate => _repathRate;
        public int DestinationSearchTriesCount => _destinationSearchTriesCount;

        public float HitBlinkingTime => _hitBlinkingTime;

        [Title("Movement")]
        [SerializeField] private float _accelerationTime = 0.2f;
        [SerializeField] private float _decelerationTime = 0.1f;

        [Space]
        [SerializeField] private float _velocityDelta = 0.01f;
        [SerializeField] private float _directionChangeTime = 0.1f;
        [SerializeField] private float _directionChangeTimeAlternative = 0.02f;

        [Title("Combat")]
        [SerializeField] private float _timeAtKnockback = 0.4f;
        [SerializeField] private float _invulnerabilityTimeAfterHit = 0.3f;

        [Title("Pathfinding")]
        [SerializeField] private float _destinationDistanceDelta = 0.2f;
        [SerializeField] private float _destinationDistanceDeltaPathfinding = 0.6f;
        [SerializeField] private float _repathRate = 0.2f;
        [SerializeField] private int _destinationSearchTriesCount = 6;

        [Space]
        [SerializeField] private float _hitBlinkingTime = 0.3f;
    }
}
