using System;
using Saving.Settings;
using UI.Menus.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Zenject;

namespace UI.Menus.Primary
{
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(PlayerInput))]
    public class MainMenuView : MonoBehaviour, IMenuView, IHideMenuNotification, IInitializable, IDisposable
    {
        private const string BootstrapSceneName = "Bootstrap";

        public event Action HideCurrent;

        [SerializeField] private GameSettings _gameSettings;

        private VisualElement _tree;

        private Button _newGame;
        private Button _loadGame;
        private Button _settings;
        private Button _exitGame;

        private VisualElement _background;
        private VisualElement _buttons;

        private SettingsView _settingsView;

        private PlayerInput _playerInput;

        private InputAction _hideMenuAction;

        public void Initialize()
        {
            BindUi();

            _playerInput = GetComponent<PlayerInput>();
            _hideMenuAction = _playerInput.actions.FindAction("Toggle Menu");

            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        public void ShowSelf()
        {
            _background.AddToClassList("align-left");
            _background.Add(_buttons);
        }

        public void HideSelf()
        {
            _background.RemoveFromClassList("align-left");
            _background.Remove(_buttons);
        }

        private void HideMenu(InputAction.CallbackContext context)
        {
            HideCurrent?.Invoke();
        }

        private static void NewGame()
        {
            SceneManager.LoadScene(BootstrapSceneName, LoadSceneMode.Single);
        }

        private static void LoadGame() { }

        private void Settings()
        {
            HideSelf();

            _settingsView ??= new SettingsView(_background, this, this, _gameSettings);
            _settingsView.ShowSelf();
        }

        private static void ExitGame()
        {
            Application.Quit();
        }

        private void BindUi()
        {
            _tree = GetComponent<UIDocument>().rootVisualElement;

            _background = _tree.Q<VisualElement>("background");
            _buttons = _tree.Q<VisualElement>("buttons");

            _newGame = _tree.Q<Button>("new-game");
            _loadGame = _tree.Q<Button>("load-game");
            _settings = _tree.Q<Button>("settings");
            _exitGame = _tree.Q<Button>("exit-game");
        }

        private void SubscribeToEvents()
        {
            _newGame.clicked += NewGame;
            _loadGame.clicked += LoadGame;
            _settings.clicked += Settings;
            _exitGame.clicked += ExitGame;

            _hideMenuAction.started += HideMenu;
        }

        private void UnsubscribeFromEvents()
        {
            _newGame.clicked -= NewGame;
            _loadGame.clicked -= LoadGame;
            _settings.clicked -= Settings;
            _exitGame.clicked -= ExitGame;

            _hideMenuAction.started -= HideMenu;
        }
    }
}
