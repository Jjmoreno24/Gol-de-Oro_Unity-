using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class BarrierCollision : MonoBehaviour
{
    public GameObject floatingTextPrefab; // Asigna este en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball")) // Asegúrate de que sea la pelota
        {
            // Descuento de puntos
            ScoreManager.Instance.AddScore(-150); // Asume que tienes un ScoreManager que maneja el score
            // Mostrar efecto visual o mensaje
            ShowFloatingText(-150, other.transform.position);
        }
    }

    void ShowFloatingText(int scoreAmount, Vector3 position)
    {
        if (floatingTextPrefab)
        {
            GameObject textGo = Instantiate(floatingTextPrefab, position, Quaternion.identity);
            textGo.GetComponent<TextMesh>().text = scoreAmount.ToString(); // Asume que usas TextMesh, cambia si usas TextMeshPro

            Destroy(textGo, 0.8f); // Destruye el texto después de 0.8 segundos
        }
    }
}*/


public class BarrierCollision : MonoBehaviour
{
    public int puntosPenalizacion = 150;  // Puntos a descontar
    public GameObject floatingTextPrefab; // Prefab del texto flotante

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) // Asegúrate de que sea la pelota
        {
            // Muestra el texto flotante
            ShowFloatingText();
            // Llama al método para descontar puntos
            ScoreManager.Instance.AddScore(-puntosPenalizacion);
        }
    }

    void ShowFloatingText()
    {
        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = "-" + puntosPenalizacion.ToString();
        Destroy(go, 0.8f); // Destruye el texto flotante después de 0.8 segundos
    }
}