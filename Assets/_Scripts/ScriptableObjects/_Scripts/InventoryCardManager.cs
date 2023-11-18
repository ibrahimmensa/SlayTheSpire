using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Newcard", menuName = "MSO/Inventory/NewInventoryCardManager")]
public class InventoryCardManager : ScriptableObject
{
    public Discardpile Discardpile;
    public List<CardsData> DP_Details;
    public int dPL;
}
[System.Serializable]
public class Discardpile
{
    public List<string> CardName;
    public List<Sprite> CardSprite;
}
