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
    public bool Attack;
    public bool Defence;
    //public bool Curse;
    public bool Medicated;
    public bool AttackDefence;
    public bool Rehuffle;
    public bool cashCards;
    public bool supportCards;
    [Space()]
    public bool stun;
    public bool addCardsNow;
    public bool addCardsInNextTurn;

    [Space()]
    public bool displayed;

    [Header("Card Group")]
    public bool rodentGroup;

    [Header("Card Details")]
    public string Card_Name;
    public int CardIndex;
    public string Card_Type;
    public string disription;

    [Header("Card Powers")]
    public int MagicPowerRequired;

    //[Header("Curse")]
    //[Range(0, 10)]
    //public int CurseEffect_min;
    //[Range(0, 10)]
    //public int CurseEffect_max;

    [Header("Attack")]
    [Range(0, 20)]
    public int Attack_min;
    [Range(0, 20)]
    public int Attack_max;
    

    [Header("Defense")]
    [Range(0, 20)]
    public int Defense_min;
    [Range(0, 20)]
    public int Defense_max;

    [Header("PlayerHeal")]
    [Range(0, 20)]
    public int PlayerHeal_min;
    [Range(0, 20)]
    public int PlayerHeal_max;

    [Header("PlayerHealth")]
    [Range(0, 20)]
    public int ReducePlayerHealth;
    [Range(0, 20)]
    public int IncreesPlayerHealth;




    [Header("Armors")]
    [Range(0, 20)]
    public int GainArmor;
    [Range(0, 20)]
    public int ReduceEnemyArmor;
    [Range(0, 10)]
    public int Evasions;
    [Range(0, 10)]
    public int Strength;

    [Header("MagicPower")]
    [Range(0, 10)]
    public int IncreesedMagicPower;

    [Header("CheckMOveDamage")]
    public int checkMoveDamage;
    public int randomEnemyAttack;
    public int DamageOnNextTurn;
    public int DefenceOnNextTurn;

    [Header("Card Sprites")]
    public Sprite cardSprite;
    public Sprite centerImg;

}


