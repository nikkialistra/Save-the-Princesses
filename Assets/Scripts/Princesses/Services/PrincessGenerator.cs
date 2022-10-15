using Princesses.Services.Repositories;

namespace Princesses.Services
{
    public class PrincessGenerator
    {
        private PrincessActiveRepository _activeRepository;

        public PrincessGenerator(PrincessActiveRepository activeRepository)
        {
            _activeRepository = activeRepository;
        }

        public void Spawn() { }
    }
}
