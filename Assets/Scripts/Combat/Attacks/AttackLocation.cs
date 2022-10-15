using UnityEngine;

namespace Combat.Attacks
{
    public class AttackLocation : MonoBehaviour
    {
        public float Rotation => transform.rotation.eulerAngles.z;

        public void UpdateRotation(float direction)
        {
            transform.rotation = Quaternion.Euler(0, 0, direction + 90f);
        }

        public void AlignWithCharacterCenter(Vector2 offset)
        {
            transform.localPosition = offset;
        }
    }
}
