using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Enemies.Tasks
{
    [Category("Enemies")]
    public class IsHeroAtViewDistance : ConditionTask<Enemy>
    {
        protected override bool OnCheck()
        {
            var distance = (agent.Hero.Position - agent.Position).magnitude;

            return distance <= agent.ViewDistance;
        }
    }
}
