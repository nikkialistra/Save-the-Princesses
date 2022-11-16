using UnityEngine;

namespace GameData.Enemies
{
    [CreateAssetMenu(fileName = "(Enemy Type)", menuName = "GameData/Specs/Enemy Specs")]
    public class EnemySpecs : ScriptableObject
    {
        public float ViewDistance;
    }
}
