using UnityEngine;

public static class GameData
{
    public static ItPlayer itPlayer = ItPlayer.Player1;
    public static float firstPlayerTime = 0f;
    public static float secondPlayerTime = 0f;
    public static int roundsLeft = 1;
    public static Platforms selectedPlatform = Platforms.PC;

    public static ItPlayer winner;

    public static event System.Action OnItPlayerChanged;


    public static void TriggerItPlayerChanged()
    {
        OnItPlayerChanged?.Invoke();
    }
}

public enum ItPlayer
{
    Player1,
    Player2,
    None,
}

public enum Platforms
{
    PC,
    Mobile
}