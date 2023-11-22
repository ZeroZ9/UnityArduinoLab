using System.Collections;
using System.IO.Ports;
using UnityEngine;

public class SerialRotation : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("COM3", 115200);
    public string receivedstring;
    public gameObject test_data;
    public Rigibody rb;
    public float SerialRotation = 0.01f;

    void Start()
    {
        data_stream.Open();
    }

    void Update() 
    {
        receivedstring = data_stream.ReadLine();

        string[] data_array = receivedstring.Split(',');
        if (data_array.Length == 3)
        {   
            float x = float.Parse(data_array[0]);
            float y = float.Parse(data_array[1]);
            float z = float.Parse(data_array[2]);

            test_data.transform.rotate(new Vector3(x,y,z) * Time.deltaTime);
        }

    }

    

}
