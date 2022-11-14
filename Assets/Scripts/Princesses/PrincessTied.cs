using System;
using System.Collections;
using System.Collections.Generic;
using Combat.Attacks;
using Combat.Attacks.Specs;
using GameData.Settings;
using UnityEngine;
using static Combat.Attacks.Specs.AttackOrigin;

namespace Princesses
{
    public class PrincessTied
    {
        public event Action Hit;
        public event Action Untie;

        public bool Tied { get; private set; } = true;

        private readonly HashSet<AttackSpecs> _hitAttacks = new();

        private int _hits;

        private readonly Princess _princess;

        public PrincessTied(Princess princess)
        {
            _princess = princess;
        }

        private void DoHit(Collider2D other)
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

            _princess.StartCoroutine(FinishUntyingAfter());
        }

        private IEnumerator FinishUntyingAfter()
        {
            yield return new WaitForSeconds(GameSettings.Princess.UntieFinishTime);

            Tied = false;
        }
    }
}
