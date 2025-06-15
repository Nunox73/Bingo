using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Gamer_Buttons_3 : MonoBehaviour
{

    [Header("Questions")]
    public TextMeshProUGUI tech_use;
    public TextMeshProUGUI tech_dificulty;

    private string[] tech_uses = { "Sim", "Não" };
    private int tech_useIndex = 0;

    private string[] tech_dificultys = { "Sim", "Não" };
    private int tech_dificultyIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        tech_use.text = GlobalVariables.tech_use;
        tech_dificulty.text = GlobalVariables.tech_dificulty;

        // Turn all the buttons Green
        //Serial.SendData("9G\n");
        SerialReader.instance.SendData("9G\n");
        SerialReader.instance.SendData("3R\n");

        // Set all the scene buttons in SerialReader
        SerialReader.instance.btn_1 = GameObject.FindWithTag("4_gamer_btn_1")?.GetComponent<Button>();
        SerialReader.instance.btn_2 = GameObject.FindWithTag("4_gamer_btn_2")?.GetComponent<Button>();
        SerialReader.instance.btn_3 = GameObject.FindWithTag("4_gamer_btn_3")?.GetComponent<Button>();
        SerialReader.instance.btn_4 = GameObject.FindWithTag("4_gamer_btn_4")?.GetComponent<Button>();
        SerialReader.instance.btn_5 = GameObject.FindWithTag("4_gamer_btn_5")?.GetComponent<Button>();
        SerialReader.instance.btn_6 = GameObject.FindWithTag("4_gamer_btn_6")?.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void btn_1_click()
    {
        // Recua para o próximo estado
        if (tech_useIndex > 0)
        {
            tech_useIndex = (tech_useIndex - 1) % tech_uses.Length;
            tech_use.text = tech_uses[tech_useIndex];
        }   
    }
    public void btn_2_click()
    {
        // Recua nas residences
        if (tech_dificultyIndex > 0)
        {
            tech_dificultyIndex = (tech_dificultyIndex - 1) % tech_dificultys.Length;
            tech_dificulty.text = tech_dificultys[tech_dificultyIndex];
        } 
    }
    public void btn_3_click()
    {
        
    }
    public void btn_4_click()
    {
        // Avança para o próximo estado
        tech_useIndex = (tech_useIndex + 1) % tech_uses.Length;
        tech_use.text = tech_uses[tech_useIndex];   
    }
    public void btn_5_click()
    {
        // Avança nas residences
        tech_dificultyIndex = (tech_dificultyIndex + 1) % tech_dificultys.Length;
        tech_dificulty.text = tech_dificultys[tech_dificultyIndex]; 
    }
    public void btn_6_click()
    {
        GlobalVariables.tech_use = tech_use.text;
        GlobalVariables.tech_dificulty = tech_dificulty.text;
    }

}
