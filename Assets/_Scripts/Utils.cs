using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

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

    public static void PlayRandomized(this AudioSource Source, params AudioClip[] Clips) {
        Source.clip = Utils.ChooseRandom(Clips);
        Source.pitch = Random.Range(0.9f, 1.1f);
        Source.Play();
    }

    public static void PlayRandomizedDelayed(this AudioSource Source, float Delay, params AudioClip[] Clips)
    {
        Source.clip = Utils.ChooseRandom(Clips);
        Source.pitch = Random.Range(0.9f, 1.1f);
        Source.PlayDelayed(Delay);
    }
}
