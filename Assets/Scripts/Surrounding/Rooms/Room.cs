using System.Linq;
using Enemies.Services;
using Pathfinding;
using Princesses.Services;
using SuperTiled2Unity;
using Surrounding.Staging;
using UnityEngine;
using Zenject;

namespace Surrounding.Rooms
{
    public class Room : MonoBehaviour
    {
        private const string PrincessSpawnPointsLayer = "PrincessSpawnPoints";
        private const string EnemySpawnPointsLayer = "EnemySpawnPoints";

        public RoomRepositories Repositories { get; private set; }

        [SerializeField] private LayerMask _foreground;

        private StageType _stageType;

        private readonly Collider2D[] _boundHits = new Collider2D[10];

        private Navigation _navigation;

        private PrincessGenerator _princessGenerator;
        private EnemyGenerator _enemyGenerator;

        private SuperMap _superMap;

        private NavGraph _navGraph;

        [Inject]
        public void Construct(Navigation navigation, RoomRepositories repositories,
            PrincessGenerator princessGenerator, EnemyGenerator enemyGenerator)
        {
            _navigation = navigation;

            Repositories = repositories;

            _princessGenerator = princessGenerator;
            _enemyGenerator = enemyGenerator;
        }

        public void Initialize(RoomKind roomKind, Transform parent)
        {
            name = roomKind.Map.name;
            transform.parent = parent;

            _stageType = roomKind.StageType;

            _superMap = Instantiate(roomKind.Map, transform);
            CenterSuperMap();

            InitializeGenerators();
        }

        public void SetupNavigation()
        {
            _navGraph = _navigation.AddNavGraphForRoom(_superMap.name, _superMap.transform.position,
                _superMap.m_Width, _superMap.m_Height);
        }

        public void GenerateCharacters()
        {
            _princessGenerator.Generate();
            _enemyGenerator.GenerateFor(_stageType);
        }

        public void Dispose()
        {
            _navigation.RemoveRoomNavGraph(_navGraph);
        }

        public bool InBounds(Vector2 point)
        {
            var size = Physics2D.OverlapPointNonAlloc(point, _boundHits);

            for (int i = 0; i < size; i++)
                if (_boundHits[i].gameObject.layer == _foreground)
                    return true;

            return false;
        }

        private void CenterSuperMap()
        {
            var offsetX = -1 * _superMap.m_Width / 2;
            var offsetY = _superMap.m_Height / 2;

            _superMap.transform.position = new Vector2(offsetX, offsetY);
        }

        private void InitializeGenerators()
        {
            var princessSpawnPointsLayer = _superMap
                .GetComponentsInChildren<SuperObjectLayer>().First(layer => layer.name == PrincessSpawnPointsLayer);

            var enemySpawnPointsLayer = _superMap
                .GetComponentsInChildren<SuperObjectLayer>().First(layer => layer.name == EnemySpawnPointsLayer);

            _princessGenerator.Initialize(princessSpawnPointsLayer);
            _enemyGenerator.Initialize(enemySpawnPointsLayer);
        }

        public class Factory : PlaceholderFactory<Room> { }
    }
}
