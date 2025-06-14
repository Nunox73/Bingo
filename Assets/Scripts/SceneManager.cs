using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
   
    

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.backgroundColor = new Color(0.749f, 0.945f, 0.984f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        
        // Add results to Total Score
        PlayerPrefs.SetInt("Player1Total", PlayerPrefs.GetInt("Player1Total") + PlayerPrefs.GetInt("Player1Score"));
        PlayerPrefs.SetInt("Player2Total", PlayerPrefs.GetInt("Player2Total") + PlayerPrefs.GetInt("Player2Score"));
        PlayerPrefs.SetInt("Player3Total", PlayerPrefs.GetInt("Player3Total") + PlayerPrefs.GetInt("Player3Score"));
        PlayerPrefs.Save();
        
        
        //move to Game scene
        SceneManager.LoadScene(sceneName);
        Debug.Log("Load " + sceneName + " Scene");
        
    }
}
