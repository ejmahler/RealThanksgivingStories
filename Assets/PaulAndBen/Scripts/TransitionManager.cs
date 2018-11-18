using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionManager : MonoBehaviour {

    [SerializeField]
    TypeText storyText;

    [SerializeField]
    TypeText titleCardText;

    [SerializeField]
    TypeText nameText;

    [SerializeField]
    private GameObject paulForward;

    [SerializeField]
    private GameObject paulBack;

    [SerializeField]
    private GameObject benForward;

    [SerializeField]
    private GameObject benBack;


    [SerializeField]
    private GameObject titleCard;

    [SerializeField]
    private GameObject instructionText;

    [SerializeField]
    private string titleString;

    [SerializeField]
    private string nameString;

    [SerializeField]
    private string storyText1;

    [SerializeField]
    private string storyText2;

    [SerializeField]
    private string storyText3;

    [SerializeField]
    private string storyText4;

    [SerializeField]
    private string storyText5;

    [SerializeField]
    private AudioClip titleClip;

    [SerializeField]
    private AudioClip nameClip;

    [SerializeField]
    private AudioClip storyClip1;

    [SerializeField]
    private AudioClip storyClip2;

    [SerializeField]
    private AudioClip storyClip3;

    [SerializeField]
    private AudioClip storyClip4;

    [SerializeField]
    private AudioClip storyClip5;

    [SerializeField]
    private GameObject titleCardFade;

    private int sceneNum;

    private bool canChangeScene;


    void Start () {
        StartCoroutine(IntroCard());
	}

    void Update() {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene();
            sceneNum++;
        }

	}

    private void ChangeScene()
    {
        switch (sceneNum)
        {
            case 0:
                titleCardFade.SetActive(true);
                titleCard.SetActive(false);
                BenTalk();
                storyText.WriteText(storyText1,storyClip1);
                break;

            case 1:
                PaulTalk();
                storyText.WriteText(storyText2, storyClip2);
                break;

            case 2:
                BenTalk();
                storyText.WriteText(storyText3, storyClip3);
                break;

            case 3:
                PaulTalk();
                storyText.WriteText(storyText4, storyClip4);
                break;

            case 4:
                BenTalk();
                storyText.WriteText(storyText5, storyClip5);
                break;

            case 5:
                SceneManager.LoadScene("BenScene");
                break;
        } 
    }

    private IEnumerator IntroCard()
    {
        titleCardText.WriteText(titleString, titleClip);
        yield return new WaitForSeconds(2f);
        nameText.WriteText(nameString, nameClip);
        yield return new WaitForSeconds(2f);
        instructionText.SetActive(true);
        canChangeScene = true;
        
    }

    private void BenTalk()
    {
        benForward.SetActive(true);
        benBack.SetActive(false);
        paulForward.SetActive(false);
        paulBack.SetActive(true);
    }

    private void PaulTalk()
    {
        benForward.SetActive(false);
        benBack.SetActive(true);
        paulForward.SetActive(true);
        paulBack.SetActive(false);
    }
}
