using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Newcard",menuName ="Card")]
public class Cards : ScriptableObject
{
    public string Card_Name;
    public Sprite artwork;

    public string Card_Type;
    public string disription;
    public string Rarity;

    public int Card_value;
    public int MagicPower;
    public int health;
}
