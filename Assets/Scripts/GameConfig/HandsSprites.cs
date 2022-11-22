using System;
using Characters.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameConfig
{
    [CreateAssetMenu(fileName = "Hands Sprites", menuName = "GameConfig/Hands Sprites")]
    public class HandsSprites : ScriptableObject
    {
        [Title("To Hero")]
        [SerializeField] private Sprite _heroLeft;
        [SerializeField] private Sprite _heroUpLeft;
        [SerializeField] private Sprite _heroUp;
        [SerializeField] private Sprite _heroUpRight;
        [SerializeField] private Sprite _heroRight;
        [SerializeField] private Sprite _heroDownRight;
        [SerializeField] private Sprite _heroDown;
        [SerializeField] private Sprite _heroDownLeft;

        [Title("To Next Princess")]
        [SerializeField] private Sprite _firstPrincessLeft;
        [SerializeField] private Sprite _firstPrincessUpLeft;
        [SerializeField] private Sprite _firstPrincessUp;
        [SerializeField] private Sprite _firstPrincessUpRight;
        [SerializeField] private Sprite _firstPrincessRight;
        [SerializeField] private Sprite _firstPrincessDownRight;
        [SerializeField] private Sprite _firstPrincessDown;
        [SerializeField] private Sprite _firstPrincessDownLeft;

        public Sprite GetForHeroToPrincess(Direction9 direction9)
        {
            return direction9 switch
            {
                Direction9.Center => null,
                Direction9.Left => _heroLeft,
                Direction9.UpLeft => _heroUpLeft,
                Direction9.Up => _heroUp,
                Direction9.UpRight => _heroUpRight,
                Direction9.Right => _heroRight,
                Direction9.DownRight => _heroDownRight,
                Direction9.Down => _heroDown,
                Direction9.DownLeft => _heroDownLeft,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public Sprite GetForPrincessToPrincess(Direction9 direction9)
        {
            return direction9 switch
            {
                Direction9.Center => null,
                Direction9.Left => _firstPrincessLeft,
                Direction9.UpLeft => _firstPrincessUpLeft,
                Direction9.Up => _firstPrincessUp,
                Direction9.UpRight => _firstPrincessUpRight,
                Direction9.Right => _firstPrincessRight,
                Direction9.DownRight => _firstPrincessDownRight,
                Direction9.Down => _firstPrincessDown,
                Direction9.DownLeft => _firstPrincessDownLeft,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
