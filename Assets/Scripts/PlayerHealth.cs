using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float armour = 0.1f; // Reduces damage taken
    public float damageReceived = 10f; // Base enemy attack damage
    public float baseAttackTime = 1f; // Attack interval in seconds
    private bool isTakingDamage = false; // Prevent multiple coroutines

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isTakingDamage) 
        {
            StartCoroutine(ApplyDamageOverTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            isTakingDamage = false;
            StopCoroutine(ApplyDamageOverTime());
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        isTakingDamage = true;
        while (isTakingDamage)
        {
            CalculateDamage(damageReceived);
            yield return new WaitForSeconds(baseAttackTime);
        }
    }

    public void CalculateDamage(float baseDamage)
    {
        // Generate a random multiplier between 0.85 and 1.15
        float randomMultiplier = 1 + Random.Range(-0.15f, 0.15f);
        
        // Apply multiplier to the base damage
        float scaledDamage = baseDamage * randomMultiplier;

        // Apply armor reduction
        float finalDamage = scaledDamage - (scaledDamage * armour);

        // Reduce player's health
        health -= finalDamage;

        Debug.Log($"Player took {finalDamage:F2} damage (Scaled: {scaledDamage:F2}, Multiplier: {randomMultiplier:F2}). Health remaining: {health:F2}");
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // Handle player death (disable movement, show game over, etc.)
        Destroy(gameObject);
    }
}
