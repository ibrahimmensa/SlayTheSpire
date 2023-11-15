using events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace demo {
    public class CardDestroyer : MonoBehaviour {
        public CardContainer container;
        public SoundManager SoundManager;
        public Sounds Sounds;
        public InventoryCardManager InventoryCardManager;
        public ScreenAnimations ScreenAnimations;
        public Cards[] toSaveCards;
        private void OnEnable()
        {
            //container = GameManager.Instance.CardContainerRef.GetComponent<CardContainer>();
        }
        public void OnCardDestroyed(CardPlayed evt) {
            var CardM = evt.card.GetComponent<CardManager>();
            //InventoryCardManager.lootedCards.Add(CardM.gameObject);
            if (container.playerCount >= CardM.Magic_power  )
            {
                SoundManager.playSound(Sounds.AttackSounds[Random.Range(0, 2)]);
                container.shake.enabled = true;
                if (CardM.Damage)
                {
                    if (GameManager.Instance.activeEnemy.EnemyHealth.fillAmount > 0)
                    {
                        if(container.CardManagement.CurseActivated)
                        {
                            GameManager.Instance.activeEnemy.EnemyHealth.fillAmount -= (CardM.EnemyDamage + 0.1f);
                        }
                        else
                        {
                            GameManager.Instance.activeEnemy.EnemyHealth.fillAmount -= CardM.EnemyDamage;
                        }
                        GameManager.Instance.activeEnemy.HealthTxt.text = GameManager.Instance.activeEnemy.EnemyHealth.fillAmount * 100 + "/100";

                        container.playerCount -= CardM.Magic_power;
                        container.PlayerCount.text = container.playerCount.ToString();
                        if (GameManager.Instance.activeEnemy.EnemyHealth.fillAmount <= 0)
                        {
                            //write file to save cards data





                            GameManager.Instance.CurseIndicator.SetActive(false);
                            GameManager.Instance.DefanceIndicator.SetActive(false);
                            GameManager.Instance.activeEnemy.gameObject.SetActive(false);
                            GameManager.Instance.LevelComplete.SetActive(true);
                            PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("Levels") + 1);
                            container.gameObject.SetActive(false);
                           
                        }
                        else
                        {
                            Instantiate(ScreenAnimations.Animations[Random.RandomRange(0, ScreenAnimations.Animations.Length)], GameManager.Instance.gameObject.transform);
                        }
                    }
                }
                else if(CardM.Defence)
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
                else if(CardM.Curse)
                {
                    if (GameManager.Instance.activeEnemy.EnemyHealth.fillAmount > 0)
                    {
                        container.CardManagement.CurseActivated = true;
                        container.CardManagement.Cursevalue = CardM.CurseEffect;
                        GameManager.Instance.CurseIndicator.SetActive(true);
                        container.playerCount -= CardM.Magic_power;
                        container.PlayerCount.text = container.playerCount.ToString();
                    }
                }
                PlayerPrefs.SetString("Card", CardM.cardName.text);
                //InventoryCardManager.discard_Pile.Add(PlayerPrefs.GetString("Card"));
                InventoryCardManager.Discardpile.CardName.Add(CardM.cardName.text.ToString());
                InventoryCardManager.Discardpile.CardSprite.Add(CardM.cardSprite);

                SaveCards(CardM.cardName.text);
                
                //InventoryCardManager.DP_Details.Add(CardM.GetComponent<Cards>());

                CardM.gameObject.transform.SetParent(GameManager.Instance.discard_Pile_Contant.transform);
                container.DestroyCard(evt.card);
            }
            else
            {
                container.ErrorMsg.SetActive(true);
            }
            
        }

        public void SaveCards(string name)
        {
            switch (name)
            {
                case "Attack_01":
                    {
                        InventoryCardManager.DP_Details.Add(toSaveCards[0]);
                        //InventoryCardManager.DP.Add(InventoryCardManager.Discardpile);
                        break;
                    }
                case "Attack_02":
                    {
                        InventoryCardManager.DP_Details.Add(toSaveCards[1]);
                        //InventoryCardManager.DP.Add(InventoryCardManager.Discardpile);
                        break;
                    }
                case "Attack_03":
                    {
                        InventoryCardManager.DP_Details.Add(toSaveCards[2]);
                        //InventoryCardManager.DP.Add(InventoryCardManager.Discardpile);
                        break;
                    }
                case "Curse_01":
                    {
                        InventoryCardManager.DP_Details.Add(toSaveCards[3]);
                        //InventoryCardManager.DP.Add(InventoryCardManager.Discardpile);
                        break;
                    }
                case "Curse_02":
                    {
                        InventoryCardManager.DP_Details.Add(toSaveCards[4]);
                        //InventoryCardManager.DP.Add(InventoryCardManager.Discardpile);
                        break;
                    }
                case "Curse_03":
                    {
                        InventoryCardManager.DP_Details.Add(toSaveCards[5]);
                       // InventoryCardManager.DP.Add(InventoryCardManager.Discardpile);
                        break;
                    }
                case "Defense_01":
                    {
                        InventoryCardManager.DP_Details.Add(toSaveCards[6]);
                        //InventoryCardManager.DP.Add(InventoryCardManager.Discardpile);
                        break;
                    }
                case "Defense_02":
                    {
                        InventoryCardManager.DP_Details.Add(toSaveCards[7]);
                       // InventoryCardManager.DP.Add(InventoryCardManager.Discardpile);
                        break;
                    }
                case "Defense_03":
                    {
                        InventoryCardManager.DP_Details.Add(toSaveCards[8]);
                       // InventoryCardManager.DP.Add(InventoryCardManager.Discardpile);
                        break;
                    }
            }
        }
    }
}
