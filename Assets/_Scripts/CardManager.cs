using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Text Power;
    public Text cardName;
    public Text Discription;
    public Text Rarity;

    [Header("Card Data")]
    public int Magic_power, CurseEffect, CardIndex,MagicPowerToIncrees;
    public float EnemyDamage, BlockedDamage,ReducePlayerHelth, IncreesPlayerHelth , Medication;
    public bool Attack, Defence, Curse,Medicated,AD_Cards,Cash_cards,Reshuffle_cards;


    [Header("Card Data")]
    public Sprite cardSprite;
    public Image centerImg;

}
