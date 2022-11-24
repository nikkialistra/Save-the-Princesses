using System;
using Combat.Weapons;
using Surrounding.Interactables.Types;
using UnityEngine;

namespace GameData.Weapons
{
    [Serializable]
    public class WeaponData
    {
        public Weapon Prefab;
        public WeaponSpecs Specs;

        [Space]
        public WeaponObject ObjectPrefab;
    }
}
