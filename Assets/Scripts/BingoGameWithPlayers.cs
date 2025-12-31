using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Required for scene management
using TMPro;
using UnityEngine.EventSystems;
using System.Threading;
using Unity.VisualScripting;

public class BingoGameWithPlayers : MonoBehaviour
{


    private List<int> bingoNumbers = new List<int>(); // All bingo numbers (1-90)
    private List<int> drawnNumbers = new List<int>(); // Numbers that have been drawn


    public int numberOfPlayers = 3; // Set the number of players
    public int numberOfCards = 1; // Set the number of cards per player
    public GameObject playerCardPrefab; // Prefab for player cards
    public Transform playerCardsParent; // Parent object to hold player cards in the UI
    public GameObject gameCanvas;
    public GameObject btn_play;
    public GameObject btn_pause;
    public Text txt_vamos;
    public GameObject PlayerCardsParent;
    public GameObject Scores;
    public GoogleSheetsSender googleSender; // Send GameData do Google Sheets
    

    [Header("Sounds")]
    public AudioSource NewNumberSound;
    public AudioSource BingoARolar;

    [Header("Drawn Settings")]
    public Text drawnNumberText; // UI text for displaying the drawn number
    public Text drawnText; // UI text "Tem o número?"
    public Text allDrawnNumbersText; // UI text for displaying all drawn numbers
    public float drawnTimerRemaining = GlobalVariables.drawnTimer;  // Set initial time
    public bool drawnTimerIsRunning = false;
    public TextMeshProUGUI DrawnTimerText;  // 

    [Header("Winner Settings")]
    public int playerLineWinner = 0; // Player that made the first line
    public int playerBingoWinner = 0; // Player that Won the Bingo
    public Text txt_Line_Winner; // UI text for displaying the Line Winner
    public Text txt_Bingo_Winner; // UI text for displaying the Line Winner
    public GameObject winnerCanvas;
    public Text txt_Reward;
    public Text txt_Name;

    [Header("Winner Timmer Settings")]
    public float timeRemaining = GlobalVariables.winnerCanvasTimer;  // Set initial time 
    public bool WinnerTimerIsRunning = false;
    public TextMeshProUGUI timerText;  // Winner canvas remaining timmer

    [Header("Start Timmer")]
    public float timeStart = 0f;  // Set initial time 

    [Header("Buttons")]
    //public GameObject btn_yes;
    public GameObject btn_1;
    public GameObject btn_2;
    public GameObject btn_3;
    public GameObject btn_4;
    public GameObject btn_5;
    public GameObject btn_6;
    public Button btn_green;
    public Button btn_red;
    public Button ScoreRefreshButton;

    private List<Player> players = new List<Player>(); // List of players

