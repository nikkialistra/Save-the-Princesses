using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Characters.Tasks
{
    [Category("Characters")]
    public class HitsImpact : ActionTask<Character>
    {
        private CharacterHitsImpact _hitsImpact;
        private CharacterMoving _moving;

        private float _stunTime;

        protected override string OnInit()
        {
            _hitsImpact = agent.HitsImpact;
            _moving = agent.Moving;

            return null;
        }

        protected override void OnExecute()
        {
            var (knockback, stun) = _hitsImpact.Transfer();
            _stunTime = stun;

            if (_stunTime > 0)
                agent.Stun(true);

            if (knockback != Vector2.zero)
                _moving.Knockback(knockback);
        }

        protected override void OnUpdate()
        {
            if (elapsedTime > _stunTime)
            {
                agent.Stun(false);
                EndAction(true);
            }
        }
    }
}
