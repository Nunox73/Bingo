using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetPlayersPrefs : MonoBehaviour
{
    // Start is cal
    public TMP_InputField Player_Name;

    
    [Header("Score Board")]
    public TextMeshProUGUI txt_grid_player1name;
    public TextMeshProUGUI txt_grid_player2name;
    public TextMeshProUGUI txt_grid_player3name;
    public TextMeshProUGUI txt_grid_player1total;
    public TextMeshProUGUI txt_grid_player2total;
    public TextMeshProUGUI txt_grid_player3total;

    public void UpdateBoard()
    {
        // Score Board
        txt_grid_player1name.text= PlayerPrefs.GetString("Player1Name");
        txt_grid_player2name.text= PlayerPrefs.GetString("Player2Name");
        txt_grid_player3name.text= PlayerPrefs.GetString("Player3Name");
        
        txt_grid_player1total.text = PlayerPrefs.GetInt("Player1Total").ToString();
        txt_grid_player2total.text = PlayerPrefs.GetInt("Player2Total").ToString();
        txt_grid_player3total.text = PlayerPrefs.GetInt("Player3Total").ToString();
    }
    void Start()
    {

        // Reset Game and Player Preferences
        PlayerPrefs.SetString("Player1Name", "Eu");
        PlayerPrefs.SetString("Player2Name", "Joaquim");
        PlayerPrefs.SetString("Player3Name", "Maria");
        PlayerPrefs.SetInt("Player1Score", 0);
        PlayerPrefs.SetInt("Player2Score", 0);
        PlayerPrefs.SetInt("Player3Score", 0);
        PlayerPrefs.SetInt("Linha",0);
        PlayerPrefs.SetInt("Bingo",0);
        PlayerPrefs.Save();
        
        UpdateBoard();

    }

    public void Reset()
    {
        
        // Reset Game and Player Preferences
        PlayerPrefs.SetString("Player1Name", "Nome do Jogador?");
        PlayerPrefs.SetString("Player2Name", "Joaquim");
        PlayerPrefs.SetString("Player3Name", "Maria");
        PlayerPrefs.SetInt("Player1Score", 0);
        PlayerPrefs.SetInt("Player2Score", 0);
        PlayerPrefs.SetInt("Player3Score", 0);
        PlayerPrefs.SetInt("Player1Total", 0);
        PlayerPrefs.SetInt("Player2Total", 0);
        PlayerPrefs.SetInt("Player3Total", 0);
        PlayerPrefs.SetInt("Linha",0);
        PlayerPrefs.SetInt("Bingo",0);
        PlayerPrefs.Save();

        Player_Name.text = PlayerPrefs.GetString("Player1Name");
       

        UpdateBoard();
    }


    
    
    void Update()
    {
        
    }
}
