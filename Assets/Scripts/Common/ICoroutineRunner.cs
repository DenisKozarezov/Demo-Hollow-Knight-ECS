using System.Collections;
using UnityEngine;

namespace Core
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        Coroutine StartCoroutine(string coroutine);
        void StopCoroutine(IEnumerator coroutine);
        void StopCoroutine(string coroutine);
    }

    public class AsyncProcessor : MonoBehaviour, ICoroutineRunner
    {

    }
}
