using System.Collections.Generic;
using Characters;
using GameData.Princesses.Appearance.Elements;
using GameData.Princesses.Appearance.Registries;
using UnityEngine;
using Zenject;
using static Princesses.Types.ElementType;

namespace Princesses
{
    public class PrincessElementControllers : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");

        [SerializeField] private Animator _head;
        [SerializeField] private Animator _garment;
        [SerializeField] private Animator _hair;
        [SerializeField] private Animator _bodyAccessory;
        [SerializeField] private Animator _headAccessory;

        private readonly List<Animator> _animators = new();

        private PrincessElementControllersRegistry _registry;

        private Princess _princess;
        private CharacterAnimator _characterAnimator;

        [Inject]
        public void Construct(PrincessElementControllersRegistry registry)
        {
            _registry = registry;

            _animators.Add(_head);
            _animators.Add(_garment);
            _animators.Add(_hair);
            _animators.Add(_bodyAccessory);
            _animators.Add(_headAccessory);
        }

        public void Initialize(Princess princess, CharacterAnimator characterAnimator)
        {
            _princess = princess;
            _characterAnimator = characterAnimator;

            FillControllers();

            _characterAnimator.UpdateFinish += UpdateElementAnimations;
        }

        public void Dispose()
        {
            _characterAnimator.UpdateFinish -= UpdateElementAnimations;
        }

        private void FillControllers()
        {
            var type = _princess.Type;

            _head.runtimeAnimatorController = _registry.GetRandomFor(type, Head);
            _garment.runtimeAnimatorController = _registry.GetRandomFor(type, Garment);
            _hair.runtimeAnimatorController = _registry.GetRandomFor(type, Hair);
            _bodyAccessory.runtimeAnimatorController = _registry.GetRandomFor(type, BodyAccessory);
            _headAccessory.runtimeAnimatorController = _registry.GetRandomFor(type, HeadAccessory);
        }

        private void UpdateElementAnimations(CharacterAnimator.AnimationStatus status)
        {
            foreach (var animator in _animators)
            {
                animator.SetFloat(MoveX, status.Velocity.x);
                animator.SetFloat(MoveY, status.Velocity.y);
            }
        }
    }
}
