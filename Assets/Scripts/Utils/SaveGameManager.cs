using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameManager
{
    public PlayerData playerData = new PlayerData();

    private static string playerDataPath = Application.persistentDataPath + "/PlayerData.json";

    public SaveGameManager() { 
        LoadGame();
    }

    public void LoadGame()
    {
        try
        {
            string playerDataJson = System.IO.File.ReadAllText(playerDataPath);
            playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);

        } catch (FileNotFoundException)
        {
            Debug.Log($"Arquivo de save não encontrado em: ${playerDataPath}");
            SaveGame();
        }
    }

    public void SaveGame()
    {
        string playerDataJson = JsonUtility.ToJson(playerData);
        System.IO.File.WriteAllText(playerDataPath, playerDataJson);
        Debug.Log($"Jogo salvo em: {playerDataPath}");
    }
}

[System.Serializable]
public class PlayerData
{
    public bool jogoIniciado = false;
    public string name = "Jogador";
    public float musicVolume = 1.0f;
    public bool muted = false;
    public List<LevelData> levelDataList = new List<LevelData>();
    public List<ColetavelName> itensColetados = new List<ColetavelName>();
}

[System.Serializable]
public class LevelData
{
    public string name;
    public int ultimaFaseConcluida = 0;
    public float score = 0;
}
