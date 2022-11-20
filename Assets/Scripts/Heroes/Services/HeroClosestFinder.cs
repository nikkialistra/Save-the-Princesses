using System;
using GameSystems;
using GameSystems.Parameters;
using UnityEngine;

namespace Heroes.Services
{
    public class HeroClosestFinder
    {
        private bool IsSingle => _gameParameters.GameMode == GameMode.Single;

        private Hero _first;
        private Hero _second;

        private bool _filledForCoop;

        private readonly GameParameters _gameParameters;

        public HeroClosestFinder(GameParameters gameParameters)
        {
            _gameParameters = gameParameters;
        }

        public void FillForSingle(Hero hero)
        {
            _first = hero;

            _filledForCoop = false;
        }

        public void FillForCoop(Hero first, Hero second)
        {
            _first = first;
            _second = second;

            _filledForCoop = true;
        }

        public Hero GetFor(Vector2 position)
        {
            return IsSingle ? _first : FindClosestHeroTo(position);
        }

        private Hero FindClosestHeroTo(Vector2 position)
        {
            if (!_filledForCoop)
                throw new InvalidOperationException("Game mode is coop, but second hero is not filled");

            var distanceToFirst = Vector2.Distance(position, _first.Position);
            var distanceToSecond = Vector2.Distance(position, _second.Position);

            return distanceToFirst <= distanceToSecond ? _first : _second;
        }
    }
}
