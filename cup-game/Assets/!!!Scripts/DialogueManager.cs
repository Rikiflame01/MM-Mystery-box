using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    private Queue<string> dialogues;
    private GameManager gameManager;

    private void Start()
    {
        dialogues = new Queue<string>();
        gameManager = FindObjectOfType<GameManager>();

        nextButton.onClick.AddListener(DisplayNextDialogue);
    }

    public void StartDialogue(string[] dialogueLines)
    {
        dialogues.Clear();
        foreach (string line in dialogueLines)
        {
            dialogues.Enqueue(line);
        }
        dialogueCanvas.SetActive(true);
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogues.Count > 0)
        {
            string dialogue = dialogues.Dequeue();
            dialogueText.text = dialogue;
        }
        else
        {
            if (gameManager.gameStateManager.currentState == GameState.PlayerSelecting && !gameManager.SelectionMade)
            {
                dialogueText.text = "Make a selection first my friend...";
            }
            else
            {
                dialogueCanvas.SetActive(false);
                gameManager.ProgressGame();
            }
        }
    }

    public void SetSelectionMade(bool made)
    {
        gameManager.SelectionMade = made;
    }

    public void DisplayMakeSelectionMessage()
    {
        dialogueText.text = "Make a selection first my friend...";
    }
}
