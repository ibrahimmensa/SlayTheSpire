using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimationController : MonoBehaviour
{
    public static CardAnimationController instance;

    public GameObject[] cards;

    public GameObject[] tempCards;

    public Sprite[] CardSprites;

    public GameObject Cardss;
    public GameObject StartPanel;
    public GameObject LevelFailPanel;
    public GameObject LevelWinPanel;
    public GameObject GameCompletePanel;

    public float timeTakenBySingleShuffle;

    public Vector3[] positionsOfCards;
    public float maxYValue;
    public float minYValue;

    bool[] movesRecord = { false, false, false }; //0(card has moved straight) 1(card has moved from upwards) 2(card has moved from downwards)

    public int roundNo = 0;

    bool flipInMatch = false;

    public Image CardOnStartPanel;


    public bool isAce;
    public bool isJack;
    public bool isParrot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartTheGame();
    }

    public void StartTheGame()
    {
        int ran = Random.Range(0, 3);
        CardOnStartPanel.sprite = CardSprites[ran];
        if (ran == 0)
        {
            isJack = true;
            isAce = false;
            isParrot = false;
        }
        else if (ran == 1)
        {
            isJack = false;
            isAce = true;
            isParrot = false;
        }
        else if (ran == 2)
        {
            isJack = false;
            isAce = false;
            isParrot = true;
        }
    }

    void RandomizeCardsAtStart()
    {
        int[] indexArr = { 0, 1, 2 };
        System.Random random = new System.Random();
        indexArr = indexArr.OrderBy(x => random.Next()).ToArray();

        for (int i = 0; i < indexArr.Length; i++)
        {
            cards[i].GetComponent<CardItem>().CardImage.sprite = CardSprites[indexArr[i]];
            if (indexArr[i] == 0)
            {
                cards[i].GetComponent<CardItem>().isJack = true;
                cards[i].GetComponent<CardItem>().isAce = false;
                cards[i].GetComponent<CardItem>().isParrot = false;
            }
            else if (indexArr[i] == 1)
            {
                cards[i].GetComponent<CardItem>().isJack = false;
                cards[i].GetComponent<CardItem>().isAce = true;
                cards[i].GetComponent<CardItem>().isParrot = false;
            }
            else if (indexArr[i] == 2)
            {
                cards[i].GetComponent<CardItem>().isJack = false;
                cards[i].GetComponent<CardItem>().isAce = false;
                cards[i].GetComponent<CardItem>().isParrot = true;
            }
        }

        if (roundNo == 0)
            timeTakenBySingleShuffle = 1.8f;
        else if (roundNo == 1)
            timeTakenBySingleShuffle = 1.4f;
        else
            timeTakenBySingleShuffle = 1f;
    }

    public void onClickPlay()
    {
        StartPanel.SetActive(false);
        Cardss.SetActive(true); 
        RandomizeCardsAtStart();
        StartCoroutine(FlipOverAndStartCardAnimation());
    }

    IEnumerator FlipOverAndStartCardAnimation()
    {
        foreach (GameObject card in cards)
        {
            card.GetComponent<Animator>().SetBool("Show", true);
        }
        yield return new WaitForSeconds(3f);
        foreach (GameObject card in cards)
        {
            card.GetComponent<Animator>().SetBool("Show", false);
        }
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(CardAnimation(0));
    }

    IEnumerator CardAnimation(float TotalTime)
    {
        if (roundNo == 0 && TotalTime > 5 && !flipInMatch)
        {
            foreach (GameObject card in cards)
            {
                card.GetComponent<Animator>().SetBool("Show", true);
            }
            yield return new WaitForSeconds(2);
            foreach (GameObject card in cards)
            {
                card.GetComponent<Animator>().SetBool("Show", false);
            }
            yield return new WaitForSeconds(1);
            flipInMatch = true;
        }

        int[] indexArrCurrent = { cards[0].GetComponent<CardItem>().positionNumber, cards[1].GetComponent<CardItem>().positionNumber, cards[2].GetComponent<CardItem>().positionNumber };
        int[] indexArrNext;
        do
        {
            System.Random random = new System.Random();
            indexArrNext = indexArrCurrent.OrderBy(x => random.Next()).ToArray();
        } while (indexArrCurrent[0] == indexArrNext[0] && indexArrCurrent[1] == indexArrNext[1] && indexArrCurrent[2] == indexArrNext[2]);

        for (int i = 0; i < cards.Length; i++)
        {
            tempCards[i] = cards[i];
        }

        //use record
        movesRecord[0] = false;
        movesRecord[1] = false;
        movesRecord[2] = false;
        float[] yPosNext = { 0, 0, 0 };
        if (Mathf.Abs(indexArrCurrent[0] - indexArrNext[0]) > 1)
        {
            int ran = Random.Range(0, 2);
            if (ran == 0)
            {
                yPosNext[0] = maxYValue;
                movesRecord[1] = true;
            }
            else if (ran == 1)
            {
                yPosNext[0] = minYValue;
                movesRecord[2] = true;
            }
        }
        if(Mathf.Abs(indexArrCurrent[1] - indexArrNext[1]) > 1)
        {
            if (movesRecord[1])
            {
                yPosNext[1] = minYValue;
                movesRecord[2] = true;
            }
            else if (movesRecord[2])
            {
                yPosNext[1] = maxYValue;
                movesRecord[1] = true;
            }
        }
        if (Mathf.Abs(indexArrCurrent[2] - indexArrNext[2]) > 1)
        {
            if (movesRecord[1])
            {
                yPosNext[2] = minYValue;
                movesRecord[2] = true;
            }
            else if (movesRecord[2])
            {
                yPosNext[2] = maxYValue;
                movesRecord[1] = true;
            }
        }
        if (!movesRecord[2])
        {
            if (yPosNext[0] == 0 && (indexArrCurrent[0] != indexArrNext[0]))
                yPosNext[0] = minYValue;
            else if (yPosNext[1] == 0 && (indexArrCurrent[1] != indexArrNext[1]))
                yPosNext[1] = minYValue;
            else if (yPosNext[2] == 0 && (indexArrCurrent[2] != indexArrNext[2]))
                yPosNext[2] = minYValue;
        }
        if (!movesRecord[1])
        {
            if (yPosNext[0] == 0 && (indexArrCurrent[0] != indexArrNext[0]))
                yPosNext[0] = maxYValue;
            else if (yPosNext[1] == 0 && (indexArrCurrent[1] != indexArrNext[1]))
                yPosNext[1] = maxYValue;
            else if (yPosNext[2] == 0 && (indexArrCurrent[2] != indexArrNext[2]))
                yPosNext[2] = maxYValue;
        }

        Vector2 Card1StartPosition = tempCards[0].transform.localPosition;
        Vector2 Card1StartPosition2 = tempCards[0].transform.localPosition;
        Vector2 Card1endPosition = positionsOfCards[indexArrNext[0]];

        Vector2 Card2StartPosition = tempCards[1].transform.localPosition;
        Vector2 Card2StartPosition2 = tempCards[1].transform.localPosition;
        Vector2 Card2endPosition = positionsOfCards[indexArrNext[1]];

        Vector2 Card3StartPosition = tempCards[2].transform.localPosition;
        Vector2 Card3StartPosition2 = tempCards[2].transform.localPosition;
        Vector2 Card3endPosition = positionsOfCards[indexArrNext[2]];

        float elapsedTime = 0;
        float elapsedTime2 = 0;
        float progress = 0;
        float progress2 = 0;

        while (progress <= 1)
        {
            if (progress < 0.5)
            {
                //1st Card
                tempCards[0].transform.localPosition = Vector2.Lerp(Card1StartPosition, new Vector3(Card1endPosition.x, tempCards[0].transform.localPosition.y), progress);
                tempCards[0].transform.localPosition = Vector2.Lerp(Card1StartPosition, new Vector3(tempCards[0].transform.localPosition.x, yPosNext[0]), progress * 2);
                Card1StartPosition2 = tempCards[0].transform.localPosition;
                //2nd Card
                tempCards[1].transform.localPosition = Vector2.Lerp(Card2StartPosition, new Vector3(Card2endPosition.x, tempCards[1].transform.localPosition.y), progress);
                tempCards[1].transform.localPosition = Vector2.Lerp(Card2StartPosition, new Vector3(tempCards[1].transform.localPosition.x, yPosNext[1]), progress * 2);
                Card2StartPosition2 = tempCards[1].transform.localPosition;
                //3rd Card
                tempCards[2].transform.localPosition = Vector2.Lerp(Card3StartPosition, new Vector3(Card3endPosition.x, tempCards[2].transform.localPosition.y), progress);
                tempCards[2].transform.localPosition = Vector2.Lerp(Card3StartPosition, new Vector3(tempCards[2].transform.localPosition.x, yPosNext[2]), progress * 2);
                Card3StartPosition2 = tempCards[2].transform.localPosition;

                progress2 = 0;
                elapsedTime2 = 0;
            }
            else
            {
                //1st Card
                tempCards[0].transform.localPosition = Vector2.Lerp(Card1StartPosition2, Card1endPosition, progress2);
                //2nd Card
                tempCards[1].transform.localPosition = Vector2.Lerp(Card2StartPosition2, Card2endPosition, progress2);
                //3rd Card
                tempCards[2].transform.localPosition = Vector2.Lerp(Card3StartPosition2, Card3endPosition, progress2);
            }
            elapsedTime += Time.unscaledDeltaTime;
            elapsedTime2 += Time.unscaledDeltaTime;
            progress = elapsedTime / timeTakenBySingleShuffle;
            progress2 = elapsedTime2 / (timeTakenBySingleShuffle / 2f);
            yield return null;
        }
        tempCards[0].transform.localPosition = Card1endPosition;
        tempCards[1].transform.localPosition = Card2endPosition;
        tempCards[2].transform.localPosition = Card3endPosition;

        tempCards[0].GetComponent<CardItem>().positionNumber = indexArrNext[0];
        tempCards[1].GetComponent<CardItem>().positionNumber = indexArrNext[1];
        tempCards[2].GetComponent<CardItem>().positionNumber = indexArrNext[2];

        cards[0] = tempCards[indexArrNext[0]];
        cards[1] = tempCards[indexArrNext[1]];
        cards[2] = tempCards[indexArrNext[2]];

        if (TotalTime + timeTakenBySingleShuffle <= 10)
            StartCoroutine(CardAnimation(TotalTime + timeTakenBySingleShuffle));
        else
        {
            cards[0].GetComponent<CardItem>().CardButton.interactable = true;
            cards[1].GetComponent<CardItem>().CardButton.interactable = true;
            cards[2].GetComponent<CardItem>().CardButton.interactable = true;
        }
    }

    public IEnumerator Results(bool isWin)
    {
        cards[0].GetComponent<CardItem>().CardButton.interactable = false;
        cards[1].GetComponent<CardItem>().CardButton.interactable = false;
        cards[2].GetComponent<CardItem>().CardButton.interactable = false;
        yield return new WaitForSeconds(1);
        Cardss.SetActive(false);
        if (!isWin)
        {
            LevelFailPanel.SetActive(true);
            roundNo = 0;
            flipInMatch = false;
        }
        else
        {
            if (roundNo < 2)
            {
                LevelWinPanel.SetActive(true);
                roundNo++;
            }
            else
            {
                GameCompletePanel.SetActive(true);
                roundNo = 0;
                flipInMatch = false;
            }
        }
    }
}
