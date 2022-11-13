using System.Collections.Generic;
using GameData.Princesses;
using Princesses.Types;
using Sirenix.OdinInspector;
using Surrounding.Staging;
using UnityEngine;
using Zenject;

namespace Princesses.Services
{
    public class PrincessFactory : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<PrincessType, PrincessData> _princessesMap = new();

        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public Princess Create(PrincessType princessType, StageType stageType)
        {
            var princessData = _princessesMap[princessType];

            var princess = _diContainer.InstantiatePrefabForComponent<Princess>(princessData.Prefab);

            princess.Initialize(princessData.InitialStats.For(stageType));

            return princess;
        }
    }
}
