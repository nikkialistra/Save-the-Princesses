using System.Collections.Generic;
using Princesses.Types;
using UnityEngine;

namespace GameData.Princesses.Spawning.Frequencies
{
    [CreateAssetMenu(fileName = "(Category Name)", menuName = "GameData/Princess Category")]
    public class PrincessCategory : ScriptableObject
    {
        public List<PrincessType> PrincessTypes;

        public PrincessType GetRandomType()
        {
            return PrincessTypes[Random.Range(0, PrincessTypes.Count)];
        }
    }
}
