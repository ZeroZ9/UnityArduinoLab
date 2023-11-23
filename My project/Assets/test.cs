using System.Collections;
using System.IO.Ports;
using UnityEngine;

public class RotateThroughtSerial : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("COM3", 115200); // Replace with your port name
    public string receivedstring;
    public GameObject test_data;

    private GameObject objectToHold;
    
    private bool isHolding = false;

    void Start()
    {
        try
        {
            data_stream.Open();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error opening serial port: " + e.Message);
        }
        
        objectToHold = GameObject.Find("Sphere");
    }

    void Update() 
    {
        if (data_stream.IsOpen)
        {
            try
            {
                receivedstring = data_stream.ReadLine();

                string[] data_array = receivedstring.Split(',');
                if (data_array.Length == 4)
                {   
                    string command = data_array[0];
                    float x = float.Parse(data_array[1]);
                    float y = float.Parse(data_array[2]);
                    float z = float.Parse(data_array[3]);

                    test_data.transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);

                    if (command == "hold" && !isHolding)
                    {
                        isHolding = true;
                        HoldObject();
                    }
                    else if (command == "release" && isHolding)
                    {
                        isHolding = false;
                        ReleaseObject();
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error reading from serial port: " + e.Message);
            }
        }
    }

    void HoldObject()
    {
        objectToHold.transform.SetParent(test_data.transform, false);
    }

    void ReleaseObject()
    {
        objectToHold.transform.SetParent(null, false);
        objectToHold.transform.position = new Vector3(0, 0, 0); // Optionally reset position
    }
}
