using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public Animator[] cards;
    public Button[] cardBtn;

    public bool aAz, jAck,parot;

    public bool onclick;
    public MiniGame MiniGame;
    Animator anim;

    private void OnEnable()
    {
        if (MiniGame.totalTurns >= 3)
        {
            MiniGame.totalTurns = 0;
            MiniGame.winCounts = 0;
            MiniGame.loseCounts = 0;
        }
        anim = GetComponent<Animator>();
        switch (MiniGame.winCounts)
        {
            case 0:
                anim.SetFloat("Speed", 0.15f);
                break;
            case 1:
                anim.SetFloat("Speed",0.5f);
                break;
            case 2:
                anim.SetFloat("Speed", 0.9f);
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SfalCards());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SfalCards()
    {
        foreach(var card in cards)
        {
            card.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void ShowCard(Animator ani)
    {
        ani.SetBool("Show", true);
        onclick = true;
    }
    public void ActiveButtons()
    {
        GetComponent<Animator>().enabled = false;
        foreach(Button btn in cardBtn)
        {
            btn.interactable = true;
        }
    }
    public void Turnoff()
    {
        gameObject.SetActive(false);
    }
}
