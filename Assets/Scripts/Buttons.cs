using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
        }

    }

    public void btn_3_click()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Main")
        {
            
        }

     }


}
