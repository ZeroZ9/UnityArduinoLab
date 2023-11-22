using System.Collections;
using System.IO.Ports;
using UnityEngine;

public class SerialRotation : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("COM3", 115200);
    public string receivedstring;
    public gameObject test_data;

    private bool isHolding = false;

    void Start()
    {
        data_stream.Open();
    }

    void Update() 
    {
        receivedstring = data_stream.ReadLine();

        string[] data_array = receivedstring.Split(',');
        if (data_array.Length == 4)
        {   
            string command = data_array[0];
            float x = float.Parse(data_array[1]);
            float y = float.Parse(data_array[2]);
            float z = float.Parse(data_array[3]);

            test_data.transform.rotate(new Vector3(x,y,z) * Time.deltaTime);

            if (command == "hold" && !isHolding)
            {
                isHolding = true;
                // TODO: test_data will hold another object
            }
            else if (command == "release" && isHolding)
            {
                isHolding = false;
                // TODO: test_data will release the object

            }
        }

    }

    

}
