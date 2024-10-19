using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        player.OnPlayerDied += EndGame;
    }

    private void EndGame()
    {
        player.OnPlayerDied -= EndGame;
    }
}
