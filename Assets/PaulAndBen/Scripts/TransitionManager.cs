using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour {

    [SerializeField]
    TypeText storyText;

    [SerializeField]
    TypeText titleCardText;

    [SerializeField]
    TypeText nameText;

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


    void Start () {
        StartCoroutine(IntroCard());
	}

	void Update () {
		
	}

    private IEnumerator IntroCard()
    {
        titleCardText.WriteText(titleString);
        yield return new WaitForSeconds(2f);
        nameText.WriteText(nameString);
        yield return new WaitForSeconds(2f);
        storyText.WriteText(storyText1);
        
    }
}
