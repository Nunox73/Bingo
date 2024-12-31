using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Change_Name : MonoBehaviour
{

    public TMP_InputField Player_Name;

    public SetPlayersPrefs scriptA;
    
    // Start is called before the first frame update
    void Start()
    {
        
      

    }

    // Update is called once per frame
    void Update()
    {
       

    }

    // This method will be called when the button is clicked
    public void ChangeName()
    {

        // Saving data
        PlayerPrefs.SetString("Player1Name", Player_Name.text);
        PlayerPrefs.Save();

        scriptA.UpdateBoard();
       
    }


}
