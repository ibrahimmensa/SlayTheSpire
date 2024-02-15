using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies_Interactions : MonoBehaviour
{
   // public InventoryCardManager InventoryCardManager;
    public Animator enemyAnimator;
    public int EnemyHealth;
    public Text HealthTxt;
    public Text TurnTxt;
    public Text DPTxt;
    public Image Effect;
    public Button endTurn;
    public Sprite BG;

    public Image HealthBar;
    public int dpText;
    public bool hasGun;
    public float waitTime;
    public int damage;
    public bool stunned;

    public CardsData [] EnemyCards;
    private void OnEnable()
    {
        dpText = 0;
    }
    void Update()
    {
        DPTxt.text = dpText.ToString();
    }
    public void EndTurn()
    {
        GameManager.Instance.EndTurn();
    }
    public void OpenDiscard()
    {
        //GameManager.Instance.Discard.SetActive(true);
        //gameObject.SetActive(false);
    }
}
