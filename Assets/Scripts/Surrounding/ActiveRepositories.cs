using Enemies.Services.Repositories;
using Surrounding.Rooms;
using Princesses.Services.Repositories;

namespace Surrounding
{
    public class ActiveRepositories
    {
        private readonly PrincessActiveRepository _princessRepository;
        private readonly EnemyActiveRepository _enemyRepository;

        public ActiveRepositories(PrincessActiveRepository princessRepository, EnemyActiveRepository enemyRepository)
        {
            _princessRepository = princessRepository;
            _enemyRepository = enemyRepository;
        }

        public void SetStartRepositories(RoomRepositories repositories)
        {
            _princessRepository.Initialize(repositories.Princesses);
            _enemyRepository.Initialize(repositories.Enemies);
        }

        public void ChangeRepositories(RoomRepositories newRepositories)
        {
            _princessRepository.ReplaceRoomRepository(newRepositories.Princesses);
            _enemyRepository.ReplaceRoomRepository(newRepositories.Enemies);
        }

        public void Dispose()
        {
            _princessRepository.Dispose();
            _enemyRepository.Dispose();
        }
    }
}
