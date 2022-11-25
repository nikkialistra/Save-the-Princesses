using Rooms.Services.RepositoryTypes;
using Rooms.Services.RepositoryTypes.Enemies;
using Rooms.Services.RepositoryTypes.Princesses;

namespace Rooms.Services
{
    public class RoomRepositories
    {
        public PrincessRoomRepository Princesses { get; }
        public EnemyRoomRepository Enemies { get; }

        public AccumulationRoomRepository Accumulations { get; }
        public WeaponObjectRoomRepository WeaponObjects { get; }

        public RoomRepositories(PrincessRoomRepository princessRepository, EnemyRoomRepository enemyRepository,
            AccumulationRoomRepository accumulationRepository, WeaponObjectRoomRepository weaponObjectRepository)
        {
            Princesses = princessRepository;
            Enemies = enemyRepository;

            Accumulations = accumulationRepository;
            WeaponObjects = weaponObjectRepository;
        }

        public void Initialize(Room room)
        {
            Princesses.Initialize(room);
            Enemies.Initialize(room);
            Accumulations.Initialize(room);
            WeaponObjects.Initialize(room);
        }

        public void Dispose()
        {
            Princesses.Dispose();
            Enemies.Dispose();
            Accumulations.Dispose();
            WeaponObjects.Dispose();
        }
    }
}
