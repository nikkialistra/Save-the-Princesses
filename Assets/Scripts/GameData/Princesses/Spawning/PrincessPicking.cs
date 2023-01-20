﻿using System.Collections.Generic;
using GameData.Princesses.Spawning.Frequencies;
using Princesses.Types;
using UnityEngine;

namespace GameData.Princesses.Spawning
{
    public class PrincessPicking
    {
        public PrincessType GetRandomPrincessType(PrincessCategoryRoomFrequencies categoryRoomFrequencies)
        {
            var frequenciesList = categoryRoomFrequencies.Frequencies;

            var princessCategory = GetPrincessCategory(frequenciesList);

            return princessCategory.GetRandomType();
        }

        private static PrincessCategory GetPrincessCategory(List<PrincessCategoryFrequencies> frequenciesList)
        {
            return frequenciesList[Random.Range(0, frequenciesList.Count)].GetRandom();
        }
    }
}