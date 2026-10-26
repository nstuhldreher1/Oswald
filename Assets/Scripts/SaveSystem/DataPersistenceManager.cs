using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
   public static DataPersistenceManager instance { get; private set; }
   private GameData gameData;
   private DynamicGameData dynamicGameData = new DynamicGameData();
   private List<IDataPersistence> dataPersistenceObjects;
   [Header("File Storage Config")]
   [SerializeField] private string fileName;
   private FileDataHandler dataHandler;
   private bool loadCheckpoint = true;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one DataPersistenceManager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

         if (string.IsNullOrEmpty(fileName))
        {
            fileName = "save.json";
        }

       
         if (loadCheckpoint)
        {
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            Debug.Log("DataPersistenceManager initialized with file path: " + Application.persistentDataPath + "/" + fileName);
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }
        else
        {
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadPrevBattle();
        }
       
    }
    public void Reload()
    {
         if (string.IsNullOrEmpty(fileName))
        {
            fileName = "save.json";
        }

       
         if (loadCheckpoint)
        {
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }
        else
        {
            LoadPrevBattle();
        }
    }
    public void NewGame()
    {
        this.gameData = new GameData
        {
            playerHealth = 100,
            maxPlayerHealth = 100,
            playerAttack = 10,
            playerSpeed = 5,
            playerPosition = Vector2.zero,
            playerLevel = 1,
        };
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if(this.gameData == null)
        {
            Debug.LogWarning("No data was found. A new game needs to be started before data can be loaded.");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("Loaded player position: " + gameData.playerPosition);
    }

    public void LoadPrevBattle()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadPrevBattleData(dynamicGameData);
        }
    }


    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        Debug.Log("Saved player position: " + gameData.playerPosition);

        dataHandler.Save(gameData);
    }

    public void SavePrevBattle()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SavePrevBattleData(ref dynamicGameData);
        }
    }

    public void SetLoadCheckpoint(bool value)
    {
        loadCheckpoint = value;
    }

    public bool GetLoadCheckpoint()
    {
        return loadCheckpoint;
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
