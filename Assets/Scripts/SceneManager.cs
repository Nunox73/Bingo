using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
   
    public TMP_InputField Player_Name;
    public TextMeshProUGUI Player_Score;

    // Start is called before the first frame update
    void Start()
    {
        Player_Name.text = PlayerPrefs.GetString("Player1Name");
        Player_Score.text = PlayerPrefs.GetInt("Player1Score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        //move to Game scene
        SceneManager.LoadScene(sceneName);
        Debug.Log("Load " + sceneName + " Scene");
    }
}
