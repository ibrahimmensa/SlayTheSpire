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
            StartCoroutine(PlayerTurn());
        }
    }
    IEnumerator PlayerTurn()
    {
        CardContainerRef.SetActive(false);
        PlayerCount.SetActive(false);
        TurnTxt.text = "Enemy turn";
        Enemy.enabled = true;
        yield return new WaitForSeconds(2.0f);
        PlayerHealth.fillAmount = PlayerHealth.fillAmount - 0.3f;
        PlayerHelthTxt.text = PlayerHealth.fillAmount * 100 + "/100";
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
}
