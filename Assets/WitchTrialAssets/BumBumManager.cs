using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BumBumManager : MonoBehaviour {

    [SerializeField]
    private string _NextScene;

	// Use this for initialization
    IEnumerator Start () {
        yield return new WaitForSeconds(2.0f);
        GetComponent<FadeToBlackScript>().currentStatus = FadeToBlackScript.FadeStatus.FadingToTransparent;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(_NextScene);
	}
}
