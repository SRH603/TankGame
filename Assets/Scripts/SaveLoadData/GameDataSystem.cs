using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Contains the data to be saved and loaded.
/// Currently, it only includes level progress.
/// </summary>
[Serializable]
public class GameData
{
    public GameData(string name)
    {
        this.name = name;
        levelProgressList = new List<LevelProgress>();
    }
    public string name;
    public List<LevelProgress> levelProgressList;
}


/// <summary>
/// Records level progress. Currently, it only tracks whether the level is cleared.
/// </summary>
[Serializable]
public class LevelProgress
{
    public LevelProgress(string levelName)
    {
        this.levelName = levelName;
        this.isCleared = false;
    }
    public bool isCleared;
    public string levelName;
}

/// <summary>
/// Defines the level information. The level name and thumbnail can be used for display in the menu.
/// </summary>
[Serializable]
public class LevelInfo
{
    public string sceneName;
    public string levelName;
    public Sprite thumbnail;
}

/// <summary>
/// Handles saving and loading of GameData
/// </summary>
public class GameDataSystem
{
    ISerializer serializer;
    string dataPath;
    string fileExtension;

    /// <summary>
    /// Constructs the data system using the given serializer and file extension.
    /// </summary>
    /// <param name="serializer"></param>
    /// <param name="fileExtension"></param>
    public GameDataSystem(ISerializer serializer, string fileExtension)
    {
        this.serializer = serializer;
        dataPath = Application.persistentDataPath;
        this.fileExtension = fileExtension;
    }

    /// <summary>
    /// Combine the data path using the file name
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private string GetDataPath(string fileName)
    {
        return Path.Combine(dataPath, string.Concat(fileName, ".", fileExtension));
    }

    /// <summary>
    /// Saves the game data. Currently, it does not check whether the data should be overwritten.
    /// </summary>
    /// <param name="gameData"></param>
    public void Save(GameData gameData)
    {
        string path = GetDataPath(gameData.name);
        File.WriteAllText(path, serializer.SerializeData<GameData>(gameData));
    }

    /// <summary>
    /// Load the data from file. If not found, it will create a new one and returns it.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public GameData Load(string fileName)
    {
        string path = GetDataPath(fileName);
        if (File.Exists(path))
        {
            return serializer.DeserializeData<GameData>(File.ReadAllText(path));
        }
        else
        {
            Debug.Log($"File not found. Generate a new one");
            GameData gameData = new GameData(fileName);
            foreach (var levelInfo in GameManager.instance.levelInfos)
            {
                gameData.levelProgressList.Add(new LevelProgress(levelInfo.levelName));
            }
            return new GameData(fileName);
        }
        return null;
    }

    /// <summary>
    /// Delete saved file.
    /// </summary>
    /// <param name="fileName"></param>
    public void Delete(string fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(GetDataPath(fileName));
        }
    }
}