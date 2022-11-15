using Characters.Moving;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Characters.Tasks
{
    [Category("Characters")]
    public class HitImpacts : ActionTask<Character>
    {
        private CharacterMoving _moving;

        private float _stunTime;

        protected override string OnInit()
        {
            _moving = agent.Moving;

            return null;
        }

        protected override void OnExecute()
        {
            var hitImpacts = _moving.TransferHitImpacts();

            _stunTime = hitImpacts.Stun;

            if (_stunTime > 0)
                agent.Stun(true);

            if (hitImpacts.Knockback != Vector2.zero)
                _moving.Knockback(hitImpacts.Knockback);
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
