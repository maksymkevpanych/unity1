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
        isDead = true;
        enemyCollider.enabled = false; 
        Debug.Log("Enemy Died!");
        Destroy(gameObject, 0.1f); 
    }
}
