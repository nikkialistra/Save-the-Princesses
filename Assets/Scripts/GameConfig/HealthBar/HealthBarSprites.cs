using UnityEngine;

namespace GameConfig.HealthBar
{
    [CreateAssetMenu(fileName = "Health Bar Sprites", menuName = "GameConfig/Health Bar Sprites")]
    public class HealthBarSprites : ScriptableObject
    {
        public Sprite Full;
        public Sprite NotFull;
        public Sprite RemainderTwoPixels;
        public Sprite RemainderOnePixel;

        [Space]
        public Sprite LightStart;
        public Sprite LightMiddle;
        public Sprite LightFinish;

        [Space]
        public Sprite RemainderLightStart;
        public Sprite RemainderLightMiddle;
        public Sprite RemainderLightFinish;
    }
}
