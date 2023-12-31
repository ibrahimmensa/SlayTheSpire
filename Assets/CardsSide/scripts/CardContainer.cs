using System.Collections.Generic;
using System.Linq;
using config;
using DefaultNamespace;
using events;
using UnityEngine;
using UnityEngine.UI;

public class CardContainer : MonoBehaviour
{
    [Header("Constraints")]
    [SerializeField]
    private bool forceFitContainer;

    [Header("Alignment")]
    [SerializeField]
    private CardAlignment alignment = CardAlignment.Center;

    [SerializeField]
    private bool allowCardRepositioning = true;

    [Header("Rotation")]
    [SerializeField]
    [Range(0f, 90f)]
    private float maxCardRotation;

    [SerializeField]
    private float maxHeightDisplacement;

    [SerializeField]
    private ZoomConfig zoomConfig;

    [SerializeField]
    private AnimationSpeedConfig animationSpeedConfig;

    [SerializeField]
    private CardPlayConfig cardPlayConfig;

    [Header("Events")]
    [SerializeField]
    private EventsConfig eventsConfig;

    public List<CardWrapper> cards = new List<CardWrapper>();

    private RectTransform rectTransform;
    private CardWrapper currentDraggedCard;

    //public Image EnemyHealth;
    //public Text HealthTxt;

    public Image PlayerHealth;
    public Text PlayerHealthTxt;

    public Text PlayerCount;
    public int playerCount;
    public GameObject ErrorMsg;
    public GameObject DefanseError;
    public Enemies enemies;
    public GameObject CardToDisplay;
    public CardsManagement CardManagement;

    public Animator shake;
    public CardManager CM;
    public Cards setCardIndex;
    GameObject CGO;

    public List<CardsData> Deck = new List<CardsData>();
    private void OnEnable()
    {
        Invoke(nameof(PlaceCards),0.5f);
        foreach(CardsData cards in CardManagement.CardData.AttackCards)
        {
            if (cards.canShow)
                Deck.Add(cards);
        }
    }
    private void OnDisable()
    {
        DestoryAllRemaining();
    }
    private void Start()
    {
        playerCount = 3;
        PlayerCount.text = playerCount.ToString();
        rectTransform = GetComponent<RectTransform>();
        cardPlayConfig.playArea = GameManager.Instance.activeEnemy.GetComponent<RectTransform>();
        InitCards();
    }

    private void InitCards()
    {
        SetUpCards();
        SetCardsAnchor();
        CardsArrange();
    }

    private void SetCardsRotation()
    {
        for (var i = 0; i < cards.Count; i++)
        {
            cards[i].targetRotation = GetCardRotation(i);
            cards[i].targetVerticalDisplacement = GetCardVerticalDisplacement(i);
        }
    }

    private float GetCardVerticalDisplacement(int index)
    {
        if (cards.Count < 3) return 0;
        // Associate a vertical displacement based on the index in the cards list
        // so that the center card is at max displacement while the edges are at 0 displacement
        return maxHeightDisplacement *
               (1 - Mathf.Pow(index - (cards.Count - 1) / 2f, 2) / Mathf.Pow((cards.Count - 1) / 2f, 2));
    }

    private float GetCardRotation(int index)
    {
        if (cards.Count < 3) return 0;
        // Associate a rotation based on the index in the cards list
        // so that the first and last cards are at max rotation, mirrored around the center
        return -maxCardRotation * (index - (cards.Count - 1) / 2f) / ((cards.Count - 1) / 2f);
    }

