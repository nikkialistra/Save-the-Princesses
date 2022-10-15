using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemies.Services
{
    public class EnemyRepository : MonoBehaviour
    {
        public event Action<Enemy> Add;
        public event Action<Enemy> Remove;

        public int Count => _enemies.Count;

        private List<Enemy> _enemies;

        public void Initialize()
        {
            _enemies = FindObjectsOfType<Enemy>().ToList();
        }

        public void Dispose()
        {
            foreach (var enemy in _enemies)
                enemy.Dispose();
        }

        public IEnumerable<Enemy> GetEnemies()
        {
            return _enemies;
        }

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
            Add?.Invoke(enemy);
        }

        public void RemoveEnemy(Enemy Enemy)
        {
            _enemies.Remove(Enemy);
            Remove?.Invoke(Enemy);
        }
    }
}
