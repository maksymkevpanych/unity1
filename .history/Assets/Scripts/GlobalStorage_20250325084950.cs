using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public int collisions;
    public int healthPickups;
    public float playTime;
}

public class GlobalStorage : MonoBehaviour
{
    public static GlobalStorage Instance { get; private set; }
    
    public int lives = 100;  // Завжди 100 при старті
    public int collisions = 0; 
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

            LoadData(); // Завантаження тільки потрібних даних

            startTime = Time.time; // Запам'ятовуємо час старту
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveData(); // Збереження при виході
    }

    public void SaveData()
    {
        GameData data = new GameData
        {
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

