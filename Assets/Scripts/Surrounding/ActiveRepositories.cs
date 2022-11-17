using Enemies.Services.Repositories;
using Surrounding.Rooms;
using Princesses.Services.Repositories;
using Surrounding.Rooms.Services;

namespace Surrounding
{
    public class ActiveRepositories
    {
        public PrincessActiveRepository Princesses { get; }
        public EnemyActiveRepository Enemies { get; }

        private bool _filled;

        public ActiveRepositories(PrincessActiveRepository princessRepository, EnemyActiveRepository enemyRepository)
        {
            Princesses = princessRepository;
            Enemies = enemyRepository;
        }

        public void FillRepositories(RoomRepositories repositories)
        {
            if (_filled)
                ChangeRepositories(repositories);
            else
                InitializeRepositories(repositories);
        }

        private void InitializeRepositories(RoomRepositories repositories)
        {
            Princesses.Initialize(repositories.Princesses);
            Enemies.Initialize(repositories.Enemies);

            _filled = true;
        }

        private void ChangeRepositories(RoomRepositories newRepositories)
        {
            Princesses.ReplaceRoomRepository(newRepositories.Princesses);
            Enemies.ReplaceRoomRepository(newRepositories.Enemies);
        }

        public void Dispose()
        {
            Princesses.Dispose();
            Enemies.Dispose();
        }
    }
}
