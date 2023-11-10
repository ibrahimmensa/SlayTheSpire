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

                container.DestroyCard(evt.card);
            }
            else
            {
                container.ErrorMsg.SetActive(true);
            }
            
        }
    }
}
