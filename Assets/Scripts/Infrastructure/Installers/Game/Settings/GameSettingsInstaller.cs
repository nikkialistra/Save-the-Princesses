using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game.Settings
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/Game Settings Installer")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private CharacterSettings _characterSettings;
        [SerializeField] private HeroSettings _heroSettings;
        [SerializeField] private PrincessSettings _princessSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_characterSettings);
            Container.BindInstance(_heroSettings);
            Container.BindInstance(_princessSettings);
        }
    }
}
