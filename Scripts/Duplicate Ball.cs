using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DuplicateBall : MonoBehaviour
{
    private Vector3 SpawnPos;
    public GameObject spawnObject;
    public TimerController timerController;
    private float newSpawnDuration = 1f;
    private int spawnCount = 0;  // Contador para llevar la cuenta de las duplicaciones
    private int maxSpawns = 4;  // Número máximo de duplicaciones permitidas

    #region Singleton

    public static DuplicateBall Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        SpawnPos = transform.position;
        timerController.StartTimer();
    }

    void SpawnNewObject()
    {
        if (spawnCount < maxSpawns)  // Solo permite duplicar si aún no se ha alcanzado el máximo
        {
            Instantiate(spawnObject, SpawnPos, Quaternion.identity);
            spawnCount++;  // Incrementa el contador de duplicaciones
            if (spawnCount == maxSpawns)
            {
                timerController.StopTimer();  // Detiene el cronómetro cuando se alcanza el último lanzamiento
            }
        }
    }

    public void NewSpawnRequest()
    {
        if (spawnCount < maxSpawns)
        {
            Invoke("SpawnNewObject", newSpawnDuration);
        }
    }
}
