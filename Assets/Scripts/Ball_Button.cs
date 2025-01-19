using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Ball_Button : MonoBehaviour
{

    private Text drawnNumber;
    public Button yesButton;
    private Button btn_play;

    [Header("Sounds")]
    public AudioSource CorrectNumber;
    public AudioSource IncorrectNumber;

    [Header("Number")]
    public TextMeshProUGUI TextNumber;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        // Find the Drawn Number GameObject by name
        GameObject textObject = GameObject.Find("txt_drawnNumber");

        if (textObject != null)
        {
            // Get the TextMeshProUGUI component
            drawnNumber = textObject.GetComponent<Text>();

            if (drawnNumber != null)
            {
                //Debug.Log("Found TextMeshProUGUI: " + drawnNumber.text);
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject with the specified name not found.");
        }

        
            // Find the Yes Button GameObject by name
            GameObject buttonObject = GameObject.Find("btn_yes");

            if (buttonObject != null)
            {
                // Get the Button component
                yesButton = buttonObject.GetComponent<Button>();

                if (yesButton != null)
                {
                    //Debug.Log("Found Button: " + yesButton.name);
                    //yesButton.onClick.AddListener(OnButtonClick);
                }
                else
                {
                    Debug.LogError("Button component not found on the GameObject.");
                }
            }
            else
            {
                Debug.LogError("GameObject with the specified name not found.");
            }

            // Find the Play Button GameObject by name
            GameObject buttonObject2 = GameObject.Find("btn_play");

            if (buttonObject2 != null)
            {
                // Get the Button component
                btn_play = buttonObject2.GetComponent<Button>();

                if (btn_play != null)
                {
                    //Debug.Log("Found Button: " + yesButton.name);
                    //yesButton.onClick.AddListener(OnButtonClick);
                }
                else
                {
                    Debug.LogError("Play Button component not found on the GameObject.");
                }
            }
            else
            {
                Debug.LogError("GameObject (Play Button) with the specified name not found.");
            }

    }

    // Update is called once per frame
    void Update()
    {
       

    }

    // This method will be called when the button is clicked
    public void OnButtonClick()
    {

        if (btn_play.gameObject.activeInHierarchy == false){
            if (drawnNumber.text == TextNumber.text) {
                StartCoroutine(CorrectNumber_Async()); 
            } else {
                StartCoroutine(IncorrectNumber_Async()); 
            }
        }
        //Debug.Log("Button was clicked!");
        
    }

    IEnumerator CorrectNumber_Async()
    {
        CorrectNumber.Play();
        bool CheckIfIsPlaying() => CorrectNumber.isPlaying;
        // Waits for the audiosource finish playing the audio
        yield return new WaitWhile(CheckIfIsPlaying);
        yesButton.onClick.Invoke(); 
    }
    IEnumerator IncorrectNumber_Async()
    {
        IncorrectNumber.Play();
        bool CheckIfIsPlaying() => IncorrectNumber.isPlaying;
        // Waits for the audiosource finish playing the audio
        yield return new WaitWhile(CheckIfIsPlaying);
        int score = PlayerPrefs.GetInt("Player1Score") - 1;
        PlayerPrefs.SetInt("Player1Score", score);
        
    }


}
