using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejercicio_2_P9 : MonoBehaviour
{
    public float smoothing_factor = 0.1f; // Ajustamos la suavidad de la interpolación Slerp
    public float acceleration_multiplier = 5.0f; // Ajustamos la velocidad de avance del objeto
    private Quaternion initial_rotation;
    private Vector3 initial_position;

    void Start()
    {
        Input.gyro.enabled = true;
        initial_rotation = Quaternion.Euler(0, 0, 180); // Obtener la orientación inicial y posición del objeto
        initial_position = transform.position;
    }

    void Update()
    {
        Quaternion attitude = Input.gyro.attitude; // Obtenemos la orientación del giroscopio
        Quaternion rotator = attitude * Quaternion.Euler(0f, 0f, 180f) * Quaternion.Euler(90f, 180f, 0f); // Aplicamos las rotaciones necesarias
        transform.rotation = Quaternion.Slerp(transform.rotation, rotator, smoothing_factor);  // Interpola entre la rotación actual y la nueva rotación
       
        Vector3 acceleration = Input.acceleration;  // Guardamos la aceleración del acelerómetro
        Vector3 movement = new Vector3(acceleration.x, acceleration.y, -acceleration.z); // Aplicamos la acelaración e invertimos el eje z
        Vector3 amount_of_movement = movement * acceleration_multiplier * Time.deltaTime;
        transform.Translate(amount_of_movement, Space.Self);  // Mueve el samuari en la dirección orientada

        if (IsOffLimit() == true) { // Se detiene el movimiento si está fuera del rango
            transform.position = initial_position;
        }
    }

    bool IsOffLimit() {
        float minLatitude = 10; // Defininimos los límites
        float maxLatitude = 30;
        float minLongitude = -10;
        float maxLongitude = -30;

        if (Input.location.status == LocationServiceStatus.Running) { // Buscamos la última ubicación conocida
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;
            if (latitude < minLatitude || latitude > maxLatitude || longitude < minLongitude || longitude > maxLongitude)  { return true; } // Vemos si está fuera de límite
        }
        return false;
    }
}
