using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;
    public AudioClip portalSound; // Clip de audio que se reproducirá
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlaySoundAndLoadScene());
        }
    }

    IEnumerator PlaySoundAndLoadScene()
    {
        audioSource.PlayOneShot(portalSound); // Reproduce el sonido
        yield return new WaitForSeconds(portalSound.length); // Espera hasta que el sonido termine
        SceneManager.LoadScene(sceneName); // Cambia la escena
    }
}
