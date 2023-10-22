using events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace demo {
    public class CardDestroyer : MonoBehaviour {
        public CardContainer container;

        private void OnEnable()
        {
            //container = GameManager.Instance.CardContainerRef.GetComponent<CardContainer>();
        }
        public void OnCardDestroyed(CardPlayed evt) {
            var CardM = evt.card.GetComponent<CardManager>();
            if (container.playerCount >= CardM.Magic_power  )
            {
                container.shake.enabled = true;
                if (CardM.Damage)
                {
                    if (container.EnemyHealth.fillAmount > 0)
                    {
                        if(container.CardManagement.CurseActivated)
                        {
                            container.EnemyHealth.fillAmount -= (CardM.EnemyDamage + 0.1f);
                        }
                        else
                        {
                            container.EnemyHealth.fillAmount -= CardM.EnemyDamage;
                        }
                        container.HealthTxt.text = container.EnemyHealth.fillAmount * 100 + "/100";

                        container.playerCount -= CardM.Magic_power;
                        container.PlayerCount.text = container.playerCount.ToString();
                        if (container.EnemyHealth.fillAmount <= 0)
                        {
                            GameManager.Instance.CurseIndicator.SetActive(false);
                            GameManager.Instance.DefanceIndicator.SetActive(false);
                            GameManager.Instance.LevelComplete.SetActive(true);
                            PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("Levels") + 1);
                            container.gameObject.SetActive(false);
                        }
                    }
                }
                else if(CardM.Defence)
                {
                    if (container.EnemyHealth.fillAmount > 0)
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
                    if (container.EnemyHealth.fillAmount > 0)
                    {
                        container.CardManagement.CurseActivated = true;
                        container.CardManagement.Cursevalue = CardM.CurseEffect;
                        GameManager.Instance.CurseIndicator.SetActive(true);
                        container.playerCount -= CardM.Magic_power;
                        container.PlayerCount.text = container.playerCount.ToString();
                    }
                }
                
                container.DestroyCard(evt.card);
            }
            else
            {
                container.ErrorMsg.SetActive(true);
            }
            
        }
    }
}
