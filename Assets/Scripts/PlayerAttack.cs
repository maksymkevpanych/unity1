using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f; // Maximum attack distance
    public float attackAngle = 45f; // Half-angle (total cone = 90 degrees)
    public int attackDamage = 20; // Damage per hit
    public float knockbackDistance = 2f; // How far the enemy is pushed
    public float knockbackSpeed = 5f; // Speed of knockback movement
    public LayerMask enemyLayer; // Assign "Enemy" layer in Inspector

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Change this key as needed
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;

            // Check if enemy is within the 90° attack cone (45° left, 45° right)
            if (Vector3.Angle(transform.forward, directionToEnemy) <= attackAngle)
            {
                // Apply damage if the enemy has a health script
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                    Debug.Log($"Hit {enemy.name} for {attackDamage} damage!");
                }

                // Apply knockback
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    Vector3 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                    Vector3 knockbackTarget = enemy.transform.position + knockbackDirection * knockbackDistance;

                    StartCoroutine(KnockbackEnemy(enemyRb, knockbackTarget));
                }
            }
        }
    }

    private System.Collections.IEnumerator KnockbackEnemy(Rigidbody enemyRb, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = enemyRb.position;

        while (elapsedTime < knockbackDistance / knockbackSpeed)
        {
            enemyRb.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / (knockbackDistance / knockbackSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemyRb.position = targetPosition; // Ensure final position is set
    }

    private void OnDrawGizmos()
    {
        // Debug visualization for attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
