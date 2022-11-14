using Saving.Settings;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Menus.Settings
{
    public class AudioView : IMenuView
    {
        private const string VisualTreePath = "UI/Markup/Menus/Settings/Audio";

        private readonly VisualElement _root;
        private readonly SettingsView _parent;

        private readonly TemplateContainer _tree;

        private readonly Button _back;
        private readonly IHideMenuNotification _hideMenuNotification;

        private readonly GameSettings _gameSettings;

        public AudioView(VisualElement root, SettingsView parent, IHideMenuNotification hideMenuNotification, GameSettings gameSettings)
        {
            _root = root;
            _parent = parent;
            _hideMenuNotification = hideMenuNotification;
            _gameSettings = gameSettings;

            var template = Resources.Load<VisualTreeAsset>(VisualTreePath);
            _tree = template.CloneTree();
            _tree.style.flexGrow = 1;

            _back = _tree.Q<Button>("back");
        }

        public void ShowSelf()
        {
            _hideMenuNotification.HideCurrent += Back;

            _root.Add(_tree);

            _back.clicked += Back;
        }

        public void HideSelf()
        {
            _hideMenuNotification.HideCurrent -= Back;

            _root.Remove(_tree);

            _back.clicked -= Back;

            _parent.ShowSelf();
        }

        private void Back()
        {
            HideSelf();
        }
    }
}
