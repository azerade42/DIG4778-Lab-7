using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int CurrentScore { get; private set; }

    private void OnEnable()
    {
        EventManager.OnTargetHit += AddTargetToScore;
        EventManager.OnPlayerDied += ResetScore;
    }

    private void OnDisable()
    {
        EventManager.OnTargetHit -= AddTargetToScore;
        EventManager.OnPlayerDied -= ResetScore;
    }

    private void AddTargetToScore(Target target)
    {
        CurrentScore += target.PointValue;
    }

    private void ResetScore() => CurrentScore = 0;

}
