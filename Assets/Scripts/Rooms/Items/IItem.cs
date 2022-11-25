using UnityEngine;

namespace Rooms.Items
{
    public interface IItem
    {
        public void SetPosition(Vector3 position, Transform parent);
    }
}
