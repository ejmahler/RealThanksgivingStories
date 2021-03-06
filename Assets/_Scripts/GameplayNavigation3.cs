﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayNavigation3 : MonoBehaviour {
    
    [SerializeField]
    private Text _textCrawler;

    [SerializeField]
    private GameObject _choiceBox;
    [SerializeField]
    private Text _choiceBox1;
    [SerializeField]
    private Text _choiceBox2;
    //[SerializeField]
    //private Text _choiceBox3;
    //[SerializeField]
    //private Text _choiceBox4;

    [SerializeField]
    private AudioSource myAudioSource;

    [SerializeField]
    private AudioSource fireSource;

    [SerializeField]
    private AudioClip fireClip;

    [System.Serializable]
    struct StoryPoints
    {
        public string dialogue;
        public string[] choices;
        public GameObject speaker;
        public string speakerAnimation;
        public AudioClip textclip;
    }

    [SerializeField]
    private StoryPoints[] _storyPoints;
    private int _currentStoryPointIterator = 0;

    private int numtransitions;


    // Use this for initialization
	void Start () {
        LoadInitialStoryText(0);
	}

    public void ButtonAdvance()
    {
        numtransitions++;
        AdvanceStory();
        fireSource.PlayOneShot(fireClip);
        if (numtransitions >= 4)
        {
            StartCoroutine(DelayEndRoutine());
        }
    }

    private IEnumerator DelayEndRoutine()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("FinalCard");
    }

    void AdvanceStory()
    {

        if( _currentStoryPointIterator < _storyPoints.Length
            && (_choiceBox.activeInHierarchy || _storyPoints[_currentStoryPointIterator].choices.Length == 0))
        {
            _currentStoryPointIterator++;
            ClearChoices();
            LoadInitialStoryText(_currentStoryPointIterator);
        }
        else if (_currentStoryPointIterator < _storyPoints.Length)
        {
            _choiceBox.SetActive(true);
            LoadChoicesInStory(_currentStoryPointIterator);
        }
        else
        {
            Debug.Log("Loading Next Scene");
        }
    }

    void LoadInitialStoryText(int iterator)
    {
        if (iterator != 0)
        {
            if (_storyPoints[iterator - 1].speaker.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Talking"))
            {
                _storyPoints[iterator - 1].speaker.GetComponent<Animator>().Play("Idle");
            }
        }

        if (iterator >= _storyPoints.Length)
        {
            SceneManager.LoadScene("FinalCard");
            return;
        }

        StoryPoints currentStoryPoint = _storyPoints[iterator];
        _textCrawler.text = currentStoryPoint.dialogue;
        myAudioSource.Stop();
        myAudioSource.clip = currentStoryPoint.textclip;
        myAudioSource.Play();
       // Debug.LogWarning("current Story Speaker" + currentStoryPoint.speakerAnimation);
        currentStoryPoint.speaker.GetComponent<Animator>().SetTrigger(currentStoryPoint.speakerAnimation);
    }

    void LoadChoicesInStory(int iterator)
    {
        _choiceBox.SetActive(true);
        _storyPoints[iterator].speaker.GetComponent<Animator>().SetTrigger("Idle");
        _choiceBox1.text = _storyPoints[iterator].choices[0];
        _choiceBox2.text = _storyPoints[iterator].choices[1];
        //_choiceBox3.text = _storyPoints[iterator].choices[2];
        //_choiceBox4.text = _storyPoints[iterator].choices[3];
    }

    void ClearChoices()
    {
        _choiceBox1.text = "";
        _choiceBox2.text = "";
        //_choiceBox3.text = "";
        //_choiceBox4.text = "";
        _choiceBox.SetActive(false);
    }
}
