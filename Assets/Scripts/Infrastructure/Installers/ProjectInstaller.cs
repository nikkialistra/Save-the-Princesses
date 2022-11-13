using Infrastructure.CoroutineRunners;
using Saving.Saves;
using Saving.Settings;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;

        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().AsSingle();
            Container.Bind<GameSaves>().AsSingle();

            Container.Bind<ICoroutineRunner>().FromInstance(_coroutineRunner);
        }
    }
}
