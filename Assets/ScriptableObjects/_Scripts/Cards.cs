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
    public string name;

    public bool canShow;
    public bool looted;
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

    [Header("Card Powers")]
    [Space()]
    public int MagicPowerRequired;
    [Range(0, 10)]

    [Header("Curse")]
    [Space()]
    public int CurseEffect_min;
    [Range(0, 10)]
    public int CurseEffect_max;
    [Range(0, 10)]

    [Header("Attack")]
    [Space()]
    public int Attack_min;
    [Range(0, 10)]
    public int Attack_max;
    [Range(0, 10)]

    [Header("Defense")]
    [Space()]
    public int Defense_min;
    [Range(0, 10)]
    public int Defense_max;
    [Range(0, 10)]

    [Header("PlayerHeal")]
    [Space()]
    public int PlayerHeal_min;
    [Range(0, 10)]
    public int PlayerHeal_max;
    [Range(0, 3)]

    [Header("MagicPower")]
    [Space()]
    public int IncreesedMagicPower;
    [Range(0, 10)]

    [Header("PlayerHealth")]
    [Space()]
    public int ReducePlayerHealth;
    [Range(0, 10)]
    public int IncreesPlayerHealth;

    [Header("Card Sprites")]
    [Space()]
    public Sprite cardSprite;
    public Sprite centerImg;

    public bool displayed;


    [Header("Card Group")]
    [Space()]
    public bool rodentGroup;
}


