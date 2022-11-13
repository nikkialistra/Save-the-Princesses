using UI.Menus.Primary;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _mainMenuView;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainMenuView>().FromInstance(_mainMenuView);
        }
    }
}
