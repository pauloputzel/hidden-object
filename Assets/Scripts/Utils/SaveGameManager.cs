using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameManager
{
    public PlayerData playerData = new PlayerData();

    private static string playerDataPath = Application.persistentDataPath + "/SaveData.json";

    public SaveGameManager() { 
        LoadGame();
    }

    public void NewGame()
    {
        PlayerData newPlayerData = new PlayerData();

        if (playerData?.musicVolume != null) newPlayerData.musicVolume = playerData.musicVolume; 
        if (playerData?.muted != null) newPlayerData.muted = playerData.muted;
        
        playerData = newPlayerData;
        string playerDataJson = JsonUtility.ToJson(playerData);
        System.IO.File.WriteAllText(playerDataPath, playerDataJson);
    }

    public void LoadGame()
    {
        try
        {
            string playerDataJson = System.IO.File.ReadAllText(playerDataPath);
            playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);

        } catch (FileNotFoundException)
        {
            NewGame();
        }
    }

    public void SaveGame()
    {
        string playerDataJson = JsonUtility.ToJson(playerData);
        System.IO.File.WriteAllText(playerDataPath, playerDataJson);
    }
}

[System.Serializable]
public class PlayerData
{
    public bool jogoIniciado = false;
    public DateTime startDate = DateTime.Now;
    public string name = "";
    public float musicVolume = 1.0f;
    public bool muted = false;
    public List<LevelData> levelDataList = new List<LevelData>();
}

[System.Serializable]
public class LevelData
{
    public string name;
    public int ultimaFaseConcluida = 0;
    public List<FaseData> faseDataList = new List<FaseData>();
}

[System.Serializable]
public class FaseData
{
    public string name;
    public float score = 0;
    public List<ColetavelName> itensColetados = new List<ColetavelName>();
}

