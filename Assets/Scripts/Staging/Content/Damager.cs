using Characters;
using UnityEngine;

namespace Staging.Content
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] private int _value;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent(typeof(Character)) is Character character)
                character.TakeDamageContinuously(_value, 0.5f);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent(typeof(Character)) is Character character)
                character.StopTakingDamage();
        }
    }
}
