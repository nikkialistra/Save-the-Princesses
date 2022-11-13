using System;
using Surrounding.Rooms;
using UnityEngine;

namespace Entities
{
    public interface IEntity : IDisposable
    {
        public void PlaceInRoom(Room room);
        public void SetPosition(Vector3 position, Transform parent);
    }
}
