using Combat.Weapons;
using GameSystems;
using GameSystems.Parameters;
using Heroes;
using Heroes.Services;

namespace Infrastructure.Controls
{
    public class HeroesControl
    {
        public Hero First { get; private set; }
        public Hero Second { get; private set; }

        private readonly GameParameters _gameParameters;
        private readonly HeroFactory _heroFactory;
        private readonly HeroClosestFinder _heroClosestFinder;

        public HeroesControl(HeroFactory heroFactory, HeroClosestFinder heroClosestFinder, GameParameters gameParameters)
        {
            _heroFactory = heroFactory;
            _heroClosestFinder = heroClosestFinder;
            _gameParameters = gameParameters;
        }

        public void Initialize()
        {
            if (_gameParameters.GameMode == GameMode.Single)
                InitializeForSingle();
            else
                InitializeForCoop();
        }

        public void Dispose()
        {
            First.Dispose();

            if (Second != null)
                Second.Dispose();
        }

        private void InitializeForSingle()
        {
            First = _heroFactory.CreateWith(WeaponType.None);

            _heroClosestFinder.FillForSingle(First);
        }

        private void InitializeForCoop()
        {
            First = _heroFactory.CreateWith(WeaponType.None);
            Second = _heroFactory.CreateWith(WeaponType.None);

            _heroClosestFinder.FillForCoop(First, Second);
        }
    }
}
