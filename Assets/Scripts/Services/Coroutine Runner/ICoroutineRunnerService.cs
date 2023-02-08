using System.Collections;
using UnityEngine;

namespace Core.Services
{
    public interface ICoroutineRunnerService
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(IEnumerator coroutine);
    }
}
