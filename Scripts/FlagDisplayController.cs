using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlagDisplayController : MonoBehaviour
{
    public Image playerFlag;
    public Image rivalFlag;
    public Sprite[] flagSprites;  // Asegúrate de asignar estos en Unity
    public TMP_Text playerTeamNameText;   // Texto del nombre del equipo del jugador
    public TMP_Text rivalTeamNameText;    // Texto del nombre del equipo del rival
    public string[] teamNames;            // Array de nombres de equipos

    void Start()
    {
        LoadFlags();
    }

    void LoadFlags()
    {
        int playerIndex = PlayerPrefs.GetInt("PlayerTeamIndex", 0); // 0 es el valor predeterminado si no se encuentra nada
        int rivalIndex = PlayerPrefs.GetInt("RivalTeamIndex", 0);
        string playerName = PlayerPrefs.GetString("PlayerTeamName", "Default Player Name");
        string rivalName = PlayerPrefs.GetString("RivalTeamName", "Default Rival Name");

        if (playerIndex >= 0 && playerIndex < flagSprites.Length && rivalIndex >= 0 && rivalIndex < flagSprites.Length)
        {
            playerFlag.sprite = flagSprites[playerIndex];
            playerTeamNameText.text = playerName;


            rivalFlag.sprite = flagSprites[rivalIndex];
            rivalTeamNameText.text = rivalName;
        }
        else
        {
            Debug.LogError("Index out of range. Player Index: " + playerIndex + ", Rival Index: " + rivalIndex);
        }
    }

}
