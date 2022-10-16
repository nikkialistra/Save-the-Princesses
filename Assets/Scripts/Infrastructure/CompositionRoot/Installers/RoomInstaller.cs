using Enemies.Services.Repositories;
using Entities.Containers;
using Infrastructure.Bootstrap;
using Surrounding.Rooms;
using Princesses.Services.Repositories;
using UnityEngine;
using Zenject;

namespace Infrastructure.CompositionRoot.Installers
{
    public class RoomInstaller : MonoInstaller
    {
        [SerializeField] private Room _room;

        [SerializeField] private PrincessContainer _princessContainer;
        [SerializeField] private EnemyContainer _enemyContainer;

        public override void InstallBindings()
        {
            Container.BindInstance(_room);

            BindContainers();
            BindAreaRepositories();

            BindBootstrap();
        }

        private void BindContainers()
        {
            Container.BindInstance(_princessContainer);
            Container.BindInstance(_enemyContainer);
        }

        private void BindAreaRepositories()
        {
            Container.Bind<PrincessRoomRepository>().AsSingle();
            Container.Bind<EnemyRoomRepository>().AsSingle();
            Container.Bind<RoomRepositories>().AsSingle();
        }

        private void BindBootstrap()
        {
            Container.BindInterfacesTo<RoomBootstrap>().AsSingle().NonLazy();
        }
    }
}
