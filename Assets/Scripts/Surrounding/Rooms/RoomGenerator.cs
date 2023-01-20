﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Surrounding.Rooms
{
    public class RoomGenerator : MonoBehaviour
    {
        [SerializeField] private List<RoomKind> _roomKinds;

        private Room.Factory _roomFactory;

        [Inject]
        public void Construct(Room.Factory roomFactory)
        {
            _roomFactory = roomFactory;
        }

        public Room Create(Transform parent)
        {
            var room = _roomFactory.Create();
            room.Initialize(_roomKinds[0], parent);

            return room;
        }
    }
}