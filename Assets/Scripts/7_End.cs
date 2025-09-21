using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class End_Buttons_7 : MonoBehaviour
{

    public SceneManagerScript scenemanagerscript;

    public GoogleSheetsSender googleSender;

    // Start is called before the first frame update
    void Start()
    {

        // Save Game Variables
        string sheetName = "Player";
        string playerName = "João";
        int score = 120;
        float timePlayed = 85.3f;

        googleSender.SendPlayerData(sheetName,playerName, score, timePlayed);

        if (GlobalVariables.buttonsConnected)
        {
            // Leds
            SerialReader.instance.SendData("9O\n"); // All Off
            SerialReader.instance.SendData("6G\n"); // 6 Green

            // Set all the scene buttons in SerialReader
            SerialReader.instance.btn_6 = GameObject.FindWithTag("7_End_btn_6")?.GetComponent<Button>();
        }
        



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btn_6_click()
    {
        scenemanagerscript.LoadScene("1.Main");
    }

}
