using System;
using System.Collections.Generic;
using System.Linq;
using Medium.Rooms;
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

        [Inject]
        public void Construct(Room room)
        {
            _room = room;
        }

        public void Initialize()
        {
            _entities = GetComponentsInChildren<E>().ToList();

            foreach (var entity in _entities)
            {
                entity.Initialize();
                entity.PlaceInRoom(_room);
            }
        }

        public void Dispose()
        {
            foreach (var entity in _entities)
                entity.Dispose();

            _entities.Clear();
        }

        public void Add(E entity)
        {
            if (!_entities.Contains(entity))
                throw new InvalidOperationException("Cannot add alerady contained entity");

            entity.PlaceInRoom(_room);
            entity.SetParent(transform);

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
