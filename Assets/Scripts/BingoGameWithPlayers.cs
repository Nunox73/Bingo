using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BingoGameWithPlayers : MonoBehaviour
{
    private List<int> bingoNumbers = new List<int>(); // All bingo numbers (1-90)
    private List<int> drawnNumbers = new List<int>(); // Numbers that have been drawn

    public Text drawnNumberText; // UI text for displaying the drawn number
    public Text txt_Line_Winner; // UI text for displaying the Line Winner
    public Text allDrawnNumbersText; // UI text for displaying all drawn numbers
    public int numberOfPlayers = 2; // Set the number of players
    public int numberOfCards = 2; // Set the number of cards per player
    public int playerLineWinner = 0; // Player that made the first line
    public int playerBigoWinner = 0; // Player that Won the Bingo
    public GameObject playerCardPrefab; // Prefab for player cards
    public Transform playerCardsParent; // Parent object to hold player cards in the UI

    private List<Player> players = new List<Player>(); // List of players

    void Start()
    {
        InitializeBingoNumbers(); // Initialize Bingo numbers
        GeneratePlayers(); // Generate players and their cards
    }

    // Initializes the Bingo numbers
    void InitializeBingoNumbers()
    {
        playerBigoWinner = 0;
        playerLineWinner = 0;
        bingoNumbers.Clear();
        drawnNumbers.Clear();

        for (int i = 1; i <= 90; i++)
        {
            bingoNumbers.Add(i); // Populate list with numbers from 1 to 90
        }

        allDrawnNumbersText.text = "Números de sairam: ";
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
        if (bingoNumbers.Count > 0)
        {
            int randomIndex = Random.Range(0, bingoNumbers.Count);
            int drawnNumber = bingoNumbers[randomIndex];

            bingoNumbers.RemoveAt(randomIndex); // Remove drawn number from available numbers
            drawnNumbers.Add(drawnNumber);

            // Update the UI to show the drawn number
            drawnNumberText.text = "Novo Número: " + drawnNumber;
            allDrawnNumbersText.text += drawnNumber + " ";

            // Mark the drawn number on each player's card
            foreach (Player player in players)
            {
                player.MarkNumber(drawnNumber);
            }

            // Check for a win condition after each number is drawn
            CheckWinConditions();
        }
        else
        {
            drawnNumberText.text = "All numbers have been drawn!";
        }
    }

    // Checks for win conditions ()
    void CheckWinConditions()
    {
        foreach (Player player in players)
        {
            
            if (player.CheckForBingo() && playerBigoWinner == 0)
            {
                drawnNumberText.text = "Jogador " + player.PlayerID + " fez BINGO, Parabéns!";
                playerBigoWinner = player.PlayerID;
                break; // Stop checking after the first winner is found
            }
            if (player.CheckForLine() && playerLineWinner == 0)
            {
                txt_Line_Winner.text = "Jogador " + player.PlayerID + " fez Linha, Parabéns!";
                playerLineWinner = player.PlayerID;
                break; // Stop checking after the first winner is found
            }
            

        }
    }

    // Resets the Bingo game
    public void ResetBingoGame()
    {
        InitializeBingoNumbers();
        GeneratePlayers();

    }
}
