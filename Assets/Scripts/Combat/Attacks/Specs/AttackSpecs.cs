using System;
using Combat.Weapons;
using Combat.Weapons.Concrete;
using GameData.Weapons;

namespace Combat.Attacks.Specs
{
    [Serializable]
    public class AttackSpecs
    {
        public AttackOrigin Origin;
        public int Damage;
        public float Knockback;
        public float Stun;
        public bool IsPenetrable;

        public AttackSpecs(AttackOrigin origin, WeaponSpecs weaponSpecs)
        {
            Origin = origin;

            Damage = weaponSpecs.Damage;
            Knockback = weaponSpecs.Knockback;
            Stun = weaponSpecs.Stun;
            IsPenetrable = weaponSpecs.Penetrable;
        }
    }
}
