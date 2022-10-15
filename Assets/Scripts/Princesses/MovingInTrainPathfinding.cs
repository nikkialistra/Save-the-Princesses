using Heroes;
using UnityEngine;
using Zenject;

namespace Princesses
{
    public class MovingInTrainPathfinding : MonoBehaviour
    {
        [SerializeField] private float _colliderCenterY = 0.2f;
        [SerializeField] private float _distanceToColliderBorder = 0.2f;

        [Space]
        [SerializeField] private float _rescanTime = 0.1f;
        [SerializeField] private float _distanceToRaycast = 0.4f;
        [SerializeField] private LayerMask _layerMask;

        [Space]
        [SerializeField] private float _obstructionAvoidTryAngle = 5f;
        [SerializeField] private int _obstructionAvoidTryMaxCount = 20;

        private bool ShouldRescan => Time.time - _lastScanTime >= _rescanTime;

        private Vector2 ColliderCenter => (Vector2)transform.position + new Vector2(0, _colliderCenterY);

        private float _lastScanTime = float.NegativeInfinity;

        private Vector2 _currentDirection = new();

        private Vector2 _currentLeftBorder = new();
        private Vector2 _currentRightBorder = new();

        private int _obstructionAvoidTryCount;

        private Hero _hero;

        [Inject]
        public void Construct(Hero hero)
        {
            _hero = hero;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(_currentLeftBorder, _currentLeftBorder + (_currentDirection * _distanceToRaycast));
            Gizmos.DrawLine(_currentRightBorder, _currentRightBorder + (_currentDirection * _distanceToRaycast));
        }

        public Vector2 FindDirectionToHero()
        {
            if (!ShouldRescan)
                return _currentDirection;
            else
                _lastScanTime = Time.time;

            _currentDirection = (_hero.PositionCenter - ColliderCenter).normalized;

            var perpendicularDirection = -1 * Vector2.Perpendicular(_currentDirection);

            _currentLeftBorder = ColliderCenter - perpendicularDirection * _distanceToColliderBorder;
            _currentRightBorder = ColliderCenter + perpendicularDirection * _distanceToColliderBorder;

            return FindDirection();
        }

        public void ResetScanning()
        {
            _lastScanTime = float.NegativeInfinity;
        }

        private Vector2 FindDirection()
        {
            while (_obstructionAvoidTryCount < _obstructionAvoidTryMaxCount)
            {
                _obstructionAvoidTryCount++;

                if (TryFindDirectionWithoutObstructions())
                    break;
            }

            _obstructionAvoidTryCount = 0;

            return _currentDirection;
        }

        private bool TryFindDirectionWithoutObstructions()
        {
            var (leftObstructed, rightObstructed) = FindObstructionsAhead(_currentDirection);

            if (!leftObstructed && !rightObstructed) return true;

            if (leftObstructed && rightObstructed)
            {
                RotateDirectionToСlosestFreePassage();
                return true;
            }

            TryAnotherDirection(leftObstructed, rightObstructed);
            return false;
        }

        private void TryAnotherDirection(bool leftObstructed, bool rightObstructed)
        {
            if (leftObstructed)
                _currentDirection = RotateDirectionTo(_currentDirection, _obstructionAvoidTryAngle);
            else if (rightObstructed)
                _currentDirection = RotateDirectionTo(_currentDirection, -1 * _obstructionAvoidTryAngle);
        }

        private void RotateDirectionToСlosestFreePassage()
        {
            var directionToRight = _currentDirection;
            var directionToLeft = _currentDirection;

            var finalDirection = _currentDirection;

            while (_obstructionAvoidTryCount < _obstructionAvoidTryMaxCount)
            {
                _obstructionAvoidTryCount++;

                if (TryFindClosestFreePassage(ref directionToRight, ref directionToLeft, ref finalDirection))
                    break;
            }

            _currentDirection = finalDirection;
        }

        private bool TryFindClosestFreePassage(ref Vector2 directionToRight, ref Vector2 directionToLeft,
            ref Vector2 finalDirection)
        {
            directionToRight = RotateDirectionTo(directionToRight, _obstructionAvoidTryAngle);
            directionToLeft = RotateDirectionTo(directionToLeft, -1 * _obstructionAvoidTryAngle);

            if (DirectionHasFreePassage(directionToRight))
            {
                finalDirection = directionToRight;
                return true;
            }

            if (DirectionHasFreePassage(directionToLeft))
            {
                finalDirection = directionToLeft;
                return true;
            }

            return false;
        }

        private bool DirectionHasFreePassage(Vector2 direction)
        {
            var (leftObstructed, rightObstructed) = FindObstructionsAhead(direction);

            return !leftObstructed && !rightObstructed;
        }

        private (bool, bool) FindObstructionsAhead(Vector2 direction)
        {
            var leftObstructed =
                Physics2D.Raycast(_currentLeftBorder, direction, _distanceToRaycast, _layerMask).collider != null;
            var rightObstructed =
                Physics2D.Raycast(_currentRightBorder, direction, _distanceToRaycast, _layerMask).collider != null;

            return (leftObstructed, rightObstructed);
        }

        private Vector2 RotateDirectionTo(Vector2 direction, float angle)
        {
            return Quaternion.Euler(0, 0, -1 * angle) * direction;
        }
    }
}
