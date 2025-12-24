using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Concurrent;
using UnityEngine.SceneManagement;

public class SerialReader : MonoBehaviour
{
    public static SerialReader instance; // Singleton instance

    public TextMeshProUGUI COM;

    [Header("Serial Config")]
    public int baudRate = 9600;
    public int readTimeout = 100;
    public int writeTimeout = 200;

    // Handshake (Versão B)
    public string handshakeRequest = "ID?";
    public string expectedResponse = "UNO_VITALSPIRIT";
    public int arduinoResetDelayMs = 1200; // UNO normalmente dá reset ao abrir a porta

    private SerialPort serialPort;
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
        Debug.Log("[SerialReader] Portas encontradas: " + string.Join(", ", ports));
        if (COM) COM.text = string.Join(", ", ports);

        // 1) Encontrar Arduino automaticamente
        bool found = TryFindArduinoPort(ports);

        GlobalVariables.buttonsConnected = found;

        if (!found)
        {
            Debug.LogError("[SerialReader] Não consegui encontrar Arduino (handshake falhou em todas as portas).");
            return;
        }

        // 2) Iniciar thread de leitura
        isRunning = true;
        serialThread = new Thread(ReadSerialData);
        serialThread.IsBackground = true;
        serialThread.Start();

        // 3) LEDs iniciais
        LEDS();
    }

    private bool TryFindArduinoPort(string[] ports)
    {
        foreach (string portName in ports)
        {
            SerialPort sp = null;
            try
            {
                sp = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                sp.ReadTimeout = 300;
                sp.WriteTimeout = writeTimeout;
                sp.NewLine = "\n";

                sp.Open();

                // Muitos Arduino UNO resetam quando a porta abre
                Thread.Sleep(arduinoResetDelayMs);

                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();

                // Enviar pedido de ID
                sp.WriteLine(handshakeRequest);

                // Ler resposta
                string resp = sp.ReadLine().Trim();
                Debug.Log($"[SerialReader] {portName} respondeu: {resp}");

                if (resp.Contains(expectedResponse))
                {
                    serialPort = sp;
                    serialPort.ReadTimeout = readTimeout;

                    Debug.Log("[SerialReader] Arduino encontrado em: " + portName);
                    if (COM) COM.text = portName;
                    return true;
                }

                // Não é o nosso Arduino
                sp.Close();
            }
            catch (Exception e)
            {
                Debug.LogWarning("[SerialReader] Falhou " + portName + " -> " + e.Message);
                try { if (sp != null && sp.IsOpen) sp.Close(); } catch { }
            }
        }

        return false;
    }

    void ReadSerialData()
    {
        while (isRunning)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    string data = serialPort.ReadLine(); // Blocking call (com timeout)
                    data = data.Trim();

                    lock (this)
                    {
                        receivedData = data;
                    }

                    // Analise the data
                    switch (data)
                    {
                        case "Button 1 Pressed!":
                            mainThreadActions.Enqueue("Button1");
                            break;
                        case "Button 2 Pressed!":
                            mainThreadActions.Enqueue("Button2");
                            break;
                        case "Button 3 Pressed!":
                            mainThreadActions.Enqueue("Button3");
                            break;
                        case "Button 4 Pressed!":
                            mainThreadActions.Enqueue("Button4");
                            break;
                        case "Button 5 Pressed!":
                            mainThreadActions.Enqueue("Button5");
                            break;
                        case "Button 6 Pressed!":
                            mainThreadActions.Enqueue("Button6");
                            break;
                        case "Button 7 Pressed!":
                            mainThreadActions.Enqueue("Button7");
                            break;

                        case " Start":
                            SendData("9O\n");
                            SendData("6G\n");
                            SendData("3R\n");
                            break;

                        default:
                            // Debug.Log("Lixo: " + data);
                            break;
                    }
                }
            }
            catch (TimeoutException) { } // Ignore timeouts
            catch (Exception e)
            {
                Debug.LogError("[SerialReader] Error in Serial Thread: " + e.Message);
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
                    if (btn_1 != null) btn_1.onClick.Invoke();
                    else Debug.LogWarning("[SerialReader] btn_1 null (rebind necessário).");
                    break;

                case "Button2":
                    if (btn_2 != null) btn_2.onClick.Invoke();
                    else Debug.LogWarning("[SerialReader] btn_2 null (rebind necessário).");
                    break;

                case "Button3":
                    if (btn_3 != null) btn_3.onClick.Invoke();
                    else Debug.LogWarning("[SerialReader] btn_3 null (rebind necessário).");
                    break;

                case "Button4":
                    if (btn_4 != null) btn_4.onClick.Invoke();
                    else Debug.LogWarning("[SerialReader] btn_4 null (rebind necessário).");
                    break;

                case "Button5":
                    if (btn_5 != null) btn_5.onClick.Invoke();
                    else Debug.LogWarning("[SerialReader] btn_5 null (rebind necessário).");
                    break;

                case "Button6":
                    if (btn_6 != null) btn_6.onClick.Invoke();
                    else Debug.LogWarning("[SerialReader] btn_6 null (rebind necessário).");
                    break;

                case "Button7":
                    if (btn_7 != null) btn_7.onClick.Invoke();
                    else Debug.LogWarning("[SerialReader] btn_7 null (rebind necessário).");
                    break;
            }
        }

        lock (this)
        {
            if (!string.IsNullOrEmpty(receivedData))
            {
                if (receivedData == "UNO_VITALSPIRIT_READY")
                {
                    // opcional
                }
                receivedData = ""; // Clear after processing
            }
        }
    }

    public void OnApplicationQuit()
    {
        ShutdownSerial();
    }

    private void OnDestroy()
    {
        ShutdownSerial();
    }

    private void ShutdownSerial()
    {
        isRunning = false;

        try
        {
            if (serialThread != null && serialThread.IsAlive)
                serialThread.Join(200);
        }
        catch { }

        try
        {
            if (serialPort != null && serialPort.IsOpen)
                serialPort.Close();
        }
        catch { }
    }

    public void SendData(string message)
    {
        try
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.WriteLine(message);
                Debug.Log("Sent: " + message);
            }
            else
            {
                Debug.LogError("[SerialReader] Serial port not open!");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[SerialReader] SendData error: " + e.Message);
        }
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
        // Ajusta às tuas tags por cena (mantive a tua lógica)
        btn_3 = GameObject.FindWithTag("1_main_btn_3")?.GetComponent<Button>();
        btn_6 = GameObject.FindWithTag("1_main_btn_6")?.GetComponent<Button>();

        LEDS();

        Debug.Log($"[SerialReader] Rebind feito. btn_3={(btn_3 != null)} btn_6={(btn_6 != null)}");
    }

    private void LEDS()
    {
        // Proteção: só tenta se a porta estiver mesmo aberta
        if (serialPort == null || !serialPort.IsOpen) return;

        for (int i = 0; i <= 6; i++)
        {
            SendData(i + "O\n"); // Turn OFF
        }
        SendData("3R\n"); // Turn Red
        SendData("6G\n"); // Turn Green
    }
}
