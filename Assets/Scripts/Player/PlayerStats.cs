using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Base Stats")]
    public int maxHealth = 5;
    public int currentHealth;
    public int meleeDamage = 1;
    public int rangedDamage = 0;
    public int stamina = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // если нужно сохранить при смене сцен
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

    // Методы для улучшения
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

    // Пример получения значений
    /*public int GetHealth() => maxHealth;
    public int GetMeleeDamage() => meleeDamage;
    public int GetRangedDamage() => rangedDamage;
    public int GetStamina() => stamina;*/
}
