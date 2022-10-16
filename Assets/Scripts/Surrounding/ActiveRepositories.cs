using Enemies.Services.Repositories;
using Surrounding.Rooms;
using Princesses.Services.Repositories;

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
            {
                ChangeRepositories(repositories);
                return;
            }

            _filled = true;

            Princesses.Initialize(repositories.Princesses);
            Enemies.Initialize(repositories.Enemies);
        }

        public void ChangeRepositories(RoomRepositories newRepositories)
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
