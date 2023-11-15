using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Sounds Sounds;
    public Scrollbar bGM_Volume;
    public AudioSource bGM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sounds.Volume = bGM_Volume.value;
        bGM.volume = bGM_Volume.value;
    }
}
