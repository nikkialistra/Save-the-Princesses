using Princesses.Services.Repositories;
using SuperTiled2Unity;

namespace Princesses.Services
{
    public class PrincessGenerator
    {
        private SuperObject[] _spawnPoints;

        private PrincessActiveRepository _activeRepository;

        public PrincessGenerator(PrincessActiveRepository activeRepository)
        {
            _activeRepository = activeRepository;
        }

        public void Initialize(SuperObjectLayer spawnPointsLayer)
        {
            _spawnPoints = spawnPointsLayer.GetComponents<SuperObject>();
        }

        public void Generate()
        {

        }

        private void Spawn() { }
    }
}
