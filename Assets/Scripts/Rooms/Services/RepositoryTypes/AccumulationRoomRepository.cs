using System.Collections.Generic;
using Rooms.Items.Containers.Types;
using Surrounding.Interactables.Types.Accumulations;
using UnityEngine;
using Zenject;

namespace Rooms.Services.RepositoryTypes
{
    public class AccumulationRoomRepository
    {
        public IEnumerable<Accumulation> Accumulations => _container.Items;

        private AccumulationContainer _container;

        [Inject]
        public void Construct(AccumulationContainer container)
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

        public void Add(Accumulation accumulation, Vector3 position)
        {
            _container.Add(accumulation, position);
        }

        public void Remove(Accumulation accumulation)
        {
            _container.Remove(accumulation);
        }
    }
}
