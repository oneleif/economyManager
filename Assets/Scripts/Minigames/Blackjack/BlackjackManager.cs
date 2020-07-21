using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackjackManager : MonoBehaviour
{
    private GameObject blackjackTableContainer;
	
    private GameObject playerCardContainer;
    private GameObject dealerCardContainer;
    public GameObject blackjackButtonPrefab;
    private GameObject blackjackButtonContainer;  

    private GameObject hitButtonObject;
    private GameObject standButtonObject;
	private GameObject newSessionButtonObject; 
    private GameObject newGameButtonObject;
    private GameObject quitGameButtonObject;
	
	private GameObject wager100ButtonObject;
	private GameObject wager500ButtonObject; 
	private GameObject wager1000ButtonObject; 

	private GameObject gameInfoObject;
	private Text gameInfoText; 
    private GameObject wagerTextObject;
	private Text wagerText; 
    private GameObject playerTotalObject;
	private Text playerTotalText;
    private GameObject dealerTotalObject;
	private Text dealerTotalText;	

	// Scriptable objects 
    public BlackjackPlayer blackjackPlayer;
    public BlackjackPlayer blackjackDealer;

    private void Start()
    {
        InitialiseNewSessionButton(); 
		blackjackPlayer.chips = 2500; 
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
        drawnCardObject.AddComponent<Image>().sprite = faceUp ? drawnCard.sprite : Deck.cardbackSprite;
		_blackjackPlayer.handTotal += drawnCard.value;

        // Check if ace should be high or low, and adjust total value accordingly 
        foreach (Card card in _blackjackPlayer.hand)
        {
		   if (card.value == 1) 
		   {
			   if (!card.isHigh && _blackjackPlayer.handTotal + 9 <= 21) 
			   {
				   card.isHigh = true; 
				   _blackjackPlayer.handTotal += 9; 
			   }
			   else if (card.isHigh && _blackjackPlayer.handTotal > 21) 
			   {
				   card.isHigh = false; 
				   _blackjackPlayer.handTotal -= 9; 
			   }
		   }
        }
				
		// Don't show the dealer's total  
		if (!_blackjackPlayer.isDealer) 
		{
			playerTotalText.text = $"Your total: {blackjackPlayer.handTotal}";
		}

        Debug.Log("Drawn card name: " + drawnCard.sprite.name + "Drawn value: " + drawnCard.value);
		
		// Automatically stand the player if they get blackjack 
		if (_blackjackPlayer.handTotal == 21) 
		{
			StandPlayer();
		}
		
		// Check if bust 
        if (_blackjackPlayer.handTotal > 21)
        {
            _blackjackPlayer.isBust = true;
			PostGame(); 
        }
    }

    private void StandPlayer()
    {
        blackjackPlayer.isStanding = true;
        standButtonObject.SetActive(false); 
    }
	
    private void SetupTable()
    {
		// Create overarching container object 
        blackjackTableContainer = new GameObject("BlackjackTableContainer");
        LayoutElement layoutElement = blackjackTableContainer.AddComponent<LayoutElement>();
        VerticalLayoutGroup verticalLayoutGroup = blackjackTableContainer.AddComponent<VerticalLayoutGroup>();
        verticalLayoutGroup.childControlWidth = true;
        verticalLayoutGroup.childControlHeight = true;
        verticalLayoutGroup.childForceExpandWidth = true;
        verticalLayoutGroup.childForceExpandHeight = true;
        verticalLayoutGroup.spacing = 32f; 
		verticalLayoutGroup.padding.left = 32; 
        blackjackTableContainer.AddComponent<Image>().color = Color.green;

        RectTransform rectTransform = blackjackTableContainer.GetComponent<RectTransform>();
        RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
		blackjackTableContainer.transform.parent = gameObject.transform;
        rectTransform.anchoredPosition = parentRectTransform.position;
        rectTransform.anchorMin = new Vector2(0f, 0f);
        rectTransform.anchorMax = new Vector2(1f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
		rectTransform.localScale = Vector3.one; 
		rectTransform.offsetMin = Vector2.zero; 
		rectTransform.offsetMax = Vector2.zero; 

		// Create header object 
        GameObject headerTextObject = new GameObject("HeaderText");
        headerTextObject.transform.parent = blackjackTableContainer.transform;
        Text headerText = headerTextObject.AddComponent<Text>();
		headerText.color = Color.black; 
        headerText.text = "Blackjack Table";
        headerText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

		// Create container for information about the current session 
        gameInfoObject = new GameObject("BlackjackGameInfo");
        gameInfoObject.transform.parent = blackjackTableContainer.transform; 
		gameInfoText = gameInfoObject.AddComponent<Text>(); 
		gameInfoText.color = Color.black; 
		gameInfoText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		gameInfoText.text = $"You have {blackjackPlayer.chips} chips. Place a bet to begin the game!"; 

		// Create container for the player's hand 
        playerCardContainer = new GameObject("PlayerCardContainer");
        playerCardContainer.transform.parent = blackjackTableContainer.transform;
        rectTransform = playerCardContainer.AddComponent<RectTransform>();
        playerCardContainer.AddComponent<GridLayoutGroup>().cellSize = new Vector2(60, 84f);
        playerTotalObject = new GameObject("PlayerTotalText");
        playerTotalObject.transform.parent = playerCardContainer.transform;
        playerTotalText = playerTotalObject.AddComponent<Text>();
        playerTotalText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        playerTotalText.color = Color.black;

		// Create container for the dealer's hand 
        dealerCardContainer = new GameObject("DealerCardContainer");
        dealerCardContainer.transform.parent = blackjackTableContainer.transform;
        dealerCardContainer.AddComponent<GridLayoutGroup>().cellSize = new Vector2(60f, 84f);
        dealerTotalObject = new GameObject("DealerTotalText");
        dealerTotalObject.transform.parent = dealerCardContainer.transform;
        dealerTotalText = dealerTotalObject.AddComponent<Text>();
        dealerTotalText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        dealerTotalText.color = Color.black; 

		// Create container object for all in-game buttons 
        blackjackButtonContainer = new GameObject("BlackjackButtonContainer");
        blackjackButtonContainer.transform.parent = blackjackTableContainer.transform;
        blackjackButtonContainer.AddComponent<GridLayoutGroup>().cellSize = new Vector2(128f, 32f);
        rectTransform = blackjackButtonContainer.AddComponent<RectTransform>();

		// Create buttons for selecting no. of chips to wager 
		wager100ButtonObject = Instantiate(blackjackButtonPrefab);
		wager100ButtonObject.name = "Wager100Button";
		wager100ButtonObject.transform.parent = blackjackButtonContainer.transform; 
		Button button = wager100ButtonObject.GetComponent<Button>();  
		button.GetComponentInChildren<Text>().text = "Wager 100";
		button.onClick.RemoveAllListeners(); 
		button.onClick.AddListener(delegate { HandleWager(blackjackPlayer, 100); });
		
		wager500ButtonObject = Instantiate(blackjackButtonPrefab);
		wager500ButtonObject.name = "Wager500Button";
		wager500ButtonObject.transform.parent = blackjackButtonContainer.transform; 
		button = wager500ButtonObject.GetComponent<Button>(); 
		button.GetComponentInChildren<Text>().text = "Wager 500";
		button.onClick.RemoveAllListeners(); 
		button.onClick.AddListener(delegate { HandleWager(blackjackPlayer, 500); });
		
		wager1000ButtonObject = Instantiate(blackjackButtonPrefab);
		wager1000ButtonObject.name = "Wager1000Button";
		wager1000ButtonObject.transform.parent = blackjackButtonContainer.transform; 
		button = wager1000ButtonObject.GetComponent<Button>();
		button.GetComponentInChildren<Text>().text = "Wager 1000";
		button.onClick.RemoveAllListeners(); 
		button.onClick.AddListener(delegate { HandleWager(blackjackPlayer, 1000); });

        // Create button for "hitting" 
        hitButtonObject = Instantiate(blackjackButtonPrefab);
		hitButtonObject.name = "HitButton"; 
        hitButtonObject.transform.parent = blackjackButtonContainer.transform;
		hitButtonObject.SetActive(false); 

        Button hitButton = hitButtonObject.GetComponent<Button>();
        hitButton.GetComponentInChildren<Text>().text = "Hit!";
        hitButton.onClick.RemoveAllListeners();
        hitButton.onClick.AddListener(delegate { DealCard(blackjackPlayer, true); });

        // Create button for "standing" 
        standButtonObject = Instantiate(blackjackButtonPrefab);
		standButtonObject.name = "StandButton"; 
        standButtonObject.transform.parent = blackjackButtonContainer.transform;
		standButtonObject.SetActive(false); 

        Button standButton = standButtonObject.GetComponent<Button>();
        standButton.GetComponentInChildren<Text>().text = "Stand";
        standButton.onClick.RemoveAllListeners();
        standButton.onClick.AddListener(delegate { StandPlayer(); });

        // Create button for "splitting" (TBC) 
        // 

        // Create new game/quit buttons  
        newGameButtonObject = Instantiate(blackjackButtonPrefab);
		newGameButtonObject.name = "NewGameButton"; 
        newGameButtonObject.transform.parent = blackjackButtonContainer.transform;
		newGameButtonObject.SetActive(false); 
        Button newGameButton = newGameButtonObject.GetComponent<Button>();
        newGameButton.GetComponentInChildren<Text>().text = "Play again";
		newGameButton.onClick.RemoveAllListeners(); 
		newGameButton.onClick.AddListener(delegate { NewGame(); });  

        quitGameButtonObject = Instantiate(blackjackButtonPrefab);
		quitGameButtonObject.name = "QuitGameButton"; 
        quitGameButtonObject.transform.parent = blackjackButtonContainer.transform;
		quitGameButtonObject.SetActive(false); 
        Button quitGameButton = quitGameButtonObject.GetComponent<Button>();
        quitGameButton.GetComponentInChildren<Text>().text = "Leave table";
		quitGameButton.onClick.RemoveAllListeners();
		quitGameButton.onClick.AddListener(delegate { QuitGame(); }); 

        blackjackDealer.isDealer = true; 
        Deck.InitialiseDeck();
        Deck.ShuffleDeck();
    }
	
	private void InitialiseNewSessionButton() 
	{
		newSessionButtonObject = Instantiate(blackjackButtonPrefab);
		newSessionButtonObject.name = "NewBlackjackSessionButton"; 
		newSessionButtonObject.transform.parent = gameObject.transform; 
		newSessionButtonObject.transform.localPosition = new Vector2(0.5f, 0.5f); 
		
		Button newSessionButton = newSessionButtonObject.GetComponent<Button>();
        newSessionButton.GetComponentInChildren<Text>().text = "Play Blackjack";
        newSessionButton.onClick.RemoveAllListeners();
        newSessionButton.onClick.AddListener(delegate { SetupTable(); });
	} 
	
	// Tried to make creating the card container creation modular 
	// But got NullReferenceException 
	// Something to do with mutating the parameters? 
	private void InitialiseCardContainer(GameObject cardContainer, GameObject totalObject, Text totalText, bool isDealer) 
	{
		string cardContainerName = isDealer ? "DealerCardContainer" : "PlayerCardContainer"; 
		cardContainer = new GameObject(cardContainerName); 
		
		cardContainer.transform.parent = blackjackTableContainer.transform;
		RectTransform rectTransform = cardContainer.AddComponent<RectTransform>();
        rectTransform.offsetMax = new Vector2(16f, 16f);
        cardContainer.AddComponent<GridLayoutGroup>().cellSize = new Vector2(60, 84f);
        //GridLayoutGroup gridLayoutGroup = playerCardContainer.AddComponent<GridLayoutGroup>();
        //gridLayoutGroup.cellSize = new Vector2(60, 84f);
		
		string totalObjectName = isDealer ? "DealerTotalText" : "PlayerTotalText";
        totalObject = new GameObject(totalObjectName);
		
        totalObject.transform.parent = cardContainer.transform;
        totalText = totalObject.AddComponent<Text>();
        totalText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        totalText.color = Color.black;
	}
	
	// Same problem with this method 
	private void InitialiseWagerButtonObject(GameObject buttonObject, BlackjackPlayer _blackjackPlayer, int wager) 
	{
		string buttonObjectName = $"Wager{wager}Button"; 
		buttonObject = Instantiate(blackjackButtonPrefab); 
		buttonObject.name = buttonObjectName;
		buttonObject.transform.parent = blackjackButtonContainer.transform; 
		Button button = buttonObject.AddComponent<Button>(); 
		button.GetComponentInChildren<Text>().text = $"Wager {wager}";
		button.onClick.RemoveAllListeners(); 
		button.onClick.AddListener(delegate { HandleWager(_blackjackPlayer, wager); }); 
	}

    private void HandleWager(BlackjackPlayer _blackjackPlayer, int wager)
    {
		if (wager <= _blackjackPlayer.chips) 
		{
			_blackjackPlayer.wager = wager; 
			_blackjackPlayer.chips -= wager; 
					
			// Game begins once player has wagered 
			NewGame();
		} 
    }
	
	// Would prefer to create SO's at run-time, but had issues with it 
    private void InitialiseScriptableObjects()
    {
        blackjackPlayer = ScriptableObject.CreateInstance<BlackjackPlayer>();
        blackjackDealer = ScriptableObject.CreateInstance<BlackjackPlayer>();
        blackjackDealer.isDealer = true;

        // This method could be extended to handle multiple players 
        // Using a BlackjackPlayer container
    }

    private void NewGame()
    {
		// Put drawn cards back into the deck before shuffling 
        if (Deck.drawnCards.Count > 0)
        {
            foreach (Card card in Deck.drawnCards)
            {
                Deck.deck.Add(card);
            }
			Deck.drawnCards.Clear(); 
        }
        Deck.ShuffleDeck();
		
		DestroyChildCardObjects(playerCardContainer.transform);
		DestroyChildCardObjects(dealerCardContainer.transform);
	
        blackjackPlayer.handTotal = 0;
        blackjackPlayer.hand = new List<Card>(); 
        blackjackPlayer.isStanding = false;
        blackjackPlayer.isBust = false;
		
        blackjackDealer.handTotal = 0;
        blackjackDealer.hand = new List<Card>();
        blackjackDealer.isStanding = false;
        blackjackDealer.isBust = false; 
        
		// Display relevant buttons 
		// hitButtonObject.SetActive(true);
        // standButtonObject.SetActive(true); 
        // newGameButtonObject.SetActive(false);
        // quitGameButtonObject.SetActive(false);
		
		foreach (Transform child in blackjackButtonContainer.transform) 
		{
			if (child.gameObject.name == "HitButton" || child.gameObject.name == "StandButton") 
			{ 
				child.gameObject.SetActive(true);
			}
			else 
			{
				child.gameObject.SetActive(false);
			}
		} 

        // Deal starting hands 
        DealCard(blackjackPlayer, true);
        DealCard(blackjackDealer, false);
        DealCard(blackjackPlayer, true);
        DealCard(blackjackDealer, true);

		gameInfoText.text = $"Current chips: {blackjackPlayer.chips} | Current wager: {blackjackPlayer.wager}"; 
		dealerTotalText.text = "Dealer's total: ?"; 
    }
	
	private void QuitGame() 
	{
		// Destroy everything 
		// Seems buggy - do we need to recursively delete? 
		DestroyImmediate(blackjackTableContainer); 
		
	}
	
	// Thought we needed this method but maybe not 
	// It's public to make it reusable elsewhere? 
	public void DeepCopyArray(GameObject[] arrayDonor, GameObject[] arrayRecipient) 
	{ 
		for (int i = 0; i < arrayDonor.Length; i++) 
		{
			arrayRecipient[i] = (GameObject)Instantiate(arrayDonor[i]); 
		} 
	} 

	public void DestroyChildCardObjects(Transform _transform) 
	{ 
		foreach (Transform child in _transform) 
		{
			if (child.gameObject.name == "CardObject") 
			{
				Destroy(child.gameObject); 
			}
		}
	}

    private void PostGame()
    {
		dealerTotalText.text = $"Dealer's total: {blackjackDealer.handTotal}"; 
		bool playerWins; 
		
		// This seems more readable than the ternary expression 
		if (blackjackPlayer.isBust) 
		{
			playerWins = false;
		}
		else 
		{
			if (blackjackDealer.isBust) 
			{
				playerWins = true; 
			}
			else 
			{ 
				if (blackjackPlayer.handTotal > blackjackDealer.handTotal) 
				{
					playerWins = true; 
				}
				else 
				{ 
					playerWins = false; 
				}
			}
		}
					
        hitButtonObject.SetActive(false);
        standButtonObject.SetActive(false); 
        newGameButtonObject.SetActive(true);
        quitGameButtonObject.SetActive(true);
		
		if (playerWins) 
		{
			blackjackPlayer.chips += blackjackPlayer.wager; 
			gameInfoText.text = $"Player wins {blackjackPlayer.wager} chips!"; 
		}
		else
		{
			gameInfoText.text = $"Player loses {blackjackPlayer.wager} chips!"; 
		}
    }
	
    private void Update()
    {		
		// Check if it's dealer's turn to draw cards 
        if (blackjackPlayer.isStanding && !blackjackDealer.isBust)
        {
            DealCard(blackjackDealer, true); 
            if (blackjackDealer.handTotal >= 17 && blackjackDealer.handTotal <= 21)
            {
				blackjackDealer.isStanding = true; 
				PostGame(); 
            }
        }
    }
}