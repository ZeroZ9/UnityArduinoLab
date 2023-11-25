using System.Collections;
using System.IO.Ports;
using UnityEngine;

public class RotateThroughSerial : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("/dev/cu.usbmodem123456781", 115200); // Replace with your port name
    public string receivedstring;
    public GameObject arm;

    private GameObject objectToHold;
    private bool isHolding = false;
    private bool isPointingToSphere = false; // New variable to track whether the arm is pointing to the sphere

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

                    arm.transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);

                    if (command == "hold" && !isHolding && isPointingToSphere)
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

        MoveArmWithKeyboard();
    } 


    // detect the arm is touching to the cube
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objectToHold)
        {
            isPointingToSphere = true;
        }
    }

    // detect the arm is not touching to the cube
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToHold)
        {
            isPointingToSphere = false;
        }
    }

    // Move the arm around with the cube
    void HoldObject()
    {
        objectToHold.transform.SetParent(arm.transform, false);
        objectToHold.transform.localPosition = Vector3.zero; // Adjust if necessary
        objectToHold.transform.localRotation = Quaternion.identity; // Adjust if necessary
    }

    // release the cube
    void ReleaseObject()
    {
        objectToHold.transform.SetParent(null, false);
        // Optionally, you can reset the position and rotation to some default state
        objectToHold.transform.position = new Vector3(0, 0, 0);
        objectToHold.transform.rotation = Quaternion.identity;
    }

    void MoveArmWithKeyboard()
    {
        float moveSpeed = 5.0f; // Speed of the movement, adjust as needed
        float rotationSpeed = 100.0f; // Speed of the rotation, adjust as needed

        // Move the arm forward and backward with W and S
        if (Input.GetKey(KeyCode.W))
        {
            arm.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            arm.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        // Move the arm left and right with A and D
        if (Input.GetKey(KeyCode.A))
        {
            arm.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            arm.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        // Rotate the arm with Q and E
        if (Input.GetKey(KeyCode.Q))
        {
            arm.transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            arm.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
