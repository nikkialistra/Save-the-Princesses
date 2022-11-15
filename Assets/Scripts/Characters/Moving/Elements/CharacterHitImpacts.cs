using Characters.Moving.HitImpacting;
using Characters.Stats;
using UnityEngine;

namespace Characters.Moving.Elements
{
    public class CharacterHitImpacts
    {
        public bool HasImpact { get; private set; }

        private readonly AllStats _stats;

        private Vector2 _knockback;
        private float _stun;

        public CharacterHitImpacts(AllStats stats)
        {
            _stats = stats;
        }

        public void Take(TakenHitImpact hitImpact)
        {
            TakeKnockback(hitImpact.Direction, hitImpact.Knockback);
            TakeStun(hitImpact.Stun);

            HasImpact = true;
        }

        public HitImpacts Transfer()
        {
            var hitImpacts = new HitImpacts
            {
                Knockback = _knockback,
                Stun = _stun
            };

            _knockback = Vector2.zero;
            _stun = 0;

            HasImpact = false;

            return hitImpacts;
        }

        private void TakeKnockback(Vector2 direction, float value)
        {
            var notAbsorbedAmount = value - _stats.KnockbackStand;

            if (notAbsorbedAmount <= 0) return;

            _knockback += direction * notAbsorbedAmount;
        }

        private void TakeStun(float value)
        {
            var notAbsorbedAmount = value - _stats.StunResistance;

            if (notAbsorbedAmount <= 0) return;

            _stun += notAbsorbedAmount;
        }
    }
}
