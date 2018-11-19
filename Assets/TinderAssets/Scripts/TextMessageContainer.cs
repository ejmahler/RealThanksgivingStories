using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextMessageContainer : MonoBehaviour {

    [SerializeField]
    private RectTransform _JohnSmithPrefab;

    [SerializeField]
    private RectTransform _PocahontasPrefab;

    [SerializeField]
    private FadeToBlackScript _FadePrefab;

    [SerializeField]
    private float NextSpawnY = 0f;

    [SerializeField]
    private float _MessageOffset = 80f;

    [SerializeField]
    private AudioClip[] _SendSounds;

    [SerializeField]
    private AudioClip _RecieveSound;

    [SerializeField]
    private DialogueChooser _Chooser;

    private RectTransform rectTransform;
    private AudioSource SendSource;
	void Awake () {
        rectTransform = GetComponent<RectTransform>();
        SendSource = GetComponent<AudioSource>();
    }

    private IEnumerator AddTextMessage(RectTransform MessagePrefab, string MessageText) {
        if(MessagePrefab == _JohnSmithPrefab) {
            SendSource.clip = Utils.ChooseRandom(_SendSounds);
        }
        else {
            SendSource.clip = _RecieveSound;
        }
        SendSource.pitch = Random.Range(0.9f, 1.1f);
        SendSource.Play();

        RectTransform NewMessage = Instantiate<RectTransform>(MessagePrefab);
        NewMessage.GetComponentInChildren<Text>().text = MessageText;
        NewMessage.SetParent(transform, false);
        NewMessage.anchoredPosition = new Vector2(0, NextSpawnY);
        NextSpawnY -= _MessageOffset;

        float OldContainerPos = rectTransform.anchoredPosition.y;
        float TargetContainerPos = OldContainerPos + _MessageOffset;
        yield return Utils.Tween(0.5f, (float Percent) =>
        {
            float NewY = Mathf.SmoothStep(OldContainerPos, TargetContainerPos, Percent);
            rectTransform.anchoredPosition = new Vector2(0, NewY);
        });
    }

    IEnumerator Start() {
        yield return new WaitForSeconds(2.0f);

        _Chooser.ShowDialogueChoice(
            "Do you like white meat?",
            "I’m gonna manifest my destiny all over you",
            "Are you the new world baby? Because I’m Plymouth Rock-hard"
        );
        yield return new WaitUntil(() => _Chooser.ChosenDialogue != null);
        yield return AddTextMessage(_JohnSmithPrefab, _Chooser.ChosenDialogue);
        yield return new WaitForSeconds(3.0f);





        yield return AddTextMessage(_PocahontasPrefab, "I’m not looking for anything serious. Just a Founding Daddy.");
        yield return new WaitForSeconds(1.0f);






        _Chooser.ShowDialogueChoice(
            "Have you heard about our Lord and Savior, Jesus Christ?",
            "Let me show you my John Hancock",
            "Baby, let’s take it slow, I’m a Virginian"
        );
        yield return new WaitUntil(() => _Chooser.ChosenDialogue != null);
        yield return AddTextMessage(_JohnSmithPrefab, _Chooser.ChosenDialogue);
        yield return new WaitForSeconds(3.0f);






        yield return AddTextMessage(_PocahontasPrefab, "You better treat me right. My ex left me with a Trail of Tears.");
        yield return new WaitForSeconds(1.0f);







        _Chooser.ShowDialogueChoice(
            "Just come over baby, I’ll keep you warm. I’ve got a lot of blankets",
            "Have you ever seen the inside of a cabin before?",
            "Let’s have a pow-wow. You can puff on my peace pipe"
        );
        yield return new WaitUntil(() => _Chooser.ChosenDialogue != null);
        yield return AddTextMessage(_JohnSmithPrefab, _Chooser.ChosenDialogue);
        yield return new WaitForSeconds(3.0f);





        yield return AddTextMessage(_PocahontasPrefab, "My phone number is 6");
        yield return new WaitForSeconds(1.0f);





        yield return new WaitForSeconds(3.0f);
        _Chooser.ShowDialogueChoice(
            "My friends are doing this Thanksgiving thing. It’s pretty low-key. U should cum."
        );
        yield return new WaitUntil(() => _Chooser.ChosenDialogue != null);
        yield return AddTextMessage(_JohnSmithPrefab, _Chooser.ChosenDialogue);
        yield return new WaitForSeconds(3.0f);


        // done! fade to the end level
        FadeToBlackScript FadeInstance = Instantiate<FadeToBlackScript>(_FadePrefab);
        yield return new WaitUntil(() => FadeInstance.IsOpaque);
        SceneManager.LoadScene("FinalCard");
    }
}
