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

    private int numHousesWarned;

    [SerializeField]
    private List<HouseScript> houses = new List<HouseScript>();

    void Start () {

        CheckText();

        for (int i = 0; i < houses.Count; i++)
        {
            houses[i].OnHouseWarned = null;
            houses[i].OnHouseWarned += HouseWarned;
        }

	}

    private void HouseWarned()
    {
        numHousesWarned++;
        CheckText();
    }

    private void CheckText()
    {
        switch (numHousesWarned)
        {
            case 0:
                storyText.WriteText(storyText1);
                break;

            case 3:
                storyText.WriteText(storyText2);
                break;

            case 6:
                storyText.WriteText(storyText3);
                break;

            case 9:
                storyText.WriteText(storyText4);
                break;

            case 13:
                SceneManager.LoadScene("BenTransitionScene");
                break;
        }
    }
}
