using System.Collections.Generic;
using System.Linq;
using Saving.Settings;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Menus.Settings
{
    public class GraphicsView : IMenuView
    {
        private const string VisualTreePath = "UI/Markup/Menus/Settings/Graphics";
        private const string DropdownContainerStyles = "UI/Styles/Common/DropdownContainer";

        private List<Resolution> _resolutions;

        private readonly VisualElement _root;
        private readonly VisualElement _uiPanel;
        private readonly SettingsView _parent;

        private readonly TemplateContainer _tree;

        private readonly Toggle _fullscreen;
        private readonly DropdownField _resolution;
        private readonly SliderInt _uiScale;
        private readonly Label _uiScaleLabel;
        private readonly VisualElement _uiScaleDragger;
        private readonly Button _back;

        private readonly IHideMenuNotification _hideMenuNotification;

        private readonly GameSettings _gameSettings;

        public GraphicsView(VisualElement root, SettingsView parent, IHideMenuNotification hideMenuNotification, GameSettings gameSettings)
        {
            _root = root;
            _uiPanel = root.parent.parent;
            _parent = parent;
            _hideMenuNotification = hideMenuNotification;
            _gameSettings = gameSettings;

            var template = Resources.Load<VisualTreeAsset>(VisualTreePath);
            _tree = template.CloneTree();
            _tree.style.flexGrow = 1;

            _fullscreen = _tree.Q<Toggle>("fullscreen");
            _resolution = _tree.Q<DropdownField>("resolution");
            _uiScale = _tree.Q<SliderInt>("ui-scale");
            _uiScaleLabel = (Label)_uiScale.Children().First();
            _uiScaleDragger = _uiScale.Q<VisualElement>("unity-dragger");

            _back = _tree.Q<Button>("back");

            UpdateFullscreenToggle();
            UpdateResolution();
        }

        public void ShowSelf()
        {
            _hideMenuNotification.HideCurrent += Back;

            _root.Add(_tree);

            _fullscreen.RegisterValueChangedCallback(OnFullscreenToggle);
            _resolution.RegisterValueChangedCallback(OnResolutionChange);
            _resolution.RegisterCallback<FocusInEvent>(OnResolutionFocus);

            _back.clicked += Back;
        }

        public void HideSelf()
        {
            _hideMenuNotification.HideCurrent -= Back;

            _root.Remove(_tree);

            _fullscreen.UnregisterValueChangedCallback(OnFullscreenToggle);
            _resolution.UnregisterValueChangedCallback(OnResolutionChange);
            _resolution.UnregisterCallback<FocusInEvent>(OnResolutionFocus);

            _back.clicked -= Back;

            _parent.ShowSelf();
        }

        private void OnFullscreenToggle(ChangeEvent<bool> _)
        {
            _gameSettings.Fullscreen = _fullscreen.value;
        }

        private void OnResolutionChange(ChangeEvent<string> _)
        {
            var index = _resolution.index;
            _gameSettings.Resolution.Value = _resolutions[index];
        }

        private void OnResolutionFocus(FocusInEvent evt)
        {
        }

        private void SetUiScaleLabelText()
        {
            var value = RoundScale(_uiScale.value);
            _uiScaleLabel.text = "Scale (" + value + "):";
        }

        private static int RoundScale(int scale)
        {
            var roundedBy5Value = scale / 5;
            var value = roundedBy5Value * 5;
            return value;
        }

        private void UpdateFullscreenToggle()
        {
            _fullscreen.value = _gameSettings.Fullscreen;
        }

        private void UpdateResolution()
        {
            FillResolutions();

            _resolution.choices = new List<string>();
            var currentIndex = 0;
            for (int i = 0; i < _resolutions.Count; i++)
            {
                var resolution = _resolutions[i];
                var resolutionText = resolution.ToString();
                _resolution.choices.Add(resolutionText);

                if (resolutionText == _gameSettings.Resolution.Value.ToString())
                    currentIndex = i;
            }

            _resolution.index = currentIndex;
        }

        private void FillResolutions()
        {
            _resolutions = Screen.resolutions.Where(resolution =>
                resolution.width > 1024 && resolution.height > 768 && resolution.refreshRate >= 60).ToList();
            _resolutions.Reverse();
        }

        private void Back()
        {
            HideSelf();
        }
    }
}
