using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Gamer_Buttons : MonoBehaviour
{

    [Header("Questions")]
    public TextMeshProUGUI age;
    public TextMeshProUGUI sex;
    public TextMeshProUGUI estadoCivil;

    private string[] sexos = { "Feminino", "Masculino" };
    private int sexoIndex = 0;
    private string[] estados = { "Solteiro(a)", "Casado(a)", "Viúvo(a)", "Divorciado(a)" };
    private int estadoIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        age.text = GlobalVariables.age.ToString();
        sex.text = GlobalVariables.sex;
        estadoCivil.text = GlobalVariables.estadoCivil;

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
        // Recua para o próximo estado
        if (estadoIndex > 0)
        {
            estadoIndex = (estadoIndex - 1) % estados.Length;
            estadoCivil.text = estados[estadoIndex];
        }
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
        // Avança para o próximo estado
        estadoIndex = (estadoIndex + 1) % estados.Length;
        estadoCivil.text = estados[estadoIndex];
    }
    public void btn_7_click()
    {
        GlobalVariables.age = int.Parse(age.text);
        GlobalVariables.sex = sex.text;
        GlobalVariables.estadoCivil = estadoCivil.text;
    }

}
