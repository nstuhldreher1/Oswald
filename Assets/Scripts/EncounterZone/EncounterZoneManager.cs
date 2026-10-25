using UnityEngine;

public class EncounterZoneManager : MonoBehaviour
{
    public static EncounterZoneManager instance { get; private set; }
    private bool isPlayerInEncounterZone = false;
    private EncounterZoneData currentEncounterZoneData;
    private EnemyData currentEnemyData;
    private EncounterZoneData currentEnemyEncounterZoneData;
    private float playerInZoneDistance = 0f;

    private float distanceThreshold = 5f; 

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
    public void SetIsPlayerInEncounterZone(bool value)
    {
        isPlayerInEncounterZone = value;
        Debug.Log("Is player in encounter zone: " + isPlayerInEncounterZone);
    }

    public bool GetIsPlayerInEncounterZone()
    {
        return isPlayerInEncounterZone;
    }

    public void SetCurrentEncounterZoneData(EncounterZoneData data)
    {
        currentEncounterZoneData = data;
    }

    public EncounterZoneData GetCurrentEncounterZoneData()
    {
        return currentEncounterZoneData;
    }

    public EnemyData GetCurrentEnemyData()
    {
        return currentEnemyData;
    }

    public EncounterZoneData GetCurrentEnemyEncounterZoneData()
    {
        return currentEnemyEncounterZoneData;
    }

    public void AddPlayerDistanceMoved(float distance)
    {
        if (isPlayerInEncounterZone)
        {
            playerInZoneDistance += distance;
            if(playerInZoneDistance >= distanceThreshold)
            {
                if (RollEncounter())
                {
                    Debug.Log("Encounter triggered!");
                    PreRandomizeEnemy();
                    LoadBattleScene();
                }
                else
                {
                    Debug.Log("No encounter this time.");
                }

                playerInZoneDistance = 0f; // Reset the distance after checking for an encounter
                
            }
        }
    }

    public void LoadBattleScene()
    {
        if (currentEnemyData != null && currentEnemyEncounterZoneData != null)
        {
            Debug.Log("Loading battle scene with enemy: " + currentEnemyData.enemyName);
            DataPersistenceManager.instance.SavePrevBattle();
            DataPersistenceManager.instance.SetLoadCheckpoint(false); 
            SceneManager.instance.LoadScene("BattleScene");
        }
        else
        {
            Debug.LogWarning("Enemy or encounter zone data is missing; cannot load battle scene.");
        }
    }


    public void PreRandomizeEnemy()
    {
        currentEnemyData = null;
        currentEnemyEncounterZoneData = null;

        if (currentEncounterZoneData != null &&
            currentEncounterZoneData.enemyTypes != null &&
            currentEncounterZoneData.enemyTypes.Length > 0)
        {
            int randomIndex = Random.Range(0, currentEncounterZoneData.enemyTypes.Length);
            currentEnemyData = currentEncounterZoneData.enemyTypes[randomIndex];
            currentEnemyEncounterZoneData = currentEncounterZoneData;
            if (currentEnemyData != null)
            {
                Debug.Log("Randomized enemy: " + currentEnemyData.enemyName);
            }
            else
            {
                Debug.LogWarning("Selected enemy data is missing from the encounter zone.");
            }
        }
        else
        {
            Debug.LogWarning("No encounter zone data available for randomizing enemy.");
        }
    }

    public bool RollEncounter()
    {
        float roll = Random.value;
        if (roll <= 0.20f) // 10% chance
        {
            PreRandomizeEnemy();
            return true;
        }
        else
        {
            return false;
        }
    }
}
