using Enemies.Services;
using Enemies.Services.Repositories;
using Entities.Containers;
using Infrastructure.Bootstrap;
using Princesses.Services;
using Princesses.Services.Repositories;
using Surrounding.Rooms;
using Surrounding.Rooms.Services;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
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
            BindGenerators();

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

        private void BindGenerators()
        {
            Container.Bind<PrincessGenerator>().AsSingle();
            Container.Bind<EnemyGenerator>().AsSingle();
        }

        private void BindBootstrap()
        {
            Container.BindInterfacesTo<RoomBootstrap>().AsSingle().NonLazy();
        }
    }
}
