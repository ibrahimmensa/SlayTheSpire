using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Panals
{
    MAIN,
    END,
    COMPLETED
}
public class MiniGameManager : MonoBehaviour
{
    public Main Main;
    public End End;
    public Completed completed;
    public Panals Panals;
    public GameObject Fader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangePanel(Panals panals)
    {
        AllOff();
        switch (panals)
        {
            case Panals.MAIN:
                {
                    Main.gameObject.SetActive(true);
                    break;
                }
            case Panals.END:
                {
                    End.gameObject.SetActive(true);
                    break;
                }
            case Panals.COMPLETED:
                {
                    completed.gameObject.SetActive(true);
                    break;
                }
        }
        
    }
    void AllOff()
    {
        Main.gameObject.SetActive(false);
        End.gameObject.SetActive(false);
        completed.gameObject.SetActive(false);
    }
}
