using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsScript : MonoBehaviour {


    [SerializeField]
    private AudioSource myAudioSource;

    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private GameplayNavigation3 menu;

    [SerializeField]
    private AudioClip clip;

    public void LogPressed()
    {
        myAnimator.SetTrigger("Fire");
        myAudioSource.PlayOneShot(clip);
    }

    public void DestroyLog()
    {
        menu.ButtonAdvance();
        gameObject.SetActive(false);
    }

}
