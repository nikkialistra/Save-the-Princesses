using System;
using System.Collections.Generic;
using System.Linq;
using Surrounding.Rooms;
using UnityEngine;
using Zenject;

namespace Entities.Containers
{
    public class EntityContainer<E> : MonoBehaviour where E : IEntity
    {
        public IReadOnlyCollection<E> Entities => _entities;
        public int Count => _entities.Count;

        private List<E> _entities;

        private Room _room;

        public void Initialize(Room room)
        {
            _room = room;
            FillFromStartEntities(room);
        }

        private void FillFromStartEntities(Room room)
        {
            _entities = GetComponentsInChildren<E>().ToList();

            foreach (var entity in _entities)
            {
                entity.Initialize();
                entity.PlaceInRoom(room);
            }
        }

        public void Dispose()
        {
            foreach (var entity in _entities)
                entity.Dispose();

            _entities.Clear();
        }

        public void Add(E entity, Vector3 position)
        {
            if (_entities.Contains(entity))
                throw new InvalidOperationException("Cannot add already contained entity");

            entity.SetPosition(position, transform);
            entity.PlaceInRoom(_room);

            _entities.Add(entity);
        }

        public void Remove(E entity)
        {
            if (!_entities.Contains(entity))
                throw new InvalidOperationException("Cannot remove not contained entity");

            entity.Dispose();

            _entities.Remove(entity);
        }
    }
}
