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
            if (container.EnemyHealth.fillAmount > 0)
            {
                container.EnemyHealth.fillAmount = container.EnemyHealth.fillAmount - 0.25f;
                container.HealthTxt.text = container.EnemyHealth.fillAmount * 100 + "/100";
                container.playerCount--;
                container.PlayerCount.text = container.playerCount.ToString();
                if (container.EnemyHealth.fillAmount == 0)
                {
                    GameManager.Instance.LevelComplete.SetActive(true);
                    container.gameObject.SetActive(false);
                }
            }
            container.DestroyCard(evt.card);
        }
    }
}
