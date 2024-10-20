using System;

public static class EventManager
{
    public static Action<Target> OnTargetHit;
    public static void TargetDestroyed(Target target) => OnTargetHit?.Invoke(target);

    public static Action OnPlayerDied;
    public static void PlayerDied() => OnPlayerDied?.Invoke();

    public static Action OnGameRestarted;
    public static void GameRestarted() => OnGameRestarted?.Invoke();

}
