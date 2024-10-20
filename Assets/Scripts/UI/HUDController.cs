using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI timeText;
    
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void OnEnable()
    {
        EventManager.OnPlayerLivesUpdated += UpdateLivesText;
        EventManager.OnScoreUpdated += UpdateScoreText;
        EventManager.OnGameTimerElapsed += UpdateTimeText;
        EventManager.OnGameEnded += ToggleGameOverText;
        EventManager.OnGameRestarted += ToggleGameOverText;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerLivesUpdated -= UpdateLivesText;
        EventManager.OnScoreUpdated -= UpdateScoreText;
        EventManager.OnGameTimerElapsed -= UpdateTimeText;
        EventManager.OnGameEnded -= ToggleGameOverText;
        EventManager.OnGameRestarted -= ToggleGameOverText;
    }

    private void UpdateLivesText(int lives) => livesText.text = $"Lives: {lives}";
    private void UpdateScoreText(int score) => scoreText.text = $"Score: {score}";
    private void UpdateTimeText(int time) => timeText.text = time.ToString();
    private void ToggleGameOverText() => gameOverText.gameObject.SetActive(!gameOverText.gameObject.activeSelf);
}
