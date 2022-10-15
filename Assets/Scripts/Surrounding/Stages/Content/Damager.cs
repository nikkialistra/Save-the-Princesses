using Combat;
using UnityEngine;

namespace Surrounding.Stages.Content
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] private int _value;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent(typeof(IDamageable)) is IDamageable damageable)
                damageable.TakeDamageContinuously(_value, 0.5f);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent(typeof(IDamageable)) is IDamageable damageable)
                damageable.StopTakingDamage();
        }
    }
}
