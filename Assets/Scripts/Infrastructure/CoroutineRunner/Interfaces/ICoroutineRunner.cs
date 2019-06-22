using System.Collections;

namespace Infrastructure.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        void RunCoroutine(IEnumerator coroutine);
    }
}


