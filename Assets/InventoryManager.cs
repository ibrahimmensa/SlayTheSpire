using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryCardManager InventoryCardManager;
    public GameObject CardsParent;
    private void OnEnable()
    {
        foreach(GameObject card in InventoryCardManager.lootedCards)
        {
            Instantiate(card, CardsParent.transform);
        }
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
