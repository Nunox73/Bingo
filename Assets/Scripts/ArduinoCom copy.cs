using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class ArduinoCom2 : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("COM10", 19200);

     string value; // Serial value

    // Start is called before the first frame update
    void Start()
    {
        data_stream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        value = data_stream.ReadLine();

        Debug.Log(value);
    }
}
