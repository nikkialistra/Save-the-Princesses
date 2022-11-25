using System.Collections.Generic;
using System.Linq;
using Princesses;
using Rooms.Entities.Containers.Types;
using UnityEngine;
using Zenject;

namespace Rooms.Services.RepositoryTypes.Princesses
{
    public class PrincessRoomRepository
    {
        public IEnumerable<Princess> Princesses => _container.Entities;
        public IEnumerable<Princess> UntiedFreePrincesses => _container.Entities.Where(princess => princess.Free);

        private PrincessContainer _container;

        [Inject]
        public void Construct(PrincessContainer container)
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

        public void Add(Princess princess, Vector3 position)
        {
            _container.Add(princess, position);
        }

        public void Remove(Princess princess)
        {
            _container.Remove(princess);
        }
    }
}
