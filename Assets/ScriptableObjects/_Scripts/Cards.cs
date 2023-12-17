using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Newcard", menuName = "MSO/Card/CardsData")]
public class Cards : ScriptableObject
{
    public CardsData[] AttackCards;
}
[System.Serializable]
public class CardsData
{
    [Header("Card Type")]
    [Space()]
    public bool Attack;
    public bool Defence;
    public bool Curse;
    public bool Medicated;
    public bool AttackDefence;
    public bool Rehuffle;
    public bool cashCards;
    [Header("Card Details")]
    [Space()]
    public string Card_Name;
    public int CardIndex;
    public string Card_Type;
    public string disription;
    public string Rarity;

    [Header("Card Powers")]
    [Space()]
    public int MagicPowerRequired;
    [Range(0, 1)]
    public int CurseEffect_min;
    [Range(0, 1)]
    public int CurseEffect_max;
    [Range(0, 1)]
    public float EnemyDamage_min;
    [Range(0, 1)]
    public float EnemyDamage_max;
    [Range(0, 1)]
    public float BlockedDamage_min;
    [Range(0, 1)]
    public float BlockedDamage_max;
    [Range(0, 1)]
    public float PlayerHeal_min;
    [Range(0, 1)]
    public float PlayerHeal_max;
    [Range(0, 3)]
    public float MagicPower;
    [Range(0, 1)]
    public float ReducePlayerHealth;
    [Range(0, 1)]
    public float IncreesPlayerHealth;

    [Header("Card Sprites")]
    [Space()]
    public Sprite cardSprite;
    public Sprite centerImg;

    public bool displayed;


    [Header("Card Group")]
    [Space()]
    public bool rodentGroup;
}


