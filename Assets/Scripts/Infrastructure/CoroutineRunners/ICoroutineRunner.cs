using System.Collections;

namespace Infrastructure.CoroutineRunners
{
    public interface ICoroutineRunner
    {
        void StartCoroutine(IEnumerator coroutine);
    }
}
