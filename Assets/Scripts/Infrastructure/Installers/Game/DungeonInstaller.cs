using Dungeons;
using Infrastructure.Bootstrap;
using Sirenix.OdinInspector;
using Surrounding.Rooms;
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
        [SerializeField] private RoomGenerator _roomGenerator;
        [SerializeField] private GameObject _roomPrefab;

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
            Container.BindInstance(_roomGenerator);

            Container.BindFactory<Room, Room.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(_roomPrefab)
                .UnderTransform(_roomGenerator.transform);
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
