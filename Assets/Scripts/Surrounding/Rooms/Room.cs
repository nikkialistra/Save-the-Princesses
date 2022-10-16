using SuperTiled2Unity;
using UnityEngine;
using Zenject;

namespace Surrounding.Rooms
{
    public class Room : MonoBehaviour
    {
        public RoomRepositories Repositories { get; private set; }

        [SerializeField] private LayerMask _foreground;

        private readonly Collider2D[] _boundHits = new Collider2D[10];

        [Inject]
        public void Construct(RoomRepositories repositories)
        {
            Repositories = repositories;
        }

        public void Initialize(RoomKind roomKind)
        {
            var superMap = Instantiate(roomKind.Map, transform);
            Center(superMap);
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

        private static void Center(SuperMap superMap)
        {
            var offsetX = -1 * superMap.m_Width / 2;
            var offsetY = superMap.m_Height / 2;

            superMap.transform.position = new Vector2(offsetX, offsetY);
        }

        public class Factory : PlaceholderFactory<Room> { }
    }
}
