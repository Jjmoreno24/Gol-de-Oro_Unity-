using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Asegúrate de incluir esto

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Arrastra el PauseMenuCanvas aquí desde el inspector

    private bool isPaused = false;

    void Awake()
    {
        // Asegúrate de que este objeto no se destruya al cargar una nueva escena
        DontDestroyOnLoad(gameObject);

        // Asegúrate de que solo haya una instancia de este objeto
        if (FindObjectsOfType<PauseMenu>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Asegurarse de que el menú de pausa esté desactivado al inicio
        pauseMenuUI.SetActive(false);
        DontDestroyOnLoad(pauseMenuUI);

        // Suscribirse al evento de carga de escena
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Asegurarse de que el cursor esté bloqueado y oculto al inicio
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Reanudar el juego
        Cursor.lockState = CursorLockMode.Locked; // Bloquear y ocultar el cursor
        Cursor.visible = false;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
        Cursor.lockState = CursorLockMode.None; // Desbloquear y mostrar el cursor
        Cursor.visible = true;
        isPaused = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Reanudar el juego antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recargar la escena actual
    }

    public void LoadSampleScene()
    {
        Time.timeScale = 1f; // Reanudar el juego antes de cargar la escena de selección
        SceneManager.LoadScene("SeleccionNivel"); // Cambia "SeleccionNivel" al nombre de tu escena de selección
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadCreditsScene()
    {
        Time.timeScale = 1f; // Reanudar el juego antes de cargar la escena de créditos
        SceneManager.LoadScene("Creditos"); // Cambia "Creditos" al nombre de tu escena de créditos
        StartCoroutine(ExitGameAfterDelay(8.5f)); // Esperar 15 segundos antes de salir del juego
    }

    private IEnumerator ExitGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
        // Esto es adicional para asegurarse de que el juego se cierre por completo en el editor de Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Método para manejar la desactivación del Canvas al cargar una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Desactivar el menú de pausa al cargar una nueva escena
        pauseMenuUI.SetActive(false);

        // Asegurarse de que el tiempo esté corriendo normalmente y el cursor esté bloqueado y oculto
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDestroy()
    {
        // Desuscribirse del evento de carga de escena para evitar errores si este objeto es destruido
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
