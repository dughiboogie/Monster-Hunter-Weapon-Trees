using System.IO;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is responsible for reading and writing data in the file system
 */
public class FileDataManager : MonoBehaviour
{
    public static FileDataManager instance;

    private List<Game> gamesData = new List<Game>();

    private void Awake()
    {
        if(instance != null) {
            Debug.LogWarning("Multiple instances of FileDataManager found!");
            return;
        }
        instance = this;

        GetGamesDataFromFolder();
    }

    public List<Game> GetGamesDataFromFolder()
    {
        gamesData.Clear();

        foreach(string file in Directory.GetFiles($"{Application.persistentDataPath}/Data", "*.json")) {
            string stringData = File.ReadAllText(file);

            Game gameData = JsonUtility.FromJson<Game>(stringData);

            Debug.Log("Reading " + gameData.gameName);

            gamesData.Add(gameData);
        }

        return gamesData;
    }

    public Game GetGameData(string gameName)
    {
        foreach(Game game in gamesData) {
            if(game.gameName == gameName) {
                return game;
            }
        }
        throw new KeyNotFoundException(gameName + " file was not found!");
    }

    public void WriteGameDataFile(Game gameData)
    {
        Directory.CreateDirectory($"{Application.persistentDataPath}/Data");

        string stringData = JsonUtility.ToJson(gameData);
        File.WriteAllText(Path.Combine($"{Application.persistentDataPath}/Data", gameData.gameName + ".json"), stringData);

        Debug.Log($"Data for {gameData.gameName} saved succesfully!");
    }

    public byte[] GetImageDataFromPath(string imagePath)
    {
        return File.ReadAllBytes(imagePath);
    }
}
