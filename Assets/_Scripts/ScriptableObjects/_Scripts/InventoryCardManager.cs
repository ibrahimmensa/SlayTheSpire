using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Newcard", menuName = "MSO/Inventory/NewInventoryCardManager")]
public class InventoryCardManager : ScriptableObject
{
    public int total_cards;
    public List<GameObject> lootedCards;
}
