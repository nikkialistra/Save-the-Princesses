using System.Collections.Generic;
using Rooms.Items.Containers.Types;
using Surrounding.Interactables.Types;
using UnityEngine;
using Zenject;

namespace Rooms.Services.RepositoryTypes
{
    public class WeaponObjectRoomRepository
    {
        public IEnumerable<WeaponObject> WeaponObjects => _container.Items;

        private WeaponObjectContainer _container;

        [Inject]
        public void Construct(WeaponObjectContainer container)
        {
            _container = container;
        }

        public void Initialize(Room room)
        {
            _container.Initialize(room);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public void Add(WeaponObject weaponObject, Vector3 position)
        {
            _container.Add(weaponObject, position);
        }

        public void Remove(WeaponObject weaponObject)
        {
            _container.Remove(weaponObject);
        }
    }
}
