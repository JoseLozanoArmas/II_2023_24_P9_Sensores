using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ejercicio1_show_information_P9 : MonoBehaviour
{
    public Text information_device;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        if (Input.location.isEnabledByUser) {
            Input.location.Start();
        } else {
            Debug.Log("La ubicación no está habilitada por el usuario.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 angularSpeed = Input.gyro.rotationRateUnbiased;
        Vector3 acceleration = Input.acceleration;
        string aux_string = "Velocidad Angular: X: " + angularSpeed.x.ToString("F2") + " Y: " + angularSpeed.y.ToString("F2") + " Z: " + angularSpeed.z.ToString("F2")
        + "\nAceleración: X: " + acceleration.x.ToString("F2") + " Y: " + acceleration.y.ToString("F2") + " Z: " + acceleration.z.ToString("F2");
        if (Input.location.status == LocationServiceStatus.Running) {
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;
            float altitude = Input.location.lastData.altitude;
            aux_string += "\nLatitud: " + latitude.ToString("F6") + "\nLongitud: " + longitude.ToString("F6") + "\nAltitud: " + altitude.ToString("F2");
        } else  {
            Debug.LogWarning("La ubicación no está activa.");
        }
        Vector3 gravity = Physics.gravity;
        aux_string += "\nGravedad: X: " + gravity.x.ToString("F2") + " Y: " + gravity.y.ToString("F2") + " Z: " + gravity.z.ToString("F2");
        information_device.text = aux_string;
    }
}
