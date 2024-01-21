using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Sounds Sounds;
    public Scrollbar soundVolume,effectVolume,musicVolume, bGM_Volume;
    public AudioSource bGM;

    private void OnEnable()
    {
        soundVolume.value = PlayerPrefs.GetFloat("sound",1);
        effectVolume.value = PlayerPrefs.GetFloat("effect",1);
        musicVolume.value = PlayerPrefs.GetFloat("music",1);
        bGM_Volume.value = PlayerPrefs.GetFloat("bg", 1);
    }
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
    public void backBtn()
    {
        PlayerPrefs.SetFloat("sound", soundVolume.value);
        PlayerPrefs.SetFloat("effect", effectVolume.value);
        PlayerPrefs.SetFloat("music", musicVolume.value);
        PlayerPrefs.SetFloat("bg", bGM_Volume.value);
    }
}
