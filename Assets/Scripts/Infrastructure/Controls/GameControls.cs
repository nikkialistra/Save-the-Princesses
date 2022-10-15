using Controls;
using Heroes.Attacks;
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

        public void Initialize(HeroAttacker heroAttacker)
        {
            _gameInterfaceControl.Initialize();
            _inputControl.Initialize(heroAttacker);
        }

        public void Dispose()
        {
            _gameInterfaceControl.Dispose();
            _inputControl.Dispose();
        }
    }
}
