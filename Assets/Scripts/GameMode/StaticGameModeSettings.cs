using UnityEngine;

public static class StaticGameModeSettings
{
    public static GameModeScriptableObject GameMode { get; private set; }
    public static int MapIndex { get; private set; }
    public static string MapName { get; private set; }

    public static void SetUp(GameModeScriptableObject gameMode, int index)
    {
        GameMode = gameMode;
        MapIndex = index;
        MapName = GameMode.Maps()[MapIndex];
    }

    public static void SetGameMode(GameModeScriptableObject gameMode)
    {
        GameMode = gameMode;
    }

    public static void SetMapIndex(int index)
    {
        MapIndex = index;
        MapName = GameMode.Maps()[MapIndex];
    }

    public static void Clear()
    {
        GameMode = null;
        MapIndex = 0;
        MapName = string.Empty;
    }
}