using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardPileManager : MonoBehaviour
{
    public InventoryCardManager ICM;
    public CardContainer cardContainer;

    CardManager CM;
    GameObject DCard;
    int index;

    int dp_Lenth;
    private void OnEnable()
    {
        dp_Lenth = ICM.DP_Details.Count;
        AddCards();
    }


    public void AddCards()
    {
        ICM.dPL = ICM.DP_Details.Count;
        for (int card = 0; card < ICM.DP_Details.Count; card++)
        {
            index = card;
            if(!ICM.DP_Details[index].displayed)
            {
                DCard = Instantiate(cardContainer.CardToDisplay, GameManager.Instance.discard_Pile_Contant.transform);
                mapdata(DCard);
            }

        }
    }
    void mapdata(GameObject C)
    {
        var cardObj = ICM.DP_Details[index];
        CM = C.GetComponent<CardManager>();

        CM.gameObject.GetComponent<Image>().sprite = ICM.Discardpile.CardSprite[index];
        CM.Power.text = cardObj.MagicPowerRequired.ToString();
        CM.cardName.text = cardObj.Card_Name;
        CM.Discription.text = cardObj.disription;
        CM.Rarity.text = cardObj.Rarity;

        CM.Magic_power = cardObj.MagicPowerRequired;
        //CM.CurseEffect = cardObj.CurseEffect;
        //CM.EnemyDamage = cardObj.EnemyDamage;
        //CM.BlockedDamage = cardObj.BlockedDamage;
        CM.Attack = cardObj.Attack;
        CM.Defence = cardObj.Defence;
        CM.Curse = cardObj.Curse;
        CM.Medicated = cardObj.Medicated;
    }
}
