using events;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace demo {
    public class CardDestroyer : MonoBehaviour {
        public CardContainer container;
        public SoundManager SoundManager;
        public Sounds Sounds;
        //public InventoryCardManager InventoryCardManager;
        public ScreenAnimations ScreenAnimations;
        public Cards toSaveCards;
        public CharactersManagment CM;
        public CardsManagement CardsManager;

        //bool for check next moves
        bool nextMove, NextTurnDamage,NextTurnDefence,twoTimesValuse;
        //int for turns counts
        int turn;
        private void OnEnable()
        {

        }
        public void OnCardDestroyed(CardPlayed evt) {
            var CardM = evt.card.GetComponent<CardManager>();
            //InventoryCardManager.lootedCards.Add(CardM.gameObject);
            if (container.playerCount >= CardM.Magic_power  )
            {
                //container.shake.enabled = true;
                if (CardM.Attack)
                {
                    if (GameManager.Instance.enemydefenceActivated || GameManager.Instance.activeEnemy.EnemyArmors > 0)
                    {
                        GameManager.Instance.blocked.SetActive(true);
                        container.playerCount -= CardM.Magic_power;
                        container.PlayerCount.text = container.playerCount.ToString();
                        GameManager.Instance.enemydefenceActivated = false;
                        if (GameManager.Instance.activeEnemy.EnemyArmors > 0)
                            GameManager.Instance.activeEnemy.EnemyArmors--;
                        return;
                    }
                    Attack(CardM);
                    GameManager.Instance.LogMsg("Player Attack: ", CardM.EnemyDamage, Color.black);
                    UnityEngine.Debug.Log("Attack: " + CardM.EnemyDamage);
                }
                else if (CardM.Defence)
                {
                    if (!GameManager.Instance.blockDefanceCards)
                    {
                        Defense(CardM);
                        GameManager.Instance.LogMsg("Player Defence: ", CardM.BlockedDamage, Color.black);
                        UnityEngine.Debug.Log("Defance: " + CardM.BlockedDamage);
                    }
                    else
                    {
                        container.DefanseError.SetActive(true);
                        return;
                    }
                }
                //else if(CardM.Curse)
                //{
                //    Curse(CardM);
                //    UnityEngine.Debug.Log("Curse: " + CardM.CurseEffect);
                //}
                else if (CardM.AD_Cards)
                {
                    AD_cards(CardM);
                    GameManager.Instance.LogMsg("Player Attack: ", CardM.EnemyDamage, Color.black);
                    GameManager.Instance.LogMsg("Player defence: ", CardM.BlockedDamage, Color.black);
                    UnityEngine.Debug.Log("Attack: " + CardM.EnemyDamage + "" + "Defance: " + CardM.BlockedDamage);
                }
                else if (CardM.Cash_cards)
                {
                    Cash_cards(CardM);
                    GameManager.Instance.LogMsg("Player Attack: ", CardM.EnemyDamage, Color.black);
                    GameManager.Instance.LogMsg("Player defence: ", CardM.BlockedDamage, Color.black);
                    UnityEngine.Debug.Log("Attack: " + CardM.EnemyDamage + "" + "Defance: " + CardM.ReducePlayerHelth + "" + "MP: " + CardM.IncreesedMagicPower);
                }
                else if (CardM.Reshuffle_cards)
                {
                    StartCoroutine(Resguffle_Handscards(CardM));
                    GameManager.Instance.LogMsg("Player reshuffle: ", 1, Color.black);
                }
                else if (CardM.Medicated)
                {
                    Medicated(CardM);
                }
                else if (CardM.Support)
                {
                    Medicated(CardM);
                }
                    container.DestroyCard(evt.card);
                GameManager.Instance.activeEnemy.dpText++;
                if(PlayerPrefs.GetFloat("effect", 1) > 0) { SoundManager.playSound(Sounds.AttackSounds[Random.Range(0, 2)]); }
            }
            else
            {
                container.ErrorMsg.SetActive(true);
            }
            
        }

        //forenemy

        public void enemyattack(CardsData CardM)
        {
            //container.shake.enabled = true;
            if (CardM.Attack)
            {
                AttackPlayer(CardM);
            }
            else if (CardM.Defence)
            {
                if (GameManager.Instance.enemydefenceActivated)
                {
                    GameManager.Instance.LogMsg("EnemyDefence: ",1, Color.red);
                    return;
                }
                else
                {
                    GameManager.Instance.LogMsg("EnemyDefence: ", 1, Color.red);
                    GameManager.Instance.enemydefenceActivated = true;
                }
            }
          
            else if (CardM.Medicated)
            {
                MedicatedPlayer(CardM);
            }
            GameManager.Instance.activeEnemy.dpText++;

        }
        void AttackPlayer(CardsData CardM)
        {
            int attackValue = Random.Range(CardM.Attack_max, CardM.Attack_min);
            UnityEngine.Debug.Log("Attack: " + attackValue);
            GameManager.Instance.PlayerHealth -= attackValue;
            GameManager.Instance.ApplyDanageToPlayer();
            GameManager.Instance.LogMsg("EnemyAttack: ", attackValue, Color.red);
            if (nextMove)
            {
                AttackOnEnemy(CardM.checkMoveDamage);
                ValuesUpdate();
                nextMove = false;
            }
        }
       void DefencePlayer(CardsData CardM)
        {
            int DefenceValue = Random.Range(CardM.Defense_max, CardM.Defense_max);
            UnityEngine.Debug.Log("defence: " + DefenceValue);
            GameManager.Instance.PlayerHealth -= DefenceValue;
            GameManager.Instance.ApplyDanageToPlayer();
        }
        void MedicatedPlayer(CardsData CardM)
        {
            int HealValue = Random.Range(CardM.PlayerHeal_min, CardM.PlayerHeal_min);
            GameManager.Instance.LogMsg("EnemyHeal: ", HealValue, Color.red);
            UnityEngine.Debug.Log("heal: " + HealValue);
            GameManager.Instance.activeEnemy.EnemyHealth += HealValue;
            if (GameManager.Instance.activeEnemy.EnemyHealth > 50)
                GameManager.Instance.activeEnemy.EnemyHealth = 50;
            GameManager.Instance.activeEnemy.HealthTxt.text = GameManager.Instance.activeEnemy.EnemyHealth.ToString() + "/50";
            GameManager.Instance.ApplyDanageToPlayer();
        }



        // for player
        void Attack(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth > 0)
            {
                container.playerCount -= CardM.Magic_power;

                switch(CardM.cardName.text)
                {
                    case "Mouse":
                        if (GameManager.Instance.RatCard == 0)
                        {
                            GameManager.Instance.RatCard++;
                        }
                        else
                        {
                            CardsManager.twoMouseCardsUsed = true;
                        }
                        break;

                    case "Red Parrot":
                        CardM.EnemyDamage *= 2;
                        break;

                    case "Cleaver":
                        GameManager.Instance.blockDefanceCards = true;
                        break;

                    case "Dual lil shooters":
                        GameManager.Instance.dualShoter = true;
                        break;

                    case "Gold Shotgun":
                        break;

                    case "Gold Scorpian":
                        CardM.Magic_power += CardM.IncreesedMagicPower;
                        BackAttackOnPlayer(CardM.ReducePlayerHelth);
                        GameManager.Instance.PlayerHelthTxt.text = GameManager.Instance.PlayerHealth.ToString() + "/20";
                        break;

                    case "Silver Scrorpian":
                        CardM.Magic_power = CardM.Magic_power + CardM.IncreesedMagicPower;
                        BackAttackOnPlayer(CardM.ReducePlayerHelth);
                        break;
                    //new cards
                    case "Swift Strike":
                        GameManager.Instance.temEvasion++;  
                        break;
                    case "Battered Blade":
                        nextMove = true;
                        break;
                    //case "Ricochet Bullet":
                    //    break;
                    case "Bandit Ambush":
                        if(GameManager.Instance.activeEnemy.stunned)
                        {
                            CardM.EnemyDamage = 6;
                        }
                        break;
                    case "Dual Pistols":
                        break;
                    case "Explosive Barrage":
                        break;
                    case "Final Showdown":
                        CardM.EnemyDamage = 12;
                        break;
                    case "Double Tap":
                        NextTurnDamage = true;
                        break;
                    case "Swift Slice":
                        GameManager.Instance.temEvasion++;
                        break;
                    case "Dual Volley":
                        break;
                    case "Crippling Strike":
                        break;
                    case "Dust Devil":
                        break;
                    case "Split Shot":
                        break;
                    case "Dead Eye Shot":
                        break;
                    case "Crippling Blow":
                        CardM.EnemyDamage *= 2;
                        break;
                    case "Ghost Pepper Shot":
                        CardM.EnemyDamage *= 2;
                        break;
                    case "Eagle Eye Shot":
                        CardM.EnemyDamage *= 2;
                        break;

                }
                if (container.playerCount < 3)
                {
                    if (!NextTurnDamage)
                    {
                        AttackOnEnemy(CardM.EnemyDamage);
                    }
                    else
                    {
                        int damage;
                        if (CardM.DamageOnNextTurn > 0)
                        {
                            damage = CardM.EnemyDamage + CardM.DamageOnNextTurn;
                            AttackOnEnemy(damage);
                        }
                        NextTurnDamage = false;
                    }
                }
                else
                {
                    AttackOnEnemy(CardM.EnemyDamage);
                }
                ValuesUpdate();
                #region
                //if (CardM.cardName.text == "Mouse")
                //{
                //    if (GameManager.Instance.RatCard == 0)
                //    {
                //        GameManager.Instance.RatCard++;
                //    }
                //    else
                //    {
                //        CardsManager.twoMouseCardsUsed = true;
                //    }
                //}
                //else if (CardM.cardName.text == "Red Parrot")
                //{
                //   // GameManager.Instance.redParrotActivated = true;
                //    CardM.EnemyDamage *= 2;
                //}
                //else if (CardM.cardName.text == "Cleaver")
                //{
                //    GameManager.Instance.blockDefanceCards = true;
                //}
                //else if (CardM.cardName.text == "Dual lil shooters")
                //{
                //    GameManager.Instance.dualShoter = true;
                //    CardM.EnemyDamage *= 2;
                //}
                //else if (CardM.cardName.text == "Gold Shotgun")
                //{
                //    GameManager.Instance.PlayerHealth -= CardM.ReducePlayerHelth;
                //    GameManager.Instance.PlayerHelthTxt.text = GameManager.Instance.PlayerHealth.ToString() + "/20";
                //}
                //else if (CardM.cardName.text == "Gold Scorpian")
                //{
                //    CardM.Magic_power += CardM.IncreesedMagicPower;
                //    GameManager.Instance.PlayerHealth -= CardM.ReducePlayerHelth;
                //    GameManager.Instance.PlayerHelthTxt.text = GameManager.Instance.PlayerHealth.ToString() + "/20";
                //    Debug.Log("Gold Scrorpian played !!");
                //}
                //else if (CardM.cardName.text == "Silver Scrorpian")
                //{
                //    CardM.Magic_power = CardM.Magic_power + CardM.IncreesedMagicPower;
                //    GameManager.Instance.PlayerHealth -= CardM.ReducePlayerHelth;
                //    GameManager.Instance.PlayerHelthTxt.text = GameManager.Instance.PlayerHealth.ToString() + "/20";
                //    Debug.Log("Silver Scrorpian played !!");
                //}
                #endregion

            }
        }
       
        void Defense(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth > 0)
            {
                if(CardM.gainArmor > 0) { GameManager.Instance.armors += CardM.gainArmor; }

                if(CardM.Medication > 0) { Medicated(CardM); }

                if(CardM.AddCardsNow) { AditionalCardInHand(); }

                if (CardM.AddCardsInNextTurn) { CardsManager.twoMouseCardsUsed = true; }

                if(CardM.reduceEnemyArmor > 0) { GameManager.Instance.activeEnemy.EnemyArmors -= CardM.reduceEnemyArmor; }

                if (CardM.stun) { GameManager.Instance.activeEnemy.enemyAnimator.enabled = false; }
                if(CardM.name == "Swift Dodge" || CardM.name == "Reinforced Tactics")
                {
                    container.CardManagement.twoMouseCardsUsed = true;
                    StartCoroutine(Resguffle_Handscards(CardM));
                }
                container.playerCount -= CardM.Magic_power;
                if(GameManager.Instance.dualShoter)
                {
                    CardM.BlockedDamage *= 2;
                }
                if(container.CardManagement.defanceValue == 0)
                {
                    container.CardManagement.DefanceActivated = true;
                    if(container.playerCount < 3)
                    {
                        if (NextTurnDefence)
                        {
                            int totalDefence = CardM.BlockedDamage += CardM.DefenceOnNextTurn;
                            container.CardManagement.defanceValue = totalDefence;
                            NextTurnDefence = false;
                        }
                        else
                        {
                            container.CardManagement.defanceValue = (int)CardM.BlockedDamage;
                        }
                    }
                    else
                    {
                        container.CardManagement.defanceValue = (int)CardM.BlockedDamage;
                    }
                }
                container.PlayerCount.text = container.playerCount.ToString();
            }
        }
        //void DefencePlayer(int )
        //{

        ////}
        //public void Curse(CardManager CardM)
        //{
        //    if (GameManager.Instance.activeEnemy.EnemyHealth > 0)
        //    {
        //        UnityEngine.Debug.Log("curse value: " + CardM.CurseEffect);
        //        container.CardManagement.CurseActivated = true;
        //        container.CardManagement.Cursevalue = CardM.CurseEffect;
        //        GameManager.Instance.CurseIndicator.SetActive(true);
        //        container.playerCount -= CardM.Magic_power;
        //        container.PlayerCount.text = container.playerCount.ToString();
        //    }
        //}
        void AD_cards(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth > 0)
            {
                container.playerCount -= CardM.Magic_power;
                if (GameManager.Instance.dualShoter)
                {
                    CardM.BlockedDamage *= 2;
                }
                if (container.CardManagement.defanceValue == 0)
                {
                    container.CardManagement.DefanceActivated = true; 
                    if (container.playerCount < 3)
                    {
                        if (NextTurnDefence)
                        {
                            int totalDefence = CardM.BlockedDamage += CardM.DefenceOnNextTurn;
                            container.CardManagement.defanceValue = totalDefence;
                            NextTurnDefence = false;
                        }
                        else
                        {
                            container.CardManagement.defanceValue = (int)CardM.BlockedDamage;
                        }
                    }
                    else
                    {
                        container.CardManagement.defanceValue = (int)CardM.BlockedDamage;
                    }
                }

                if (container.CardManagement.CurseActivated)
                {
                    GameManager.Instance.activeEnemy.EnemyHealth -= (CardM.EnemyDamage +1);
                }
                else
                {
                    AttackOnEnemy(CardM.EnemyDamage);
                }
                ValuesUpdate();
            }
        }
       
        void Cash_cards(CardManager CardM)
        {
            if (GameManager.Instance.activeEnemy.EnemyHealth > 0)
            {
                
                if (GameManager.Instance.dualShoter)
                {
                    CardM.ReducePlayerHelth *= 2;
                }
                if (container.CardManagement.defanceValue == 0)
                {
                    container.CardManagement.defanceValue = (int)CardM.BlockedDamage;
                }

                container.playerCount -= CardM.Magic_power;
                ValuesUpdate();
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
        void Medicated(CardManager CardM)
        {
            GameManager.Instance.LogMsg("Player Medication: ", CardM.Medication, Color.green);
            HealPlayer(CardM.Medication);
            container.playerCount -= CardM.Magic_power;
            container.PlayerCount.text = container.playerCount.ToString();
        }

        void Support(CardManager CardM)
        {
            switch(CardM.name)
            {
                case "Dusty Duress":
                    twoTimesValuse = true;
                    break;
            }
        }


        //common cards funtionalities

        void AttackOnEnemy(int AttackValue)
        {
            if (GameManager.Instance.dualShoter)
            {
                AttackValue *= 2;
            }
           
            GameManager.Instance.activeEnemy.EnemyHealth -= AttackValue;
        }
        void BackAttackOnPlayer(int AttackValue)
        {
            GameManager.Instance.PlayerHealth -= AttackValue;
            GameManager.Instance.PlayerHelthTxt.text = GameManager.Instance.PlayerHealth.ToString() + "/20";
        }
        void HealPlayer(int Healpoints)
        {
            GameManager.Instance.PlayerHealth += Healpoints;
            if (GameManager.Instance.PlayerHealth > 20)
                GameManager.Instance.PlayerHealth = 20;

            GameManager.Instance.PlayerHelthTxt.text = GameManager.Instance.PlayerHealth.ToString() + "/20";
        }
        void HealEnemy(int HealPoints)
        {
            GameManager.Instance.activeEnemy.EnemyHealth += HealPoints;
            float health = (float)(GameManager.Instance.activeEnemy.EnemyHealth * 2) / 100;
            GameManager.Instance.activeEnemy.HealthBar.fillAmount = (float)(health);
        }
        void AditionalCardInHand()
        {
            container.AddAditionalCard();
        }

        void ValuesUpdate()
        {
            GameManager.Instance.activeEnemy.HealthTxt.text = GameManager.Instance.activeEnemy.EnemyHealth.ToString() + "/50";
            float health = (float)(GameManager.Instance.activeEnemy.EnemyHealth * 2) / 100;
            GameManager.Instance.activeEnemy.HealthBar.fillAmount = (float)(health);
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
        void LevelComplete()
        {
            if(CM.LoadLevel >= 23) { CM.LoadLevel =0; }
            else { CM.LoadLevel++; }
            
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
        void Effects()
        {
            Instantiate(ScreenAnimations.Effects[Random.Range(0, ScreenAnimations.Effects.Length)], GameManager.Instance.gameObject.transform);
            //Instantiate(GameManager.Instance.enemies.All_Enemies[GameManager.Instance.CM.LoadLevel].Effect, transform);
        }
        
    }
}
