using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialReader : MonoBehaviour
{

    SerialPort serialPort = new SerialPort("COM10", 19200, Parity.None, 8, StopBits.One);
    private Thread serialThread;
    private bool isRunning = true;
    private string receivedData = "";
    

    void Start()
    {
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
                        receivedData = data;
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
        lock (this) 
        {
            if (!string.IsNullOrEmpty(receivedData))
            {
                Debug.Log("Received: " + receivedData);
                if (receivedData == "1"){
                    // Testar o envio das cores
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


}
