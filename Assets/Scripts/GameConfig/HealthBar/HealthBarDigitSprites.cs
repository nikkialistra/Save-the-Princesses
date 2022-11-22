using System.Collections.Generic;
using UnityEngine;

namespace GameConfig.HealthBar
{
    [CreateAssetMenu(fileName = "Health Bar Digit Sprites", menuName = "GameConfig/Health Bar Digit Sprites")]
    public class HealthBarDigitSprites : ScriptableObject
    {
        public List<Sprite> Digits;
        public Sprite this[int index] => Digits[index];
    }
}
