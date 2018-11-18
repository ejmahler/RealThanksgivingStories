using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCardManager : MonoBehaviour {

	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }

	}
}
