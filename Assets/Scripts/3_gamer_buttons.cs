using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Gamer_Buttons_2 : MonoBehaviour
{

    [Header("Questions")]
    public TextMeshProUGUI estadoCivil;
    public TextMeshProUGUI residence;

    private string[] estados = { "Solteiro(a)", "Casado(a)", "Viúvo(a)", "Divorciado(a)" };
    private int estadoIndex = 1;

    private string[] residences = { "Casa, sozinho(a)", "Casa, com outros familiares", "Residência assistida" };
    private int residenceIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        estadoCivil.text = GlobalVariables.estadoCivil;
        residence.text = GlobalVariables.residence;

        // Button LEDs
        for (int i = 0; i <= 6; i++){
            SerialReader.instance.SendData(i + "G\n"); // Turn Green
        }
        SerialReader.instance.SendData("3R\n"); // Turn Red

        // Set all the scene buttons in SerialReader
        SerialReader.instance.btn_1 = GameObject.FindWithTag("3_gamer_btn_1")?.GetComponent<Button>();
        SerialReader.instance.btn_2 = GameObject.FindWithTag("3_gamer_btn_2")?.GetComponent<Button>();
        SerialReader.instance.btn_3 = GameObject.FindWithTag("3_gamer_btn_3")?.GetComponent<Button>();
        SerialReader.instance.btn_4 = GameObject.FindWithTag("3_gamer_btn_4")?.GetComponent<Button>();
        SerialReader.instance.btn_5 = GameObject.FindWithTag("3_gamer_btn_5")?.GetComponent<Button>();
        SerialReader.instance.btn_6 = GameObject.FindWithTag("3_gamer_btn_6")?.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

        }

    }
    public void btn_1_click()
    {
        // Recua para o próximo estado
        if (estadoIndex > 0)
        {
            estadoIndex = (estadoIndex - 1) % estados.Length;
            estadoCivil.text = estados[estadoIndex];
        }   
    }
    public void btn_2_click()
    {
        // Recua nas residences
        if (residenceIndex > 0)
        {
            residenceIndex = (residenceIndex - 1) % residences.Length;
            residence.text = residences[residenceIndex];
        } 
    }
    public void btn_3_click()
    {
        
    }
    public void btn_4_click()
    {
        // Avança para o próximo estado
        estadoIndex = (estadoIndex + 1) % estados.Length;
        estadoCivil.text = estados[estadoIndex];   
    }
    public void btn_5_click()
    {
        // Avança nas residences
        residenceIndex = (residenceIndex + 1) % residences.Length;
        residence.text = residences[residenceIndex]; 
    }
    public void btn_6_click()
    {
        GlobalVariables.estadoCivil = estadoCivil.text;
        GlobalVariables.residence = residence.text;
    }

}
