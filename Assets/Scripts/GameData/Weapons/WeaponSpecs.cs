using System;

namespace GameData.Weapons
{
    [Serializable]
    public class WeaponSpecs
    {
        public int Damage;

        public float Knockback;
        public float Stun;

        public float AttackDistance;
        public float AttackSpeed;
        public float StrokeSpeed;

        public bool Penetrable;
    }
}
