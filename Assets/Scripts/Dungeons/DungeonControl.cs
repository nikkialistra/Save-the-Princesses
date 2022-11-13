﻿using System;
using Cysharp.Threading.Tasks;
using Heroes;
using UnityEngine;
using Zenject;

namespace Dungeons
{
    public class DungeonControl : MonoBehaviour
    {
        [SerializeField] private Transform _heroSpawnPoint;

        private Dungeon _dungeon;
        private Hero _hero;

        [Inject]
        public void Construct(Dungeon dungeon, Hero hero)
        {
            _dungeon = dungeon;
            _hero = hero;
        }

        public async UniTaskVoid Initialize(Action onFinish)
        {
            await _dungeon.Initialize();

            _hero.PlaceAt(_heroSpawnPoint.position);

            onFinish();
        }

        public void Dispose()
        {
            _dungeon.Dispose();
        }
    }
}
