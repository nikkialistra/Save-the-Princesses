using System;
using GameSystems;
using Surrounding;
using Saving.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Zenject;

namespace UI.Menus.Primary
{
    [RequireComponent(typeof(UIDocument))]
    public class GameMenuToggle : MonoBehaviour, IHideMenuNotification
    {
        public event Action HideCurrent;

        public event Action GamePause;
        public event Action GameResume;

        private VisualElement _root;

        private GameMenuView _gameMenuView;

        private TimeToggling _timeToggling;

        private GameSettings _gameSettings;

        private PlayerInput _playerInput;

        private InputAction _toggleMenuAction;

        [Inject]
        public void Construct(TimeToggling timeToggling,
            GameSettings gameSettings,
            PlayerInput playerInput)
        {
            _timeToggling = timeToggling;
            _gameSettings = gameSettings;
            _playerInput = playerInput;
        }

        public void Initialize()
        {
            _root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("root");

            _gameMenuView = new GameMenuView(_root, this, _gameSettings);

            _toggleMenuAction = _playerInput.actions.FindAction("Toggle Menu");

            SubscribeToEvents();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                DoResume();
            else
                DoPause();
        }

        private void DoPause()
        {
            _timeToggling.Pause();
            GamePause?.Invoke();
        }

        private void DoResume()
        {
            _timeToggling.Resume();
            GameResume?.Invoke();
        }

        private void ToggleMenu(InputAction.CallbackContext context)
        {
            if (_gameMenuView.ShownSubView)
                HideCurrent?.Invoke();
            else
                ToggleGameMenu();
        }

        private void ToggleGameMenu()
        {
            if (_gameMenuView.Shown)
                _gameMenuView.HideSelf();
            else
                _gameMenuView.ShowSelf();
        }

        private void SubscribeToEvents()
        {
            _gameMenuView.Pause += DoPause;
            _gameMenuView.Resume += DoResume;

            _toggleMenuAction.started += ToggleMenu;
        }

        private void UnsubscribeFromEvents()
        {
            _gameMenuView.Pause += DoPause;
            _gameMenuView.Resume -= DoResume;

            _toggleMenuAction.started -= ToggleMenu;
        }
    }
}
