using DungeonSystem;
using Enemies.Services;
using Infrastructure.Bootstrap;
using Medium.Rooms;
using Princesses.Services;
using Princesses.Services.Repositories;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Infrastructure.CompositionRoot.Installers
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

            BindBootstrap();
        }

        private void BindRooms()
        {
            Container.BindInstance(_roomGenerator);

            Container.BindFactory<Room, Room.Factory>()
                .FromComponentInNewPrefab(_roomPrefab)
                .UnderTransform(_roomGenerator.transform);
        }

        private void BindBootstrap()
        {
            Container.BindInterfacesTo<DungeonBootstrap>().AsSingle().NonLazy();
        }
    }
}
