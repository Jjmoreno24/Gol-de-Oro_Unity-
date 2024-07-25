using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Asegúrate de que esta línea esté incluida para acceder a 'Image'
using UnityEngine.Audio;
using TMPro;  // Añade esta línea para usar TextMesh Pro
using UnityEngine.SceneManagement;  // Asegúrate de incluir esto para cargar escenas

[System.Serializable]  // Esto permite que la clase se muestre en el inspector de Unity
public class Team
{
    public string teamName;
    public Sprite teamSprite;
    public Color nameColor;  // Añade un color para el nombre del equipo
}

public class TeamSelection : MonoBehaviour
{
    public Image playerTeamImage;
    public Image rivalTeamImage;
    public TMP_Text playerTeamName;   // Cambia Text a TMP_Text
    public TMP_Text rivalTeamName;    // Cambia Text a TMP_Text
    public List<Team> teams; // Lista de equipos

    public AudioSource backgroundMusic; // AudioSource para la música de fondo
    public AudioSource buttonClickSound; // AudioSource para los sonidos de clic de los botones
    public AudioSource readyButtonSound; // AudioSource para el sonido del botón "Listo"

    private int currentPlayerTeamIndex = 0;
    private int currentRivalTeamIndex = 0;

    void Start()
    {
        UpdateTeamDisplays();  // Actualiza la visualización inicial de los equipos
        backgroundMusic.Play(); // Inicia la reproducción de la música de fondo
    }

    public void NextPlayerTeam()
    {
        currentPlayerTeamIndex = (currentPlayerTeamIndex + 1) % teams.Count;
        UpdateTeamDisplays();
        buttonClickSound.Play(); // Reproduce el sonido al cambiar de equipo
    }

    public void PreviousPlayerTeam()
    {
        if (currentPlayerTeamIndex == 0)
            currentPlayerTeamIndex = teams.Count - 1;
        else
            currentPlayerTeamIndex--;
        UpdateTeamDisplays();
        buttonClickSound.Play(); // Reproduce el sonido al cambiar de equipo
    }

    public void NextRivalTeam()
    {
        currentRivalTeamIndex = (currentRivalTeamIndex + 1) % teams.Count;
        UpdateTeamDisplays();
        buttonClickSound.Play(); // Reproduce el sonido al cambiar de equipo
    }

    public void PreviousRivalTeam()
    {
        if (currentRivalTeamIndex == 0)
            currentRivalTeamIndex = teams.Count - 1;
        else
            currentRivalTeamIndex--;
        UpdateTeamDisplays();
        buttonClickSound.Play(); // Reproduce el sonido al cambiar de equipo
    }

    private void UpdateTeamDisplays()
    {
        // Actualizar la imagen y el nombre para el jugador
        playerTeamImage.sprite = teams[currentPlayerTeamIndex].teamSprite;
        playerTeamName.text = teams[currentPlayerTeamIndex].teamName;
        //playerTeamName.color = teams[currentPlayerTeamIndex].nameColor;  // Aplicar color

        // Actualizar la imagen y el nombre para el rival
        rivalTeamImage.sprite = teams[currentRivalTeamIndex].teamSprite;
        rivalTeamName.text = teams[currentRivalTeamIndex].teamName;
        //rivalTeamName.color = teams[currentRivalTeamIndex].nameColor;  // Aplicar color
    }

    public void SaveTeamSelections()
    {
        PlayerPrefs.SetInt("PlayerTeamIndex", currentPlayerTeamIndex);
        PlayerPrefs.SetString("PlayerTeamName", teams[currentPlayerTeamIndex].teamName);
        PlayerPrefs.SetInt("RivalTeamIndex", currentRivalTeamIndex);
        PlayerPrefs.SetString("RivalTeamName", teams[currentRivalTeamIndex].teamName);
        PlayerPrefs.Save();  // Asegúrate de guardar las PlayerPrefs
        Debug.Log("Saving Player Team Index: " + currentPlayerTeamIndex + ", Name: " + teams[currentPlayerTeamIndex].teamName);
        Debug.Log("Saving Rival Team Index: " + currentRivalTeamIndex + ", Name: " + teams[currentRivalTeamIndex].teamName);
    }


    public void LoadTeamSelections()
    {
        // Carga las selecciones previas si existen
        currentPlayerTeamIndex = PlayerPrefs.GetInt("PlayerTeamIndex", currentPlayerTeamIndex);
        currentRivalTeamIndex = PlayerPrefs.GetInt("RivalTeamIndex", currentRivalTeamIndex);
    }

    public void OnReadyButtonPressed()
    {
        SaveTeamSelections();  // Guarda las selecciones de equipo
        PlayerPrefs.SetString("ActiveCanvas", "LevelsCanvas");
        PlayerPrefs.Save();
        //SceneManager.LoadScene("SampleScene");  // Carga la escena del menú principal
        readyButtonSound.Play(); // Reproduce el sonido específico para el botón "Listo"
        StartCoroutine(WaitAndLoadScene()); // Espera antes de cargar la siguiente escena
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(1.5f); // Espera 1.5 segundos para permitir que el sonido se reproduzca completamente
        SceneManager.LoadScene("SampleScene");  // Carga la escena del menú principal
    }
}


