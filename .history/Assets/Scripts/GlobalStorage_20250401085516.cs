using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public float lives;
    public float collisions;
    public int healthPickups;
    public float playTime;
}

public class GlobalStorage : MonoBehaviour
{
    public static GlobalStorage Instance { get; private set; }
    
    public float lives = 100f;  
    public float collisions = 0; 
    public int healthPickups = 0; 
    private float startTime; 

    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            savePath = Path.Combine(Application.persistentDataPath, "gameData.json");

            LoadData(); // Завантаження даних із файлу
            
            startTime = Time.time; // Запам'ятовуємо час старту
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveData(); 
    }

    public void SaveData()
    {
        GameData data = new GameData
        {
            lives = lives,
            collisions = collisions,
            healthPickups = healthPickups,
            playTime = Time.time - startTime
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game data saved: " + savePath);
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);

            lives = data.lives;
            if (lives <=0){
                lives=100;
            }
            collisions = data.collisions;
            healthPickups = data.healthPickups;
            startTime = Time.time - data.playTime;

            Debug.Log("Game data loaded.");
        }
        else
        {
            Debug.LogWarning("No save file found, using default values.");
        }
    }
}
