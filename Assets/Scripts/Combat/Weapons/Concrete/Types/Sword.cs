﻿using System;
using Combat.Weapons.Enums;
using GameData.Weapons;
using UnityEngine;
using static Combat.StrokeType;

namespace Combat.Weapons.Concrete.Types
{
    public class Sword : ConcreteWeapon
    {
        public override WeaponType Type => WeaponType.Sword;
        public override StrokeType LastStroke => _lastStroke;
        public override float AttackEndTime { get; set;  }

        [SerializeField] private float _timeToContinueSeries = 0.75f;

        private StrokeType _lastStroke = Second;

        private float TimeFromLastStroke => Time.time - _timeOfLastStroke;

        private WeaponSpecs _specs;

        private float _timeOfLastStroke = float.NegativeInfinity;

        public override void Initialize(WeaponSpecs specs)
        {
            _specs = specs;
        }

        public override bool TryStroke()
        {
            var strokeStarted = _lastStroke switch
            {
                Second => TryStartNewSeries(),
                First => TryMakeSecondStroke(),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (strokeStarted)
            {
                _timeOfLastStroke = Time.time;
                AttackEndTime = _timeOfLastStroke + _specs.AttackSpeed;
            }

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