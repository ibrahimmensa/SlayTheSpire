using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IDataPersistence
{

    private static GameManager _instance;

    [Header("ScriptableObject Refrances")]
    public Enemies enemies;
    public Sounds Sounds;
    public ScreenAnimations ScreenAnimations;
    public CardsManagement cardsManagement;
    public CharactersManagment CM;


    [Header("Save Data")]
    public DataPersistenceManager DataPersistenceManager;


    public int RatCard;
   // public bool redParrotActivated;
    public bool blockDefanceCards;
    public bool dualShoter;

    [Header("Cards")]
    public GameObject cardContainer;
    public GameObject CardContainerRef;
    public GameObject CardDestory;
    public Transform cardParent;


    [Header("Player")]
    public GameObject PlayerCount;
    public int PlayerHealth;
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
    public GameObject discard_Pile_Contant;
    public GameObject Discard;

    public int deathCount;

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
        activeEnemy = Instantiate(enemies.All_Enemies[CM.LoadLevel], transform);
        num = Random.Range(0, Sounds.Background_Music.Length);
        BG.sprite = Sounds.BGs[num];
        SoundManager.bGM.clip = Sounds.Background_Music[num];
        SoundManager.bGM.Play();
        RatCard = 0;
       // redParrotActivated = false;
        blockDefanceCards = false;
        dualShoter = false;
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
            IfCurse();
        }
        CardContainerRef.SetActive(false);
        PlayerCount.SetActive(false);
        activeEnemy.TurnTxt.text = "Enemy turn";
        yield return new WaitForSeconds(2.0f);
        activeEnemy.enemyAnimator.SetBool("Attack", true);
        yield return new WaitForSeconds(2.0f);
        if (cardsManagement.DefanceActivated)
        {
            IfDefanse();
        }
        else
        {
            ApplyDanageToPlayer();
        }


        Instantiate(enemies.All_Enemies[CM.LoadLevel].Effect,transform);
        SoundManager.playSound(Sounds.AttackSounds[Random.Range(0, 2)]);
        yield return new WaitForSeconds(0.2f);
        activeEnemy.enemyAnimator.SetBool("Attack", false);
        CardContainerRef.SetActive(true);
        PlayerCount.SetActive(true);
        CardContainerRef.GetComponent<CardContainer>().PlayerCount.text = CardContainerRef.GetComponent<CardContainer>().playerCount.ToString();
        activeEnemy.TurnTxt.text = "Player Turn";
        Invoke(nameof(BtnOn), 2f);
        CardDestory.SetActive(true);
    }


    //Funtionality
    //---------------------------------------------------------------------------------------------------------------------------------------------
    public void IfCurse()
    {
        cardsManagement.Cursevalue--;
        if (cardsManagement.Cursevalue <= 0)
        {
            cardsManagement.Cursevalue = 0;
            cardsManagement.CurseActivated = false;
            CurseIndicator.SetActive(false);
        }
    }
    public void IfDefanse()
    {
        cardsManagement.defanceValue--;
        if (cardsManagement.defanceValue <= 0)
        {
            cardsManagement.defanceValue = 0;
            cardsManagement.DefanceActivated = false;
            DefanceIndicator.SetActive(false);
        }
        blocked.SetActive(true);
    }
    public void ApplyDanageToPlayer()
    {
        CardContainerRef.GetComponent<CardContainer>().shake.enabled = true;
        PlayerHealth -= 3;
        PlayerHelthTxt.text = PlayerHealth.ToString() + "/20";
        if (PlayerHealth <= 0)
        {
            PlayerDead();
        }
    }
    public void PlayerDead()
    {
        activeEnemy.gameObject.SetActive(false);
        LevelFailed.SetActive(true);
        DataPersistenceManager.gameData.deathCount++;
        deathCount++;
        CM.TotalDeaths += deathCount;
    }


    //Buttons
    //------------------------------------------------------------------------------------------------------------------------------
    public void Next()
    {
        DataPersistenceManager.SaveGame();
        SceneManager.LoadScene(1);
    }
    public void EndTurn()
    {
        CardContainerRef.GetComponent<CardContainer>().playerCount = 0;
        activeEnemy.endTurn.interactable = false;
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
    }
    public void discardBack()
    {
        activeEnemy.gameObject.SetActive(true);
        for(int i=0;i<discard_Pile_Contant.transform.childCount;i++)
        {
            Destroy(discard_Pile_Contant.transform.GetChild(i));
        }
    }

    public void BtnOn()
    {
        activeEnemy.endTurn.interactable = true;
    }
    public void BtnOff()
    {
        activeEnemy.endTurn.interactable = false;
    }
    //Data Saving
    //----------------------------------------------------------------------------------------------------------------------------------
    public void loadData(GameData data)
    {
        //CM.TotalDeaths = data.deathCount;
        //deathCount = CM.TotalDeaths;
    }

    public void SaveData(ref GameData data)
    {
        //data.deathCount = CM.TotalDeaths;
    }
}
