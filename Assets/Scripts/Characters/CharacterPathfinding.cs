using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(RVOController))]
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(CharacterMoving))]
    public class CharacterPathfinding : MonoBehaviour
    {
        public event Action<Vector2> MoveWith;
        public event Action<Vector2> MoveTo;
        public event Action Stop;

        public float RepathRate { private get; set; }

        [SerializeField] private bool _shouldLocallyAvoid;
        [Space]
        [SerializeField] private bool _showPath;

        private bool ShouldRepath => Time.time - _lastRepath >= RepathRate;

        private float _lastRepath = float.NegativeInfinity;

        private Vector2 _destination = Vector2.negativeInfinity;

        private Seeker _seeker;
        private RVOController _rvoController;
        private LineRenderer _lineRenderer;
        private CharacterMoving _moving;

        public void Initialize()
        {
            _seeker = GetComponent<Seeker>();
            _rvoController = GetComponent<RVOController>();
            _lineRenderer = GetComponent<LineRenderer>();
            _moving = GetComponent<CharacterMoving>();
        }

        private void Update()
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
            _seeker.StartPath(transform.position, _destination, OnFinish);
            _lastRepath = Time.time;
        }

        private void OnFinish(Path path)
        {
            if (float.IsNegativeInfinity(_destination.x)) return;

            var vectorPath = path.vectorPath;

            if (vectorPath.Count > 1)
                UpdateMove(vectorPath);
            else
                Stop?.Invoke();

            if (_showPath)
                ShowPathLine(vectorPath);
        }

        private void UpdateMove(List<Vector3> vectorPath)
        {
            if (_shouldLocallyAvoid)
                UpdateRvo(vectorPath[1]);
            else
                MoveTo?.Invoke(vectorPath[1]);
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
