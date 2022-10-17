﻿using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Infrastructure.CompositionRoot.Settings
{
    [Serializable]
    public class HeroSettings
    {
        public float InversionAngle => _inversionAngle;
        public float MinTimeToChangeInversion => _minTimeToChangeInversion;
        public float InversionFlickeringDelta => _inversionFlickeringDelta;

        public float HitInvulnerabilityTime => _hitInvulnerabilityTime;

        public float ButtonPressToleranceTime => _buttonPressToleranceTime;

        public float RescanRate => _rescanRate;
        public float DistanceToGather => _distanceToGather;

        [Title("Inversion")]
        [SerializeField] private float _inversionAngle = 90f;
        [SerializeField] private float _minTimeToChangeInversion = 0.02f;
        [SerializeField] private float _inversionFlickeringDelta = 0.5f;

        [Space]
        [SerializeField] private float _hitInvulnerabilityTime = 1f;

        [Space]
        [SerializeField] private float _buttonPressToleranceTime = 0.05f;

        [Title("Princess Gathering")]
        [SerializeField] private float _rescanRate = 0.1f;
        [SerializeField] private float _distanceToGather = 1.2f;
    }
}