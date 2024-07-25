using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CanvasManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject optionsCanvas;
    public GameObject levelsCanvas;

    [Header("Options")]
    public Slider volumeFX;
    public Slider volumeMaster;
    public Toggle mute;
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;

    private float lastVolume;
    private float lastClickVolume;

    void Start()
    {
        // Configura el estado inicial del Canvas basado en PlayerPrefs o usa un valor predeterminado
        string activeCanvas = PlayerPrefs.GetString("ActiveCanvas", "MainMenuCanvas");
        PlayerPrefs.DeleteKey("ActiveCanvas"); // Opcional: limpiar la preferencia después de leerla para que no persista incorrectamente
        ActivateCanvas(activeCanvas);
        //ShowMainMenu(); // Asegúrate de que el MainMenu se muestre al inicio

        // Configurar los listeners de los sliders para que llamen a los métodos de cambio de volumen
        volumeFX.onValueChanged.AddListener(ChangeVolumeFx);
        volumeMaster.onValueChanged.AddListener(ChangeVolumeMaster);
        mute.onValueChanged.AddListener(delegate { SetMute(); });

        // Establecer los valores iniciales de los sliders basados en el AudioMixer
        float currentFxVolume;
        float currentMasterVolume;

        if (mixer.GetFloat("VolFX", out currentFxVolume))
        {
            volumeFX.value = currentFxVolume;
        }

        if (mixer.GetFloat("VolMaster", out currentMasterVolume))
        {
            volumeMaster.value = currentMasterVolume;
        }

        // Configurar límites de los sliders
        volumeFX.minValue = -80f; // Límite mínimo en decibelios
        volumeFX.maxValue = 0f;   // Límite máximo en decibelios
        volumeMaster.minValue = -80f;
        volumeMaster.maxValue = 0f;

        // Aplicar los valores iniciales al AudioMixer
        ChangeVolumeFx(volumeFX.value);
        ChangeVolumeMaster(volumeMaster.value);

        //string activeCanvas = PlayerPrefs.GetString("ActiveCanvas", "MainMenuCanvas");
        //ActivateCanvas(activeCanvas);
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        levelsCanvas.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
        levelsCanvas.SetActive(false);
        PlaySoundButton(); // Reproduce el sonido al abrir el menú de opciones
    }

    public void ShowLevelsMenu()
    {
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        levelsCanvas.SetActive(true);
        PlaySoundButton(); // Reproduce el sonido al abrir el menú de niveles
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame called");
        PlaySoundButton(); // Reproduce el sonido al salir del juego
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetMute()
    {
        if (mute.isOn)
        {
            mixer.GetFloat("VolMaster", out lastVolume);  // Guardar el volumen actual
            mixer.SetFloat("VolMaster", -80);  // Silenciar música de fondo

            mixer.GetFloat("VolFX", out float lastClickVolume);
            mixer.SetFloat("VolFX", -80);  // Silenciar sonidos de clic
        }
        else
        {
            mixer.SetFloat("VolMaster", lastVolume);  // Restaurar volumen de música de fondo
            mixer.SetFloat("VolFX", lastClickVolume);  // Restaurar volumen de sonidos de clic
        }
    }


    public void ChangeVolumeMaster(float v)
    {
        if (v > volumeMaster.maxValue) v = volumeMaster.maxValue;
        if (v < volumeMaster.minValue) v = volumeMaster.minValue;

        mixer.SetFloat("VolMaster", v); // Ajusta este nombre para que coincida con el parámetro del AudioMixer
    }

    public void ChangeVolumeFx(float v)
    {
        if (v > volumeFX.maxValue) v = volumeFX.maxValue;
        if (v < volumeFX.minValue) v = volumeFX.minValue;

        mixer.SetFloat("VolFX", v); // Ajusta este nombre para que coincida con el parámetro del AudioMixer
    }


    public void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Team Selection -Nivel1"); // Asegúrate de que el nombre de la escena es correcto
    }
    
    public void ActivateCanvas(string canvasName)
    {
        mainMenuCanvas.SetActive(canvasName == "MainMenuCanvas");
        optionsCanvas.SetActive(canvasName == "OptionsCanvas");
        levelsCanvas.SetActive(canvasName == "LevelsCanvas");
    }
    public void LoadPenaltyLevel()
    {
        SceneManager.LoadScene("Penalty - Nivel 2"); // Cambia "PenaltyLevelSceneName" por el nombre real de tu escena de penalty
    }

    public void LoadNewLevel()
    {
        SceneManager.LoadScene("Penalty Barrera N3"); // Cambia "NombreDeTuNuevaEscena" por el nombre real de tu escena
    }


}
