using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlackScript : MonoBehaviour {

    public enum FadeStatus { FadingToBlack, FadingToTransparent, Black }
    public FadeStatus currentStatus;

    private CanvasGroup canvasGroup;

    [SerializeField]
    private float _FadeToBlackTime = 1.0f;

    [SerializeField]
    private float _FadeToTransparentTime = 1.0f;

    public bool IsOpaque { get { return canvasGroup.alpha >= 1.0f; }}


    // Use this for initialization
    void Awake () {

        canvasGroup = GetComponent<CanvasGroup>();
        switch(currentStatus) {
            case FadeStatus.Black:
                canvasGroup.alpha = 1f;
                break;
            case FadeStatus.FadingToBlack:
                canvasGroup.alpha = 0f;
                break;
            case FadeStatus.FadingToTransparent:
                canvasGroup.alpha = 1f;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(currentStatus == FadeStatus.FadingToBlack) {
            canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha + Time.deltaTime / _FadeToBlackTime);

            if(IsOpaque) {
                currentStatus = FadeStatus.Black;
            }
        }
        else if (currentStatus == FadeStatus.FadingToTransparent)
        {
            canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha - Time.deltaTime / _FadeToTransparentTime);

            if(canvasGroup.alpha <= 0.0f) {
                Destroy(gameObject);
            }
        }
    }
}
