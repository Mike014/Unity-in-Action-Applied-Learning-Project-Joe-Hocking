using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    
    [Header("Pitch → X, Yaw → Y, Roll → Z")]
    [SerializeField]
    public float pitch;
    public float yaw; // quanti gradi per frame
    public float roll;

    void Update()
    {
        transform.Rotate(pitch, yaw, roll, Space.Self); // ruota l’oggetto
        // transform.Rotate(x, y, z);
        // Pitch → asse X(su/ giù)
        // Yaw → asse Y(destra/ sinistra)
        // Roll → asse Z(inclinazione laterale)
        // Space.Self Rotate Locale
        // Space.World Rotate Global
        // Space.Self → ruota seguendo se stesso
        // Space.World → ruota seguendo il mondo
    }

}
