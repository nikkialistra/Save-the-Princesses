using Heroes;
using Saving.Progress.Dungeon;
using UI.HealthBar;
using UI.Stats;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(HealthBarView))]
    [RequireComponent(typeof(HealthBarDigitsView))]
    [RequireComponent(typeof(GoldView))]
    public class GameInterfaceControl : MonoBehaviour
    {
        private HealthBarView _healthBarView;
        private HealthBarDigitsView _healthBarDigitsView;
        private AmmoView _ammoView;
        private StatsView _statsView;
        private GoldView _goldView;

        private Hero _hero;

        private VisualElement _uiRoot;

        [Inject]
        public void Construct(Hero hero)
        {
            _hero = hero;
        }

        public void Initialize()
        {
            FillComponents();

            _healthBarDigitsView.Initialize();
            _healthBarView.Initialize(_hero.Health);
            _ammoView = new AmmoView(_uiRoot, _hero.Collector.Ammo);
            _statsView = new StatsView(_uiRoot, _hero);
            _goldView.Initialize(_hero.Collector.Gold);
        }

        public void Dispose()
        {
            _healthBarView.Dispose();
            _ammoView.Dispose();
            _statsView.Dispose();
            _goldView.Dispose();
        }

        public void LoadProgress(DungeonProgress progress)
        {

        }

        private void FillComponents()
        {
            _uiRoot = GetComponent<UIDocument>().rootVisualElement;

            _healthBarView = GetComponent<HealthBarView>();
            _healthBarDigitsView = GetComponent<HealthBarDigitsView>();
            _goldView = GetComponent<GoldView>();
        }
    }
}
