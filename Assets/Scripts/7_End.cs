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
        //string sheetName = "Player";
        string PlayerID = GlobalVariables.PlayerID;
        string Age = GlobalVariables.age.ToString();
        string Sex = GlobalVariables.sex;
        string EstadoCivil = GlobalVariables.estadoCivil;
        string Residence = GlobalVariables.residence;
        string TechUse = GlobalVariables.tech_use;
        string TechDificulty = GlobalVariables.tech_dificulty;
        string Q1 = GlobalVariables.TAMQuest[0, 1].ToString();
        string Q2 = GlobalVariables.TAMQuest[1, 1].ToString();
        string Q3 = GlobalVariables.TAMQuest[2, 1].ToString();
        string Q4 = GlobalVariables.TAMQuest[3, 1].ToString();
        string Q5 = GlobalVariables.TAMQuest[4, 1].ToString();
        string Q6 = GlobalVariables.TAMQuest[5, 1].ToString();
        string Q7 = GlobalVariables.TAMQuest[6, 1].ToString();
        string Q8 = GlobalVariables.TAMQuest[7, 1].ToString();
        string Q9 = GlobalVariables.TAMQuest[8, 1].ToString();
        string Q10 = GlobalVariables.TAMQuest[9, 1].ToString();
        string Q11 = GlobalVariables.TAMQuest[10, 1].ToString();
        string Q12 = GlobalVariables.TAMQuest[11, 1].ToString();
        string Q13 = GlobalVariables.TAMQuest[12, 1].ToString();
        string Q14 = GlobalVariables.TAMQuest[13, 1].ToString();
        string Q15 = GlobalVariables.TAMQuest[14, 1].ToString();


        googleSender.SendPlayerData(
            PlayerID, Age, Sex, EstadoCivil, Residence, TechUse, TechDificulty,
            Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11,Q12,Q13,Q14,Q15);

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
