using Controls;
using UI;

namespace Infrastructure.Controls
{
    public class GameControls
    {
        private readonly HeroesControl _heroesControl;
        private readonly GameInterfaceControl _gameInterfaceControl;
        private readonly InputControl _inputControl;

        public GameControls(HeroesControl heroesControl, GameInterfaceControl gameInterfaceControl, InputControl inputControl)
        {
            _heroesControl = heroesControl;
            _gameInterfaceControl = gameInterfaceControl;
            _inputControl = inputControl;
        }

        public void Initialize()
        {
            _heroesControl.Initialize();
            _gameInterfaceControl.Initialize(_heroesControl);
            _inputControl.Initialize(_heroesControl);
        }

        public void Dispose()
        {
            _heroesControl.Dispose();
            _gameInterfaceControl.Dispose();
            _inputControl.Dispose();
        }
    }
}
