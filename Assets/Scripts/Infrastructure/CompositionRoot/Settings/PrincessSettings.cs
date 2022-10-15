using System;
using UnityEngine;

namespace Infrastructure.CompositionRoot.Settings
{
    [Serializable]
    public class PrincessSettings
    {
        public int HitsToUntie => _hitsToUntie;

        public float DistanceToHeroToMove => _distanceToHero;
        public float DistanceBetweenPrincessesToMove => _distanceBetweenPrincesses;

        public float DistanceToHeroToStop => _distanceToHero - _additionalDistanceBeforeStop;
        public float DistanceBetweenPrincessesToStop => _distanceBetweenPrincesses - _additionalDistanceBeforeStop;

        public float DistancePercentForRegularSpeed => _distancePercentForRegularSpeed;
        public float DistancePercentForHeroSpeed => _distancePercentForHeroSpeed;
        public float DistancePercentForElevatedSpeed => _distancePercentForElevatedSpeed;

        public float AccelerationTime => _accelerationTime;
        public float DecelerationTime => _decelerationTime;

        public float DistanceToFinishLinking => _distanceToFinishLinking;

        [SerializeField] private int _hitsToUntie = 3;

        [Space]
        [SerializeField] private float _distanceToHero = 0.7f;
        [SerializeField] private float _distanceBetweenPrincesses = 0.6f;
        [SerializeField] private float _additionalDistanceBeforeStop = 0.12f;

        [Space]
        [SerializeField] private float _distancePercentForRegularSpeed = 0.8f;
        [SerializeField] private float _distancePercentForHeroSpeed = 0.8f;
        [SerializeField] private float _distancePercentForElevatedSpeed = 0.8f;

        [Space]
        [SerializeField] private float _accelerationTime = 0.1f;
        [SerializeField] private float _decelerationTime = 0.1f;

        [Space]
        [SerializeField] private float _distanceToFinishLinking = 0.8f;
    }
}
