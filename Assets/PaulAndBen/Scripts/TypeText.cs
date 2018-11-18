using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeText : MonoBehaviour {

    [SerializeField]
    private Text storyText;

    [SerializeField]
    private float textPause;

    [SerializeField]
    private AudioSource myAudioSource;

    private string newText;

    public void WriteText(string _text, AudioClip _textClip = null)
    {
        newText = _text;
        storyText.text = "";

        if (myAudioSource != null && _textClip != null)
        {
            PlayClipAudio(_textClip);
        }

        StartCoroutine(TextRoutine(newText));
    }

    private void PlayClipAudio(AudioClip _clip)
    {
        myAudioSource.Stop();
        myAudioSource.clip = _clip;
        myAudioSource.Play();
    }

    private IEnumerator TextRoutine (string _text)
    {
        int i = 0;
        foreach (char letter in _text.ToCharArray())
        {
            i++;
            storyText.text += letter;
            yield return new WaitForSeconds(textPause);
        }

      
    }
}
