using Infrastructure.Bootstrap;
using Infrastructure.Loading;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public static bool GameStarted;

        [SerializeField] private LoadingScreen _loadingScreen;

        public override void InstallBindings()
        {
            Container.BindInstance(_loadingScreen);

            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<GameLoading>().AsSingle();

            Container.BindInterfacesTo<SessionBootstrap>().AsSingle().NonLazy();

            GameStarted = true;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetInstaller()
        {
            GameStarted = false;
        }
    }
}
