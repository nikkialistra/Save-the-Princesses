using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

namespace Characters.Moving.Elements
{
    public class CharacterPathfinding
    {
        public event Action<Vector2> MoveWith;
        public event Action<Vector2> MoveTo;
        public event Action Stop;

        public float RepathRate { private get; set; }

        private bool ShouldRepath => Time.time - _lastRepath >= RepathRate;

        private float _lastRepath = float.NegativeInfinity;

        private Vector2 _destination = Vector2.negativeInfinity;

        private readonly CharacterMoving _moving;
        private readonly Seeker _seeker;
        private readonly RVOController _rvoController;
        private readonly LineRenderer _lineRenderer;

        public CharacterPathfinding(CharacterMoving moving, Seeker seeker,
            RVOController rvoController, LineRenderer lineRenderer)
        {
            _moving = moving;
            _seeker = seeker;
            _rvoController = rvoController;
            _lineRenderer = lineRenderer;
        }

        public void Tick()
        {
            if (float.IsNegativeInfinity(_destination.x) || !ShouldRepath) return;

            SearchPath();
        }

        public void SetDestination(Vector2 value)
        {
            _destination = value;
        }

        public void ResetDestination()
        {
            _destination = Vector2.negativeInfinity;
        }

        private void SearchPath()
        {
            _seeker.StartPath(_seeker.transform.position, _destination, OnFinish);
            _lastRepath = Time.time;
        }

        private void OnFinish(Path path)
        {
            if (float.IsNegativeInfinity(_destination.x)) return;

            var vectorPath = path.vectorPath;

            if (vectorPath.Count > 1)
                UpdateMove(vectorPath[1]);
            else
                Stop?.Invoke();

            if (_moving.ShowPath)
                ShowPathLine(vectorPath);
        }

        private void UpdateMove(Vector3 destination)
        {
            if (_moving.ShouldLocallyAvoid)
                UpdateRvo(destination);
            else
                MoveTo?.Invoke(destination);
        }

        private void UpdateRvo(Vector2 destination)
        {
            _rvoController.SetTarget(destination, _moving.MovementSpeed, _moving.MovementSpeed);

            MoveWith?.Invoke(_rvoController.velocity);
        }

        private void ShowPathLine(List<Vector3> vectorPath)
        {
            if (vectorPath.Count <= 2)
            {
                _lineRenderer.positionCount = 0;
                return;
            }

            _lineRenderer.positionCount = vectorPath.Count;

            for (int i = 0; i < vectorPath.Count; i++)
                _lineRenderer.SetPosition(i, vectorPath[i]);
        }
    }
}
