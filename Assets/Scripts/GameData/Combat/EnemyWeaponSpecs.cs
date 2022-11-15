using System.Collections.Generic;
using GameSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameData.Combat
{
    [CreateAssetMenu(fileName = "(Weapon Type)", menuName = "GameData/Enemy Weapon Specs")]
    public class EnemyWeaponSpecs : SerializedScriptableObject
    {
        public Dictionary<GameDifficulty, WeaponSpecs> Specs = new();

        public WeaponSpecs this[GameDifficulty gameDifficulty] => Specs[gameDifficulty];
    }
}
