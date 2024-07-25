using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetection : MonoBehaviour
{
    public GoalCounter goalCounter; // Referencia al contador de goles

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball")) // Aseg�rate de que la pelota tenga el tag "Ball"
        {
            goalCounter.RecordPlayerAttempt(true); // Notifica que ha sido un gol
        }
    }
}
