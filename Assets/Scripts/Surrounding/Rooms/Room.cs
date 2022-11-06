using System.Linq;
using Enemies.Services;
using Pathfinding;
using Princesses.Services;
using SuperTiled2Unity;
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

        private readonly Collider2D[] _boundHits = new Collider2D[10];

        private Navigation _navigation;

        private PrincessGenerator _princessGenerator;
        private EnemyGenerator _enemyGenerator;

        private SuperMap _superMap;

        [Inject]
        public void Construct(Navigation navigation, RoomRepositories repositories,
            PrincessGenerator princessGenerator, EnemyGenerator enemyGenerator)
        {
            _navigation = navigation;

            Repositories = repositories;

            _princessGenerator = princessGenerator;
            _enemyGenerator = enemyGenerator;
        }

        public void Initialize(RoomKind roomKind)
        {
            _superMap = Instantiate(roomKind.Map, transform);

            CenterSuperMap();

            InitializeGenerators();
            InitializeNavigation();

            GenerateCharacters();
        }

        public void Dispose()
        {

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

        private void InitializeNavigation()
        {
            _navigation.AddGridForRoom(_superMap.name, _superMap.transform.position, _superMap.m_Width, _superMap.m_Height);
        }

        private void GenerateCharacters()
        {
            _princessGenerator.Generate();
            _enemyGenerator.Generate();
        }

        public class Factory : PlaceholderFactory<Room> { }
    }
}
