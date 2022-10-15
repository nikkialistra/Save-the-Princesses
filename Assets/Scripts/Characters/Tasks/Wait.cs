using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Characters.Tasks
{
    [Category("Characters")]
    public class Wait : ActionTask
    {
        public float TimeMin = 0;
        public float TimeMax = 0;

        private float _waitTime;

        protected override void OnExecute()
        {
            _waitTime = Random.Range(TimeMin, TimeMax);
        }

        protected override void OnUpdate()
        {
            if (elapsedTime >= _waitTime)
                EndAction(true);
        }
    }
}
