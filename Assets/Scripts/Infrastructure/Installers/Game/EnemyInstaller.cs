using Enemies;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Enemy _enemy;

        public override void InstallBindings()
        {
            Container.BindInstance(_enemy);
        }
    }
}
