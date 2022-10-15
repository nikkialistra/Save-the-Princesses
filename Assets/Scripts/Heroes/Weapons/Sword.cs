﻿using System;
using Combat.Attacks;
using Combat.Weapons;
using UnityEngine;
using static Combat.Attacks.StrokeType;

namespace Heroes.Weapons
{
    public class Sword : ConcreteWeapon
    {
        public override WeaponSpecs Specs => _specs;
        public override StrokeType LastStroke => _lastStroke;

        [SerializeField] private WeaponSpecs _specs;
        [SerializeField] private float _timeToContinueSeries = 0.75f;

        private StrokeType _lastStroke = Second;

        private float TimeFromLastStroke => Time.time - _timeOfLastStroke;

        private float _timeOfLastStroke = float.NegativeInfinity;

        public override bool TryStroke()
        {
            var strokeStarted = _lastStroke switch
            {
                Second => TryStartNewSeries(),
                First => TryMakeSecondStroke(),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (strokeStarted)
                _timeOfLastStroke = Time.time;

            return strokeStarted;
        }

        public override void ResetStroke()
        {
            _lastStroke = Second;
        }

        private bool TryStartNewSeries()
        {
            if (TimeFromLastStroke < _specs.AttackSpeed)
                return false;

            SetLastStrokeTo(First);
            return true;
        }

        private bool TryMakeSecondStroke()
        {
            if (TimeFromLastStroke < _specs.StrokeSpeed)
                return false;

            if (TimeFromLastStroke < _timeToContinueSeries)
                SetLastStrokeTo(Second);
            else
                TryStartNewSeries();

            return true;
        }

        private void SetLastStrokeTo(StrokeType stroke)
        {
            _lastStroke = stroke;
        }
    }
}