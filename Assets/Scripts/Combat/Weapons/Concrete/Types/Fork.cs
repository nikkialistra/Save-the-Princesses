using GameData.Weapons;
using UnityEngine;
using static Combat.StrokeType;

namespace Combat.Weapons.Concrete.Types
{
    public class Fork : ConcreteWeapon
    {
        public override StrokeType LastStroke => First;

        private float TimeFromLastStroke => Time.time - _timeOfLastStroke;

        private WeaponSpecs _specs;

        private float _timeOfLastStroke = float.NegativeInfinity;

        public override void Initialize(WeaponSpecs specs)
        {
            _specs = specs;
        }

        public override bool TryStroke()
        {
            var strokeStarted = TimeFromLastStroke >= _specs.AttackSpeed;

            if (strokeStarted)
                _timeOfLastStroke = Time.time;

            return strokeStarted;
        }

        public override void ResetStroke() { }
    }
}
