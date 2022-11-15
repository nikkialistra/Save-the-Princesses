using UnityEngine;

namespace Combat.Attacks
{
    public class AttackLocation
    {
        public float Rotation => _transform.rotation.eulerAngles.z;

        private readonly Transform _transform;

        public AttackLocation(Transform transform)
        {
            _transform = transform;
        }

        public void UpdateRotation(float direction)
        {
            _transform.rotation = Quaternion.Euler(0, 0, direction + 90f);
        }

        public void AlignWithCharacterCenter(Vector2 offset)
        {
            _transform.localPosition = offset;
        }
    }
}
