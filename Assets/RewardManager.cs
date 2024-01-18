using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{

    public CardsManagement CardManagement;
    public CardManager[] CM;

    public List<CardsData> cardsNotInDeck = new List<CardsData>();

    CardsData cardObj;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 25);
        foreach (CardsData card in CardManagement.CardData.AttackCards)
        {
            if (!card.canShow)
                cardsNotInDeck.Add(card);
        }
        for(int a=0;a<CM.Length;a++)
        {
            mapData(CM[a].gameObject,a);
        }
    }

    public void mapData(GameObject card,int Index)
    {
        if(cardsNotInDeck.Count > 0)
        {
            
            cardObj = cardsNotInDeck[Random.Range(0, cardsNotInDeck.Count)];

            CM[Index].Power.text = cardObj.MagicPowerRequired.ToString();
            CM[Index].cardName.text = cardObj.Card_Name;
            CM[Index].Discription.text = cardObj.disription;
            CM[Index].Magic_power = cardObj.MagicPowerRequired;
            CM[Index].CurseEffect = Random.Range(cardObj.CurseEffect_min, cardObj.CurseEffect_max);
            CM[Index].EnemyDamage = Random.Range(cardObj.Attack_min, cardObj.Attack_max);
            CM[Index].BlockedDamage = Random.Range(cardObj.Defense_min, cardObj.Defense_max);
            CM[Index].Medication = Random.Range(cardObj.PlayerHeal_min, cardObj.PlayerHeal_max);
            CM[Index].ReducePlayerHelth = cardObj.ReducePlayerHealth;
            CM[Index].IncreesPlayerHelth = cardObj.IncreesPlayerHealth;
            CM[Index].IncreesedMagicPower = cardObj.IncreesedMagicPower;
            CM[Index].Attack = cardObj.Attack;
            CM[Index].Defence = cardObj.Defence;
            CM[Index].Curse = cardObj.Curse;
            CM[Index].Medicated = cardObj.Medicated;
            CM[Index].AD_Cards = cardObj.AttackDefence;
            CM[Index].Cash_cards = cardObj.cashCards;
            CM[Index].Reshuffle_cards = cardObj.Rehuffle;
            CM[Index].gameObject.GetComponent<Image>().sprite = cardObj.cardSprite;
            CM[Index].centerImg.sprite = cardObj.centerImg;
            CM[Index].CardIndex = cardObj.CardIndex;

            if (cardObj.Attack) { CM[Index].Rarity.text = "Attack"; }
            else if (cardObj.Defence) { CM[Index].Rarity.text = "Defense"; }
            else if (cardObj.Curse) { CM[Index].Rarity.text = "Curse"; }
            else if (cardObj.Medicated) { CM[Index].Rarity.text = "Medicated"; }
            else if (cardObj.AttackDefence) { CM[Index].Rarity.text = "AttackDefence"; }
            else if (cardObj.cashCards) { CM[Index].Rarity.text = "Cash"; }
            else if (cardObj.Rehuffle) { CM[Index].Rarity.text = "Rehuffle"; }
        }
        else
        {
            gameObject.SetActive(false);

        }
    }
    public void Claim(int a)
    {
        CardManagement.CardData.AttackCards[CM[a].CardIndex].canShow = true; 
        CardManagement.CardData.AttackCards[CM[a].CardIndex].looted = true;
        PlayerPrefs.SetInt(CardManagement.CardData.AttackCards[CM[a].CardIndex].name, 1);
    }
}
