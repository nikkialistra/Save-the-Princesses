using System;
using Combat.Weapons;

namespace GameData.Weapons
{
    [Serializable]
    public class WeaponData
    {
        public Weapon Prefab;
        public WeaponSpecs Specs;
    }
}
