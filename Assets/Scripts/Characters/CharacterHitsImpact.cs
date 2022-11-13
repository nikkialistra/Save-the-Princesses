using Characters.Stats;
using UnityEngine;

namespace Characters
{
    public class CharacterHitsImpact
    {
        public bool HasImpact { get; private set; }

        private readonly AllStats _stats;

        private Vector2 _knockback;
        private float _stun;

        public CharacterHitsImpact(AllStats stats)
        {
            _stats = stats;
        }

        public void Take(Vector2 direction, float knockback, float stun)
        {
            TakeKnockback(direction, knockback);
            TakeStun(stun);

            HasImpact = true;
        }

        public (Vector2, float) Transfer()
        {
            var knockback = _knockback;
            var stun = _stun;

            _knockback = Vector2.zero;
            _stun = 0;

            HasImpact = false;

            return (knockback, stun);
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
