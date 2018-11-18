using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeText : MonoBehaviour {

    [SerializeField]
    private Text storyText;

    [SerializeField]
    private float textPause;

    private string newText;

    public void WriteText(string _text)
    {
        newText = _text;
        storyText.text = "";
        StartCoroutine(TextRoutine(newText));
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
