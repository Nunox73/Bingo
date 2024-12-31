using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadPlayersPrefs : MonoBehaviour
{
    
    public TextMeshProUGUI txt_grid_player1name;
    public TextMeshProUGUI txt_grid_player2name;
    public TextMeshProUGUI txt_grid_player3name;
    public TextMeshProUGUI txt_grid_player1score;
    public TextMeshProUGUI txt_grid_player2score;
    public TextMeshProUGUI txt_grid_player3score;
    
    // Start is called before the first frame update
    public void OnButtonClick()
    {
        
        // Score Board
        txt_grid_player1name.text= PlayerPrefs.GetString("Player1Name");
        txt_grid_player2name.text= PlayerPrefs.GetString("Player2Name");
        txt_grid_player3name.text= PlayerPrefs.GetString("Player3Name");
        
        txt_grid_player1score.text = PlayerPrefs.GetInt("Player1Score").ToString();
        txt_grid_player2score.text = PlayerPrefs.GetInt("Player2Score").ToString();
        txt_grid_player3score.text = PlayerPrefs.GetInt("Player3Score").ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
