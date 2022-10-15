﻿using System.Collections.Generic;
using Infrastructure.CompositionRoot.Settings;
using Princesses;
using Princesses.Services;
using Princesses.Services.Repositories;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Heroes
{
    public class HeroPrincessGathering : MonoBehaviour
    {
        private PrincessActiveRepository _activeRepository;
        private HeroSettings _settings;

        private readonly List<Princess> _gatherWishedPrincesses = new();
        private Princess _targetPrincess;

        private float _lastScanTime = float.NegativeInfinity;

        private InputAction _interactAction;

        private PlayerInput _playerInput;

        [Inject]
        private void Construct(PrincessActiveRepository activeRepository, HeroSettings settings, PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _activeRepository = activeRepository;
            _settings = settings;

            _interactAction = _playerInput.actions.FindAction("Interact");
        }

        public void Initialize()
        {
            _interactAction.started += TryGather;
        }

        public void Dispose()
        {
            _interactAction.started -= TryGather;
        }

        public void Tick()
        {
            if (Time.time - _lastScanTime >= _settings.RescanRate)
            {
                Rescan();
                _lastScanTime = Time.time;
            }
        }

        private void Rescan()
        {
            FindGatherWishedPrincesses();

            if (_gatherWishedPrincesses.Count > 0)
                UpdateTargetPrincess(GetClosestPrincess());
            else
                ClearTargetPrincess();
        }

        private void FindGatherWishedPrincesses()
        {
            _gatherWishedPrincesses.Clear();

            foreach (var princess in _activeRepository.UntiedFreePrincesses)
            {
                if (Vector2.Distance(transform.position, princess.Position) <=
                    _settings.DistanceToGather)
                {
                    _gatherWishedPrincesses.Add(princess);
                    princess.ShowGatherWish();
                }
                else
                {
                    princess.HideGatherWish();
                }
            }
        }

        private void UpdateTargetPrincess(Princess princess)
        {
            if (princess == _targetPrincess) return;

            ClearTargetPrincess();

            _targetPrincess = princess;
            _targetPrincess.ShowGatherHands();
        }

        private Princess GetClosestPrincess()
        {
            Princess closestPrincess = null;
            var closestDistance = float.PositiveInfinity;

            foreach (var princess in _gatherWishedPrincesses)
            {
                if (Vector2.Distance(transform.position, princess.Position) < closestDistance)
                {
                    closestPrincess = princess;
                    closestDistance = Vector2.Distance(transform.position, princess.Position);
                }
            }

            return closestPrincess;
        }

        private void ClearTargetPrincess()
        {
            if (_targetPrincess == null) return;

            _targetPrincess.HideGatherWish();
            _targetPrincess = null;
        }

        private void TryGather(InputAction.CallbackContext _)
        {
            if (_targetPrincess != null)
                _targetPrincess.Gather();
        }
    }
}
