﻿using System;

namespace Combat.Weapons
{
    [Serializable]
    public class WeaponSpecs
    {
        public int Damage;
        public float AttackSpeed;
        public float StrokeSpeed;

        public float AttackDistance;
        public float Knockback;
        public float Stun;

        public bool Penetrable;
    }
}
