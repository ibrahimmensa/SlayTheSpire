using events;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace demo {
    public class CardDestroyer : MonoBehaviour {
        public CardContainer container;
        public SoundManager SoundManager;
        public Sounds Sounds;
        public InventoryCardManager InventoryCardManager;
        public ScreenAnimations ScreenAnimations;
        public Cards toSaveCards;
        public CharactersManagment CM;
        public CardsManagement CardsManager;
        private void OnEnable()
        {
            container.CardManagement.Cursevalue = 0;
            container.CardManagement.defanceValue = 0;
            container.CardManagement.CurseActivated = false;
            container.CardManagement.DefanceActivated = false;
        }
        public void OnCardDestroyed(CardPlayed evt) {
            var CardM = evt.card.GetComponent<CardManager>();
            //InventoryCardManager.lootedCards.Add(CardM.gameObject);
            if (container.playerCount >= CardM.Magic_power  )
            {
                SoundManager.playSound(Sounds.AttackSounds[Random.Range(0, 2)]);
                container.shake.enabled = true;
                if (CardM.Attack)
                {
                    Attack(CardM);
                    Debug.Log("Attack: " + CardM.EnemyDamage);
                }
                else if(CardM.Defence)
                {
                    if(!GameManager.Instance.blockDefanceCards)
                    {
                        Defense(CardM);
                        Debug.Log("Defance: " + CardM.BlockedDamage);
                    }
                    else
                    {
                        container.DefanseError.SetActive(true);
                    }
                }
                else if(CardM.Curse)
                {
                    Curse(CardM);
                    Debug.Log("Curse: " + CardM.CurseEffect);
                }
                else if(CardM.AD_Cards)
                {
                    AD_cards(CardM);
                    Debug.Log("Attack: " + CardM.EnemyDamage+""+ "Defance: " + CardM.BlockedDamage);
                }
                else if(CardM.Cash_cards)
                {
                    Cash_cards(CardM);
                    Debug.Log("Attack: " + CardM.EnemyDamage + "" + "Defance: " + CardM.ReducePlayerHelth + "" + "MP: " + CardM.IncreesedMagicPower);
                }
                else if (CardM.Reshuffle_cards)
                {
                    StartCoroutine(Resguffle_Handscards(CardM));
                }
                else if (CardM.Medicated)
                {
                    Medicated(CardM);
                }
                save(CardM);
                container.DestroyCard(evt.card);
            }
            else
            {
                container.ErrorMsg.SetActive(true);
            }
            
        }
        public void save(CardManager CardM)
        {
            PlayerPrefs.SetString("Card", CardM.cardName.text);
            InventoryCardManager.Discardpile.CardName.Add(CardM.cardName.text.ToString());
            InventoryCardManager.Discardpile.CardSprite.Add(CardM.cardSprite);
            saveCard(CardM);
            CardM.gameObject.transform.SetParent(GameManager.Instance.discard_Pile_Contant.transform);
        }
        public void saveCard(CardManager CardM)
        {
            if (CardM.Attack)
            {
                InventoryCardManager.DP_Details.Add(toSaveCards.AttackCards[CardM.CardIndex]);
            }
        }
        public void Attack(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth > 0)
            {
                if (CardM.cardName.text == "Mouse")
                {
                    if (GameManager.Instance.RatCard == 0)
                    {
                        GameManager.Instance.RatCard++;
                    }
                    else
                    {
                        CardsManager.twoMouseCardsUsed = true;
                    }
                }
                else if (CardM.cardName.text == "Red Parrot")
                {
                   // GameManager.Instance.redParrotActivated = true;
                    CardM.EnemyDamage *= 2;
                }
                else if (CardM.cardName.text == "Cleaver")
                {
                    GameManager.Instance.blockDefanceCards = true;
                }
                else if (CardM.cardName.text == "Dual lil shooters")
                {
                    GameManager.Instance.dualShoter = true;
                    CardM.EnemyDamage *= 2;
                }
                else if (CardM.cardName.text == "Gold Shotgun")
                {
                    GameManager.Instance.PlayerHealth -= CardM.ReducePlayerHelth;
                    GameManager.Instance.PlayerHelthTxt.text = GameManager.Instance.PlayerHealth.ToString() + "/20";
                }
                else if (CardM.cardName.text == "Gold Scorpian")
                {
                    CardM.Magic_power += CardM.IncreesedMagicPower;
                    GameManager.Instance.PlayerHealth -= CardM.ReducePlayerHelth;
                    GameManager.Instance.PlayerHelthTxt.text = GameManager.Instance.PlayerHealth.ToString() + "/20";
                    Debug.Log("Gold Scrorpian played !!");
                }
                else if (CardM.cardName.text == "Silver Scrorpian")
                {
                    CardM.Magic_power = CardM.Magic_power + CardM.IncreesedMagicPower;
                    GameManager.Instance.PlayerHealth -= CardM.ReducePlayerHelth;
                    GameManager.Instance.PlayerHelthTxt.text = GameManager.Instance.PlayerHealth.ToString() + "/20";
                    Debug.Log("Silver Scrorpian played !!");
                }

                GameManager.Instance.activeEnemy.EnemyHealth -= CardM.EnemyDamage;
                GameManager.Instance.activeEnemy.HealthTxt.text = GameManager.Instance.activeEnemy.EnemyHealth.ToString() + "/50";
                container.playerCount -= CardM.Magic_power;
                container.PlayerCount.text = container.playerCount.ToString();
                if (GameManager.Instance.activeEnemy.EnemyHealth <= 0)
                {
                    LevelComplete();
                }
                else
                {
                    Effects();
                }
            }
        }
        public void Defense(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth > 0)
            {
                container.CardManagement.DefanceActivated = true;
                if(GameManager.Instance.dualShoter)
                {
                    CardM.BlockedDamage *= 2;
                }
                container.CardManagement.defanceValue = (int)CardM.BlockedDamage;
                GameManager.Instance.DefanceIndicator.SetActive(true);
                container.playerCount -= CardM.Magic_power;
                container.PlayerCount.text = container.playerCount.ToString();
            }
        }
        public void Curse(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth > 0)
            {
                Debug.Log("curse value: " + CardM.CurseEffect);
                container.CardManagement.CurseActivated = true;
                container.CardManagement.Cursevalue = CardM.CurseEffect;
                GameManager.Instance.CurseIndicator.SetActive(true);
                container.playerCount -= CardM.Magic_power;
                container.PlayerCount.text = container.playerCount.ToString();
            }
        }
        public void AD_cards(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth > 0)
            {
                container.CardManagement.DefanceActivated = true;
                if (GameManager.Instance.dualShoter)
                {
                    CardM.BlockedDamage *= 2;
                }
                container.CardManagement.defanceValue = (int)CardM.BlockedDamage;
                GameManager.Instance.DefanceIndicator.SetActive(true);
                container.playerCount -= CardM.Magic_power;
                container.PlayerCount.text = container.playerCount.ToString();


                if (container.CardManagement.CurseActivated)
                {
                    GameManager.Instance.activeEnemy.EnemyHealth -= (CardM.EnemyDamage +1);
                }
                else
                {
                    if (GameManager.Instance.dualShoter)
                    {
                        CardM.EnemyDamage *= 2;
                    }
                    GameManager.Instance.activeEnemy.EnemyHealth -= CardM.EnemyDamage;
                }
                GameManager.Instance.activeEnemy.HealthTxt.text = GameManager.Instance.activeEnemy.EnemyHealth.ToString() + "/50";


                if (GameManager.Instance.activeEnemy.EnemyHealth <= 0)
                {
                    LevelComplete();
                }
                else
                {
                    Effects();
                }
            }
        }

        public void Cash_cards(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth > 0)
            {
                
                if (GameManager.Instance.dualShoter)
                {
                    CardM.ReducePlayerHelth *= 2;
                }
                container.CardManagement.defanceValue = (int)CardM.BlockedDamage;

                if(CardM.BlockedDamage > 0)
                    container.CardManagement.DefanceActivated = true;

                GameManager.Instance.DefanceIndicator.SetActive(true);
                container.playerCount -= CardM.Magic_power;
                container.PlayerCount.text = container.playerCount.ToString();


                GameManager.Instance.activeEnemy.EnemyHealth -= CardM.EnemyDamage;
                GameManager.Instance.activeEnemy.HealthTxt.text = GameManager.Instance.activeEnemy.EnemyHealth.ToString() +"/50";


                if (GameManager.Instance.activeEnemy.EnemyHealth <= 0)
                {
                    LevelComplete();
                }
                else
                {
                    Effects();
                }
            }
        }
        IEnumerator Resguffle_Handscards(CardManager CardM)
        {
            container.playerCount -= CardM.Magic_power;
            if (container.playerCount > 0)
            {
                GameManager.Instance.BtnOff();
                GameManager.Instance.CardContainerRef.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                GameManager.Instance.CardContainerRef.SetActive(true);
                container.PlayerCount.text = container.playerCount.ToString();
                yield return new WaitForSeconds(0.1f);
                GameManager.Instance.BtnOn();
            }
        }
        public void Medicated(CardManager CardM)
        {
            GameManager.Instance.PlayerHealth += CardM.Medication;
            if (GameManager.Instance.PlayerHealth > 20)
                GameManager.Instance.PlayerHealth = 20;
            GameManager.Instance.PlayerHelthTxt.text = GameManager.Instance.PlayerHealth.ToString() + "/20";
        }
        public void LevelComplete()
        {
            CM.LoadLevel++;
            GameManager.Instance.CurseIndicator.SetActive(false);
            GameManager.Instance.DefanceIndicator.SetActive(false);
            GameManager.Instance.activeEnemy.gameObject.SetActive(false);
            GameManager.Instance.LevelComplete.SetActive(true);
            if (CM.LoadLevel > PlayerPrefs.GetInt("Levels", 0))
            {
                PlayerPrefs.SetInt("Levels", CM.LoadLevel);
            }
            container.gameObject.SetActive(false);
        }
        public void Effects()
        {
            Instantiate(ScreenAnimations.Animations[Random.Range(0, ScreenAnimations.Animations.Length)], GameManager.Instance.gameObject.transform);
        }
    }
}
