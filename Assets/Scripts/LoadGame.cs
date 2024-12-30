using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadGame : MonoBehaviour
{
    
    public Button ScoreRefreshButton;   
    // Start is called before the first frame update
    void Start()
    {
    
        ScoreRefreshButton.onClick.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
