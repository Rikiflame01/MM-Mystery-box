using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameStateManager gameStateManager;
    public DialogueManager dialogueManager;
    public CupShuffler cupShuffler;

    private int currentRound = 0;
    private int successfulGuesses = 0;
    public bool SelectionMade { get; set; }
    private bool isSelectionCorrect;

    private string[][] dialogues = new string[][]
    {
        new string[]
        {
            "You there, with the spark of adventure in your eye, come, come!",
            "Win my game and I'll give you a mystery prize! All you have to do is find the marble under these cups.",
            "Get it right five times and the mystery box is yours!"
        },
        new string[]
        {
            "It's hard isn't it - to make the right choice? Especially when you don't know the outcome.",
            "But if you correctly followed the marble, the outcome isn’t truly unknown, is it?"
        },
        new string[]
        {
            "What do you think of death? Personally, I find life much more alluring to ponder.",
            "Do you think death is the end or the beginning? I like to think that it's the middle.",
            "Death is but a cup, and we are the ball. Or perhaps death is the ball, and we are the cups.",
            "Or maybe death is me, and you are the cup? No, I don’t think that’s right…"
        },
        new string[]
        {
            "For most, life and death is not a choice. But this is! Which cup has the ball?"
        },
        new string[]
        {
            "So why did you decide to play my game today?",
            "Life is all about choice, even choosing the right or wrong cup can completely change the course of one’s life.",
            "Or perhaps the choices were chosen for you. What if every choice you make is predetermined and the results are set in stone?",
            "Now, do you choose the cup, or does the cup choose you? In this moment, the cup and yourself are linked and must choose each other."
        },
        new string[]
        {
            "What is this? It’s a marble but it wasn’t the marble. This one is the wrong dimension. Ah, well I guess it counts."
        },
        new string[]
        {
            "What motivates you to choose? To play? Is it the prize or do you play for the thrill of playing?",
            "Do you think that life is a game we can play? Can we win or lose at the game of life? What is the score, and who is counting?",
            "If life is a game, it should be fun, don’t you think? I hope you’re having fun, my friend."
        },
        new string[]
        {
            "If you are playing to win, You must choose correctly."
        },
        new string[]
        {
            "What is the prize anyway? It is unknown to you but known to me. Does it hold more value for you or me in this state?",
            "The unknown. Some find it scary, others find it exciting. When one finds the unknown, it is destroyed, for it is no longer what it was.",
            "Some say that if anyone finally understands the mysteries of our universe, it will be destroyed and replaced with an even more bizarre and inexplicable one. I think this has already happened."
        },
        new string[]
        {
            "The mystery box is so close, you’ve almost got it. And perhaps the end of its mystery is close too? The choice is yours, friend."
        },
        new string[]
        {
            "You have done it, my friend! The mystery box and the key to unlock it are all yours.",
            "I hope you find meaning in it, no matter what resides within."
        }
    };

    private void Awake()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        cupShuffler = FindObjectOfType<CupShuffler>();
    }

    private void Start()
    {
        gameStateManager.ChangeState(GameState.Default);
        dialogueManager.StartDialogue(dialogues[currentRound]);
        cupShuffler.ResetCupsToInitialPositions(); // Reset cups to initial positions at the start
    }

    public void ProgressGame()
    {
        if (gameStateManager.currentState == GameState.Default)
        {
            gameStateManager.ChangeState(GameState.Playing);
            StartCoroutine(StartPlayingStateDialogue());
        }
        else if (gameStateManager.currentState == GameState.PlayerSelecting)
        {
            if (SelectionMade)
            {
                if (isSelectionCorrect)
                {
                    if (currentRound == dialogues.Length / 2 - 1)
                    {
                        EndGame(true); // End game if it was the final round
                    }
                    else
                    {
                        gameStateManager.ChangeState(GameState.PlayerCorrect);
                        dialogueManager.StartDialogue(new string[] { "You’ve got it! Let’s do another round!" });
                    }
                }
                else
                {
                    gameStateManager.ChangeState(GameState.PlayerIncorrect);
                    dialogueManager.StartDialogue(new string[] { "Oh no, I’m sorry but that is wrong. There is no mystery prize for you today." });
                }
            }
            else
            {
                dialogueManager.DisplayMakeSelectionMessage(); // Display message to prompt the user to make a selection
            }
        }
        else if (gameStateManager.currentState == GameState.PlayerCorrect)
        {
            successfulGuesses++;
            if (successfulGuesses >= 5)
            {
                EndGame(true);
            }
            else
            {
                currentRound++;
                gameStateManager.ChangeState(GameState.Default);
                SelectionMade = false;
                dialogueManager.StartDialogue(dialogues[currentRound * 2]); // Start the next round's initial dialogue
            }
        }
        else if (gameStateManager.currentState == GameState.PlayerIncorrect)
        {
            EndGame(false);
        }
    }

    private IEnumerator StartPlayingStateDialogue()
    {
        // Wait for shuffling to complete
        yield return new WaitForSeconds(3f); // Adjust this duration based on your shuffling animation duration
        dialogueManager.StartDialogue(dialogues[currentRound * 2 + 1]); // Start the selection phase dialogue
        gameStateManager.ChangeState(GameState.PlayerSelecting);
    }

    public void SetSelection(bool isCorrect)
    {
        if (SelectionMade)
            return; // Ignore further selections

        SelectionMade = true;
        isSelectionCorrect = isCorrect;
        dialogueManager.SetSelectionMade(true);
    }

    private void EndGame(bool isWin)
    {
        if (isWin)
        {
            dialogueManager.StartDialogue(dialogues[dialogues.Length - 1]);
        }
        else
        {
            dialogueManager.StartDialogue(new string[] { "How unfortunate. Or losing is the path to victory? But today, and for you, there is no mystery prize." });
        }

        StartCoroutine(ReloadSceneAfterDelay(3f)); // Reload the scene after 3 seconds
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        cupShuffler.ResetCupsToInitialPositions(); // Reset cups to initial positions when the scene reloads
    }
}
