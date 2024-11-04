using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private CanvasGroup dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;
    public bool isDialoguePlaying { get; private set; }
    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }

        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        isDialoguePlaying = false;
        dialoguePanel.alpha = 0;
        dialoguePanel.blocksRaycasts = false;
        dialoguePanel.interactable = false;
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!isDialoguePlaying)
        {
            return;
        }
    }

    private void OnDestroy()
    {
        isDialoguePlaying=false;
        instance = null;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        isDialoguePlaying = true;
        dialoguePanel.alpha = 1;
        dialoguePanel.blocksRaycasts = true;
        dialoguePanel.interactable = true;
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        isDialoguePlaying = false;
        dialoguePanel.alpha = 0;
        dialoguePanel.blocksRaycasts = false;
        dialoguePanel.interactable = false;
        dialogueText.text = "";
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;

            // Add listener to detect click for each choice button
            int choiceIndex = index; // Local copy of index for closure
            choices[index].GetComponent<Button>().onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            index++;
        }

        // Disable unused choice buttons
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        currentStory.Continue();

        // Clear previous listeners to prevent stacking
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
            choices[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }

        ContinueStory();
    }
}