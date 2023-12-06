using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "MSO/Player/NewPlayer")]
public class Characters : ScriptableObject
{
    public string name;
    public int Health;
    public int Gold;
    public string Description;
    public Sprite CharacterBG;
}
