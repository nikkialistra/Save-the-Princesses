using UnityEngine;

namespace Combat.Weapons.Concrete
{
    public abstract class ConcreteWeapon : MonoBehaviour
    {
        public abstract bool TryStroke();
        public abstract void ResetStroke();

        public abstract StrokeType LastStroke { get; }
    }
}
