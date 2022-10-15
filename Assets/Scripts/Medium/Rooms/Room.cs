﻿using System;
using UnityEngine;
using Zenject;

namespace Medium.Rooms
{
    public class Room : MonoBehaviour
    {
        public event Action Spawn;

        public RoomRepositories Repositories => _repositories;

        [SerializeField] private LayerMask _foreground;

        private readonly Collider2D[] _boundHits = new Collider2D[10];

        private RoomRepositories _repositories;

        [Inject]
        public void Construct(RoomRepositories repositories)
        {
            _repositories = repositories;
        }

        public void Initialize(RoomKind roomKind)
        {
            Instantiate(roomKind.Map, transform);

            Spawn?.Invoke();
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

        public class Factory : PlaceholderFactory<Room> { }
    }
}
