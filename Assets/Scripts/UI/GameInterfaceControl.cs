using Heroes;
using Saving.Progress.Dungeon;
using UI.HealthBar;
using UI.Stats;
using UnityEngine;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(HealthBarView))]
    [RequireComponent(typeof(HealthBarDigitsView))]
    [RequireComponent(typeof(AmmoView))]
    [RequireComponent(typeof(StatsView))]
    [RequireComponent(typeof(GoldView))]
    public class GameInterfaceControl : MonoBehaviour
    {
        private HealthBarView _healthBarView;
        private HealthBarDigitsView _healthBarDigitsView;
        private AmmoView _ammoView;
        private StatsView _statsView;
        private GoldView _goldView;

        private Hero _hero;

        [Inject]
        public void Construct(Hero hero)
        {
            _hero = hero;
        }

        public void Initialize()
        {
            FillComponents();

            _healthBarDigitsView.Initialize();
            _healthBarView.Initialize(_hero);
            _ammoView.Initialize();
            _statsView.Initialize(_hero);
            _goldView.Initialize();
        }

        public void Dispose()
        {
            _healthBarView.Dispose();
            _statsView.Dispose();
        }

        public void LoadProgress(DungeonProgress progress)
        {

        }

        private void FillComponents()
        {
            _healthBarView = GetComponent<HealthBarView>();
            _healthBarDigitsView = GetComponent<HealthBarDigitsView>();
            _ammoView = GetComponent<AmmoView>();
            _statsView = GetComponent<StatsView>();
            _goldView = GetComponent<GoldView>();
        }
    }
}
