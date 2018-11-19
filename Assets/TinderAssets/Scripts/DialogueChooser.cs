using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChooser : MonoBehaviour {

    [SerializeField]
    private Button _ChoicePrefab;

    private Dictionary<Button, string> CurrentDialogueMapping = new Dictionary<Button, string>();
    public string ChosenDialogue { get; private set; }

    private void ClearDialogueChoices() {
        foreach (var kvp in CurrentDialogueMapping)
        {
            Destroy(kvp.Key.gameObject);
        }
        CurrentDialogueMapping.Clear();
    }

    public void ShowDialogueChoice(params string[] Choices) {
        ClearDialogueChoices();
        ChosenDialogue = null;

        for (int i = 0; i < Choices.Length; i++) {
            Button buttonInstance = Instantiate<Button>(_ChoicePrefab);
            buttonInstance.transform.SetParent(transform);
            buttonInstance.GetComponentInChildren<Text>().text = (i + 1).ToString() + ": " + Choices[i];
            buttonInstance.onClick.AddListener(() => OnButtonClicked(buttonInstance));
            CurrentDialogueMapping[buttonInstance] = Choices[i];
        }
    }

    public void OnButtonClicked(Button b) {
        ChosenDialogue = CurrentDialogueMapping[b];

        ClearDialogueChoices();
    }
}
