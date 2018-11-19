using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    public static IEnumerator Tween(float duration, System.Action<float> TweenFn)
    {
        float BeginTime = Time.time;
        while (Time.time - BeginTime < duration)
        {
            float percent = (Time.time - BeginTime) / duration;
            TweenFn(percent);
            yield return null;
        }
        TweenFn(1);
    }

    public static T ChooseRandom<T>(T[] Options) {
        int index = Random.Range(0, Options.Length);
        return Options[index];
    }
}
