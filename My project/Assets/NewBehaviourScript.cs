using System.IO.Ports;
using System.Collections;
using UnityEngine;

public class MPU6050Controller : MonoBehaviour
{
    Vector3 position;
    Vector3 rotation;
    public Vector3 rotationOffset;
    SerialPort serialPort;
    public GameObject Cube; // Assign the GameObject you want to rotate in the Inspector
    public float speedFactor = 15.0f;

    void Start()
    {
        Debug.Log("This is a debug message");
        // Adjust the port name and baud rate to match your Arduino setup
        serialPort = new SerialPort("/dev/cu.usbmodem123456781", 115200);
        serialPort.Open();
    }

    void Update()
    {   
        Debug.Log("This is a debug message");
        if (serialPort.IsOpen)
        {
            try
            {
                string dataString = serialPort.ReadLine();
                if (!string.IsNullOrEmpty(dataString))
                {
                    print(dataString);
                    // Split the string and parse each component
                    string[] splitData = dataString.Split('/');
                    if (splitData.Length == 5 && splitData[0] == "r")
                    {
                        float w = float.Parse(splitData[1]);
                        float x = float.Parse(splitData[2]);
                        float y = float.Parse(splitData[3]);
                        float z = float.Parse(splitData[4]);

                        // Apply the quaternion to the GameObject
                        Cube.transform.localRotation = Quaternion.Lerp(Cube.transform.localRotation, new Quaternion(w, y, x, z), Time.deltaTime * speedFactor);
                    }
                    Cube.transform.parent.transform.eulerAngles = rotationOffset;
                }
            }
            catch (System.Exception)
            {
                // Handle exceptions or ignore
            }
        }
    }
}
