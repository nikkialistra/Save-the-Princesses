using Characters.Stats;
using Characters.Stats.Character;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(CharacterStats))]
    public class CharacterHitsImpact : MonoBehaviour
    {
        public bool HasImpact { get; private set; }

        private CharacterStats _stats;

        private Vector2 _knockback;
        private float _stun;

        public void Initialize()
        {
            _stats = GetComponent<CharacterStats>();
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
            var notAbsorbedAmount = value - _stats.KnockbackStand.Value;

            if (notAbsorbedAmount <= 0) return;

            _knockback += direction * notAbsorbedAmount;
        }

        private void TakeStun(float value)
        {
            var notAbsorbedAmount = value - _stats.StunResistance.Value;

            if (notAbsorbedAmount <= 0) return;

            _stun += notAbsorbedAmount;
        }
    }
}
