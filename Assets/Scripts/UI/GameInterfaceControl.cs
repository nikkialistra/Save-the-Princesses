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
            _healthBarView.Initialize(_hero);
            _ammoView.Initialize(_uiRoot);
            _statsView.Initialize(_uiRoot, _hero);
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
            _uiRoot = GetComponent<UIDocument>().rootVisualElement;

            _healthBarView = GetComponent<HealthBarView>();
            _healthBarDigitsView = GetComponent<HealthBarDigitsView>();
            _goldView = GetComponent<GoldView>();
        }
    }
}
