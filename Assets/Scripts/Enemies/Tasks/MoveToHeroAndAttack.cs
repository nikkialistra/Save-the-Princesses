using Characters;
using Characters.Moving;
using Characters.Moving.Elements;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Enemies.Tasks
{
    [Category("Enemies")]
    public class MoveToHeroAndAttack : ActionTask<Enemy>
    {
        private CharacterMoving _moving;
        private EnemyAttacker _attacker;

        protected override string OnInit()
        {
            _moving = agent.Moving;
            _attacker = agent.Attacker;

            return null;
        }

        protected override void OnUpdate()
        {
            if (TryMoveToHero()) return;

            _attacker.UpdateAttackRotation();
            _attacker.TryAttack();
        }

        private bool TryMoveToHero()
        {
            if ((agent.Hero.Position - agent.Position).magnitude <= _attacker.AttackDistance)
            {
                _moving.Stop();
                return false;
            }

            _moving.FindPathTo(agent.Hero.Position);
            return true;
        }
    }
}
