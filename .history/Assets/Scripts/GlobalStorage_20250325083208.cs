using UnityEngine;
using System.Collections.Generic;

public class GlobalStorage : MonoBehaviour
{
    public static GlobalStorage Instance { get; private set; }

    public List<float> recordTable = new List<float>(); // Таблиця рекордів
    public int lives = 3; // Кількість життів
    public int collisions = 0; // Кількість зіткнень
    public float levelStartTime; // Час старту рівня
    public int healthPickupsCollected = 0; // Зібрані Health Pickup

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Глобальний об'єкт
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        levelStartTime = Time.time; // Запам'ятовуємо час запуску рівня
    }

    public float GetLevelElapsedTime()
    {
        return Time.time - levelStartTime; // Час, що минув з початку рівня
    }

    public void AddRecord(float record)
    {
        recordTable.Add(record);
        recordTable.Sort(); // Сортуємо, щоб рекорди були в порядку
    }

    public void TakeDamage()
    {
        lives--;
        collisions++;

        if (lives <= 0)
        {
            Debug.Log("Гравець програв!");
        }
    }

    public void CollectHealthPickup()
    {
        healthPickupsCollected++;
    }
}
