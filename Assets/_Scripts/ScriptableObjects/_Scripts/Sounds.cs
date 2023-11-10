using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Sounds", menuName = "MSO/Sounds/NewSounds")]
public class Sounds : ScriptableObject
{
    [Range(0,1)]
    public float Volume;
    public AudioClip[] AttackSounds;
    public AudioClip levelcomplete;
    public AudioClip LevelFailed;

    public AudioClip[] Background_Music;
    public Sprite[] BGs;

}
