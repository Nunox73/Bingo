using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Game_Buttons : MonoBehaviour
{

 
 

    // Start is called before the first frame update
    void Start()
    {

        // Leds
        SerialReader.instance.SendData("90\n"); // All Off
        SerialReader.instance.SendData("3R\n"); // 3 Red
        SerialReader.instance.SendData("6G\n"); // 6 Green

        // Set all the scene buttons in SerialReader
        SerialReader.instance.btn_1 = GameObject.FindWithTag("5_game_btn_1")?.GetComponent<Button>();
            //SerialReader.instance.btn_1.SetActive(false);
        SerialReader.instance.btn_2 = GameObject.FindWithTag("5_game_btn_2")?.GetComponent<Button>();
        SerialReader.instance.btn_3 = GameObject.FindWithTag("5_game_btn_3")?.GetComponent<Button>();
        SerialReader.instance.btn_4 = GameObject.FindWithTag("5_game_btn_4")?.GetComponent<Button>();
        SerialReader.instance.btn_5 = GameObject.FindWithTag("5_game_btn_5")?.GetComponent<Button>();
        SerialReader.instance.btn_6 = GameObject.FindWithTag("5_game_btn_6")?.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void btn_1_click()
    {
        
    }
    public void btn_2_click()
    {
        
    }
    public void btn_3_click()
    {
        
    }
    public void btn_4_click()
    {
          
    }
    public void btn_5_click()
    {
        
    }
    public void btn_6_click()
    {
        
    }

}
