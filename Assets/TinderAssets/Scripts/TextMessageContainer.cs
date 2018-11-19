using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMessageContainer : MonoBehaviour {

    [SerializeField]
    private RectTransform _JohnSmithPrefab;

    [SerializeField]
    private RectTransform _PocahontasPrefab;

    [SerializeField]
    private float NextSpawnY = 0f;

    [SerializeField]
    private float _MessageOffset = 80f;

    RectTransform rectTransform;
	void Awake () {
        rectTransform = GetComponent<RectTransform>();
    }

    private IEnumerator AddTextMessage(RectTransform MessagePrefab, string MessageText) {
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

        // TODO: first text choice
        yield return AddTextMessage(_JohnSmithPrefab, "Are you the new world baby? Because I’m Plymouth Rock-hard.");
        yield return new WaitForSeconds(3.0f);

        yield return AddTextMessage(_PocahontasPrefab, "I’m not looking for anything serious. Just a Founding Daddy.");
        yield return new WaitForSeconds(1.0f);

        // TODO: second text choice
        yield return AddTextMessage(_JohnSmithPrefab, "Have you heard about our Lord and Savior, Jesus Christ?");
        yield return new WaitForSeconds(3.0f);

        yield return AddTextMessage(_PocahontasPrefab, "You better treat me right. My ex left me with a Trail of Tears.");
        yield return new WaitForSeconds(1.0f);

        // TODO: third text choice
        yield return AddTextMessage(_JohnSmithPrefab, "Just come over baby, I’ll keep you warm. I’ve got a lot of blankets.");
        yield return new WaitForSeconds(3.0f);

        yield return AddTextMessage(_PocahontasPrefab, "*Sends phone number* “My phone number is “6”");
        yield return new WaitForSeconds(1.0f);

        // TODO: final text choice, not really a choice
        yield return AddTextMessage(_JohnSmithPrefab, "My friends are doing this Thanksgiving thing. It’s pretty low-key. U should cum.");
        yield return new WaitForSeconds(3.0f);

        // TODO: fade
    }
}
