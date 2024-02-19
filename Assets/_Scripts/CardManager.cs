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
    public int Magic_power, CurseEffect, CardIndex, IncreesedMagicPower;
    public int EnemyDamage, BlockedDamage,ReducePlayerHelth, IncreesPlayerHelth , Medication,gainArmor,reduceEnemyArmor,checkNextMove,RandomEnemyAttack, DamageOnNextTurn, DefenceOnNextTurn;
    public bool Attack, Defence, Curse,Medicated,AD_Cards,Cash_cards,Reshuffle_cards,Support,stun,AddCardsNow,AddCardsInNextTurn;


    [Header("Card Data")]
    public Sprite cardSprite;
    public Image centerImg;

}
