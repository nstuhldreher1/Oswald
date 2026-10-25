using UnityEngine;

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

    public void SetEnemyData(EnemyData data, EncounterZoneData encounterZoneData)
    {
        enemyData = data;
        currentEnemyHealth = data.health;
        currentEnemyAttack = data.attack;
        currentEnemySpeed = data.speed;
        currentEnemyName = data.enemyName;
        currentEnemySprite = data.enemySprite;
        enemyLevel = calculateEnemyLevel(encounterZoneData);
        experienceReward = calculateExperienceReward(enemyLevel, data.baseExperienceReward);
    }

    public int calculateEnemyLevel(EncounterZoneData encounterZoneData)
    {
        int minLevel = encounterZoneData.levelMin;
        int maxLevel = encounterZoneData.levelMax;

        return Random.Range(minLevel, maxLevel + 1);
    }

    public int calculateExperienceReward(int enemyLevel, int baseExperienceReward)
    {
        return baseExperienceReward * Mathf.RoundToInt(Mathf.Pow(enemyLevel, 1.25f)); 
    }


   
}
