using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int PlayerID;
    private int[,] card = new int[3, 9];  // 3x9 Bingo card
    private bool[,] marked = new bool[3, 9];  // Tracks which numbers have been marked
    public bool HasWon = false;
    public bool HasLine = false;

    
    public TextMeshProUGUI playerNameText; // Name of the Player in Scene Game
    public TextMeshProUGUI txt_player_name; // Name of the Player in Player Card Prefab
    public GameObject cardUIGrid;
    public GameObject cardNumberPrefab;

    private List<TextMeshProUGUI> cardNumberTexts = new List<TextMeshProUGUI>();

    // Variables to control de number of number in each line
    private int NumRow0;
    private int NumRow1;
    private int NumRow2;

    // Initializes the player with a Bingo card
    public void InitializePlayer(int id)
    {
        PlayerID = id;
        //playerNameText.text = "Jogador " + PlayerID;

        // Generate a random Bingo card for the player
        GenerateCard();
    }

    // Generates the player's Bingo card (15 random numbers)
    void GenerateCard()
    {

        // Initialize a dictionary to store available numbers in each range
        Dictionary<int, List<int>> numberRanges = new Dictionary<int, List<int>>();
        for (int i = 0; i < 9; i++)
        {
            int numberAdded = 0;
            int maxNumbersAllowed = 9;
            List<int> range = new List<int>();

            if (i == 0)
            {
                numberAdded = 1;
                maxNumbersAllowed = 9;
            }
            else if(i == 9)
            {
                // Ter espaço para ainda mais um
                maxNumbersAllowed = 11;
            }

            do
            {
                range.Add(i*10 + numberAdded);
                numberAdded++;
            } while (numberAdded <= maxNumbersAllowed);

            numberRanges.Add(i, range);
        }

        // Prepare the final list of card numbers
        List<int> cardNumbers = new List<int>();

        // Track how many numbers we've picked in total
        int totalNumbersToPick = 15;

        // Track how many numbers have been picked from each range
        Dictionary<int, int> numbersPickedFromRange = new Dictionary<int, int>();

        // Ensure we pick 15 numbers, no more than 2 from each range
        while (cardNumbers.Count < totalNumbersToPick)
        {

            int rangeIndex = Random.Range(0,9);
            
                // Ensure that we don't exceed 2 numbers from this range
                if (!numbersPickedFromRange.ContainsKey(rangeIndex))
                {
                    numbersPickedFromRange[rangeIndex] = 0;
                }

                if (numbersPickedFromRange[rangeIndex] <= 1 && numberRanges[rangeIndex].Count > 0)
                {
                    // Randomly pick a number from the current range
                    int randomIndex = Random.Range(0, numberRanges[rangeIndex].Count);
                    int chosenNumber = numberRanges[rangeIndex][randomIndex];

                    // Add the number to the Bingo card and remove from the range to prevent duplicates
                    cardNumbers.Add(chosenNumber);
                    numberRanges[rangeIndex].RemoveAt(randomIndex);

                    // Update the count for this range
                    numbersPickedFromRange[rangeIndex]++;

                    // Stop if we've selected exactly 15 numbers
                    if (cardNumbers.Count == totalNumbersToPick)
                    {
                        break;
                    }
                }
            
        }

        // Sort the numbers
        cardNumbers.Sort();

        // Fill the card array and set marked to false
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                card[i, j] = -1;  // Empty by default
                marked[i, j] = false;
            }
        }

        // Assuming 'cardNumbers' contains the numbers already sorted by range, lets split them by column
        int numIndex = 0;  // Tracks the current index in the sorted card numbers list
        for (numIndex = 0; numIndex <= 14; numIndex++)
        {
                int row = 0;
                int col = Mathf.FloorToInt(cardNumbers[numIndex]/10);
                while (row <= 2)
                {
                    if (card.GetLength(0) > row && card.GetLength(1) > col && card[row,col] < 0)
                    {
                        card[row, col] = cardNumbers[numIndex];
                        break; // This will exit the loop as intended
                    } else
                    {
                        //Debug.Log("");
                    }
                    row++; // Increment row
                }

        }


        // Spit the numbers and spaces inside each column
        //int numIndex = 0;  // Tracks the current index in the sorted card numbers list
        for (numIndex = 0; numIndex <= 14; numIndex++)
        {
            //int row = 0;
            int col = 0;
            while (col <= 8)
            {
                if (card[2, col] < 0) // Check if the 3rd row is empty ==> then we need to organize
                {
                    if (card[1, col] < 0) // 2nd row is empty
                    {
                        if (card[0, col] > 0) // only 1 number
                        {
                            if ((card[0, col] % 10) > 5)
                            {
                                card[2, col] = card[0, col];
                                card[0, col] = -1;
                            } else if ((card[0, col] % 10) == 5)
                            {
                                card[1, col] = card[0, col];
                                card[0, col] = -1;
                            }
                        }
                    } else // we have 2 numbers on this colum
                    {
                        // 2nd line
                        if ((card[1, col] % 10) >= 6)
                        {
                            card[2, col] = card[1, col];
                            card[1, col] = -1;
                        }
                        if ((card[0, col] % 10) >= 5)
                        {
                            card[1, col] = card[0, col];
                            card[0, col] = -1;
                        } 
                    }
                }
                col++; // Increment row
            }

        }

 

        // Count non-empty fields in each row
        for (int row = 0; row < 3; row++)
        {
            int nonEmptyCount = 0;
            for (int col = 0; col < 9; col++)
            {
                if (card[row, col] > 0)
                {
                    nonEmptyCount++;
                    switch (row) {
                    case 0:
                        NumRow0 ++;
                        break;
                    case 1:
                        NumRow1 ++;
                        break; 
                    case 2:
                        NumRow2 ++;
                        break;
                    }
                }
            }
            
        }

        // Secure that the 3rd Row only has 5 numbers
        if (NumRow2 != 5) { 
            if (NumRow2 > 5){ // need to reduce number
                for (int col = 0; col < 9; col++) {
                    if (card[1, col] < 0  && NumRow2 != 5)
                    {
                        card[1, col] = card[2, col];
                        card[2, col] = -1;
                        NumRow2 --;
                        NumRow1 ++;
                    }
                }
            } 
            if (NumRow2 < 5) { // need to increase number
                for (int col = 0; col < 9; col++) {
                    if (card[2, col] < 0 && card[1, col] > 0 && NumRow2 != 5)
                    {
                        card[2, col] = card[1, col];
                        card[1, col] = -1;
                        NumRow2 ++;
                        NumRow1 --;
                    }
                }
            }

            
        }

        // Secure that the 2nd Row only has 5 numbers
        if (NumRow1 != 5) { 
            if (NumRow1 > 5){ // need to reduce number
                for (int col = 0; col < 9; col++) {
                    if (card[0, col] < 0 && NumRow1 != 5)
                    {
                        card[0, col] = card[1, col];
                        card[1, col] = -1;
                        NumRow1 --;
                        NumRow0 ++;
                    }
                }
            } 
            if (NumRow1 < 5) { // need to increase number
                for (int col = 0; col < 9; col++) {
                    if (card[1, col] < 0 && card[0, col] > 0 && NumRow1 != 5)
                    {
                        card[1, col] = card[0, col];
                        card[0, col] = -1;
                        NumRow1 ++;
                        NumRow0 --;
                    }
                }
            }

            
        }

        if (NumRow0 != 5 || NumRow1 != 5 || NumRow2 != 5) {
                Debug.Log("ERRO: Numero de numeros nas linhas");
                Debug.Log($"Jogador {PlayerID}");
                Debug.Log($"Linha 0: {NumRow0}");
                Debug.Log($"Linha 1: {NumRow1}");
                Debug.Log($"Linha 2: {NumRow2}");
        }

        // Validate the numbers in the card
        // int numberInCard = 0;
        // for (int row = 0; row < 3; row++)
        // {
        //     for (int col = 0; col < 9; col++)
        //     {

        //         if (card[row, col] == -1)
        //         {

        //         }
        //         else
        //         {
        //             numberInCard++;
        //         }
        //     }

        // }
            // Create the card UI
            CreateCardUI();
    }


    // Creates the card UI
    void CreateCardUI()
    {
        // Clear previous numbers
        foreach (Transform child in cardUIGrid.transform)
        {
            Destroy(child.gameObject);
        }

        cardNumberTexts.Clear();

        // Populate the card UI with the numbers from the card array
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                // Instantiate a new number prefab into the UI grid
                GameObject newCardNumber = Instantiate(cardNumberPrefab, cardUIGrid.transform);

                // Get the TextMeshProUGUI component to display the number
                TextMeshProUGUI numberText = newCardNumber.GetComponent<TextMeshProUGUI>();
                // Get the Image component of the number
                //Image numberImage = newCardNumber.GetComponent<Image>();
                // Find the child GameObject by name
                Transform childTransform  = newCardNumber.transform.Find("Square");
                Image numberImage = childTransform.GetComponent<Image>();

                if (numberText != null)
                {
                    // Check if the card slot is empty (-1 means an empty slot)
                    if (card[row, col] == -1)
                    {
                        numberText.text = "";  // Empty space
                    }
                    else
                    {
                        numberText.text = card[row, col].ToString();  // Set the number
 //                       Image imagem = newCardNumber.transform.Find("White Ball").GetComponent<Image>(); //newCardNumber.GetComponentInChildren<Image>();
 //                       imagem.enabled = false;
                        if (numberImage != null)
                        {
                            // Get the current color of the image
                            Color currentColor = Color.white;
                            // Set the alpha value to 0 to make it fully transparent
                            currentColor.a = 0f; // Fully transparent
                            numberImage.color = currentColor;
                        }
                    }

                    // Store the text reference for further use (like marking numbers)
                    cardNumberTexts.Add(numberText);
                }
            }
        }
    }

    // Marks the number on the card if it exists
    public void MarkNumber(int number)
    {

        bool haveit = false; // used to validate if prssing the green button without the number
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (card[row, col] == number)
                {
                    marked[row, col] = true;
                    int index = row * 9 + col;
                    if (cardNumberTexts[index].color != Color.red)
                    {
                        cardNumberTexts[index].color = Color.red;  // Mark the number
                        PlayerPrefs.SetInt("Player" + PlayerID.ToString() + "Score", PlayerPrefs.GetInt("Player" + PlayerID.ToString() + "Score") + 1);
                        PlayerPrefs.Save();
                        haveit = true;
                    }

                }
            }
        }
        if  (haveit == false && PlayerID.ToString() == "1"){ // In case of a false "Tenho"
            PlayerPrefs.SetInt("Player" + PlayerID.ToString() + "Score", PlayerPrefs.GetInt("Player" + PlayerID.ToString() + "Score") - 1);
            PlayerPrefs.Save();
        }
    }
    
    

    // Checks for win conditions (Line and Bingo)
    public bool CheckForLine()
    {
        // Check for complete lines
        int linesComplete = 0;
        for (int row = 0; row < 3; row++)
        {
            bool lineComplete = true;
            for (int col = 0; col < 9; col++)
            {
                if (card[row, col] != -1 && !marked[row, col])
                {
                    lineComplete = false;
                    break;
                }
            }
            if (lineComplete)
            {
                linesComplete++;
            }
        }
        // Check for win conditions
        if (linesComplete == 1 && !HasLine)
        {
            //Debug.Log("Jogador " + PlayerID + " fez LINHA!");

            HasLine = true;
            if (PlayerPrefs.GetInt("Linha") == 0)
            {
                PlayerPrefs.SetInt("Player" + PlayerID.ToString() + "Score", PlayerPrefs.GetInt("Player" + PlayerID.ToString() + "Score") + 4);
                PlayerPrefs.SetInt("Linha", 1);
                PlayerPrefs.Save();
            }

            //BingoGameWithPlayers.playerLineWinner = PlayerID;
            return true;
        }
        return false;
    }
    public bool CheckForBingo()
    {
        int cardComplete = 0;
        // Check for complete card
        for (int row = 0; row < 3; row++)
        {
            //bool lineComplete = true;
            for (int col = 0; col < 9; col++)
            {
                if (card[row, col] != -1 && marked[row, col])
                {
                    cardComplete++;
                    //break;
                }
            }
        }

        if (cardComplete == 15)
        {
            //Debug.Log("Jogador " + PlayerID + " fez BINGO!");
            HasWon = true;
            
                PlayerPrefs.SetInt("Player" + PlayerID.ToString() + "Score", PlayerPrefs.GetInt("Player" + PlayerID.ToString() + "Score") + 9);
                PlayerPrefs.SetInt("Bingo",1);
                PlayerPrefs.Save();
            
            return true;
        }
        return false;
    }

void Start()
    {
        
    }

}
