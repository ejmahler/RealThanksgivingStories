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

        TextType.WriteText(_FirstHalfText, _FirstHalfAudio);

        yield return new WaitForSeconds(_FirstHalfAudio.length + 1.0f);

        yield return Utils.Tween(0.5f, (float Percent) =>
        {
            TextBoxCanvasGroup.alpha = 1.0f - Percent;
        });
        TextBox.text = "";
        TextBoxCanvasGroup.alpha = 1.0f;

        TextType.WriteText(_SecondHalfText, _SecondHalfAudio);

        yield return new WaitForSeconds(_SecondHalfAudio.length + 1.0f);
        Fade.currentStatus = FadeToBlackScript.FadeStatus.FadingToTransparent;
    }


}


