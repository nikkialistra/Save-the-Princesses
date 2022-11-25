using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Staging
{
    public class Stages
    {
        public Stage CurrentStage { get; private set; }

        private readonly List<Stage> _stages = new();

        private readonly Stage.Factory _stageFactory;

        public Stages(Stage.Factory stageFactory)
        {
            _stageFactory = stageFactory;
        }

        public async UniTask StartFirstStage()
        {
            var stage = _stageFactory.Create();
            _stages.Add(stage);

            await stage.Initialize(StageType.RuinedDungeon);

            CurrentStage = stage;
        }

        public void Dispose()
        {
            foreach (var stage in _stages)
                stage.Dispose();
        }
    }
}
