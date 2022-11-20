using Infrastructure.Controls;
using Saving.Progress.Dungeon;
using UI.HealthBar;
using UI.Stats;
using UnityEngine;
using UnityEngine.UIElements;

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

        private VisualElement _uiRoot;

        public void Initialize(HeroesControl heroesControl)
        {
            FillComponents();

            var hero = heroesControl.First;

            _healthBarDigitsView.Initialize();
            _healthBarView.Initialize(hero.Health);
            _ammoView = new AmmoView(_uiRoot, hero.Collector.Ammo);
            _statsView = new StatsView(_uiRoot, hero);
            _goldView.Initialize(hero.Collector.Gold);
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
