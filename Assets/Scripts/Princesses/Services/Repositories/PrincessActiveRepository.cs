using System.Collections.Generic;
using UnityEngine;

namespace Princesses.Services.Repositories
{
    public class PrincessActiveRepository
    {
        public IEnumerable<Princess> Princesses => _roomRepository.Princesses;
        public IEnumerable<Princess> UntiedFreePrincesses => _roomRepository.UntiedFreePrincesses;

        private PrincessRoomRepository _roomRepository;

        public void Initialize(PrincessRoomRepository initialRepository)
        {
            _roomRepository = initialRepository;
        }

        public void Dispose()
        {
            _roomRepository.Dispose();
        }

        public void ReplaceRoomRepository(PrincessRoomRepository newRepository)
        {
            _roomRepository = newRepository;
        }

        public void Add(Princess princess, Vector3 position)
        {
            _roomRepository.Add(princess, position);
        }

        public void Remove(Princess princess)
        {
            _roomRepository.Remove(princess);
        }
    }
}
