using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public Animator[] cards;
    public Button[] cardBtn;

    public bool aAz, jAck;

    public bool onclick;
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
