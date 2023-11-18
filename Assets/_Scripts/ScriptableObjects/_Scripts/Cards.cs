using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Newcard", menuName = "MSO/Card/CardsData")]
public class Cards : ScriptableObject
{
    public CardsData[] cardsDetails;
}
[System.Serializable]
public class CardsData
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
    public int CardIndex;
    public string Card_Type;
    public string disription;
    public string Rarity;

    [Header("Card Powers")]
    [Space()]
    public int MagicPowerRequired;
    public int CurseEffect;
    public float EnemyDamage;
    public float BlockedDamage;


    [Header("Card Powers")]
    [Space()]
    public Sprite cardSprite;
}


