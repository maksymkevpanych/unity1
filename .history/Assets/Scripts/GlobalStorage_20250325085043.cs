using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    public static GlobalStorage Instance;

    public float lives = 100f;
    public int recordScore = 0;
    public int collisions = 0;
    public float levelStartTime;
    public int healthPickupsCollected = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        levelStartTime = Time.time;
    }

    
}
public void CollectHealthPickup()
    {
        healthPickupsCollected++;
    }