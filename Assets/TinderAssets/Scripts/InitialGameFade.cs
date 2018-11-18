using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialGameFade : MonoBehaviour {

    [SerializeField]
    [TextArea]
    private string _FirstHalfText;

    [SerializeField]
    [TextArea]
    private string _SecondHalfText;

    [SerializeField]
    private AudioClip _FirstHalfAudio;

    [SerializeField]
    private AudioClip _SecondHalfAudio;

    // Use this for initialization
    IEnumerator Start () {
        FadeToBlackScript Fade = GetComponent<FadeToBlackScript>();
        Text TextBox = GetComponentInChildren<Text>();
        CanvasGroup TextBoxCanvasGroup = TextBox.GetComponent<CanvasGroup>();
        AudioSource DialogueSource = GetComponent<AudioSource>();
        TypeText TextType = GetComponent<TypeText>();

        TextType.WriteText(_FirstHalfText);

        yield return new WaitForSeconds(8.0f);

        yield return Tween(0.5f, (float Percent) =>
        {
            TextBoxCanvasGroup.alpha = 1.0f - Percent;
        });
        TextBox.text = "";
        TextBoxCanvasGroup.alpha = 1.0f;

        TextType.WriteText(_SecondHalfText);

        yield return new WaitForSeconds(8.0f);
        Fade.currentStatus = FadeToBlackScript.FadeStatus.FadingToTransparent;
    }

    public static IEnumerator Tween(float duration, System.Action<float> TweenFn) {
        float BeginTime = Time.time;
        while(Time.time - BeginTime < duration) {
            float percent = (Time.time - BeginTime) / duration;
            TweenFn(percent);
            yield return null;
        }
        TweenFn(1);
    }
}


