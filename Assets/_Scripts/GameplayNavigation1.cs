using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayNavigation1 : MonoBehaviour {
    
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
    private float _mouseClickBuffer = 0.2f;

    private bool waitForButton;

    // Use this for initialization
	void Start () {
        LoadInitialStoryText(0);
	}
	
	// Update is called once per frame
	void Update () {
        MouseClickListener();
	}

    void MouseClickListener()
    {
        if (!waitForButton)
        {
            if (_mouseClickBuffer > 0)
            {
                _mouseClickBuffer -= Time.deltaTime;
                return;
            }

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                AdvanceStory();
                _mouseClickBuffer = 0.2f;
            }
        }
    }

    public void ButtonAdvance()
    {
        AdvanceStory();
    }

    void AdvanceStory()
    {

        if( _currentStoryPointIterator < _storyPoints.Length
            && (_choiceBox.activeInHierarchy || _storyPoints[_currentStoryPointIterator].choices.Length == 0))
        {
            waitForButton = false;
            _currentStoryPointIterator++;
            ClearChoices();
            LoadInitialStoryText(_currentStoryPointIterator);
        }
        else if (_currentStoryPointIterator < _storyPoints.Length)
        {
            waitForButton = true;
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
            SceneManager.LoadScene("WitchTrial4");
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
