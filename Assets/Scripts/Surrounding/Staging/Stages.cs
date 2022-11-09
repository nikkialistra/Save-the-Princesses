using System.Collections.Generic;

namespace Surrounding.Staging
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

        public void StartFirstStage()
        {
            var stage = _stageFactory.Create();
            _stages.Add(stage);

            stage.Initialize();

            CurrentStage = stage;
        }

        public void Dispose()
        {
            foreach (var stage in _stages)
                stage.Dispose();
        }
    }
}
