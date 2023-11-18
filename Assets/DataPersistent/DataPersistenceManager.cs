using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    public GameData gameData;
    private List<IDataPersistence> dataPersistentsObjects;

    [SerializeField]
    private string fileName;

    public FileDataHandller dataHandller;
    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {

        }
        Instance = this;
    }
    private void Start()
    {
        dataHandller = new FileDataHandller(Application.persistentDataPath,fileName);
        dataPersistentsObjects = FindAllDataPersistenceObject();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandller.Load();

        if(this.gameData == null)
        {
            NewGame();
        }
        else
        {
            Debug.Log("Data found");
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistentsObjects)
        {
            dataPersistenceObj.loadData(gameData);
        }
    }
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistentsObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandller.SaveData(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObject()
    {
        IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistencesObjects);
    }
}