    void Start()
    {
        InitializeBingoNumbers(); // Initialize Bingo numbers
        GeneratePlayers(); // Generate players and their cards
        winnerCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        // Disable Player 1 buttons
        //btn_disable(); managed by the buttons script
        drawnText.enabled = false;
    }
    void Update()
    {
        // Check if the Winner timer is running
        if (WinnerTimerIsRunning)
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
                // Register the time that Winner Canvas was active
                if (txt_Reward.text == "Linha")
                {
                    GlobalVariables.linha = timerText.ToString();
                    DrawBingoNumber();
                    }
                else if (txt_Reward.text == "Bingo")
                {
                    GlobalVariables.bingo = timerText.ToString();
                }
                //Debug.Log("Time has run out!");
                WinnerTimerIsRunning = false;
                timeRemaining = GlobalVariables.winnerCanvasTimer; // Reset the winner timer time
                winnerCanvas.SetActive(false);
                gameCanvas.SetActive(true);

            }
        }

        // Time to press the Jogar button
        if (SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text == "Jogar")
        {
            GlobalVariables.timeToStart += Time.deltaTime;
        }


        // Check if the Drawn timer is running and Winner Canvas is off
        if (drawnTimerIsRunning && !WinnerTimerIsRunning && winnerCanvas.activeSelf == false)
        {
            // Check if there's still time remaining
            if (drawnTimerRemaining > 0)
            {
                // Reduce the remaining time by the time since the last frame
                drawnTimerRemaining -= Time.deltaTime;

                // Update the timer UI (optional)
                DisplayTime(drawnTimerRemaining);
                ScoreRefreshButton.onClick.Invoke();
            }
            else
            {
                //Debug.Log("Drawn Time has run out!");
                btn_disable();
                NoButton();
                ResetDrawnTimer();
                DrawBingoNumber();
            }
        }
    }

    // Resets Drawn Timer
    void ResetDrawnTimer()
    {
        drawnTimerIsRunning = false;
        drawnTimerRemaining = GlobalVariables.drawnTimer;
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

        allDrawnNumbersText.text = "Números que sairam: ";
        drawnNumberText.text = "";
        drawnText.enabled = false;
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
        for (int i = 1; i < numberOfPlayers; i++)
        {


        }

    }


    // Draws a random Bingo number
    public void DrawBingoNumber()
    {
        StartCoroutine(DrawBingoNumber_Async());
        
    }

    IEnumerator DrawBingoNumber_Async()
    {
        if (bingoNumbers.Count > 0 && playerBingoWinner == 0 && (winnerCanvas.activeSelf == false))
        {
            
            drawnNumberText.text = "";
            drawnText.enabled = false;
            // Disable all the game buttons
                SerialReader.instance.btn_1.gameObject.SetActive(false);
                SerialReader.instance.btn_2.gameObject.SetActive(false);
                SerialReader.instance.btn_4.gameObject.SetActive(false);
                SerialReader.instance.btn_5.gameObject.SetActive(false);
            // Play Sound
            BingoARolar.Play();
            bool CheckIfIsPlaying() => BingoARolar.isPlaying;
            // Waits for the audiosource finish playing the audio
            yield return new WaitWhile(CheckIfIsPlaying);

            NewNumberSound.Play();


            int randomIndex = Random.Range(0, bingoNumbers.Count);
            int drawnNumber = bingoNumbers[randomIndex];

            bingoNumbers.RemoveAt(randomIndex); // Remove drawn number from available numbers
            drawnNumbers.Add(drawnNumber);

            // Update the UI to show the drawn number
            drawnNumberText.text = drawnNumber.ToString();
            drawnText.enabled = true;
            // Enable all the game buttons
                SerialReader.instance.btn_1.gameObject.SetActive(true);
                SerialReader.instance.btn_2.gameObject.SetActive(true);
                SerialReader.instance.btn_4.gameObject.SetActive(true);
                SerialReader.instance.btn_5.gameObject.SetActive(true);
            allDrawnNumbersText.text += drawnNumber + " ";

            // Enable Player 1 no_button
            btn_enable();

            //Enable Timer
            ResetDrawnTimer();
            drawnTimerIsRunning = true;

        }
        else
        {
            if (playerBingoWinner > 0)
            {
                drawnNumberText.text = "BINGO";
                drawnText.enabled = false;
                DrawnTimerText.text = "";
                //Change the btn_6 teste to "Seguinte"
                SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text = "Seguinte";
                SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().fontSize = 20;
                // Disable all the game buttons
                SerialReader.instance.btn_1.gameObject.SetActive(false);
                SerialReader.instance.btn_2.gameObject.SetActive(false);
                SerialReader.instance.btn_4.gameObject.SetActive(false);
                SerialReader.instance.btn_5.gameObject.SetActive(false);
                //btn_disable();
                ScoreRefreshButton.onClick.Invoke();
                // Turn on the buttons 3 and 6
                SerialReader.instance.SendData("3R\n"); //Turn 3 Red
                SerialReader.instance.SendData("6G\n"); //Turn 6 Green
            }
            else
            {
                drawnNumberText.text = "Já sairam todos os números!";
                drawnText.enabled = false;
                // Disable all the game buttons
                SerialReader.instance.btn_1.gameObject.SetActive(false);
                SerialReader.instance.btn_2.gameObject.SetActive(false);
                SerialReader.instance.btn_4.gameObject.SetActive(false);
                SerialReader.instance.btn_5.gameObject.SetActive(false);
            }

        }

        yield return null;
    }

    // Checks for win conditions ()
    void CheckWinConditions()
    {
        foreach (Player player in players)
        {

            if (player.CheckForBingo() && playerBingoWinner == 0)
            {
                txt_Bingo_Winner.text = PlayerPrefs.GetString("Player" + player.PlayerID + "Name") + " fez BINGO";
                playerBingoWinner = player.PlayerID;
                txt_Name.text = PlayerPrefs.GetString("Player" + player.PlayerID + "Name");
                txt_Reward.text = "Bingo";
                timeRemaining = GlobalVariables.winnerCanvasTimer; // Reset the winner timer time
                winnerCanvas.SetActive(true);
                
                gameCanvas.SetActive(false);
                WinnerTimerIsRunning = true;
                // Leds
                    SerialReader.instance.SendData("9O\n"); // All Off
                    SerialReader.instance.SendData("6G\n"); // 6 Green
                drawnTimerIsRunning = false;
                //btn_pause.SetActive(false);
                break; // Stop checking after the first winner is found
            }
            if (player.CheckForLine() && playerLineWinner == 0)
            {
                txt_Line_Winner.text = PlayerPrefs.GetString("Player" + player.PlayerID + "Name") + " fez Linha";
                playerLineWinner = player.PlayerID;
                txt_Name.text = PlayerPrefs.GetString("Player" + player.PlayerID + "Name");
                txt_Reward.text = "Linha";
                timeRemaining = GlobalVariables.winnerCanvasTimer; // Reset the winner timer time
                winnerCanvas.SetActive(true);
                gameCanvas.SetActive(false);
                WinnerTimerIsRunning = true;
                // Leds
                    SerialReader.instance.SendData("9O\n"); // All Off
                    SerialReader.instance.SendData("6G\n"); // 6 Green
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
        //btn_play.SetActive(true);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;  // Optional: Adjust the time so the display doesn't show 00:00 too early

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  // Divide by 60 to get minutes
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);  // Modulus operator to get seconds

        // Update the text (if using TextMeshPro)
        if (WinnerTimerIsRunning)
        {
            if (timerText != null)
            {
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
        else if (drawnTimerIsRunning)
        {
            DrawnTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }

    // Check Player 1 number
    public void YesButton()
    {
        btn_disable();
        // Save the game buttons click
        if (SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text == "Pausa")
        {
            AddTimeClicks(1);
        }
        foreach (Player player in players)
        {
            if (player.PlayerID == 1)
            {
                player.MarkNumber(int.Parse(drawnNumberText.text));

            }

        }

        //int score = PlayerPrefs.GetInt("Player1Score") - 1;
        //PlayerPrefs.SetInt("Player1Score", score);

        // Enviar dados para GameData
        GoogleGameData("Green");

        // Validate all the other players cards numbers
        markNumbers();
        // Check Winning Conditions
        CheckWinConditions();
        // Drawn a new number
        DrawBingoNumber();
        
    }

    public void GoogleGameData(string color)
    {
        // Enviar dados para GameData
        googleSender.SendGameData(
            GlobalVariables.PlayerID, GlobalVariables.lastButton.ToString() , drawnNumberText.text.ToString(), (GlobalVariables.drawnTimer-drawnTimerRemaining).ToString(), color, PlayerPrefs.GetInt("Player1Score").ToString());

    }

    public void NoButton()
    {
        btn_disable();
        // Validate all the other players cards numbers
        // Save the game buttons click
        if (SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text == "Pausa")
        {
            AddTimeClicks(1);
        }
        markNumbers();
        // Enviar dados para GameData
        GoogleGameData("Red");
        // Check Winning Conditions
        CheckWinConditions();
        // Drawn a new number
        DrawBingoNumber();


    }


    public void AddTimeClicks(float YesNo)
    {
        for (int i = 0; i < GlobalVariables.timeToClick.GetLength(0); i++) // percorre as 40 linhas
        {
            bool isRowFree = true;

            for (int j = 0; j < GlobalVariables.timeToClick.GetLength(1); j++) // percorre as 4 colunas
            {
                if (GlobalVariables.timeToClick[i, j] != 0f)
                {
                    isRowFree = false;
                    break;
                }
            }

            if (isRowFree)
            {
                GlobalVariables.timeToClick[i, 0] = GlobalVariables.lastButton; // Button ID
                GlobalVariables.timeToClick[i, 1] = int.Parse(drawnNumberText.text); // Bingo Number at the present time
                GlobalVariables.timeToClick[i, 2] = drawnTimerRemaining; // Time remaining
                GlobalVariables.timeToClick[i, 3] = YesNo; // Color Buton 0 = Red; 1 = Green
                break;
            }
        }

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

        if (drawnTimerRemaining == GlobalVariables.drawnTimer)
        {
            DrawBingoNumber();
        }
        else
        {
            drawnTimerIsRunning = true;
            btn_enable();
            //btn_yes.SetActive(true);
        }
        //btn_play.SetActive(false);
        //btn_pause.SetActive(true);
    }

    public void btn_Pause()
    {
        drawnTimerIsRunning = false;
        btn_disable();
        //btn_yes.SetActive(false);
        //btn_play.SetActive(true);
        //btn_pause.SetActive(false);
        SerialReader.instance.SendData("3R\n"); //Turn 3 Red
        SerialReader.instance.SendData("6G\n"); //Turn 6 Green
    }

    public void btn_disable()
    {
        
        if (GlobalVariables.buttonsConnected){          
            for (int i = 0; i < 8; i++){ 
                SerialReader.instance.SendData(i + "O\n"); // Turn off all the buttons
                //Thread.Sleep(500); // Pauses for 0.5 second (500 milliseconds)
            }
        } else {
            SerialReader.instance.btn_1.gameObject.SetActive(false);
            SerialReader.instance.btn_2.gameObject.SetActive(false);
            SerialReader.instance.btn_4.gameObject.SetActive(false);
            SerialReader.instance.btn_5.gameObject.SetActive(false);
        }
    }

    public void btn_enable()
    {

        int[] options = { 1, 2, 4, 5 };

        int greenIndex = Random.Range(0, options.Length);
        GlobalVariables.greenButton = options[greenIndex];

        // escolher um índice 0..options.Length-2 e “saltar” o greenIndex
        int redIndex = Random.Range(0, options.Length - 1);
        if (redIndex >= greenIndex) redIndex++;

        GlobalVariables.redButton = options[redIndex];



        //btn_disable(); // Disable all buttons

        if (GlobalVariables.buttonsConnected)
        {
            SerialReader.instance.SendData("6G\n"); //Turn the Pause Button to Green
            SerialReader.instance.SendData(GlobalVariables.greenButton + "G\n"); //Activate the Green Button
            SerialReader.instance.SendData(GlobalVariables.redButton + "R\n"); //Activate the Red Button
        }

        Color greenColor = new Color(0.29f, 0.75f, 0.09f, 0.68f);
        Color redColor   = new Color(0.75f, 0.52f, 0.09f, 0.68f);

        DisableAllOptionButtons();
        
        // Botão verde
        ApplyButtonUI(GlobalVariables.greenButton, greenColor, "Tenho");

        // Botão vermelho
        ApplyButtonUI(GlobalVariables.redButton, redColor, "Não Tenho");


        
        
    }

    void DisableAllOptionButtons()
    {
        if (SerialReader.instance == null) return;

        Button[] buttons =
        {
            SerialReader.instance.btn_1,
            SerialReader.instance.btn_2,
            SerialReader.instance.btn_4,
            SerialReader.instance.btn_5
        };

        foreach (var btn in buttons)
        {
            if (btn != null)
                btn.gameObject.SetActive(false);
        }
    }

    void ApplyButtonUI(int buttonId, Color color, string label)
    {
        if (SerialReader.instance == null)
            return;

        Button btn = buttonId switch
        {
            1 => SerialReader.instance.btn_1,
            2 => SerialReader.instance.btn_2,
            4 => SerialReader.instance.btn_4,
            5 => SerialReader.instance.btn_5,
            _ => null
        };

        if (btn == null)
        {
            Debug.LogWarning($"[UI] Botão {buttonId} não encontrado ou não rebindado.");
            return;
        }

        var img = btn.GetComponent<Image>();
        var txt = btn.GetComponentInChildren<TextMeshProUGUI>();

        if (img != null) img.color = color;
        if (txt != null) txt.text = label;

        btn.gameObject.SetActive(true);
    }


}
