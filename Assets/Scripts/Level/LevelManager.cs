using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WavesManager
{
    public int EnemyCount;
    public int EnemyHealth;
    public float EnemySpeed;
}

[System.Serializable]
public class Levels
{
    public int Level;
    public List<WavesManager> wavesManagers;
    public bool IsCompleted;
}

[System.Serializable]
public class LevelSaveData
{
    public List<Levels> Levels;
    public int CurrentLevelNum;
    public int MaxLevelUnlockedNum;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager inst;

    public List<Levels> Levels = new List<Levels>();

    public int CurrentLevelNum;
    public int MaxLevelUnlockedNum = 1;

    private const string SAVE_KEY = "LEVEL_DATA";

    private void Awake()
    {

        inst = this;
        LoadLevelData();
    }

    private void Start()
    {

    }

    public void SaveLevelData()
    {
        LevelSaveData saveData = new LevelSaveData();

        saveData.Levels = Levels;
        saveData.CurrentLevelNum = CurrentLevelNum;
        saveData.MaxLevelUnlockedNum = MaxLevelUnlockedNum;

        string json = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();

        Debug.Log("Level Data Saved");
        Debug.Log(json);
    }

    public void LoadLevelData()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            Debug.Log("No save data found. Using default levels.");

            CurrentLevelNum = 0;
            MaxLevelUnlockedNum = 1;

            SaveLevelData();
            return;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);

        LevelSaveData saveData = JsonUtility.FromJson<LevelSaveData>(json);

        Levels = saveData.Levels;
        CurrentLevelNum = saveData.CurrentLevelNum;
        MaxLevelUnlockedNum = saveData.MaxLevelUnlockedNum;

        Debug.Log("Level Data Loaded");
    }

    private void OnApplicationPause(bool pause)
    {
        SaveLevelData();
    }

    private void OnApplicationQuit()
    {
        SaveLevelData();
    }


}