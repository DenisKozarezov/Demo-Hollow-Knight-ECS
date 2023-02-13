using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public static class Delay
    {
        public static void For(float seconds, Action andThen)
        {
            if (seconds <= 0f) andThen();
            else
                Contexts.sharedInstance.game.coroutineRunner.Value.StartCoroutine(DoDelay(seconds, andThen));
        }

        private static IEnumerator DoDelay(float time, Action andThen)
        {
            yield return new WaitForSeconds(time);
            andThen();
        }
    }
}
