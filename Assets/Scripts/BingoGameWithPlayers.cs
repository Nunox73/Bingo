using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Required for scene management
using TMPro;

public class BingoGameWithPlayers : MonoBehaviour
{
    
    
    private List<int> bingoNumbers = new List<int>(); // All bingo numbers (1-89)
    private List<int> drawnNumbers = new List<int>(); // Numbers that have been drawn

    
    public int numberOfPlayers = 3; // Set the number of players
    public int numberOfCards = 1; // Set the number of cards per player
    public GameObject playerCardPrefab; // Prefab for player cards
    public Transform playerCardsParent; // Parent object to hold player cards in the UI
    public GameObject gameCanvas;
    public GameObject btn_play;
    public GameObject btn_pause;

    [Header("Sounds")]
    public AudioSource NewNumberSound;
    public AudioSource CorrectNumber;
    public AudioSource WrongNumber;

    [Header("Drawn Settings")]
    public Text drawnNumberText; // UI text for displaying the drawn number
    public Text allDrawnNumbersText; // UI text for displaying all drawn numbers
    public float drawnTimerRemaining = 10f;  // Set initial time
    public bool drawnTimerIsRunning = false;
    public TextMeshProUGUI DrawnTimerText;  // Assign your TextMeshPro UI element

    [Header("Winner Settings")]
    public int playerLineWinner = 0; // Player that made the first line
    public int playerBingoWinner = 0; // Player that Won the Bingo
    public Text txt_Line_Winner; // UI text for displaying the Line Winner
    public Text txt_Bingo_Winner; // UI text for displaying the Line Winner
    public GameObject winnerCanvas;
    public Text txt_Reward;
    public Text txt_Name;

    [Header("Winner Timmer Settings")]
    public float timeRemaining = 5f;  // Set initial time 
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;  // Assign your TextMeshPro UI element

    [Header("Player 1 Settings")]
    public GameObject btn_yes;
    public GameObject btn_no;

    private List<Player> players = new List<Player>(); // List of players

    void Start()
    {
        InitializeBingoNumbers(); // Initialize Bingo numbers
        GeneratePlayers(); // Generate players and their cards
        winnerCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        // Disable Player 1 buttons
        btn_no.SetActive(false);
        btn_yes.SetActive(false);
    }
    void Update()
    {
        // Check if the Winner timer is running
        if (timerIsRunning)
        {
            // Check if there's still time remaining
            if (timeRemaining > 0)
            {
                // Reduce the remaining time by the time since the last frame
                timeRemaining -= Time.deltaTime;

                // Update the timer UI (optional)
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timerIsRunning = false;
                timeRemaining = 5; // Reset the winner timer time
                winnerCanvas.SetActive(false);
                gameCanvas.SetActive(true);
            }
        }

        // Check if the Drawn timer is running
        if (drawnTimerIsRunning && !timerIsRunning)
        {           
            // Check if there's still time remaining
            if (drawnTimerRemaining > 0)
            {
                // Reduce the remaining time by the time since the last frame
                drawnTimerRemaining -= Time.deltaTime;

                // Update the timer UI (optional)
                DisplayTime(drawnTimerRemaining);
            }
            else
            {
                //Debug.Log("Drawn Time has run out!");
                ResetDrawnTimer();
                DrawBingoNumber();
            }
        }
    }

    // Resets Drawn Timer
    void ResetDrawnTimer()
    {
        drawnTimerIsRunning = false;
        drawnTimerRemaining = 10;
    }

    // Initializes the Bingo numbers
    void InitializeBingoNumbers()
    {
        playerBingoWinner = 0;
        playerLineWinner = 0;
        bingoNumbers.Clear();
        drawnNumbers.Clear();

        for (int i = 1; i < 90; i++)
        {
            bingoNumbers.Add(i); // Populate list with numbers from 1 to 89
        }

        allDrawnNumbersText.text = "N�meros que sairam: ";
        drawnNumberText.text = "";
    }

