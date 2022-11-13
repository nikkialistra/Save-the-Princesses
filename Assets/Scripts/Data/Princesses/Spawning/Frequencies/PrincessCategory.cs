﻿using System.Collections.Generic;
using Princesses.Types;
using UnityEngine;

namespace Data.Princesses.Spawning.Frequencies
{
    [CreateAssetMenu(fileName = "(Category Name)", menuName = "Data/Princess Category")]
    public class PrincessCategory : ScriptableObject
    {
        public List<PrincessType> PrincessTypes;

        public PrincessType GetRandomType()
        {
            return PrincessTypes[Random.Range(0, PrincessTypes.Count)];
        }
    }
}