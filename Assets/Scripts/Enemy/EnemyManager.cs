using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance  { get; private set; }

    private EnemyData enemyData;
    private int currentEnemyAttack;
    private int currentEnemyHealth;
    private int currentEnemySpeed;
    private string currentEnemyName;
    private Sprite currentEnemySprite;
    private int experienceReward;
    private int enemyLevel;
    public event Action EnemyStatsChanged;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void SetCurrentEnemyHealth(int health)
    {
        currentEnemyHealth = health;
        EnemyStatsChanged?.Invoke();
    }

    public void SetCurrentEnemyAttack(int attack)
    {
        currentEnemyAttack = attack;
    }

    public void SetCurrentEnemySpeed(int speed)
    {
        currentEnemySpeed = speed;
    }

    public void SetCurrentEnemyName(string name)
    {
        currentEnemyName = name;
    }

    public int GetCurrentEnemyHealth()
    {
        return currentEnemyHealth;
    }
    public int GetCurrentEnemyAttack()
    {
        return currentEnemyAttack;
    }
    public int GetCurrentEnemySpeed()
    {
        return currentEnemySpeed;
    }

    public string GetCurrentEnemyName()
    {
        return currentEnemyName;
    }

    public int GetCurrentEnemyExperienceReward()
    {
        return experienceReward;
    }

    public int GetCurrentEnemyLevel()
    {
        return enemyLevel;
    }

    public void SetEnemyData(EnemyData data, EncounterZoneData encounterZoneData)
    {
        enemyData = data;
        currentEnemyName = data.enemyName;
        currentEnemySprite = data.enemySprite;
        enemyLevel = calculateEnemyLevel(encounterZoneData);
        currentEnemyHealth = CalculateHealthWithLevel(data.health, enemyLevel);
        currentEnemyAttack = CalculateStatWithLevel(data.attack, enemyLevel);
        currentEnemySpeed = CalculateStatWithLevel(data.speed, enemyLevel);
        experienceReward = calculateExperienceReward(enemyLevel, data.baseExperienceReward);
        EnemyStatsChanged?.Invoke(); // Notify listeners that enemy stats have changed
    }

    public int calculateEnemyLevel(EncounterZoneData encounterZoneData)
    {
        int minLevel = encounterZoneData.levelMin;
        int maxLevel = encounterZoneData.levelMax;

        return UnityEngine.Random.Range(minLevel, maxLevel + 1);
    }

    public int calculateExperienceReward(int enemyLevel, int baseExperienceReward)
    {
        return baseExperienceReward * Mathf.RoundToInt(Mathf.Pow(enemyLevel, 1.25f)); 
    }

    public int CalculateStatWithLevel(int baseStat, int level)
    {
        return Mathf.RoundToInt(baseStat + (baseStat * 0.15f * (level - 1)));
    }
    private int CalculateHealthWithLevel(int baseHealth, int level)
    {
        return Mathf.RoundToInt(baseHealth + (5 * (level - 1)));
    }
}
