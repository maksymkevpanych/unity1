using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Animator zombieAnim;
    public float health = 50f;
    public bool isDead = false;
    private Collider enemyCollider;

    private void Awake(){
        zombieAnim = GetComponent<Animator>();
    }
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
        GetComponent<ZombieFollow>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        zombieAnim.SetBool("isFollowing",false);
        zombieAnim.SetBool("isDead",true);
        Debug.Log("Enemy Died! Score: ");
        
        Destroy(gameObject, 5f);
    }
}
