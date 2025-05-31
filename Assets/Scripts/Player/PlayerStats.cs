using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Base Stats")]
    public int maxHealth { get; private set; } = 5;
    public int currentHealth;
    public int meleeDamage = 1;
    public int rangedDamage = 0;
    public int stamina = 5;

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
    }

    private void Start()
    {
        currentHealth = maxHealth;

    }

    
    public void IncreaseHealth(int amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
    }

    public void IncreaseMeleeDamage(int amount)
    {
        meleeDamage += amount;
    }

    public void IncreaseRangedDamage(int amount)
    {
        rangedDamage += amount;
    }

    public void IncreaseStamina(int amount)
    {
        stamina += amount;
    }

    public int GetHealth()
    {
        return maxHealth;
    }

    public int GetStamina()
    {
        return stamina;
    }

    public int GetMeleeDamage()
    {
        return meleeDamage;
    }

    public int GetRangedDamage()
    {
        return rangedDamage;
    }

}
