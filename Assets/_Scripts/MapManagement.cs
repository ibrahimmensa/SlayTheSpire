using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManagement : MonoBehaviour
{
    public GameObject[] Levels;
    public CharactersManagment CM;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("Levels", 1); i++)
        {
            Levels[i].GetComponent<Button>().interactable = true;
        }
        // loadMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(int num)
    {
        CM.LoadLevel = num;
        Invoke(nameof(load), 11.0f);
    }
    void load()
    {
        SceneManager.LoadScene(1);
    }
    public MapRows maPRows = new MapRows();
    bool TF;
    public void loadMap()
    {
        for(int i = 0; i < 3; i++) 
        {
            if(i!=0)
                TF = true;

            
            for (int Rows = 0; Rows < maPRows.mapRows.Length; Rows++)
            {
                if (!TF)
                {
                    foreach (var j in maPRows.mapRows[Rows].objects)
                    {
                        j.SetActive(false);
                    }
                }
                maPRows.mapRows[Rows].objects[Random.Range(0, maPRows.mapRows[Rows].objects.Length)].SetActive(true);
            }
        }

    }
}
[System.Serializable]
public class MapRows 
{
    public MapObjects[] mapRows; 
}
[System.Serializable]
public class MapObjects 
{
    public GameObject[] objects;
}
