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
    public int Magic_power, CurseEffect, CardIndex;
    public float EnemyDamage, BlockedDamage;
    public bool Attack, Defence, Curse,Medicated;


    [Header("Card Data")]
    public Sprite cardSprite;

}
