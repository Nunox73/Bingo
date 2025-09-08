using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class End_Buttons_7 : MonoBehaviour
{

    public SceneManagerScript scenemanagerscript;

    // Start is called before the first frame update
    void Start()
    {


        // Turn all the buttons Red
        SerialReader.instance.SendData("9R\n");
        SerialReader.instance.SendData("6G\n");

        // Set all the scene buttons in SerialReader

        SerialReader.instance.btn_6 = GameObject.FindWithTag("7_End_btn_6")?.GetComponent<Button>();

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
