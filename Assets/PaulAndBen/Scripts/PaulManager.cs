using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaulManager : MonoBehaviour {

    [SerializeField]
    private TypeText storyText;

    [SerializeField]
    private string storyText1;

    [SerializeField]
    private string storyText2;

    [SerializeField]
    private string storyText3;

    [SerializeField]
    private string storyText4;

    [SerializeField]
    private AudioClip narr1;

    [SerializeField]
    private AudioClip narr2;

    [SerializeField]
    private AudioClip narr3;

    [SerializeField]
    private AudioClip narr4;

    [SerializeField]
    private MoveCharacter paul;

    [SerializeField]
    private GameObject instructionText;

    [SerializeField]
    private GameObject storyTimeText;

    private int numHousesWarned;

    [SerializeField]
    private List<HouseScript> houses = new List<HouseScript>();

    void Start () {

        StartCoroutine(CheckText());

        for (int i = 0; i < houses.Count; i++)
        {
            houses[i].OnHouseWarned = null;
            houses[i].OnHouseWarned += HouseWarned;
        }

	}

    private void HouseWarned()
    {
        numHousesWarned++;
        StartCoroutine(CheckText());
    }

    private IEnumerator CheckText()
    {
        switch (numHousesWarned)
        {
            case 0:
                instructionText.SetActive(false);
                storyTimeText.SetActive(true);
                paul.MovementStatus(false);
                storyText.WriteText(storyText1, narr1);
                StartCoroutine(PauseMovement(narr1.length));
                break;

            case 2:
                instructionText.SetActive(false);
                storyTimeText.SetActive(true);
                paul.MovementStatus(false);
                yield return new WaitForSeconds(2f);
                storyText.WriteText(storyText2, narr2);
                StartCoroutine(PauseMovement(narr2.length));
                break;

            case 4:
                instructionText.SetActive(false);
                storyTimeText.SetActive(true);
                paul.MovementStatus(false);
                yield return new WaitForSeconds(2f);
                storyText.WriteText(storyText3, narr3);
                StartCoroutine(PauseMovement(narr3.length));
                break;

            case 6:
                instructionText.SetActive(false);
                storyTimeText.SetActive(true);
                paul.MovementStatus(false);
                yield return new WaitForSeconds(2f);
                storyText.WriteText(storyText4, narr4);
                StartCoroutine(PauseMovement(narr4.length));
                break;

            case 9:
                instructionText.SetActive(false);
                storyTimeText.SetActive(true);
                paul.MovementStatus(false);
                StartCoroutine(DelaySceneChange());
                break;
        }
    }

    private IEnumerator PauseMovement(float _pauseTime)
    {

        yield return new WaitForSeconds(_pauseTime);
        paul.MovementStatus(true);
        instructionText.SetActive(true);
        storyTimeText.SetActive(false);
    }

    private IEnumerator DelaySceneChange()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("BenTransitionScene");
    }
}
