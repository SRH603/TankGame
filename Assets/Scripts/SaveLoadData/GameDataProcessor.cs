using System.Collections.Generic;

/// <summary>
/// Helper class to process the GameData. Mainly checking if the level has been cleared or not.
/// </summary>
public static class GameDataProcessor
{
    private static List<LevelProgress> levelProgressList;
    private static Dictionary<string, bool> levelProgressLookup;

    /// <summary>
    /// Update dictionary whenever the progress has been updated.
    /// </summary>
    /// <param name="list"></param>
    public static void UpdateLevelProgress(List<LevelProgress> list)
    {
        levelProgressList = list;
        levelProgressLookup = new Dictionary<string, bool>();
        foreach (LevelProgress progress in levelProgressList)
        {
            levelProgressLookup[progress.levelName] = progress.isCleared;
        }
    }
    public static bool IsLevelCleared(string levelName)
    {
        return levelProgressLookup[levelName];
    }

    /// <summary>
    /// Check if this game data has any progress. (some level has been cleared)
    /// </summary>
    /// <returns></returns>
    public static bool HasProgress()
    {
        bool hasProgress = false;
        foreach (LevelProgress progress in levelProgressList)
        {
            if (progress.isCleared)
            {
                hasProgress = true;
                break;
            }
        }
        return hasProgress;
    }
}