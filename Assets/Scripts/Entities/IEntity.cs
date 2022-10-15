using System;
using Medium.Rooms;
using UnityEngine;
using Zenject;

namespace Entities
{
    public interface IEntity : IInitializable, IDisposable
    {
        public void PlaceInRoom(Room room);
        public void SetParent(Transform parent);
    }
}
