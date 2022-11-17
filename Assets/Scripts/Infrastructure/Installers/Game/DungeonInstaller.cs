using Dungeons;
using GameData.Rooms;
using Infrastructure.Bootstrap;
using Sirenix.OdinInspector;
using Surrounding.Rooms;
using Surrounding.Rooms.Services;
using Surrounding.Staging;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public class DungeonInstaller : MonoInstaller
    {
        [SerializeField] private DungeonControl _dungeonControl;

        [Space]
        [SerializeField] private Dungeon _dungeon;

        [Title("Rooms")]
        [SerializeField] private RoomFrequencyRegistry _roomFrequencyRegistry;
        [SerializeField] private GameObject _roomPrefab;
        [SerializeField] private Transform _roomsParent;

        public override void InstallBindings()
        {
            Container.BindInstance(_dungeonControl);
            Container.BindInstance(_dungeon);

            BindRooms();
            BindStages();

            BindBootstrap();
        }

        private void BindRooms()
        {
            Container.BindInstance(_roomFrequencyRegistry);

            Container.Bind<RoomGenerator>().AsSingle();

            Container.BindInstance(_roomsParent).WhenInjectedInto<RoomGenerator>();
        }

        private void BindStages()
        {
            Container.Bind<Stages>().AsSingle();

            Container.BindFactory<Stage, Stage.Factory>()
                .FromNewComponentOnNewGameObject();
        }

        private void BindBootstrap()
        {
            Container.BindInterfacesTo<DungeonBootstrap>().AsSingle().NonLazy();
        }
    }
}
