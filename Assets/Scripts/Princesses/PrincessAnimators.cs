using System.Collections;
using System.Collections.Generic;
using GameData.Settings;
using UnityEngine;

namespace Princesses
{
    public class PrincessAnimators : MonoBehaviour
    {
        private static readonly int TiedId = Animator.StringToHash("tied");
        private static readonly int TiedHitId = Animator.StringToHash("tied_hit");
        private static readonly int UntieId = Animator.StringToHash("untie");

        [SerializeField] private List<Animator> _animators;
        [SerializeField] private Animator _rope;
        [SerializeField] private Animator _light;

        private GameObject _ropeAngLightParent;

        public void Initialize()
        {
            _ropeAngLightParent = _rope.transform.parent.gameObject;
        }

        public void TiedHit()
        {
            foreach (var animator in _animators)
                animator.SetTrigger(TiedHitId);

            _rope.SetTrigger(TiedHitId);
        }

        public void Untie()
        {
            foreach (var animator in _animators)
                animator.SetBool(TiedId, false);

            _rope.SetBool(TiedId, false);

            _light.SetTrigger(UntieId);

            StartCoroutine(DisableRopeAndLightAfter());
        }

        private IEnumerator DisableRopeAndLightAfter()
        {
            yield return new WaitForSeconds(GameSettings.Princess.TimeToDisableRopeAndLight);

            _ropeAngLightParent.SetActive(false);
        }
    }
}
