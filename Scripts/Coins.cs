using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public AudioClip sonidoMoneda; // Sonido al recoger la moneda.
    public AudioSource audioSource; // AudioSource para reproducir sonidos.
    private int valor = 500; // Puntos otorgados por la moneda de oro.

    private void Start()
    {
        // Configura el valor directamente en el script, ya que solo usamos un tipo de moneda.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball")) // Asegúrate de que la pelota tenga la etiqueta "Ball".
        {
            audioSource.PlayOneShot(sonidoMoneda); // Reproducir sonido de moneda.
            ScoreManager.Instance.AddScore(valor); // Añadir puntos al marcador.
            gameObject.SetActive(false); // Desactivar la moneda temporalmente.
            Invoke("ReactivateCoin", 5); // Reactivar la moneda después de 5 segundos.
        }
    }

    void ReactivateCoin()
    {
        gameObject.SetActive(true); // Reactivar la moneda.
    }
}