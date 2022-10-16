using HubSystem;
using Infrastructure.Bootstrap;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public class HubInstaller : MonoInstaller
    {
        [SerializeField] private Hub _hub;

        public override void InstallBindings()
        {
            Container.BindInstance(_hub);

            BindBootstrap();
        }

        private void BindBootstrap()
        {
            Container.BindInterfacesTo<HubBootstrap>().AsSingle().NonLazy();
        }
    }
}
