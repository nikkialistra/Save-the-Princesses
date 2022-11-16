using System.Collections.Generic;
using UnityEngine;

namespace GameData.Princesses.Spawning.Frequencies
{
    [CreateAssetMenu(fileName = "(Room Type)", menuName = "GameData/Frequencies/Princess Category Room Frequencies")]
    public class PrincessCategoryRoomFrequencies : ScriptableObject
    {
        public List<PrincessCategoryFrequencies> Frequencies;
    }
}
