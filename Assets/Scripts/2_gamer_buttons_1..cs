using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Gamer_Buttons : MonoBehaviour
{

    [Header("Questions")]
    public TextMeshProUGUI age;
    public TextMeshProUGUI sex;


    private string[] sexos = { "Feminino", "Masculino" };
    private int sexoIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        age.text = GlobalVariables.age.ToString();
        sex.text = GlobalVariables.sex;

        // Button LEDs
        for (int i = 0; i < 6; i++){
            SerialReader.instance.SendData(i + "G\n"); // Turn Green
        }
        SerialReader.instance.SendData("3R\n"); // Turn Red

        // Set all the scene buttons in SerialReader
        SerialReader.instance.btn_1 = GameObject.FindWithTag("2_gamer_btn_1")?.GetComponent<Button>();
        SerialReader.instance.btn_2 = GameObject.FindWithTag("2_gamer_btn_2")?.GetComponent<Button>();
        SerialReader.instance.btn_3 = GameObject.FindWithTag("2_gamer_btn_3")?.GetComponent<Button>();
        SerialReader.instance.btn_4 = GameObject.FindWithTag("2_gamer_btn_4")?.GetComponent<Button>();
        SerialReader.instance.btn_5 = GameObject.FindWithTag("2_gamer_btn_5")?.GetComponent<Button>();
        SerialReader.instance.btn_6 = GameObject.FindWithTag("2_gamer_btn_6")?.GetComponent<Button>();


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
        int currentAge = int.Parse(age.text); // Converte texto para número
        currentAge--;                         // Decrementa
        age.text = currentAge.ToString();     // Atualiza o texto    
    }
    public void btn_2_click()
    {
        // Avança para o próximo estado
        sexoIndex = (sexoIndex + 1) % sexos.Length;
        sex.text = sexos[sexoIndex];
    }
    public void btn_3_click()
    {

    }
    public void btn_4_click()
    {
        int currentAge = int.Parse(age.text); // Converte texto para número
        currentAge++;                         // Incrementa
        age.text = currentAge.ToString();     // Atualiza o texto    
    }
    public void btn_5_click()
    {
        // Avança para o próximo estado
        sexoIndex = (sexoIndex + 1) % sexos.Length;
        sex.text = sexos[sexoIndex];
    }
    public void btn_6_click()
    {     
        GlobalVariables.age = int.Parse(age.text);
        GlobalVariables.sex = sex.text;
    }


}
