using Controls;
using Heroes;
using UI;

namespace Infrastructure.Controls
{
    public class GameControls
    {
        private readonly GameInterfaceControl _gameInterfaceControl;
        private readonly InputControl _inputControl;

        public GameControls(GameInterfaceControl gameInterfaceControl, InputControl inputControl)
        {
            _gameInterfaceControl = gameInterfaceControl;
            _inputControl = inputControl;
        }

        public void Initialize(Hero hero)
        {
            _gameInterfaceControl.Initialize(hero);
            _inputControl.Initialize(hero);
        }

        public void Dispose()
        {
            _gameInterfaceControl.Dispose();
            _inputControl.Dispose();
        }
    }
}
