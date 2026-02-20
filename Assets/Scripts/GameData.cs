using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameData
{
    public static bool endIfPLayer2Slow = false;
    public static ItPlayer itPlayer = ItPlayer.Player1;
    public static float firstPlayerTime = 0f;
    public static float secondPlayerTime = 0f;
    public static int roundsLeft = 2;
    public static Platforms selectedPlatform = Platforms.PC;

    public static ItPlayer winner;

    public static event System.Action OnItPlayerChanged;


    public static void TriggerItPlayerChanged()
    {
        OnItPlayerChanged?.Invoke();
    }

    public static void ResetGameData()
    {
        itPlayer = ItPlayer.Player1;
        firstPlayerTime = 0f;
        secondPlayerTime = 0f;
        roundsLeft = 2;
        winner = ItPlayer.None;
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