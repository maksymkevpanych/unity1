using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float armour = 0.1f;
    public float damageReceived = 10f;
    public float baseAttackTime = 1f;
    private Coroutine damageCoroutine;

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        health = Mathf.Min(health + amount, 100f);
        Debug.Log("Player healed! Current health: " + health);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null && !enemyHealth.isDead)
            {
                if (damageCoroutine == null) 
                {
                    damageCoroutine = StartCoroutine(ApplyDamageOverTime(enemyHealth));
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StopTakingDamage();
        }
    }

    private IEnumerator ApplyDamageOverTime(EnemyHealth enemyHealth)
    {
        while (enemyHealth != null && !enemyHealth.isDead)
        {
            CalculateDamage(damageReceived);
            yield return new WaitForSeconds(baseAttackTime);
        }

        // Stop taking damage if the enemy dies
        StopTakingDamage();
    }

    private void StopTakingDamage()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    public void CalculateDamage(float baseDamage)
    {
        float randomMultiplier = 1 + Random.Range(-0.15f, 0.15f);
        float scaledDamage = baseDamage * randomMultiplier;
        float finalDamage = scaledDamage - (scaledDamage * armour);
        health -= finalDamage;

        Debug.Log($"Player took {finalDamage:F2} damage. Health remaining: {health:F2}");
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        Destroy(gameObject);
    }
}
