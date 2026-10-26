using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    public static PlayerManager instance  { get; private set; }

    private int maxPlayerHealth = 100;
    private int currentPlayerAttack = 10;
    private int currentPlayerHealth = 100;
    private int currentPlayerSpeed = 5;
    private int playerLevel = 1;
    private int playerExperience = 0;
    private int experienceToNextLevel = 100;
    private Vector2 playerPosition;
    public event Action PlayerStatsChanged;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SetCurrentPlayerHealth(int health)
    {
        currentPlayerHealth = health;
        PlayerStatsChanged?.Invoke(); // Notify listeners that player stats have changed
    }

    public void SetCurrentPlayerAttack(int attack)
    {
        currentPlayerAttack = attack;
    }
    public void SetCurrentPlayerSpeed(int speed)
    {
        currentPlayerSpeed = speed;
    }

    public void SetCurrentPlayerPosition(Vector2 position)
    {
        playerPosition = position;
    }
    public void SetCurrentPlayerLevel(int level)
    {
        playerLevel = level;
    }
    public void SetCurrentPlayerExperience(int experience)
    {
        playerExperience = experience;
        PlayerStatsChanged?.Invoke(); // Notify listeners that player stats have changed
    }
    public void SetExperienceToNextLevel(int experience)
    {
        experienceToNextLevel = experience;
        PlayerStatsChanged?.Invoke(); // Notify listeners that player stats have changed
    }

    public Vector2 GetCurrentPlayerPosition()
    {
        return playerPosition;
    }

    public int GetCurrentPlayerHealth()
    {
        return currentPlayerHealth;
    }
    public int GetCurrentPlayerAttack()
    {
        return currentPlayerAttack;
    }
    public int GetCurrentPlayerSpeed()
    {
        return currentPlayerSpeed;
    }

    public int GetCurrentPlayerLevel()
    {
        return playerLevel;
    }
    public int GetCurrentPlayerExperience()
    {
        return playerExperience;
    }
    public int GetExperienceToNextLevel()
    {
        return experienceToNextLevel;
    }

    public int GetMaxPlayerHealth()
    {
        return maxPlayerHealth;
    }
    public void LoadData(GameData data)
    {
        playerLevel = data.playerLevel;
        maxPlayerHealth = CalculateHealthWithLevel(data.maxPlayerHealth, data.playerLevel);
        currentPlayerHealth = data.playerHealth;
        currentPlayerAttack = CalculateStatWithLevel(data.playerAttack, data.playerLevel);
        currentPlayerSpeed = CalculateStatWithLevel(data.playerSpeed, data.playerLevel);
        playerPosition = data.playerPosition;
        experienceToNextLevel = CalculateExperienceToNextLevel(data.playerLevel);
        playerExperience = data.playerExperience;
        Debug.Log("Player data loaded: Health = " + currentPlayerHealth + ", Attack = " + currentPlayerAttack + ", Speed = " + currentPlayerSpeed);
    }
    private int CalculateExperienceToNextLevel(int level)
    {
        return 100 * Mathf.RoundToInt(Mathf.Pow(level, 1.25f)); ; 
    }

    public void SaveData(ref GameData data)
    {
        data.playerHealth = currentPlayerHealth;
        data.playerPosition = playerPosition;
        data.playerLevel = playerLevel;
        data.playerExperience = playerExperience;
    }

    public void LevelUp()
    {
        playerLevel++;
        experienceToNextLevel = CalculateExperienceToNextLevel(playerLevel);
        playerExperience = 0; // Reset experience after leveling up
        PlayerStatsChanged?.Invoke(); // Notify listeners that player stats have changed
    }

    public void LoadPrevBattleData(DynamicGameData data)
    {
        playerPosition = data.playerPosition;
    }

    public void SavePrevBattleData(ref DynamicGameData data)
    {
        
        data.playerPosition = playerPosition;
    }

    public int CalculateStatWithLevel(int baseStat, int level)
    {
        return Mathf.RoundToInt(baseStat + (baseStat * 0.15f * (level - 1)));
    }

     private int CalculateHealthWithLevel(int baseHealth, int level)
    {
        return Mathf.RoundToInt(baseHealth + (10 * (level - 1)));
    }

}
