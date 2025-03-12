using System;
using System.IO.Ports;
using UnityEngine;

public class ArduinoCom : MonoBehaviour
{

    SerialPort serialPort = new SerialPort("COM10", 19600, Parity.None, 8, StopBits.One);
    void Start()
    {
        
        try
        {
            serialPort.Open(); // Open the serial port
            Console.WriteLine("Serial port opened successfully. (Console)");
            Debug.Log("Serial port opened successfully. (Debug)");
        }
        catch (Exception ex)
        {
            Debug.Log("Error opening serial port: " + ex.Message);
        }

        serialPort.Close(); // Close when done
    }

    void ReadSerial()
    {
         try
        {
            serialPort.Open();
            Debug.Log("Waiting for data...");

            string receivedData = serialPort.ReadLine(); // Reads until newline (`\n`)
            Debug.Log("Received: " + receivedData);
        }
        catch (Exception ex)
        {
            Debug.Log("Error: " + ex.Message);
        }
        finally
        {
            serialPort.Close();
        }
    }
}
