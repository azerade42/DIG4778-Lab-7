using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, ISaveable
{
    public int CurrentScore { get; private set; }

    private void OnEnable()
    {
        EventManager.OnTargetHit += AddTargetToScore;
        EventManager.OnGameRestarted += ResetScore;

        SaveManager.Instance.AddToSavedBehaviors(this);
    }

    private void OnDisable()
    {
        EventManager.OnTargetHit -= AddTargetToScore;
        EventManager.OnGameRestarted -= ResetScore;
    }

    public void SaveData()
    {
        SaveManager.Instance.SaveData.Score = CurrentScore;
    }

    public void LoadData()
    {
        CurrentScore = SaveManager.Instance.SaveData.Score;
        EventManager.ScoreUpdated(CurrentScore);
    }

    private void AddTargetToScore(Target target)
    {
        CurrentScore += target.PointValue;
        EventManager.ScoreUpdated(CurrentScore);
    }

    private void ResetScore()
    {
        CurrentScore = 0;
        EventManager.ScoreUpdated(CurrentScore);
    }
}