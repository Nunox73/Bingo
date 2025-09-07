using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Questi_Buttons_6_1 : MonoBehaviour
{

 

    // Start is called before the first frame update
    void Start()
    {


        // Turn all the buttons Red
        SerialReader.instance.SendData("9R\n");
        SerialReader.instance.SendData("6G\n");

        // Set all the scene buttons in SerialReader

        SerialReader.instance.btn_6 = GameObject.FindWithTag("6_questi_btn_6")?.GetComponent<Button>();

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