    // Generates Bingo cards for each player
    void GeneratePlayers()
    {
        // Clear previous players' cards
        foreach (Transform child in playerCardsParent)
        {
            Destroy(child.gameObject);
        }
        players.Clear();

        // Create a Bingo card for each player
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject playerCardGO = Instantiate(playerCardPrefab, playerCardsParent);
            Player newPlayer = playerCardGO.GetComponent<Player>();
            if (newPlayer != null)
            {
                newPlayer.InitializePlayer(i + 1); // Initialize player with a unique Bingo card
                players.Add(newPlayer);
            }
        }
    }

   
    // Draws a random Bingo number
    public void DrawBingoNumber()
    {
        if (bingoNumbers.Count > 0 && playerBingoWinner == 0)
        {
           
            // Play Sound
            NewNumberSound.Play();

            int randomIndex = Random.Range(0, bingoNumbers.Count);
            int drawnNumber = bingoNumbers[randomIndex];

            bingoNumbers.RemoveAt(randomIndex); // Remove drawn number from available numbers
            drawnNumbers.Add(drawnNumber);

            // Update the UI to show the drawn number
            drawnNumberText.text = "" + drawnNumber;
            allDrawnNumbersText.text += drawnNumber + " ";

            // Enable Player 1 buttons
            btn_no.SetActive(true);
            btn_yes.SetActive(true);

            //Enable Timer
            ResetDrawnTimer();
            drawnTimerIsRunning = true;
           
        }
        else
        {
            if (playerBingoWinner > 0)
            {
                drawnNumberText.text = "BINGO";
                DrawnTimerText.text = "";
                btn_no.SetActive(false);
                btn_yes.SetActive(false);
            } else
            {
                drawnNumberText.text = "J� sairam todos os n�meros!";
            }
            
        }
    }

    // Checks for win conditions ()
    void CheckWinConditions()
    {
        foreach (Player player in players)
        {
            
            if (player.CheckForBingo() && playerBingoWinner == 0)
            {
                txt_Bingo_Winner.text = "Jogador " + player.PlayerID + " fez BINGO";
                playerBingoWinner = player.PlayerID;
                txt_Name.text = "Jogador " + player.PlayerID;
                txt_Reward.text = "Bingo";
                winnerCanvas.SetActive(true);
                gameCanvas.SetActive(false);
                timerIsRunning = true;
                drawnTimerIsRunning = false;
                btn_pause.SetActive(false);
                break; // Stop checking after the first winner is found
            }
            if (player.CheckForLine() && playerLineWinner == 0)
            {
                txt_Line_Winner.text = "Jogador " + player.PlayerID + " fez Linha";
                playerLineWinner = player.PlayerID;
                txt_Name.text = "Jogador " + player.PlayerID;
                txt_Reward.text = "Linha";
                winnerCanvas.SetActive(true);
                gameCanvas.SetActive(false);
                timerIsRunning = true;
                break; // Stop checking after the first winner is found
            }
            

        }
    }

    // Resets the Bingo game
    public void ResetBingoGame()
    {
        InitializeBingoNumbers();
        GeneratePlayers();
        txt_Bingo_Winner.text = "";
        txt_Line_Winner.text = "";
        drawnTimerIsRunning = false;
        btn_play.SetActive(true);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;  // Optional: Adjust the time so the display doesn't show 00:00 too early

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  // Divide by 60 to get minutes
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);  // Modulus operator to get seconds

        // Update the text (if using TextMeshPro)
        if (timerIsRunning)
        {
            if (timerText != null)
            {
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        } else if (drawnTimerIsRunning)
        {
            DrawnTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }

    // Check Player 1 number
    public void YesButton()
    {
        foreach (Player player in players)
        {
            if (player.PlayerID == 1)
            {
                player.MarkNumber(int.Parse(drawnNumberText.text));
                
            }

        }
        // Validate all the other players cards numbers
        markNumbers();
        // Check Winning Conditions
        CheckWinConditions();      
        // Drawn a new number
        DrawBingoNumber();
    }

    public void NoButton()
    {
        // Validate all the other players cards numbers
        markNumbers();
        // Check Winning Conditions
        CheckWinConditions();
        // Drawn a new number
        DrawBingoNumber();
    }


    public void markNumbers()
    {
        // Mark the drawn number on each player's card
        foreach (Player player in players)
        {
            if (player.PlayerID > 1)
            {
                player.MarkNumber(int.Parse(drawnNumberText.text));
            }

        }
    }

    public void btn_Start()
    {
        
        if (drawnTimerRemaining == 5)
        {
            DrawBingoNumber();
        } else
        {
            drawnTimerIsRunning = true;
            btn_no.SetActive(true);
            btn_yes.SetActive(true);
        }
        btn_play.SetActive(false);
        btn_pause.SetActive(true);
    }

    public void btn_Pause()
    {
        drawnTimerIsRunning = false;
        btn_no.SetActive(false);
        btn_yes.SetActive(false);
        btn_play.SetActive(true);
        btn_pause.SetActive(false);
    }

}
