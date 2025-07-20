using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Game_Buttons : MonoBehaviour
{

    public BingoGameWithPlayers bingogamewithplayers;
    public SceneManagerScript scenemanagerscript;
 

    // Start is called before the first frame update
    void Start()
    {

        // Leds
        SerialReader.instance.SendData("9O\n"); // All Off
        SerialReader.instance.SendData("3R\n"); // 3 Red
        SerialReader.instance.SendData("6G\n"); // 6 Green

        // Set all the scene buttons in SerialReader
        SerialReader.instance.btn_1 = GameObject.FindWithTag("5_game_btn_1")?.GetComponent<Button>();
            //SerialReader.instance.btn_1.interactable = false; // Disables the button
            SerialReader.instance.btn_1.gameObject.SetActive(false);
        SerialReader.instance.btn_2 = GameObject.FindWithTag("5_game_btn_2")?.GetComponent<Button>();
            SerialReader.instance.btn_2.gameObject.SetActive(false);
        SerialReader.instance.btn_3 = GameObject.FindWithTag("5_game_btn_3")?.GetComponent<Button>();
        SerialReader.instance.btn_4 = GameObject.FindWithTag("5_game_btn_4")?.GetComponent<Button>();
            SerialReader.instance.btn_4.gameObject.SetActive(false);
        SerialReader.instance.btn_5 = GameObject.FindWithTag("5_game_btn_5")?.GetComponent<Button>();
            SerialReader.instance.btn_5.gameObject.SetActive(false);
        SerialReader.instance.btn_6 = GameObject.FindWithTag("5_game_btn_6")?.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void btn_1_click()
    {
        if (SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text == "Pausa")
        {
            if (SerialReader.instance.btn_1.GetComponentInChildren<TextMeshProUGUI>().text == "Não Tenho" && SerialReader.instance.btn_1.gameObject.activeSelf)
            {
                bingogamewithplayers.NoButton();
            }
            else if (SerialReader.instance.btn_1.GetComponentInChildren<TextMeshProUGUI>().text == "Tenho" && SerialReader.instance.btn_1.gameObject.activeSelf)
            {
                bingogamewithplayers.YesButton();
            }
        }
        
    }
    public void btn_2_click()
    {
        if (SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text == "Pausa")
        {
            if (SerialReader.instance.btn_2.GetComponentInChildren<TextMeshProUGUI>().text == "Não Tenho" && SerialReader.instance.btn_2.gameObject.activeSelf)
            {
                bingogamewithplayers.NoButton();
            }
            else if (SerialReader.instance.btn_2.GetComponentInChildren<TextMeshProUGUI>().text == "Tenho" && SerialReader.instance.btn_2.gameObject.activeSelf)
            {
                bingogamewithplayers.YesButton();
            }
        }
    }
    public void btn_3_click()
    {
        scenemanagerscript.LoadScene("1.Main");
    }
    public void btn_4_click()
    {
        if (SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text == "Pausa")
        {
            if (SerialReader.instance.btn_4.GetComponentInChildren<TextMeshProUGUI>().text == "Não Tenho" && SerialReader.instance.btn_4.gameObject.activeSelf)
            {
                bingogamewithplayers.NoButton();
            }
            else if (SerialReader.instance.btn_4.GetComponentInChildren<TextMeshProUGUI>().text == "Tenho" && SerialReader.instance.btn_4.gameObject.activeSelf)
            {
                bingogamewithplayers.YesButton();
            }
        }
    }
    public void btn_5_click()
    {
        if (SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text == "Pausa")
        {
            if (SerialReader.instance.btn_5.GetComponentInChildren<TextMeshProUGUI>().text == "Não Tenho" && SerialReader.instance.btn_5.gameObject.activeSelf)
            {
                bingogamewithplayers.NoButton();
            }
            else if (SerialReader.instance.btn_5.GetComponentInChildren<TextMeshProUGUI>().text == "Tenho" && SerialReader.instance.btn_5.gameObject.activeSelf)
            {
                bingogamewithplayers.YesButton();
            }
        }
    }
    public void btn_6_click()
    {
        if (SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text == "Jogar")
        {
            SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text = "Pausa";
            bingogamewithplayers.txt_vamos.gameObject.SetActive(false); // Remove the initial message
            bingogamewithplayers.PlayerCardsParent.gameObject.SetActive(true);
            bingogamewithplayers.Scores.gameObject.SetActive(true);
            bingogamewithplayers.btn_Start();
        }
        else if (SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text == "Pausa")
        {
            SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text = "Jogar";
            bingogamewithplayers.btn_Pause();
            SerialReader.instance.btn_1.gameObject.SetActive(false);
            SerialReader.instance.btn_2.gameObject.SetActive(false);
            SerialReader.instance.btn_4.gameObject.SetActive(false);
            SerialReader.instance.btn_5.gameObject.SetActive(false);
        } else if (SerialReader.instance.btn_6.GetComponentInChildren<TextMeshProUGUI>().text == "Seguinte"){
            scenemanagerscript.LoadScene("6.Questi");
        }
        
    }

}
