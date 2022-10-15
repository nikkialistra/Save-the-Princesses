using System.Collections;
using UnityEngine;

namespace Infrastructure.CoroutineRunners
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public new void StartCoroutine(IEnumerator coroutine)
        {
            base.StartCoroutine(coroutine);
        }
    }
}
