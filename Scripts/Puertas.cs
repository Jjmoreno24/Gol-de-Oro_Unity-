using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : MonoBehaviour
{
    public bool doorOpen = false;
    public float doorOpenAngle = -3.5f;
    public float doorCloseAngle = 90.0f;
    public float smooth = 3.0f;

   /* public AudioClip openDoor;
    public AudioClip closeDoor;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (doorOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorOpen = true;
            PlaySound();
            // Aquí puedes mostrar un mensaje en la UI indicando que la puerta se está abriendo
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorOpen = false;
            PlaySound();
            // Aquí puedes ocultar el mensaje de la UI indicando que la puerta se está cerrando
        }
    }

    private void PlaySound()
    {
        if (doorOpen && openDoor != null)
        {
            audioSource.PlayOneShot(openDoor);
        }
        else if (!doorOpen && closeDoor != null)
        {
            audioSource.PlayOneShot(closeDoor);
        }
    }*/
}
