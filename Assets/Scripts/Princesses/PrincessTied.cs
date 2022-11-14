using System;
using System.Collections;
using System.Collections.Generic;
using Combat.Attacks;
using Combat.Attacks.Specs;
using Infrastructure.Installers.Game.Settings;
using UnityEngine;
using static Combat.Attacks.Specs.AttackOrigin;

namespace Princesses
{
    public class PrincessTied : MonoBehaviour
    {
        public event Action Hit;
        public event Action Untie;

        public bool Tied { get; private set; } = true;

        [SerializeField] private float _untieFinishTime = 0.6f;

        private readonly HashSet<AttackSpecs> _hitAttacks = new();

        private int _hits;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_hits >= GameSettings.Princess.HitsToUntie) return;

            var attackSpecs = other.GetComponentInParent<Attack>()?.Specs;

            if (attackSpecs is not { Origin: Hero }) return;

            UpdateHits(attackSpecs);
        }

        private void UpdateHits(AttackSpecs attackSpecs)
        {
            if (_hitAttacks.Contains(attackSpecs)) return;

            _hits++;

            _hitAttacks.Add(attackSpecs);

            if (_hits >= GameSettings.Princess.HitsToUntie)
                StartUntying();
            else
                Hit?.Invoke();
        }

        private void StartUntying()
        {
            Untie?.Invoke();

            _hitAttacks.Clear();
            enabled = false;

            StartCoroutine(FinishUntyingAfter());
        }

        private IEnumerator FinishUntyingAfter()
        {
            yield return new WaitForSeconds(_untieFinishTime);

            Tied = false;
        }
    }
}
