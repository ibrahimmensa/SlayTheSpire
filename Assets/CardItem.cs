using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    public int positionNumber;
    public bool isAce;
    public bool isJack;
    public bool isParrot;

    public Image CardImage;
    public Button CardButton;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickCard()
    {
        anim.SetBool("Show", true);
        if((isAce && CardAnimationController.instance.isAce) || (isJack && CardAnimationController.instance.isJack) || (isParrot && CardAnimationController.instance.isParrot))
        {
            StartCoroutine(CardAnimationController.instance.Results(true));
        }
        else
        {
            StartCoroutine(CardAnimationController.instance.Results(false));
        }
    }
}
