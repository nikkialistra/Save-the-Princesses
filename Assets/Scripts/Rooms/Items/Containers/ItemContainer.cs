using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rooms.Items.Containers
{
    public class ItemContainer<TItem> : MonoBehaviour where TItem : IItem
    {
        public IReadOnlyCollection<TItem> Items => _items;

        private List<TItem> _items;

        private Room _room;

        public void Initialize(Room room)
        {
            _room = room;
        }

        public void Dispose()
        {
            _items.Clear();
        }

        public void Add(TItem item, Vector3 position)
        {
            if (_items.Contains(item))
                throw new InvalidOperationException("Cannot add already contained item");

            item.SetPosition(position, transform);

            _items.Add(item);
        }

        public void Remove(TItem item)
        {
            if (!_items.Contains(item))
                throw new InvalidOperationException("Cannot remove not contained item");

            _items.Remove(item);
        }
    }
}
