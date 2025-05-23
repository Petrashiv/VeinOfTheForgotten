using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    public GameObject coinPrefab;
    public float flashDuration = 0.1f;
    public Transform healthBar;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Vector3 originalScale;
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        if (healthBar != null) 
        {
            originalScale = healthBar.localScale;
            originalScale.y = 0.2f;
            originalScale.x = 2f;
        }
            
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        StartCoroutine(FlashRed());
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    System.Collections.IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float healthPercent = Mathf.Clamp01((float)currentHealth / maxHealth);
            float safeScaleX = Mathf.Max(0.01f, originalScale.x * healthPercent); // не даём стать 0
            healthBar.localScale = new Vector3(safeScaleX, originalScale.y, originalScale.z);
        }
    }

    void Die()
    {
        
        Destroy(gameObject);
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
        if (coinPrefab != null)
            Instantiate(coinPrefab, transform.position, Quaternion.identity);

        

        Destroy(gameObject);

    }
}
