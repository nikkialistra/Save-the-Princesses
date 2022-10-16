using System.Collections.Generic;
using System.Linq;
using SuperTiled2Unity;
using UnityEngine;

namespace Surrounding.Rooms
{
    public class SpawnPoints
    {
        private readonly SuperObject[] _allPoints;
        private readonly HashSet<SuperObject> _freePoints;

        public SpawnPoints(SuperObject[] allPoints)
        {
            _allPoints = allPoints;
            _freePoints = _allPoints.ToHashSet();
        }

        public IEnumerable<Vector3> TakeSome(int count)
        {
            if (count > _freePoints.Count)
                count = _freePoints.Count;

            for (int i = 0; i < count; i++)
                yield return GetRandomPoint();
        }
        private Vector3 GetRandomPoint()
        {
            var randomIndex = Random.Range(0, _freePoints.Count);
            var randomSuperObject = _freePoints.ElementAt(randomIndex);

            _freePoints.Remove(randomSuperObject);

            return randomSuperObject.transform.position;
        }
    }
}
