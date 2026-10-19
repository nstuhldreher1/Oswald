using UnityEngine;

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

    public void LoadData(GameData data)
    {
        maxPlayerHealth = data.playerHealth;
        currentPlayerHealth = data.playerHealth;
        currentPlayerAttack = data.playerAttack;
        currentPlayerSpeed = data.playerSpeed;
        playerPosition = data.playerPosition;
        Debug.Log("Player data loaded: Health = " + currentPlayerHealth + ", Attack = " + currentPlayerAttack + ", Speed = " + currentPlayerSpeed);
    }

    public void SaveData(ref GameData data)
    {
        data.maxPlayerHealth = maxPlayerHealth;
        data.playerHealth = currentPlayerHealth;
        data.playerAttack = currentPlayerAttack;
        data.playerSpeed = currentPlayerSpeed;
        data.playerPosition = playerPosition;
    }

    public void LoadPrevBattleData(DynamicGameData data)
    {
        maxPlayerHealth = data.playerHealth;
        currentPlayerHealth = data.playerHealth;
        currentPlayerAttack = data.playerAttack;
        currentPlayerSpeed = data.playerSpeed;
        playerPosition = data.playerPosition;
    }

    public void SavePrevBattleData(ref DynamicGameData data)
    {
        data.maxPlayerHealth = maxPlayerHealth;
        data.playerHealth = currentPlayerHealth;
        data.playerAttack = currentPlayerAttack;
        data.playerSpeed = currentPlayerSpeed;
        data.playerPosition = playerPosition;
    }
}
