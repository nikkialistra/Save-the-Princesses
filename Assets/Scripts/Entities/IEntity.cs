﻿using System;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Entities
{
    public interface IEntity : IInitializable, IDisposable
    {
        public void PlaceInRoom(Room room);
        public void SetPosition(Vector3 position, Transform parent);
    }
}
