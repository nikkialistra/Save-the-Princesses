using System.Collections.Generic;
using GameData.Settings;
using Princesses;
using Rooms.Services.RepositoryTypes.Princesses;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Heroes
{
    public class HeroPrincessGathering
    {
        private readonly PrincessActiveRepository _activeRepository;

        private readonly List<Princess> _gatherWishedPrincesses = new();
        private Princess _targetPrincess;

        private float _lastScanTime = float.NegativeInfinity;

        private Hero _hero;

        private readonly InputAction _interactAction;

        public HeroPrincessGathering(PrincessActiveRepository activeRepository, Hero hero, PlayerInput playerInput)
        {
            _activeRepository = activeRepository;
            _hero = hero;

            _interactAction = playerInput.actions.FindAction("Interact");

            _interactAction.started += TryGather;
        }

        public void Dispose()
        {
            _interactAction.started -= TryGather;
        }

        public void Tick()
        {
            if (Time.time - _lastScanTime >= GameSettings.Hero.RescanRate)
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
                if (Vector2.Distance(_hero.Position, princess.Position) <=
                    GameSettings.Hero.DistanceToGather)
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
            _targetPrincess.ShowHands();
        }

        private Princess GetClosestPrincess()
        {
            Princess closestPrincess = null;
            var closestDistance = float.PositiveInfinity;

            foreach (var princess in _gatherWishedPrincesses)
            {
                if (Vector2.Distance(_hero.Position, princess.Position) < closestDistance)
                {
                    closestPrincess = princess;
                    closestDistance = Vector2.Distance(_hero.Position, princess.Position);
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
                _targetPrincess.GatherTo(_hero.Train);
        }
    }
}
