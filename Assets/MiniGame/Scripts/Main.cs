using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject[] obj;
    public Animation gameAnimatons;
    public string[] AnimationNames;
    public MiniGame MiniGame;
    private void OnEnable()
    {
        obj[Random.Range(0,obj.Length)].SetActive(true);
        GameAlgo();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameAlgo()
    {
        if (MiniGame.totalTurns < 1)
            return;

      //  gameAnimatons[AnimationNames[0]].speed += 0.5f;

    }
}
