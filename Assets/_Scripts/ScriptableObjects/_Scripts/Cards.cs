using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Newcard",menuName ="Card")]
public class Cards : ScriptableObject
{
    [Header("Card Type")]
    [Space()]
    public bool Damage;
    public bool Defence;
    public bool Curse;
    public bool Medicated;
    [Header("Card Details")]
    [Space()]
    public string Card_Name;
    public string Card_Type;
    public string disription;
    public string Rarity;

    [Header("Card Powers")]
    [Space()]
    public int MagicPowerRequired;
    public int CurseEffect;
    public float EnemyDamage;
    public float BlockedDamage;
}
