using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //public InventoryCardManager InventoryCardManager;
    public CardsManagement cardsManagement;
    public GameObject cardsParent;
    public GameObject looted_Cards;
    CardManager CM;
    public List<CardsData> lootedCards = new List<CardsData>();
    bool populated;
    private void OnEnable()
    {
        if (populated)
            return;
        foreach (CardsData card in cardsManagement.CardData.AttackCards)
        {
            if(card.looted) 
            {
                lootedCards.Add(card);
            }
        }
        mapData();
    }
    public void mapData()
    {
        for(int i = 0; i < lootedCards.Count; i++)
        {
            GameObject card = Instantiate(looted_Cards, cardsParent.transform);
            CardsData cardObj;
            cardObj = lootedCards[i];
            CM = card.GetComponent<CardManager>();

            CM.Power.text = cardObj.MagicPowerRequired.ToString();
            CM.cardName.text = cardObj.Card_Name;
            CM.Discription.text = cardObj.disription;
            CM.Magic_power = cardObj.MagicPowerRequired;
           // CM.CurseEffect = Random.Range(cardObj.CurseEffect_min, cardObj.CurseEffect_max);
            CM.EnemyDamage = Random.Range(cardObj.Attack_min, cardObj.Attack_max);
            CM.BlockedDamage = Random.Range(cardObj.Defense_min, cardObj.Defense_max);
            CM.Medication = Random.Range(cardObj.PlayerHeal_min, cardObj.PlayerHeal_max);
            CM.ReducePlayerHelth = cardObj.ReducePlayerHealth;
            CM.IncreesPlayerHelth = cardObj.IncreesPlayerHealth;
            CM.IncreesedMagicPower = cardObj.IncreesedMagicPower;
            CM.Attack = cardObj.Attack;
            CM.Defence = cardObj.Defence;
           // CM.Curse = cardObj.Curse;
            CM.Medicated = cardObj.Medicated;
            CM.AD_Cards = cardObj.AttackDefence;
            CM.Cash_cards = cardObj.cashCards;
            CM.Reshuffle_cards = cardObj.Rehuffle;
            CM.gameObject.GetComponent<Image>().sprite = cardObj.cardSprite;
            CM.centerImg.sprite = cardObj.centerImg;

            if (cardObj.Attack) { CM.Rarity.text = "Attack"; }
            else if (cardObj.Defence) { CM.Rarity.text = "Defence"; }
           // else if (cardObj.Curse) { CM.Rarity.text = "Curse"; }
            else if (cardObj.Medicated) { CM.Rarity.text = "Medicated"; }
            else if (cardObj.AttackDefence) { CM.Rarity.text = "AttackDefence"; }
            else if (cardObj.cashCards) { CM.Rarity.text = "Cash"; }
            else if (cardObj.Rehuffle) { CM.Rarity.text = "Reshuffle"; }
        }
        populated = true;
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
