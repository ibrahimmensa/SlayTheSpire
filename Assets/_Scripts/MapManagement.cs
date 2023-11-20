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
        for(int i=0;i< PlayerPrefs.GetInt("Levels", 1);i++)
        {
            Levels[i].GetComponent<Button>().interactable = true;
        }
        
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
}
