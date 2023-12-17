using events;
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
                    Defense(CardM);
                    Debug.Log("Defance: " + CardM.BlockedDamage);
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
            //else if (CardM.Defence)
            //{
            //    InventoryCardManager.DP_Details.Add(toSaveCards.DefanceCards[CardM.CardIndex]);
            //}
            //else if (CardM.Curse)
            //{
            //    InventoryCardManager.DP_Details.Add(toSaveCards.curseCards[CardM.CardIndex]);
            //}
            //else if (CardM.Medicated)
            //{
            //    InventoryCardManager.DP_Details.Add(toSaveCards.MedicatedCards[CardM.CardIndex]);
            //}
        }
        public void Attack(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth.fillAmount > 0)
            {
                if (container.CardManagement.CurseActivated)
                {
                    GameManager.Instance.activeEnemy.EnemyHealth.fillAmount -= (CardM.EnemyDamage + 0.1f);
                }
                else
                {
                    GameManager.Instance.activeEnemy.EnemyHealth.fillAmount -= CardM.EnemyDamage;
                }
                float h = GameManager.Instance.activeEnemy.EnemyHealth.fillAmount * 100;
                GameManager.Instance.activeEnemy.HealthTxt.text = (int)h + "/100";


                container.playerCount -= CardM.Magic_power;
                container.PlayerCount.text = container.playerCount.ToString();
                if (GameManager.Instance.activeEnemy.EnemyHealth.fillAmount <= 0)
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
            if (GameManager.Instance.activeEnemy.EnemyHealth.fillAmount > 0)
            {
                container.CardManagement.DefanceActivated = true;
                container.CardManagement.defanceValue = (int)CardM.BlockedDamage;
                GameManager.Instance.DefanceIndicator.SetActive(true);
                container.playerCount -= CardM.Magic_power;
                container.PlayerCount.text = container.playerCount.ToString();
            }
        }
        public void Curse(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth.fillAmount > 0)
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
            if (GameManager.Instance.activeEnemy.EnemyHealth.fillAmount > 0)
            {
                container.CardManagement.DefanceActivated = true;
                container.CardManagement.defanceValue = (int)CardM.BlockedDamage;
                GameManager.Instance.DefanceIndicator.SetActive(true);
                container.playerCount -= CardM.Magic_power;
                container.PlayerCount.text = container.playerCount.ToString();


                if (container.CardManagement.CurseActivated)
                {
                    GameManager.Instance.activeEnemy.EnemyHealth.fillAmount -= (CardM.EnemyDamage + 0.1f);
                }
                else
                {
                    GameManager.Instance.activeEnemy.EnemyHealth.fillAmount -= CardM.EnemyDamage;
                }
                float h = GameManager.Instance.activeEnemy.EnemyHealth.fillAmount * 100;
                GameManager.Instance.activeEnemy.HealthTxt.text = (int)h + "/100";


                if (GameManager.Instance.activeEnemy.EnemyHealth.fillAmount <= 0)
                {
                    LevelComplete();
                }
                else
                {
                    Effects();
                }
            }
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
