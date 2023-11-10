using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [Header("ScriptableObject Refrances")]
    public Enemies enemies;
    public Sounds Sounds;
    public ScreenAnimations ScreenAnimations;
    public CardsManagement cardsManagement;


    [Header("Cards")]
    public GameObject cardContainer;
    public GameObject CardContainerRef;
    public GameObject CardDestory;
    public Transform cardParent;


    [Header("Player")]
    public GameObject PlayerCount;
    public Image PlayerHealth;
    public Text PlayerHelthTxt;



    [Header("Screens")]
    public GameObject LevelComplete;
    public GameObject LevelFailed;


    [Header("Screens")]
    public GameObject CurseIndicator;
    public GameObject DefanceIndicator;
    public GameObject blocked;

    public Enemies_Interactions activeEnemy;
    public SoundManager SoundManager;
    public Image BG;
    public int num;
    public Text DiscardpileTxt;

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
    private void OnEnable()
    {
        activeEnemy = Instantiate(enemies.All_Enemies[(PlayerPrefs.GetInt("Levels", 0))], transform);
        num = Random.Range(0, Sounds.Background_Music.Length);
        BG.sprite = Sounds.BGs[num];
        SoundManager.bGM.clip = Sounds.Background_Music[num];
        SoundManager.bGM.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        activeEnemy.TurnTxt.text = "Player Turn";
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
            if (LevelFailed.activeSelf)
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
        activeEnemy.TurnTxt.text = "Enemy turn";
        yield return new WaitForSeconds(2.0f);
        activeEnemy.enemyAnimator.SetBool("Attack", true);
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
            PlayerHealth.fillAmount -= 0.3f;
            PlayerHelthTxt.text = PlayerHealth.fillAmount * 100 + "/100";
            if(PlayerHealth.fillAmount <= 0)
            {
                activeEnemy.gameObject.SetActive(false);
                LevelFailed.SetActive(true);
            }
        }
        //Instantiate(ScreenAnimations.Animations[0],transform);
        SoundManager.playSound(Sounds.AttackSounds[Random.Range(0, 2)]);
        yield return new WaitForSeconds(0.2f);
        activeEnemy.enemyAnimator.SetBool("Attack", false);
        CardContainerRef.SetActive(true);
        PlayerCount.SetActive(true);
        CardContainerRef.GetComponent<CardContainer>().PlayerCount.text = CardContainerRef.GetComponent<CardContainer>().playerCount.ToString();
        activeEnemy.TurnTxt.text = "Player Turn";
    }
    public void Next()
    {
        SceneManager.LoadScene(1);
    }
    public void EndTurn()
    {
        CardContainerRef.GetComponent<CardContainer>().playerCount = 0;
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
