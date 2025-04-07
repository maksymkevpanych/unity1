using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour
{
    public float armour = 0.1f;
    public float damageReceived = 10f;
    public float baseAttackTime = 1f;
    private Coroutine damageCoroutine;

    private void Update()
    {
        if (GlobalStorage.Instance.lives <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        GlobalStorage.Instance.lives = Mathf.Min(GlobalStorage.Instance.lives + amount, 100);
        Debug.Log("Player healed! Current health: " + GlobalStorage.Instance.lives);
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

        GlobalStorage.Instance.lives -= finalDamage;
        GlobalStorage.Instance.collisions++;
        Debug.Log($"Player took {finalDamage:F2} damage. Health remaining: {GlobalStorage.Instance.lives:F2}");
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        GameObject.Set
        Destroy(gameObject);
        
    }   
}
