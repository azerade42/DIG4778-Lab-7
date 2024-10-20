using System;

public static class EventManager
{
    public static Action<Target> OnTargetHit;
    public static void TargetHit(Target target) => OnTargetHit?.Invoke(target);

    public static Action<Target> OnTargetMiss;
    public static void TargetMissed(Target target) => OnTargetMiss?.Invoke(target);

    public static Action<int> OnScoreUpdated;
    public static void ScoreUpdated(int currentScore) => OnScoreUpdated?.Invoke(currentScore);
    
    public static Action<int> OnGameTimerElapsed;
    public static void GameTimerElapsed(int currentTime) => OnGameTimerElapsed?.Invoke(currentTime);

    public static Action<int> OnPlayerLivesUpdated;
    public static void PlayerLivesUpdated(int damage) => OnPlayerLivesUpdated?.Invoke(damage);

    public static Action OnPlayerDied;
    public static void PlayerDied() => OnPlayerDied?.Invoke();

    public static Action OnGameEnded;
    public static void GameEnded() => OnGameEnded?.Invoke();

    public static Action OnGameRestarted;
    public static void GameRestarted() => OnGameRestarted?.Invoke();

}
