using System;
using System.Collections;
using UnityEngine;

public static class YieldHelper
{
    public static IEnumerator WaitForSeconds(float totalWaitTime, bool realTime = false)
    {
        float time = 0.0f;
        while (time < totalWaitTime)
        {
            time += (realTime ? Time.unscaledDeltaTime : Time.deltaTime);
            yield return null;
        }
    }

    public static IEnumerator WaitUntil(Func<bool> func)
    {
        yield return func();
    }
}