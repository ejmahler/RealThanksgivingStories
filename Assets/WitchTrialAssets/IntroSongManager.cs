using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSongManager : MonoBehaviour {

    [SerializeField]
    private string _NextScene;

    IEnumerator Start() {
        yield return new WaitForSeconds(1.0f);
        GetComponent<AudioSource>().Play();
        GetComponent<Animator>().enabled = true;
    }

    public void OnSongDone() {
        SceneManager.LoadScene(_NextScene);
    }
}
