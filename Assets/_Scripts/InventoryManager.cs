using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryCardManager InventoryCardManager;
    public CardsManagement CardsManagement;
    public GameObject CardsParent;
    private void OnEnable()
    {
        //foreach(GameObject card in InventoryCardManager.lootedCards)
        //{
        //    Instantiate(card, CardsParent.transform);
        //}
        //for(int i=0;i<InventoryCardManager.discard_Pile.Count;i++)
        //{
        //    if(CardsManagement.MyCards[i].Card_Name == InventoryCardManager.discard_Pile[i])
        //    {
        //       // Instantiate(InventoryCardManager.lootedCards[i], CardsParent.transform);
        //    }
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
