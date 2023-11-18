using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "mYCardsManager", menuName = "MSO/Card/NewCardsManager")]
public class CardsManagement : ScriptableObject
{
    public Cards CardData;
    public int Cursevalue;
    public bool CurseActivated;
    public int defanceValue;
    public bool DefanceActivated;
}
