using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public Animator[] cards;
    public MiniCard[] miniCards;
    public Image[] miniCardsImage;
    public Sprite[] miniCardSprites;
    public Button[] cardBtn;

    public bool aAz, jAck,parot;

    public bool onclick;
    public MiniGame MiniGame;
    Animator anim;

    private void OnEnable()
    {
        int[] indexArr = { 0, 1, 2 };
        System.Random random = new System.Random();
        indexArr = indexArr.OrderBy(x => random.Next()).ToArray();

        for(int i=0;i<indexArr.Length;i++)
        {
            miniCardsImage[i].sprite = miniCardSprites[indexArr[i]];
            if (indexArr[i] == 0)
            {
                miniCards[i].parot = true;
                miniCards[i].aAz = false;
                miniCards[i].jAck = false;
            }
            else if (indexArr[i] == 1)
            {
                miniCards[i].parot = false;
                miniCards[i].aAz = true;
                miniCards[i].jAck = false;
            }
            else if (indexArr[i] == 2)
            {
                miniCards[i].parot = false;
                miniCards[i].aAz = false;
                miniCards[i].jAck = true;
            }
        }

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
                anim.SetFloat("Speed", 0.2f);
                break;
            case 1:
                anim.SetFloat("Speed",0.38f);
                break;
            case 2:
                anim.SetFloat("Speed", 0.7f);
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
