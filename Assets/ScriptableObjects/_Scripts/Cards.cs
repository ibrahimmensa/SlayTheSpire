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

    public bool stun;
    public bool temporaryEvasion;
    public bool addCardsInHand;

    [Header("Curse")]
    [Space()]
    [Range(0, 10)]
    public int CurseEffect_min;
    [Range(0, 10)]
    public int CurseEffect_max;

    [Header("Attack")]
    [Space()]
    [Range(0, 10)]
    public int Attack_min;
    [Range(0, 10)]
    public int Attack_max;
    

    [Header("Defense")]
    [Space()]
    [Range(0, 10)]
    public int Defense_min;
    [Range(0, 10)]
    public int Defense_max;

    [Header("PlayerHeal")]
    [Space()]
    [Range(0, 3)]
    public int PlayerHeal_min;
    [Range(0, 10)]
    public int PlayerHeal_max;

    [Header("MagicPower")]
    [Space()]
    [Range(0, 10)]
    public int IncreesedMagicPower;

    [Header("PlayerHealth")]
    [Space()]
    [Range(0, 10)]
    public int ReducePlayerHealth;
    [Range(0, 10)]
    public int IncreesPlayerHealth;


    [Header("PlayerHealth")]
    [Space()]
    [Range(0, 10)]
    public int GainArmor;
    [Range(0, 10)]
    public int ReduceArmor;

    [Header("Card Sprites")]
    [Space()]
    public Sprite cardSprite;
    public Sprite centerImg;

    public bool displayed;


    [Header("Card Group")]
    [Space()]
    public bool rodentGroup;
}


