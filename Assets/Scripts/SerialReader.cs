using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Concurrent;
using UnityEngine.SceneManagement;



// Instructions:
// Message format "2R\n"
//    1st character is a number
//        1 to 7 represents the LED button
//        9 represents all LEDs
//    2nd character represents the color
//        G --> Green
//        R --> Red
//        O --> Off

// Examples:
//  Send "9R\n" (All the LEDs turn Red)
// Send "1G\n" (LED 1 turn Green)
// Send "9O\n" (All the LEDs turn Off)


public class SerialReader : MonoBehaviour
{

    public static SerialReader instance; // Singleton instance

    public TextMeshProUGUI COM;

    [Header("Serial Config")]
    SerialPort serialPort = new SerialPort("COM10", 9600, Parity.None, 8, StopBits.One);
    private Thread serialThread;
    private bool isRunning = true;
    private string receivedData = "";


     [Header("Buttons")]
    public Button btn_1;
    public Button btn_2;
    public Button btn_3;
    public Button btn_4;
    public Button btn_5;
    public Button btn_6;
    public Button btn_7;
    public Button btn_8;
    private ConcurrentQueue<string> mainThreadActions = new ConcurrentQueue<string>();



    
    void Awake()
    {
        if (instance == null) // First instance
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void Start()
    {
        string[] ports = SerialPort.GetPortNames();
        Debug.Log("[ArduinoAutoDetector] Portas encontradas: " + string.Join(", ", ports));
        COM.text = string.Join(", ", ports);

        serialPort.ReadTimeout = 100; // Prevents indefinite blocking

        try
        {
            serialPort.Open();
            serialThread = new Thread(ReadSerialData);
            serialThread.Start();
            GlobalVariables.buttonsConnected = true;
            
        }
        catch (Exception e)
        {
            Debug.LogError("Serial Port Error: " + e.Message);
            GlobalVariables.buttonsConnected = false;
        }
    }

    void ReadSerialData()
    {
        while (isRunning)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    string data = serialPort.ReadLine(); // Blocking call
                    lock (this)
                    {


                        // Analise the data
                        receivedData = data;
                        Debug.Log("Received: " + receivedData);
                        
                        switch (receivedData)
                        {
                            case "Button 1 Pressed!":
                                //Debug.Log("Button 1 Clicked!");
                                mainThreadActions.Enqueue("Button1");
                                break;
                            case "Button 2 Pressed!":
                                //Debug.Log("Button 2 Clicked!");
                                mainThreadActions.Enqueue("Button2");
                                break;
                            case "Button 3 Pressed!":
                                //Debug.Log("Button 3 Clicked!");
                                mainThreadActions.Enqueue("Button3");
                                break;
                            case "Button 4 Pressed!":
                                //Debug.Log("Button 4 Clicked!");
                                mainThreadActions.Enqueue("Button4");
                                break;
                            case "Button 5 Pressed!":
                                //Debug.Log("Button 5 Clicked!");
                                mainThreadActions.Enqueue("Button5");
                                break;
                            case "Button 6 Pressed!":
                                //Debug.Log("Button 6 Clicked!");
                                mainThreadActions.Enqueue("Button6");
                                break;
                            case "Button 7 Pressed!":
                                //Debug.Log("Button 7 Clicked!");
                                mainThreadActions.Enqueue("Button7");
                                break;
                            case " Start":
                                SendData("9O\n");
                                SendData("6G\n");
                                SendData("3R\n");
                                break;
                            default:
                                Debug.Log("Lixo");
                                break;
                        }
                        
                    }
                }
            }
            catch (TimeoutException) { } // Ignore timeouts to keep the thread running
            catch (Exception e)
            {
                
                Debug.LogError("Error in Serial Thread: " + e.Message);
            }
        }
    }

    void Update()
    {

        // Handle queued Unity actions
        while (mainThreadActions.TryDequeue(out string action))
        {
            switch (action)
            {
                case "Button1":
                    btn_1.onClick.Invoke(); // Now it's safe to invoke
                    Debug.Log("Button 1 clicked via serial");
                    break;
                case "Button2":
                    btn_2.onClick.Invoke(); // Now it's safe to invoke
                    Debug.Log("Button 2 clicked via serial");
                    break;
                case "Button3":
                    btn_3.onClick.Invoke(); // Now it's safe to invoke
                    Debug.Log("Button 3 clicked via serial");
                    break;
                case "Button4":
                    btn_4.onClick.Invoke(); // Now it's safe to invoke
                    Debug.Log("Button 4 clicked via serial");
                    break;
                case "Button5":
                    btn_5.onClick.Invoke(); // Now it's safe to invoke
                    Debug.Log("Button 5 clicked via serial");
                    break;
                case "Button6":
                    btn_6.onClick.Invoke(); // Now it's safe to invoke
                    Debug.Log("Button 6 clicked via serial");
                    break;
                case "Button7":
                    btn_7.onClick.Invoke(); // Now it's safe to invoke
                    Debug.Log("Button 7 clicked via serial");
                    break;


            }
        }
        lock (this)
        {
            if (!string.IsNullOrEmpty(receivedData))
            {
                Debug.Log("Received: " + receivedData);
                if (receivedData == "UNO_VITALSPIRIT_READY") // First instance
                {
                    //SerialReader.instance.SendData(0 + "O\n"); //Turn all the buttons off
                }
                receivedData = ""; // Clear after processing
            }
        }
    }

    public void OnApplicationQuit() //Call when leaving the Scene
    {
        isRunning = false;
        serialThread.Join(); // Ensure thread ends
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    public void SendData(string message)
    {
        if (serialPort.IsOpen)
        {
            serialPort.WriteLine(message); // Send message
            Debug.Log("Sent: " + message);
        }
        else
        {
            Debug.LogError("Serial port not open!");
        }
    }

    public void btn_3_click()
    {
        Debug.LogError("btn_3_click ");
    }

    public void btn_6_click()
    {
        Debug.LogError("btn_6_click ");
    }
    private void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

private void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    RebindButtonsForCurrentScene();
}

private void RebindButtonsForCurrentScene()
{
    // Exemplo: tenta encontrar botões por TAG (ajusta as tags às tuas cenas)
    
    btn_3 = GameObject.FindWithTag("1_main_btn_3")?.GetComponent<Button>();

    btn_6 = GameObject.FindWithTag("1_main_btn_6")?.GetComponent<Button>();
   

    Debug.Log($"[SerialReader] Rebind feito. btn_1={(btn_1!=null)} btn_2={(btn_2!=null)} btn_3={(btn_3!=null)} ...");
}

}
