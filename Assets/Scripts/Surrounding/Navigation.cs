using Pathfinding;
using UnityEngine;

namespace Surrounding
{
    public class Navigation : MonoBehaviour
    {
        private const float CellSize = 0.5f;
        private const int DimensionMultiplier = 2;

        [SerializeField] private LayerMask _collisionMask;

        public void AddGridForRoom(string roomName, Vector3 position, int width, int height)
        {
            var data = AstarPath.active.data;

            var gridGraph = data.AddGraph(typeof(GridGraph)) as GridGraph;

            FillGridDefaultParameters(gridGraph);

            gridGraph!.name = roomName;
            gridGraph!.center = GetRoomCenter(position, width, height);

            gridGraph.SetDimensions(width * DimensionMultiplier, height * DimensionMultiplier, CellSize);

            AstarPath.active.Scan();
        }

        private void FillGridDefaultParameters(GridGraph gridGraph)
        {
            gridGraph.rotation = new Vector3(90f, 0, 0);

            var collision = gridGraph.collision;

            collision.use2D = true;
            collision.type = ColliderType.Ray;
            collision.mask = _collisionMask;
        }

        private static Vector3 GetRoomCenter(Vector3 position, int width, int height)
        {
            return new Vector3(position.x + width / 2,
                position.y - height / 2);
        }
    }
}
