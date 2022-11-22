using Combat.Weapons;
using GameSystems;
using GameSystems.Parameters;
using Heroes;
using Heroes.Services;
using Zenject;

namespace Infrastructure.Controls
{
    public class HeroesControl : ITickable, IFixedTickable
    {
        public Hero First { get; private set; }
        public Hero Second { get; private set; }

        private bool IsCoop => _gameParameters.GameMode == GameMode.Coop;

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
            First.Train.Dispose();
            First.Dispose();

            if (Second != null)
            {
                Second.Train.Dispose();
                Second.Dispose();
            }
        }

        public void Tick()
        {
            First.Tick();

            if (IsCoop)
                Second.Tick();
        }

        public void FixedTick()
        {
            First.FixedTick();

            if (IsCoop)
                Second.FixedTick();
        }

        private void InitializeForSingle()
        {
            First = _heroFactory.CreateWith(WeaponType.NoWeapon, "Single Hero");

            _heroClosestFinder.FillForSingle(First);
        }

        private void InitializeForCoop()
        {
            First = _heroFactory.CreateWith(WeaponType.NoWeapon, "First Hero");
            Second = _heroFactory.CreateWith(WeaponType.NoWeapon, "Second Hero");

            _heroClosestFinder.FillForCoop(First, Second);
        }
    }
}
