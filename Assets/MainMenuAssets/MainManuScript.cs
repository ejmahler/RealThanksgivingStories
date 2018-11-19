using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManuScript : MonoBehaviour {

    [SerializeField]
    private FadeToBlackScript _FadePrefab;

    [SerializeField]
    private FadeToBlackScript _CreditsPrefab;

    [SerializeField]
    private FadeToBlackScript _TitleCardFadeInstance;

    bool IsLoadingLevel = false;
    FadeToBlackScript CreditsInstance;

    public void OnTinderClicked() {
        StartCoroutine(LoadLevel("TinderScreen1"));
    }
    public void OnTrialClicked() {
        StartCoroutine(LoadLevel("WitchTrial_IntroSong"));
    }
    public void OnBennyFranksClicked() {
        StartCoroutine(LoadLevel("PaulScene"));
    }

    IEnumerator LoadLevel(string LevelName) {
        if(IsLoadingLevel) {
            yield break;
        }
        IsLoadingLevel = true;

        FadeToBlackScript FadeInstance = Instantiate<FadeToBlackScript>(_FadePrefab);
        yield return new WaitUntil(() => FadeInstance.IsOpaque);

        SceneManager.LoadScene(LevelName);
    }

    void Update()
    {
        if(!_TitleCardFadeInstance && !IsLoadingLevel && Input.GetKeyDown(KeyCode.Tab)) {
            if(!CreditsInstance) {
                CreditsInstance = Instantiate<FadeToBlackScript>(_CreditsPrefab);
            }
            else {
                switch(CreditsInstance.currentStatus) {
                    case FadeToBlackScript.FadeStatus.Black:
                        CreditsInstance.currentStatus = FadeToBlackScript.FadeStatus.FadingToTransparent;
                        break;
                    case FadeToBlackScript.FadeStatus.FadingToBlack:
                        CreditsInstance.currentStatus = FadeToBlackScript.FadeStatus.FadingToTransparent;
                        break;
                    case FadeToBlackScript.FadeStatus.FadingToTransparent:
                        CreditsInstance.currentStatus = FadeToBlackScript.FadeStatus.FadingToBlack;
                        break;
                }
            }
        }
    }
}
