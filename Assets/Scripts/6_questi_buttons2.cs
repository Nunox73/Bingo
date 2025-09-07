using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Questi_Buttons_6_2 : MonoBehaviour
{

    [Header("Question")]
    public TextMeshProUGUI TextQuestion;
    private int QuestionID = 0;

    // Start is called before the first frame update
    void Start()
    {


        // Turn all the buttons Red
        SerialReader.instance.SendData("9G\n");
        SerialReader.instance.SendData("6R\n");
        SerialReader.instance.SendData("7R\n");

        // Set all the scene buttons in SerialReader
        SerialReader.instance.btn_1 = GameObject.FindWithTag("6_questi2_btn_1")?.GetComponent<Button>();
        SerialReader.instance.btn_2 = GameObject.FindWithTag("6_questi2_btn_2")?.GetComponent<Button>();
        SerialReader.instance.btn_3 = GameObject.FindWithTag("6_questi2_btn_3")?.GetComponent<Button>();
        SerialReader.instance.btn_4 = GameObject.FindWithTag("6_questi2_btn_4")?.GetComponent<Button>();
        SerialReader.instance.btn_5 = GameObject.FindWithTag("6_questi2_btn_5")?.GetComponent<Button>();
        SerialReader.instance.btn_6 = GameObject.FindWithTag("6_questi2_btn_6")?.GetComponent<Button>();

        // Set 1st Question
        TextQuestion.text = GlobalVariables.TAMQuest[0, 0];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void btn_1_click()
    {
        GlobalVariables.TAMQuest[QuestionID, 1] = "1";
        ChangeQuestion();
    }
    public void btn_2_click()
    {
        GlobalVariables.TAMQuest[QuestionID, 1] = "2";
        ChangeQuestion();
    }
    public void btn_3_click()
    {
        GlobalVariables.TAMQuest[QuestionID, 1] = "3";
        ChangeQuestion();
    }
    public void btn_4_click()
    {
        GlobalVariables.TAMQuest[QuestionID, 1] = "4";
        ChangeQuestion();
    }
    public void btn_5_click()
    {
        GlobalVariables.TAMQuest[QuestionID, 1] = "5";
        ChangeQuestion();
    }
    public void btn_6_click()
    {

    }
    public void ChangeQuestion()
    {
        QuestionID = QuestionID + 1;
        // Set Next Question
        if (QuestionID <= 14)
        {
            TextQuestion.text = GlobalVariables.TAMQuest[QuestionID, 0];
        }
        
    }

}
