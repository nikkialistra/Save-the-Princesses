using UnityEngine;

namespace GameData.Weapons
{
    [CreateAssetMenu(fileName = "(Weapon Type)", menuName = "GameData/Weapon Specs")]
    public class WeaponSpecs : ScriptableObject
    {
        public int Damage;

        [Space]
        public float Knockback;
        public float Stun;

        [Space]
        public float AttackDistance;
        public float AttackSpeed;

        [Space]
        public int NumberOfStrokes;
        public float StrokeSpeed;

        [Space]
        public bool Penetrable;
    }
}
