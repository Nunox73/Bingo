using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Ball_Button : MonoBehaviour
{

    private Text drawnNumber;
    public Button yesButton;

    [Header("Sounds")]
    public AudioSource CorrectNumber;

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

        

    }

    // Update is called once per frame
    void Update()
    {
       

    }

    // This method will be called when the button is clicked
    public void OnButtonClick()
    {

        if (yesButton == null)
        {

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
        }
        //Debug.Log("Button was clicked!");
        if (drawnNumber.text == TextNumber.text) {
                CorrectNumber.Play();
                yesButton.onClick.Invoke();
                
        }
    }


}