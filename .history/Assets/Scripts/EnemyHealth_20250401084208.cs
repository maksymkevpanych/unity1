using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;
    public bool isDead = false;
    private Collider enemyCollider;

    private void Start()
    {
        enemyCollider = GetComponent<Collider>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; // Захист від повторного виклику
        isDead = true;

        // Вимкнення всіх коллайдерів, якщо у ворога є декілька
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        // Оновлення глобальної статистики
       
        Debug.Log("Enemy Died! Score: ");

        Destroy(gameObject, 0.1f);
    }
}
