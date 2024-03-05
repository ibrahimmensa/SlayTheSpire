using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "mYCardsManager", menuName = "MSO/Card/NewCardsManager")]
public class CardsManagement : ScriptableObject
{
    public Cards CardData;

    [Header("Curse")]
    public int Cursevalue;
    public bool CurseActivated;

    [Header("Defence")]
    public int defanceValue;
    public bool DefanceActivated;

    [Header("Relics")]
    public bool Relics;
    public Text relicName;


    [Header("others")]
    public int turns;
    public bool twoMouseCardsUsed;
}
