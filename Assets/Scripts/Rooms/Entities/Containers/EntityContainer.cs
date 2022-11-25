using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rooms.Entities.Containers
{
    public class EntityContainer<TEntity> : MonoBehaviour where TEntity : IEntity
    {
        public IReadOnlyCollection<TEntity> Entities => _entities;

        private List<TEntity> _entities;

        private Room _room;

        public void Initialize(Room room)
        {
            _room = room;
            FillFromStartEntities(room);
        }

        private void FillFromStartEntities(Room room)
        {
            _entities = GetComponentsInChildren<TEntity>().ToList();

            foreach (var entity in _entities)
                entity.PlaceInRoom(room);
        }

        public void Dispose()
        {
            foreach (var entity in _entities)
                entity.Dispose();

            _entities.Clear();
        }

        public void Add(TEntity entity, Vector3 position)
        {
            if (_entities.Contains(entity))
                throw new InvalidOperationException("Cannot add already contained entity");

            entity.SetPosition(position, transform);
            entity.PlaceInRoom(_room);

            _entities.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            if (!_entities.Contains(entity))
                throw new InvalidOperationException("Cannot remove not contained entity");

            entity.Dispose();

            _entities.Remove(entity);
        }
    }
}
