using Princesses;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public class PrincessInstaller : MonoInstaller
    {
        [SerializeField] private Princess _princess;

        public override void InstallBindings()
        {
            Container.BindInstance(_princess);
        }
    }
}
