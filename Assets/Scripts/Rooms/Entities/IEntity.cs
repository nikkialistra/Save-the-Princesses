using System;
using UnityEngine;

namespace Rooms.Entities
{
    public interface IEntity : IDisposable
    {
        public void PlaceInRoom(Room room);
        public void SetPosition(Vector3 position, Transform parent);
    }
}
