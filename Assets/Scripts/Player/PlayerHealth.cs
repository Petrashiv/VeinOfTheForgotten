using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    private int meleedamage;
    private DeathScreenManager deathScreenManager;
    private TextMeshProUGUI healthText;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        
        if (PlayerStats.Instance != null)
        {
            maxHealth = PlayerStats.Instance.maxHealth;
            currentHealth = maxHealth;
            meleedamage = PlayerStats.Instance.meleeDamage;
        }
        else
        {
            
            Debug.LogWarning("PlayerStats.Instance не найден!");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        deathScreenManager = FindObjectOfType<DeathScreenManager>();

        GameObject healthTextObj = GameObject.Find("HealthText");
        if (healthTextObj != null)
        {
            healthText = healthTextObj.GetComponent<TextMeshProUGUI>();
        }

        UpdateHealthText();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        if (deathScreenManager != null)
        {
            int totalCoins = CoinManager.Instance != null ? CoinManager.Instance.GetCoinCount() : 0;
            deathScreenManager.ShowDeathScreen(totalCoins);
        }

        gameObject.SetActive(false);
    }

    public void AddCoins(int amount)
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoins(amount);
        }
    }
}
