using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat.Attacks
{
    [RequireComponent(typeof(Animator))]
    public class AttackAnimator : MonoBehaviour
    {
        private const string NoStrokeTag = "NoStroke";

        public event Action Start;
        public event Action End;

        public bool IsStroking { get; private set; }

        [SerializeField] private List<AnimationClip> _strokes = new();

        private readonly List<int> _strokeHashes = new();

        private StrokeType _lastStroke;

        private static readonly int CancelStrokeTag = Animator.StringToHash("cancelStroke");

        private Animator _animator;

        public void Initialize()
        {
            _animator = GetComponent<Animator>();

            foreach (var stroke in _strokes)
                _strokeHashes.Add(Animator.StringToHash(stroke.name));

            _lastStroke = (StrokeType)_strokes.Count;
        }

        private void Update()
        {
            var isStroking = _animator.GetCurrentAnimatorStateInfo(0).IsTag(NoStrokeTag) == false;

            NotifyOnStatusChange(isStroking);

            IsStroking = isStroking;
        }

        private void NotifyOnStatusChange(bool isStroking)
        {
            if (isStroking == IsStroking) return;

            if (isStroking)
                Start?.Invoke();
            else
                End?.Invoke();
        }

        public void Stroke(StrokeType stroke)
        {
            _animator.ResetTrigger(CancelStrokeTag);

            var strokeAnimationHash = GetCorrespondingAnimation(stroke);
            _animator.Play(strokeAnimationHash);
        }

        public void CancelStroke()
        {
            _animator.SetTrigger(CancelStrokeTag);
        }

        private int GetCorrespondingAnimation(StrokeType stroke)
        {
            CheckValidity(stroke);

            return stroke switch
            {
                StrokeType.First => _strokeHashes[0],
                StrokeType.Second => _strokeHashes[1],
                StrokeType.Third => _strokeHashes[2],
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void CheckValidity(StrokeType stroke)
        {
            if ((int)stroke > (int)_lastStroke)
                throw new InvalidOperationException($"Cannot make {stroke} stroke when last possible is {_lastStroke}");
        }
    }
}
