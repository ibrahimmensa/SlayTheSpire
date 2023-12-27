using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "MSO/Player/NewPlayer")]
public class Characters : ScriptableObject
{
    public bool Unlocked;
    public bool Selected;
    [Space()]
    public string name;
    public int Health;
    public int Gold;
    public string Description;
    public Sprite CharacterBG;

    [Space()]
    public int total_horses_count;
    public int total_Potion_count;
    public int total_Relic_count;

    [Space()]
    public int Available_horses;
    public int Available_Potion;
    public int Available_Relic;
    public int magicPoints;
}
