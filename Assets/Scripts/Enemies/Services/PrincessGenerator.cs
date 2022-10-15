using Enemies.Services.Repositories;

namespace Enemies.Services
{
    public class EnemyGenerator
    {
        private EnemyActiveRepository _activeRepository;

        public EnemyGenerator(EnemyActiveRepository activeRepository)
        {
            _activeRepository = activeRepository;
        }

        public void Spawn() { }
    }
}
