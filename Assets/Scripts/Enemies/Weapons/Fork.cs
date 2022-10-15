using Combat.Attacks;
using Combat.Weapons;
using UnityEngine;
using static Combat.Attacks.StrokeType;

namespace Enemies.Weapons
{
    public class Fork : ConcreteWeapon
    {
        public override WeaponSpecs Specs => _specs;
        public override StrokeType LastStroke => First;

        [SerializeField] private WeaponSpecs _specs;

        private float TimeFromLastStroke => Time.time - _timeOfLastStroke;

        private float _timeOfLastStroke = float.NegativeInfinity;

        public override bool TryStroke()
        {
            var strokeStarted = TimeFromLastStroke >= _specs.AttackSpeed;

            if (strokeStarted)
                _timeOfLastStroke = Time.time;

            return strokeStarted;
        }

        public override void ResetStroke()
        {

        }
    }
}
