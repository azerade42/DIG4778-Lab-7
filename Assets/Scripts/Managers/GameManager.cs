
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int TimeRemaining { get; private set; }
    [SerializeField] private int startingTime;

    private void OnEnable()
    {
        EventManager.OnPlayerDied += EndGame;
        EventManager.OnGameRestarted += StartGame;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDied -= EndGame;
        EventManager.OnGameRestarted -= StartGame;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        TimeRemaining = startingTime;
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        EventManager.GameTimerElapsed(TimeRemaining);

        while (TimeRemaining-- > 0)
        {
            yield return new WaitForSeconds(1);
            EventManager.GameTimerElapsed(TimeRemaining);
        }
        EndGame();
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        EventManager.GameEnded();
    }
}
