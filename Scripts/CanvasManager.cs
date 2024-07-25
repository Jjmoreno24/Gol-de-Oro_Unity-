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
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
        PlaySoundButton(); // Reproduce el sonido al abrir el menú de opciones
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
            // Guardar el volumen actual
            mixer.GetFloat("VolMaster", out lastVolume);
            mixer.GetFloat("VolFX", out lastClickVolume);

            // Silenciar
            mixer.SetFloat("VolMaster", -80);
            mixer.SetFloat("VolFX", -80);
        }
        else
        {
            // Restaurar el volumen
            mixer.SetFloat("VolMaster", lastVolume);
            mixer.SetFloat("VolFX", lastClickVolume);
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

    public void ActivateCanvas(string canvasName)
    {
        mainMenuCanvas.SetActive(canvasName == "MainMenuCanvas");
        optionsCanvas.SetActive(canvasName == "OptionsCanvas");
    }

    public void PlayGame()
    {
        PlaySoundButton(); // Reproduce el sonido al presionar el botón de jugar
        SceneManager.LoadScene("SeleccionNivel"); // Cambia a la escena "SeleccionNivel"
    }
}
