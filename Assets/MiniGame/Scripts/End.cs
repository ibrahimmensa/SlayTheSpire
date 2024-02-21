using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public MiniGame MiniGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
    {
        MiniGame.totalTurns = 0;
        MiniGame.winCounts = 0;
        MiniGame.loseCounts = 0;
        SceneManager.LoadScene("MiniGame_01");
    }
}
