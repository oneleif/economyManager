using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackjackManager : MonoBehaviour
{
    private GameObject blackjackTableContainer;

    //private GameObject handContainer; 

    // rename to playerHand 
    private GameObject playerCardContainer;
    private GameObject dealerCardContainer;
    public GameObject blackjackButtonPrefab;
    private GameObject blackjackButtonContainer;

    private GameObject hitButtonObject;
    private GameObject standButtonObject;
    private GameObject newGameButtonObject;
    private GameObject quitGameButtonObject;

    private GameObject wagerTextObject;
    private GameObject playerTotalObject;
    private GameObject dealerTotalObject; 

    public BlackjackPlayer blackjackPlayer;
    public BlackjackPlayer blackjackDealer;

    [SerializeField]
    private int cardWidth, cardHeight, buttonWidth, buttonHeight;

    private void Start()
    {
        // Dealer gets 2 cards (1 face down), Player gets 2 cards 

        SetupTable();
        NewGame();
        Debug.Log(Deck.cardSprites[0].name); 
        //DealStartingHand(); 

        // init again (but don't reload sprites) - or get a separate method for re-ordering them 

    }
    //private void DealToDealer()
    //{


    //    if (Dealer.total > 21)
    //    {
    //        Dealer.isBust = true;

    //    }
    //}

    // redund?
    private void DealStartingHand()
    {
        DealCard(blackjackPlayer, true);
        DealCard(blackjackDealer, false);
        DealCard(blackjackPlayer, true);
        DealCard(blackjackDealer, true);
    }
    private void DealCard(BlackjackPlayer _blackjackPlayer, bool faceUp)
    {
        int randomIndex = Random.Range(0, Deck.deck.Count);
        Card drawnCard = Deck.deck[randomIndex];
        Deck.deck.RemoveAt(randomIndex);
        Deck.drawnCards.Add(drawnCard);
        _blackjackPlayer.hand.Add(drawnCard);
        GameObject drawnCardObject = new GameObject("CardObject");
        drawnCardObject.transform.parent = _blackjackPlayer.isDealer ? dealerCardContainer.transform : playerCardContainer.transform;
        drawnCardObject.AddComponent<Image>().sprite = faceUp ? drawnCard.sprite : Deck.cardBackSprite;
        _blackjackPlayer.handTotal += drawnCard.value;

        Debug.Log("Drawn card name: " + drawnCard.sprite.name + "Drawn value: " + drawnCard.value);

        //// Check if ace should be high or low  
        //foreach (Card card in _blackjackPlayer.hand)
        //{
        // >= 17 && <= 21? 
        //    if (card.value == 1 && _blackjackPlayer.handTotal + 10 <= 21)
        //    {
        //        _blackjackPlayer.handTotal += 10; 
        //    }
        //    else
        //    {
        //        _blackjackPlayer.handTotal -= 10; 
        //    }

        //}

        if (_blackjackPlayer.handTotal > 21)
        {
            _blackjackPlayer.isBust = true;
            // if isBust trigger game over? here
        }

        _blackjackPlayer.isUpNext = !_blackjackPlayer.isUpNext;
    }

    private void StandPlayer()
    {
        blackjackPlayer.isStanding = true;
        standButtonObject.SetActive(false); 
    }

    //private void ShuffleDeck()
    //{

    //    cards.RemoveAt(0); 
    //}

    private void DisplayScore()
    {
        // OnGUI 
    }

    // This should just be for the TABLE (not btns, etc.) 
    private void SetupTable()
    {
        //gameObject.GetComponent<LayoutElement>().minWidth = 512;

        blackjackTableContainer = new GameObject("BlackjackTableContainer");
        //blackjackTableContainer.transform.parent = gameObject.transform;
        //blackjackTableContainer.transform.localPosition = new Vector2(0f, 0f);
        LayoutElement layoutElement = blackjackTableContainer.AddComponent<LayoutElement>();
        VerticalLayoutGroup verticalLayoutGroup = blackjackTableContainer.AddComponent<VerticalLayoutGroup>();
        //verticalLayoutGroup.spacing = 32; 
        //layoutElement.flexibleWidth = 64;
        //layoutElement.flexibleHeight = 128;
        //layoutElement.preferredWidth = 256;
        //layoutElement.preferredWidth = gameObject.GetComponent<RectTransform>().rect.width; 
        //layoutElement.preferredHeight = 512; 
        //verticalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
        verticalLayoutGroup.childControlWidth = true;
        verticalLayoutGroup.childControlHeight = true;
        verticalLayoutGroup.childForceExpandWidth = true;
        verticalLayoutGroup.childForceExpandHeight = true;
        verticalLayoutGroup.spacing = 32f; 

        // Boundary 
        blackjackTableContainer.AddComponent<Image>().color = Color.green;

        RectTransform rectTransform = blackjackTableContainer.GetComponent<RectTransform>();

        // do parent.transform after setting parent?
        RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();

        //rectTransform1.anchorMin = new Vector2(0.25f, 0.25f);
        //rectTransform1.anchorMax = new Vector2(0.75f, 0.75f);
        rectTransform.anchoredPosition = parentRectTransform.position;
        rectTransform.anchorMin = new Vector2(1f, 0f);
        rectTransform.anchorMax = new Vector2(0f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = gameObject.GetComponent<RectTransform>().rect.size;
        blackjackTableContainer.transform.parent = gameObject.transform;
        //rectTransform1.off

        //RectTransform GORT = gameObject.GetComponent<RectTransform>();
        //GORT.anchorMin = new Vector2(0f, 0f);
        //GORT.anchorMax = new Vector2(1f, 1f); 
        //GORT.rect.left 

        //GameObject handContainer = new GameObject("HandContainer");
        //handContainer.transform.parent = blackjackTableContainer.transform; 
        //HorizontalLayoutGroup horizontalLayoutGroup = handContainer.AddComponent<HorizontalLayoutGroup>();

        GameObject minigameTextObject = new GameObject("MinigameText");
        minigameTextObject.transform.parent = blackjackTableContainer.transform;
        Text minigameText = minigameTextObject.AddComponent<Text>();
        minigameText.text = "Blackjack Table";
        minigameText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        // Change to grid layout group container for each stat? Or delineate with tabs (spacing)? 
        GameObject playerStatsContainer = new GameObject("PlayerStats");
        playerStatsContainer.transform.parent = blackjackTableContainer.transform; 
        Text playerChipsText = playerStatsContainer.AddComponent<Text>();
        playerChipsText.text = $"Current chips: {blackjackPlayer.chips}";
        //Text playerWagerText = playerStatsContainer.AddComponent<Text>().text = $"" 

        playerCardContainer = new GameObject("PlayerCardContainer");
        playerCardContainer.transform.parent = blackjackTableContainer.transform;
        rectTransform = playerCardContainer.AddComponent<RectTransform>();
        rectTransform.offsetMax = new Vector2(16f, 16f);
        playerCardContainer.AddComponent<GridLayoutGroup>().cellSize = new Vector2(60, 84f);
        //GridLayoutGroup gridLayoutGroup = playerCardContainer.AddComponent<GridLayoutGroup>();
        //gridLayoutGroup.cellSize = new Vector2(60, 84f);
        playerTotalObject = new GameObject("PlayerTotalText");
        playerTotalObject.transform.parent = playerCardContainer.transform;
        Text playerTotalText = playerTotalObject.AddComponent<Text>();
        playerTotalText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        playerTotalText.color = Color.black;
        playerTotalText.text = $"Your total: {blackjackPlayer.handTotal}";

        dealerCardContainer = new GameObject("DealerCardContainer");
        dealerCardContainer.transform.parent = blackjackTableContainer.transform;
        dealerCardContainer.AddComponent<GridLayoutGroup>().cellSize = new Vector2(60f, 84f);
        dealerTotalObject = new GameObject("DealerTotalText");
        dealerTotalObject.transform.parent = dealerCardContainer.transform;
        Text dealerTotalText = dealerTotalObject.AddComponent<Text>();
        dealerTotalText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        dealerTotalText.color = Color.black; 
        dealerTotalText.text = $"Dealer's total: {blackjackDealer.handTotal}";

        blackjackButtonContainer = new GameObject("BlackjackButtonContainer");
        //blackjackButtonContainer.transform.parent = blackjackTableContainer.transform;
        blackjackButtonContainer.transform.parent = blackjackTableContainer.transform;
        blackjackButtonContainer.AddComponent<GridLayoutGroup>().cellSize = new Vector2(128f, 32f);
        rectTransform = blackjackButtonContainer.AddComponent<RectTransform>();


        // Create button for "hitting" 
        hitButtonObject = Instantiate(blackjackButtonPrefab);
        hitButtonObject.transform.parent = blackjackButtonContainer.transform;
        rectTransform = hitButtonObject.GetComponent<RectTransform>();
        //hitButtonObject.transform.localPosition = new Vector2(1f, 1f);

        //dealButtonObject.transform.localPosition = new Vector3(
        //    dealButtonObject.transform.parent.position.x - 128f, dealButtonObject.transform.parent.position.y - 128f);

        Button hitButton = hitButtonObject.GetComponent<Button>();
        hitButton.GetComponentInChildren<Text>().text = "Hit!";
        hitButton.onClick.RemoveAllListeners();
        hitButton.onClick.AddListener(delegate { DealCard(blackjackPlayer, true); });



        // Create button for "standing" 
        standButtonObject = Instantiate(blackjackButtonPrefab);
        standButtonObject.transform.parent = blackjackButtonContainer.transform;
        rectTransform = hitButtonObject.GetComponent<RectTransform>();
        //hitButtonObject.transform.localPosition = new Vector2(2f, 1f);

        Button standButton = standButtonObject.GetComponent<Button>();
        standButton.GetComponentInChildren<Text>().text = "Stand";
        standButton.onClick.RemoveAllListeners();
        standButton.onClick.AddListener(delegate { StandPlayer(); });


        // Create input field for betting chips 
        wagerTextObject = new GameObject("WagerText");
        Text wagerText = wagerTextObject.AddComponent<Text>();
        //wagerText.interactable = true; 
        //wagerText.characterLimit = Math.Floor(Math.Log10(blackjackPlayer.chips) + 1);


        // Create button for "splitting" 
        // 
        // TBC 


        // Create new game/quit btns 
        newGameButtonObject = Instantiate(blackjackButtonPrefab);
        newGameButtonObject.transform.parent = blackjackButtonContainer.transform;
        rectTransform = newGameButtonObject.GetComponent<RectTransform>();
        Button newGameButton = newGameButtonObject.GetComponent<Button>();
        newGameButton.GetComponentInChildren<Text>().text = "Play again";


        quitGameButtonObject = Instantiate(blackjackButtonPrefab);
        quitGameButtonObject.transform.parent = blackjackButtonContainer.transform;
        rectTransform = quitGameButtonObject.GetComponent<RectTransform>();
        Button quitGameButton = quitGameButtonObject.GetComponent<Button>();
        quitGameButton.GetComponentInChildren<Text>().text = "Leave table";

        // Todos: 
        // break out the btn creation code into a separate dedicated method (de-bloat setup) 
        // 

        blackjackDealer.isDealer = true; 
        Deck.InitialiseDeck();
        Deck.ShuffleDeck();

    }

    private void InitialiseScriptableObjects()
    {
        blackjackPlayer = ScriptableObject.CreateInstance<BlackjackPlayer>();
        blackjackDealer = ScriptableObject.CreateInstance<BlackjackPlayer>();
        blackjackDealer.isDealer = true;

        // This method could be extended to handle multiple players 
        // Using a BlackjackPlayer container
    }

    private void InitialisePlayerHandContainers()
    {
        //foreach (BlackjackPlayer _blackjackPlayer in blackjackPlayerContainer)
        //{

        //}
    }

    private void HandleWager()
    {
        
        //if (wagerInput > blackjackPlayer.wager) then wagerInput = .wager
    }

    private void NewGame()
    { 
        if (Deck.drawnCards.Count > 0)
        {
            foreach (Card card in Deck.drawnCards)
            {
                Deck.deck.Add(card);
                Deck.drawnCards.Remove(card); 
            }
        }
        Deck.ShuffleDeck();

        blackjackPlayer.handTotal = 0;
        blackjackPlayer.hand = new List<Card>(); 
        blackjackPlayer.isStanding = false;
        blackjackPlayer.isBust = false;

        blackjackDealer.handTotal = 0;
        blackjackDealer.hand = new List<Card>();
        blackjackDealer.isStanding = false;
        blackjackDealer.isBust = false; 
        dealerTotalObject.SetActive(false); 
        

		hitButtonObject.SetActive(true);
        standButtonObject.SetActive(true); 
        newGameButtonObject.SetActive(false);
        quitGameButtonObject.SetActive(false);

        // Deal starting hands 
        DealCard(blackjackPlayer, true);
        DealCard(blackjackDealer, false);
        DealCard(blackjackPlayer, true);
        DealCard(blackjackDealer, true);



        // code/method for destroying the contents of the hand containers? 

    }

    private void Cleanup()
    {
        blackjackDealer.handTotal = 0;
        blackjackDealer.isBust = false;
        blackjackPlayer.handTotal = 0;
        blackjackPlayer.isBust = false;

        //blackjackDealer.isUpNext = false
        //blackjackPlayer.isUpNext = true;
    }

    private void PostGame()
    {
        hitButtonObject.SetActive(false);
        standButtonObject.SetActive(false); 
        newGameButtonObject.SetActive(true);
        quitGameButtonObject.SetActive(true);
        dealerTotalObject.SetActive(true); 
		
		if (!blackjackPlayer.isBust) 
		{
			blackjackPlayer.chips += blackjackPlayer.wager; 
		}
    }

    private void Update()
    {
        if (blackjackPlayer.isStanding)
        {
            DealCard(blackjackDealer, true); 
            if (blackjackDealer.handTotal >= 17 && blackjackDealer.handTotal <= 21)
            {
                blackjackDealer.isStanding = true; 
            }
        }
        else if (blackjackPlayer.isBust)
        {
            
        }
        //if (blackjackPlayer.isBust || blackjackDealer.isBust)
        //{
        //    PostGame(); 
        //}


        //if (blackjackPlayer.isStanding && !blackjackDealer.isBust)
        //{

        //}

        //if (blackjackDealer.isUpNext && !blackjackDealer.isBust)
        //{
        //    DealCard(blackjackDealer, false);
        //}


        // check if player has an ace - if so, allow them to choose high or low? (soft hand)* 
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        //GUI.Label(new Rect(10, 0, 0, 0), $"Player total: {Player.handtotal}", style);
    }
}