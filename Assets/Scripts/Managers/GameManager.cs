
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnPlayerDied += RestartGame;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDied -= RestartGame;
    }

    private void RestartGame()
    {
        EventManager.GameRestarted();
    }
}
