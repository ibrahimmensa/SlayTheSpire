using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "mYCardsManager", menuName = "CardsManager")]
public class CardsManagement : ScriptableObject
{
    public Cards[] MyCards;
    public int Cursevalue;
    public bool CurseActivated;
    public int defanceValue;
    public bool DefanceActivated;
}
