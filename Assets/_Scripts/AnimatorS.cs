using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AttackAni()
    {
        GetComponent<Animator>().enabled = false;
    }
    public void PlayVFX()
    {

        Instantiate(GameManager.Instance.enemies.All_Enemies[GameManager.Instance.CM.LoadLevel].Effect, transform);
        GameManager.Instance.CardContainerRef.GetComponent<CardContainer>().shake.enabled = true;
        GameManager.Instance.SoundManager.playSound(GameManager.Instance.Sounds.AttackSounds[Random.Range(0, 2)]); 
        if (GameManager.Instance.cardsManagement.DefanceActivated)
        {
            GameManager.Instance.IfDefanse();
        }
        GameManager.Instance.activeEnemy.enemyAnimator.SetBool("Attack", false);
    }
}
