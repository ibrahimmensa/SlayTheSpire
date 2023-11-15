using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sounds sounds;
    public AudioSource bGM;
    public AudioSource GameSounds; 
    private void OnEnable()
    {
        bGM.volume = sounds.Volume;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playSound(AudioClip audio)
    {
        GameSounds.Stop();
        GameSounds.PlayOneShot(audio, 1);
    }
}
