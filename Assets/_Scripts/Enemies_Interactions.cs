using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies_Interactions : MonoBehaviour
{
    public InventoryCardManager InventoryCardManager;
    public Animator enemyAnimator;
    public Image EnemyHealth;
    public Text HealthTxt;
    public Text TurnTxt;
    public Text DPTxt;

    public void EndTurn()
    {
        GameManager.Instance.EndTurn();
    }
    private void Update()
    {

        DPTxt.text = InventoryCardManager.DP_Details.Count.ToString();
    }
    public void OpenDiscard()
    {
        GameManager.Instance.Discard.SetActive(true);
        gameObject.SetActive(false);
    }
}