    void Update()
    {
        UpdateCards();
    }
    int cd;
    void PlaceCards()
    {
        int CardsInHand;
        CardManagement.turns++;
        if(CardManagement.twoMouseCardsUsed)
        {
            CardsInHand = 6;
            CardManagement.twoMouseCardsUsed = false;
            GameManager.Instance.RatCard = 0;
        }
        else
        {
            CardsInHand = 5;
        }
        for (cd = 0; cd < CardsInHand; cd++)
        {
            CGO = Instantiate(CardToDisplay, gameObject.transform);
            mapData(CGO);
        }
    }
    public void mapData(GameObject card)
    {
        CardsData cardObj;
        //cardObj = CardManagement.CardData.AttackCards[Random.Range(0, CardManagement.CardData.AttackCards.Length)];

        cardObj = Deck[Random.Range( 0,Deck.Count)];

        //if (cd <2)
        //{
        //}
        //else if(cd <4)
        //{
        //    cardObj = CardManagement.CardData.AD_Cards[Random.Range(0, CardManagement.CardData.AD_Cards.Length)];
        //}
        //else
        //{
        //    cardObj = CardManagement.CardData.DefanceCards[Random.Range(0, CardManagement.CardData.DefanceCards.Length)];
        //}
        CM = card.GetComponent<CardManager>();

        CM.Power.text = cardObj.MagicPowerRequired.ToString();
        CM.cardName.text = cardObj.Card_Name;
        CM.Discription.text = cardObj.disription;
        CM.Magic_power = cardObj.MagicPowerRequired;
        CM.CurseEffect = Random.Range(cardObj.CurseEffect_min, cardObj.CurseEffect_max);
        CM.EnemyDamage = Random.Range(cardObj.Attack_min, cardObj.Attack_max);
        CM.BlockedDamage = Random.Range(cardObj.Defense_min, cardObj.Defense_max);
        CM.Medication = Random.Range(cardObj.PlayerHeal_min, cardObj.PlayerHeal_max);
        CM.ReducePlayerHelth = cardObj.ReducePlayerHealth;
        CM.IncreesPlayerHelth = cardObj.IncreesPlayerHealth;
        CM.IncreesedMagicPower = cardObj.IncreesedMagicPower;
        CM.Attack = cardObj.Attack;
        CM.Defence = cardObj.Defence;
        CM.Curse = cardObj.Curse;
        CM.Medicated = cardObj.Medicated;
        CM.AD_Cards = cardObj.AttackDefence;
        CM.Cash_cards = cardObj.cashCards;
        CM.Reshuffle_cards = cardObj.Rehuffle;
        CM.gameObject.GetComponent<Image>().sprite = cardObj.cardSprite;
        CM.centerImg.sprite = cardObj.centerImg;

        if (cardObj.Attack) {CM.Rarity.text = "Attack";}
        else if(cardObj.Defence) { CM.Rarity.text = "Defense"; }
        else if(cardObj.Curse) { CM.Rarity.text = "Curse"; }
        else if (cardObj.Medicated) { CM.Rarity.text = "Medicated"; }
        else if (cardObj.AttackDefence) { CM.Rarity.text = "AttackDefence"; }
        else if (cardObj.cashCards) { CM.Rarity.text = "Cash"; }
        else if (cardObj.Rehuffle) { CM.Rarity.text = "Rehuffle"; }
    }
    public void CardsArrange()
    {
        for(int a=Random.Range(0,4); a<cards.Count;a++)
        {
            var temp = cards[a];
            cards.Remove(cards[a]);
            cards.Add(temp);
        }
    }
    void SetUpCards()
    {
        cards.Clear();
        foreach (Transform card in transform)
        {
            var wrapper = card.GetComponent<CardWrapper>();
            if (wrapper == null)
            {
                wrapper = card.gameObject.AddComponent<CardWrapper>();
            }

            cards.Add(wrapper);

            AddOtherComponentsIfNeeded(wrapper);

            // Pass child card any extra config it should be aware of
            wrapper.zoomConfig = zoomConfig;
            wrapper.animationSpeedConfig = animationSpeedConfig;
            wrapper.eventsConfig = eventsConfig;
            wrapper.container = this;
        }
    }

    private void AddOtherComponentsIfNeeded(CardWrapper wrapper)
    {
        var canvas = wrapper.GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = wrapper.gameObject.AddComponent<Canvas>();
        }

        canvas.overrideSorting = true;

