using Enemies.Services;
using Infrastructure.Bootstrap;
using Princesses.Services;
using Rooms;
using Rooms.Entities.Containers.Types;
using Rooms.Services;
using Rooms.Services.RepositoryTypes.Enemies;
using Rooms.Services.RepositoryTypes.Princesses;
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
