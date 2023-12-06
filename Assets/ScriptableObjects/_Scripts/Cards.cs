using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Newcard", menuName = "MSO/Card/CardsData")]
public class Cards : ScriptableObject
{
    public CardsData[] AttackCards;
    public CardsData[] DefanceCards;
    public CardsData[] AD_Cards;
    public CardsData[] curseCards;
    public CardsData[] MedicatedCards;
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


    [Header("Card Powers")]
    [Space()]
    public Sprite cardSprite;
    public Sprite centerImg;

    public bool displayed;
}