        if (wrapper.GetComponent<GraphicRaycaster>() == null)
        {
            wrapper.gameObject.AddComponent<GraphicRaycaster>();
        }
    }

    private void UpdateCards()
    {
        if (transform.childCount != cards.Count)
        {
            InitCards();
        }

        if (cards.Count == 0)
        {
            return;
        }

        SetCardsPosition();
        SetCardsRotation();
        SetCardsUILayers();
        UpdateCardOrder();
    }

    private void SetCardsUILayers()
    {
        for (var i = 0; i < cards.Count; i++)
        {
            cards[i].uiLayer = zoomConfig.defaultSortOrder + i;
        }
    }

    private void UpdateCardOrder()
    {
        if (!allowCardRepositioning || currentDraggedCard == null) return;

        // Get the index of the dragged card depending on its position
        var newCardIdx = cards.Count(card => currentDraggedCard.transform.position.x > card.transform.position.x);
        var originalCardIdx = cards.IndexOf(currentDraggedCard);
        if (newCardIdx != originalCardIdx)
        {
            cards.RemoveAt(originalCardIdx);
            if (newCardIdx > originalCardIdx && newCardIdx < cards.Count - 1)
            {
                newCardIdx--;
            }

            cards.Insert(newCardIdx, currentDraggedCard);
        }
        // Also reorder in the hierarchy
        currentDraggedCard.transform.SetSiblingIndex(newCardIdx);
    }

    private void SetCardsPosition()
    {
        // Compute the total width of all the cards in global space
        var cardsTotalWidth = cards.Sum(card => card.width * card.transform.lossyScale.x);
        // Compute the width of the container in global space
        var containerWidth = rectTransform.rect.width * transform.lossyScale.x;
        if (forceFitContainer && cardsTotalWidth > containerWidth)
        {
            DistributeChildrenToFitContainer(cardsTotalWidth);
        }
        else
        {
            DistributeChildrenWithoutOverlap(cardsTotalWidth);
        }
    }

    private void DistributeChildrenToFitContainer(float childrenTotalWidth)
    {
        // Get the width of the container
        var width = rectTransform.rect.width * transform.lossyScale.x;
        // Get the distance between each child
        var distanceBetweenChildren = (width - childrenTotalWidth) / (cards.Count - 1);
        // Set all children's positions to be evenly spaced out
        var currentX = transform.position.x - width / 2;
        foreach (CardWrapper child in cards)
        {
            var adjustedChildWidth = child.width * child.transform.lossyScale.x;
            child.targetPosition = new Vector2(currentX + adjustedChildWidth / 2, transform.position.y);
            currentX += adjustedChildWidth + distanceBetweenChildren;
        }
    }

    private void DistributeChildrenWithoutOverlap(float childrenTotalWidth)
    {
        var currentPosition = GetAnchorPositionByAlignment(childrenTotalWidth);
        foreach (CardWrapper child in cards)
        {
            var adjustedChildWidth = child.width * child.transform.lossyScale.x;
            child.targetPosition = new Vector2(currentPosition + adjustedChildWidth / 2, transform.position.y);
            currentPosition += adjustedChildWidth;
        }
    }

    private float GetAnchorPositionByAlignment(float childrenWidth)
    {
        var containerWidthInGlobalSpace = rectTransform.rect.width * transform.lossyScale.x;
        switch (alignment)
        {
            case CardAlignment.Left:
                return transform.position.x - containerWidthInGlobalSpace / 2;
            case CardAlignment.Center:
                return transform.position.x - childrenWidth / 2;
            case CardAlignment.Right:
                return transform.position.x + containerWidthInGlobalSpace / 2 - childrenWidth;
            default:
                return 0;
        }
    }

    private void SetCardsAnchor()
    {
        foreach (CardWrapper child in cards)
        {
            child.SetAnchor(new Vector2(0, 0.5f), new Vector2(0, 0.5f));
        }
    }

    public void OnCardDragStart(CardWrapper card)
    {
        currentDraggedCard = card;
    }

    public void OnCardDragEnd()
    {
        // If card is in play area, play it!
        if (IsCursorInPlayArea())
        {
            eventsConfig?.OnCardPlayed?.Invoke(new CardPlayed(currentDraggedCard));
            if (cardPlayConfig.destroyOnPlay)
            {
                DestroyCard(currentDraggedCard);
            }
        }
        currentDraggedCard = null;
    }
    void DestoryAllRemaining()
    {
        for (int a = 0; a < gameObject.transform.childCount; a++)
        {
            DestroyCard(transform.GetChild(a).GetComponent<CardWrapper>());
        }
    }
    public void DestroyCard(CardWrapper card)
    {
        cards.Remove(card);
        eventsConfig.OnCardDestroy?.Invoke(new CardDestroy(card));

        Destroy(card.gameObject);
    }

    private bool IsCursorInPlayArea()
    {
        if (cardPlayConfig.playArea == null) return false;

        var cursorPosition = Input.mousePosition;
        var playArea = cardPlayConfig.playArea;
        var playAreaCorners = new Vector3[4];
        playArea.GetWorldCorners(playAreaCorners);
        return cursorPosition.x > playAreaCorners[0].x &&
               cursorPosition.x < playAreaCorners[2].x &&
               cursorPosition.y > playAreaCorners[0].y &&
               cursorPosition.y < playAreaCorners[2].y;

    }
}
