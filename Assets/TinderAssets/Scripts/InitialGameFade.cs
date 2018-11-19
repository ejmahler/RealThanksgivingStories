using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private float _FirstHalfDelay;

    [SerializeField]
    private float _SecondHalfDelay;

    [SerializeField]
    private bool _AllowSpaceToSkip = false;

    [SerializeField]
    private string _LevelToLoad;

    private FadeToBlackScript Fade;
    private TypeText Typer;
    void Awake()
    {
        Fade = GetComponent<FadeToBlackScript>();
        Typer = GetComponent<TypeText>();
    }

    // Use this for initialization
    IEnumerator Start () {
        Text TextBox = GetComponentInChildren<Text>();
        CanvasGroup TextBoxCanvasGroup = TextBox.GetComponent<CanvasGroup>();

        Typer.WriteText(_FirstHalfText, _FirstHalfAudio);

        yield return new WaitForSeconds(_FirstHalfAudio == null ? _FirstHalfDelay : _FirstHalfAudio.length + 1.0f);

        yield return Utils.Tween(0.5f, (float Percent) =>
        {
            TextBoxCanvasGroup.alpha = 1.0f - Percent;
        });
        TextBox.text = "";
        TextBoxCanvasGroup.alpha = 1.0f;

        Typer.WriteText(_SecondHalfText, _SecondHalfAudio);

        yield return new WaitForSeconds(_SecondHalfAudio == null ? _SecondHalfDelay : _SecondHalfAudio.length + 1.0f);
        Fade.currentStatus = FadeToBlackScript.FadeStatus.FadingToTransparent;
    }

    void Update()
    {
        if(_AllowSpaceToSkip && Input.GetKeyDown(KeyCode.Space)) {
            StopAllCoroutines();
            Fade.currentStatus = FadeToBlackScript.FadeStatus.FadingToTransparent;
            Typer.StopAllCoroutines();
            Typer.WriteText("");
        }

        if(Fade.currentStatus == FadeToBlackScript.FadeStatus.FadingToTransparent) {
            if (_LevelToLoad != null)
            {
                DontDestroyOnLoad(gameObject);
                SceneManager.LoadScene(_LevelToLoad);
            }
        }
    }
}
