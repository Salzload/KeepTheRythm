using System;

public static class GameEvents
{
    public static Action<float> OnLoveAdded;
    public static Action<float> OnLoveRemoved;
    public static Action<float> OnLoveChangedNormalized;
    public static Action<float> OnSpeedMultiplierChanged;

    public static Action<string> OnPowerupCollected;

    public static Action<string> OnPlaySFX;
    public static Action<string> OnReactionTriggered;

    public static Action<int> OnRoundStarted;
    public static Action OnGameOver;
    public static Action OnVictory;
}