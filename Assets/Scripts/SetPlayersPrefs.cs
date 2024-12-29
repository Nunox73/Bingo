using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetPlayersPrefs : MonoBehaviour
{
    // Start is cal
    public TMP_InputField Player_Name;
    public TextMeshProUGUI Player_Score;
    void Start()
    {
        
        // Reset Game and Player Preferences
        PlayerPrefs.SetString("Player1Name", "Nome do Jogador?");
        PlayerPrefs.SetString("Player2Name", "Joaquim");
        PlayerPrefs.SetString("Player3Name", "Maria");
        PlayerPrefs.SetInt("Player1Score", 0);
        PlayerPrefs.SetInt("Player2Score", 0);
        PlayerPrefs.SetInt("Player3Score", 0);
        PlayerPrefs.Save();

        Player_Name.text = PlayerPrefs.GetString("Player1Name");
        Player_Score.text = PlayerPrefs.GetInt("Player1Score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
