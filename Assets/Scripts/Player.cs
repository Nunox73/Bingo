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

    public TextMeshProUGUI playerNameText;
    public GameObject cardUIGrid;
    public GameObject cardNumberPrefab;

    private List<TextMeshProUGUI> cardNumberTexts = new List<TextMeshProUGUI>();

    // Initializes the player with a Bingo card
    public void InitializePlayer(int id)
    {
        PlayerID = id;
        playerNameText.text = "Player " + PlayerID;

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
            List<int> range = new List<int>();
            for (int j = i * 10 + 1; j <= i * 10 + 10 && j <= 90; j++) // Creates ranges 1-9, 10-19, ..., 80-90
            {
                range.Add(j);
            }
            numberRanges.Add(i, range);
        }

        // Prepare the final list of card numbers
        List<int> cardNumbers = new List<int>();

        // Track how many numbers we've picked in total
        int totalNumbersToPick = 15;

        // Track how many numbers have been picked from each range
        Dictionary<int, int> numbersPickedFromRange = new Dictionary<int, int>();

        // Ensure we pick 15 numbers, no more than 3 from each range
        while (cardNumbers.Count < totalNumbersToPick)
        {
            for (int rangeIndex = 0; rangeIndex < 9; rangeIndex++)
            {
                // Ensure that we don't exceed 3 numbers from this range
                if (!numbersPickedFromRange.ContainsKey(rangeIndex))
                {
                    numbersPickedFromRange[rangeIndex] = 0;
                }

                if (numbersPickedFromRange[rangeIndex] <= 3 && numberRanges[rangeIndex].Count > 0)
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
        }

        // Sort the numbers into the 3x9 card grid
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

        // Assuming 'cardNumbers' contains the numbers already sorted by range
        int numIndex = 0;  // Tracks the current index in the sorted card numbers list
        for (numIndex = 0; numIndex <= 14; numIndex++)
        {
                int row = 0;
                int col = Mathf.FloorToInt(cardNumbers[numIndex]/10);
                while (row < 2)
                {
                    if (card.GetLength(0) > row && card.GetLength(1) > col && card[row,col] < 0)
                    {
                        card[row, col] = cardNumbers[numIndex];
                        break; // This will exit the loop as intended
                    } else
                    {
                        Debug.Log("");
                    }
                    row++; // Increment row
                }

        }



    // Iterate through rows and assign sorted numbers to random columns, max 3 per range
    //for (int row = 0; row < 3; row++)
    //{
    //    List<int> randomColumns = GetRandomColumns();  // Get random columns for each row (3 max per row)
    
    //    // Assign numbers to the columns, keeping track of the total
    //    foreach (int col in randomColumns) 
    //    {
    //        // Ensure we don’t exceed the total available numbers or overflow the list
    //        if (numIndex < cardNumbers.Count)
    //        {
    //            card[row, col] = cardNumbers[numIndex++];  // Assign the number and increment the index
    //        }                
    //    }
    //}

        // Create the card UI
        CreateCardUI();
    }

    // Generates 5 random columns for a row
    List<int> GetRandomColumns()
    {
        List<int> columns = new List<int>();
        while (columns.Count < 5)
        {
            int randomColumn = Random.Range(0, 9);
            if (!columns.Contains(randomColumn))
            {
                columns.Add(randomColumn);
            }
        }
        return columns;
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
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (card[row, col] == number)
                {
                    marked[row, col] = true;
                    int index = row * 9 + col;
                    cardNumberTexts[index].color = Color.red;  // Mark the number
                }
            }
        }
    }

    // Checks for win conditions (One Line, Two Lines, Full House)
    public bool CheckForWin()
    {
        int linesComplete = 0;

        // Check for complete lines
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
        if (linesComplete == 1)
        {
            Debug.Log("Player " + PlayerID + " has a ONE LINE!");
            HasWon = true;
            return true;
        }
        else if (linesComplete == 2)
        {
            Debug.Log("Player " + PlayerID + " has TWO LINES!");
            HasWon = true;
            return true;
        }
        else if (linesComplete == 3)
        {
            Debug.Log("Player " + PlayerID + " has a FULL HOUSE!");
            HasWon = true;
            return true;
        }

        return false;
    }
}
