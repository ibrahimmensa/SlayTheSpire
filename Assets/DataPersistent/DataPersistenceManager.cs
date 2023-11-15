using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IDataPersistence> dataPersistentsObjects;

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
       // dataPersistentsObjects = FindAllDataPersistenceObject();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        if(this.gameData == null)
        {
            NewGame();
        }
    }
    public void SaveGame()
    {

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    //private List<IDataPersistence> FindAllDataPersistenceObject()
    //{
    //    IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectOfType<MonoBehaviour>()
    //}
}
