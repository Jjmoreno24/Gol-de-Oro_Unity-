using System.Collections;
using UnityEngine;
using TMPro;

public class GoalCounter : MonoBehaviour
{
    public TMP_Text playerGoalText; // Texto para goles del jugador
    public TMP_Text rivalGoalText;  // Texto para goles del rival
    public Canvas rivalCanvas;      // Canvas que muestra el tiro del rival
    private int playerGoals = 0;
    private int rivalGoals = 0;
    private int attempts = 0;       // Contador de intentos del jugador
    public int maxAttempts = 5;     // Máximo de intentos permitidos

    void Start()
    {
        UpdateGoalText();
        rivalCanvas.enabled = false; // Asegúrate de que el canvas del rival esté desactivado al inicio
    }

    public void RecordPlayerAttempt(bool isGoal)
    {
        if (attempts < maxAttempts)
        {
            attempts++;
            if (isGoal)
            {
                playerGoals++;
                ScoreManager.Instance.AddScore(100); // Añade puntos por gol
            }
            UpdateGoalText();
            StartCoroutine(ProcessRivalAttempt()); // Inicia el proceso para mostrar el tiro del rival
        }
        else
        {
            EndGame();
        }
    }

    IEnumerator ProcessRivalAttempt()
    {
        yield return new WaitForSeconds(2); // Espera 2 segundos para simular el tiempo antes del tiro del rival

        rivalCanvas.enabled = true; // Muestra el canvas del rival

        // Simula el resultado del tiro del rival de forma aleatoria
        yield return new WaitForSeconds(1); // Espera 1 segundo para mostrar el resultado del rival

        bool rivalScores = Random.value > 0.5f; // 50% de probabilidad de que el rival anote
        if (rivalScores)
        {
            rivalGoals++;
        }
        UpdateGoalText();

        yield return new WaitForSeconds(1); // Espera otro segundo antes de ocultar el canvas del rival

        rivalCanvas.enabled = false; // Oculta el canvas del rival

        if (attempts >= maxAttempts)
        {
            EndGame();
        }
    }

    private void UpdateGoalText()
    {
        playerGoalText.text = "" + playerGoals.ToString();
        rivalGoalText.text = "" + rivalGoals.ToString();
    }

    private void EndGame()
    {
        Debug.Log("Game Over: Todos los intentos han sido completados.");
        ResultsManager resultsManager = FindObjectOfType<ResultsManager>();
        if (resultsManager != null)
        {
            resultsManager.ShowResults(playerGoals, rivalGoals);
        }
    }

}
