using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    public Canvas resultsCanvas;
    public Image playerFlag;
    public Image rivalFlag;
    public TMP_Text playerScoreText;
    public TMP_Text rivalScoreText;
    public Sprite[] flagSprites; // Asegúrate de asignar las banderas correspondientes en el inspector
    private int playerGoals;
    private int rivalGoals;

    void Start()
    {
        resultsCanvas.enabled = false; // Asegura que el canvas está desactivado al comenzar
    }

    public void ShowResults(int playerGoals, int rivalGoals)
    {
        this.playerGoals = playerGoals;
        this.rivalGoals = rivalGoals;

        // Actualizar banderas y goles
        playerFlag.sprite = flagSprites[PlayerPrefs.GetInt("PlayerTeamIndex")];
        rivalFlag.sprite = flagSprites[PlayerPrefs.GetInt("RivalTeamIndex")];

        playerScoreText.text = playerGoals.ToString();
        rivalScoreText.text = rivalGoals.ToString();

        // Destacar en amarillo el mayor puntaje
        if (playerGoals > rivalGoals)
        {
            playerScoreText.color = Color.yellow;
            rivalScoreText.color = Color.white;
        }
        else if (rivalGoals > playerGoals)
        {
            rivalScoreText.color = Color.yellow;
            playerScoreText.color = Color.white;
        }
        else
        {
            playerScoreText.color = Color.white;
            rivalScoreText.color = Color.white;
        }

        resultsCanvas.enabled = true;
    }

    public void HideResults()
    {
        resultsCanvas.enabled = false;
    }
}
