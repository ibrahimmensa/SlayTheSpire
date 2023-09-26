using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public GameObject cardContainer;
    public GameObject CardContainerRef;
    public GameObject CardDestory;
    public Transform cardParent;


    public GameObject PlayerCount;
    public Image PlayerHealth;
    public Text PlayerHelthTxt;

    public Animator Enemy;

    public Image EnemyHealth;
    public Text HealthTxt;


    public GameObject LevelComplete;
    public Text TurnTxt;

    public CardsManagement cardsManagement;
    public GameObject CurseIndicator;
    public GameObject DefanceIndicator;
    public GameObject blocked;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        // Check if an instance already exists.
        if (_instance != null && _instance != this)
        {
            // Destroy this instance if it's not the first one.
            Destroy(this.gameObject);
            return;
        }

        // Set this instance as the singleton instance.
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //CardContainerRef = Instantiate(cardContainer, cardParent);
        //CardContainerRef.GetComponent<CardContainer>().EnemyHealth = EnemyHealth;
        //CardContainerRef.GetComponent<CardContainer>().HealthTxt = HealthTxt;
        //CardContainerRef.GetComponent<CardContainer>().PlayerCount = Card_PlayerCount;
        //CardContainerRef.transform.SetSiblingIndex(3);
        //CardDestory.SetActive(true);
        TurnTxt.text = "Player Turn";
    }

    // Update is called once per frame
    void Update()
    {
        if(CardContainerRef.GetComponent<CardContainer>().playerCount == 0)
        {
            CardContainerRef.GetComponent<CardContainer>().playerCount = 3;
            CardDestory.SetActive(false);

            if (LevelComplete.activeSelf)
                return;

            StartCoroutine(PlayerTurn());
        }
    }
    IEnumerator PlayerTurn()
    {

        if (cardsManagement.CurseActivated)
        {
            cardsManagement.Cursevalue--;
            if (cardsManagement.Cursevalue == 0)
            {
                cardsManagement.CurseActivated = false;
                CurseIndicator.SetActive(false);
            }
        }
        CardContainerRef.SetActive(false);
        PlayerCount.SetActive(false);
        TurnTxt.text = "Enemy turn";
        Enemy.enabled = true;
        yield return new WaitForSeconds(2.0f);
        if (cardsManagement.DefanceActivated)
        {
            cardsManagement.defanceValue--;
            if (cardsManagement.defanceValue == 0)
            {
                cardsManagement.DefanceActivated = false;
                DefanceIndicator.SetActive(false);
            }
            blocked.SetActive(true);
        }
        else
        {
            PlayerHealth.fillAmount = PlayerHealth.fillAmount - 0.3f;
            PlayerHelthTxt.text = PlayerHealth.fillAmount * 100 + "/100";
        }
        yield return new WaitForSeconds(0.2f);
        TurnTxt.text = "Player Turn";
        CardContainerRef.SetActive(true);
        PlayerCount.SetActive(true);
        CardContainerRef.GetComponent<CardContainer>().PlayerCount.text = CardContainerRef.GetComponent<CardContainer>().playerCount.ToString();
    }
    public void Next()
    {
        SceneManager.LoadScene(1);
    }
    public void EndTurn()
    {
        CardContainerRef.GetComponent<CardContainer>().playerCount = 0;
    }
}
