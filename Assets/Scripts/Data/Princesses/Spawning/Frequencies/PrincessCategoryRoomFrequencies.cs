using System.Collections.Generic;
using UnityEngine;

namespace Data.Princesses.Spawning.Frequencies
{
    [CreateAssetMenu(fileName = "(Room Type)", menuName = "GameData/Princess Category Room Frequencies")]
    public class PrincessCategoryRoomFrequencies : ScriptableObject
    {
        public List<PrincessCategoryFrequencies> Frequencies;
    }
}
