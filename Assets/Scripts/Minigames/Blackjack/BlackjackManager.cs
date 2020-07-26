using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlackjackManager : MonoBehaviour
{
	// Prefabs 
	public GameObject blackjackButtonPrefab;

	// Container objects 
	[SerializeField]
	private GameObject parentContainer;
    private GameObject blackjackButtonContainer;  
    private GameObject blackjackTableContainer;
	private GameObject playerCardContainer;
	private GameObject dealerCardContainer;

	// Play buttons
	private GameObject hitButtonObject;
	private GameObject standButtonObject;
	private GameObject newGameButtonObject;
	private GameObject quitGameButtonObject;

	// Wager buttons
	private GameObject lowWagerObject;
	private GameObject mediumWagerObject;
	private GameObject highWagerObject;

	// Game Info
	private Text gameInfo;
	private Text playerTotal;
	private Text dealerTotal;

	// Scriptable objects 
    public BlackjackPlayer blackjackPlayer;
    public BlackjackPlayer blackjackDealer;

	public enum PlayButtonType
	{
		Hit, Stand, NewGame, QuitGame
	}

	private void Start()
    {
        InitialiseNewSessionButton(); 
		blackjackPlayer.chips = BlackjackConstants.playerStartingChips;
    }

	public void InitialiseNewSessionButton()
	{
		GameObject newSessionButtonObject = Instantiate(blackjackButtonPrefab);
		newSessionButtonObject.name = BlackjackConstants.newSessionButtonName;
		newSessionButtonObject.transform.parent = parentContainer.transform;
		newSessionButtonObject.transform.localPosition = Vector2.one / 2f;

		Button newSessionButton = newSessionButtonObject.GetComponent<Button>();
		newSessionButton.GetComponentInChildren<Text>().text = BlackjackConstants.newSessionButtonText;
		newSessionButton.onClick.RemoveAllListeners();
		newSessionButton.onClick.AddListener(delegate { SetupTable(); });
	}
	private void SetupTable()
	{
		blackjackTableContainer = BlackjackUtils.InitialiseTableContainer(parentContainer);

        BlackjackUtils.InitialiseHeader(blackjackTableContainer);
		gameInfo = BlackjackUtils.InitialiseGameInfo(blackjackTableContainer, blackjackPlayer);

		playerCardContainer = BlackjackUtils.InitialiseCardContainer(blackjackTableContainer, isDealer: false);
		playerTotal = BlackjackUtils.InitializeTotalText(playerCardContainer, isDealer: false);
		dealerCardContainer = BlackjackUtils.InitialiseCardContainer(blackjackTableContainer, isDealer: true);
		dealerTotal = BlackjackUtils.InitializeTotalText(dealerCardContainer, isDealer: true);

		blackjackButtonContainer = BlackjackUtils.InitialiseButtonContainer(blackjackTableContainer);

		lowWagerObject = InitialiseWagerButtonObject(BlackjackConstants.lowWager);
		mediumWagerObject = InitialiseWagerButtonObject(BlackjackConstants.mediumWager);
		highWagerObject = InitialiseWagerButtonObject(BlackjackConstants.highWager);

        hitButtonObject = InitializePlayButton(blackjackButtonContainer, blackjackButtonPrefab, PlayButtonType.Hit);
        standButtonObject = InitializePlayButton(blackjackButtonContainer, blackjackButtonPrefab, PlayButtonType.Stand);
        newGameButtonObject = InitializePlayButton(blackjackButtonContainer, blackjackButtonPrefab, PlayButtonType.NewGame);
        quitGameButtonObject = InitializePlayButton(blackjackButtonContainer, blackjackButtonPrefab, PlayButtonType.QuitGame);

        blackjackDealer.isDealer = true;
        Deck.InitialiseDeck();
        Deck.ShuffleDeck();
    }

	private GameObject InitialiseWagerButtonObject(int wager)
	{
		GameObject buttonObject = Instantiate(blackjackButtonPrefab, blackjackButtonContainer.transform);
		buttonObject.name = $"Wager{wager}Button";
		Button button = buttonObject.GetComponent<Button>();
		button.GetComponentInChildren<Text>().text = $"Wager {wager}";
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(delegate { HandleWager(wager); });
		return buttonObject;
	}

	private void HandleWager(int wager)
	{
		if (wager <= blackjackPlayer.chips)
		{
			blackjackPlayer.wager = wager;
			
			//Toggle off wager buttons
			lowWagerObject.SetActive(false);
			mediumWagerObject.SetActive(false);
			highWagerObject.SetActive(false);
			
			// Game begins once player has wagered 
			NewGame();
        }
        else
        {
			gameInfo.text = $"Insufficient chips! You have {blackjackPlayer.chips} remaining."; 
        }
	}

	private void NewGame()
	{
		blackjackPlayer.chips -= blackjackPlayer.wager;

		Deck.ShuffleDeck();

        DestroyChildCardObjects(playerCardContainer.transform);
        DestroyChildCardObjects(dealerCardContainer.transform);

        blackjackPlayer.Init();
		blackjackDealer.Init();

		TogglePlayButtons(inGame: true);

		// Deal starting hands 
		DealCard(blackjackPlayer, faceUp: true);
		DealCard(blackjackDealer, faceUp: false);
		DealCard(blackjackPlayer, faceUp: true);
		DealCard(blackjackDealer, faceUp: true);

        gameInfo.GetComponent<Text>().text = $"Current chips: {blackjackPlayer.chips} | Current wager: {blackjackPlayer.wager}";
        dealerTotal.text = BlackjackConstants.dealerUnknownTotalText;
    }

	private void DealCard(BlackjackPlayer _blackjackPlayer, bool faceUp)
    {
		Card drawnCard = Deck.GetRandomCard();

		_blackjackPlayer.AddCardToHand(drawnCard);

        GameObject drawnCardObject = new GameObject("CardObject");
        drawnCardObject.transform.parent = _blackjackPlayer.isDealer ? dealerCardContainer.transform : playerCardContainer.transform;
        drawnCardObject.AddComponent<Image>().sprite = faceUp ? drawnCard.sprite : Deck.cardbackSprite;

		// Don't show the dealer's total  
		if (!_blackjackPlayer.isDealer) 
		{
            playerTotal.text = $"Your total: {blackjackPlayer.handTotal}";
        }

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

	private void PostGame()
	{
        dealerTotal.text = $"Dealer's total: {blackjackDealer.handTotal}";
        bool playerWins  = false;

		if (!blackjackPlayer.isBust)
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
			}
		}

		TogglePlayButtons(inGame: false);

        if (playerWins)
		{
			blackjackPlayer.chips += 2 * blackjackPlayer.wager;
            gameInfo.text = $"Player wins {blackjackPlayer.wager} chips!";
        }
		else
		{
            gameInfo.text = $"Player loses {blackjackPlayer.wager} chips!";
        }
	}

	private void TogglePlayButtons(bool inGame)
    {
		hitButtonObject.SetActive(inGame);
		standButtonObject.SetActive(inGame);
		newGameButtonObject.SetActive(!inGame);
		quitGameButtonObject.SetActive(!inGame);
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

	public void DestroyChildCardObjects(Transform _transform) 
	{ 
		foreach (Transform child in _transform) 
		{
			if (child.gameObject.GetComponent<Image>()) 
			{
				Destroy(child.gameObject); 
			}
		}
	}

	public GameObject InitializePlayButton(GameObject parentObject, GameObject blackjackButtonPrefab, PlayButtonType buttonType)
	{
		GameObject gameObject = Instantiate(blackjackButtonPrefab);
		gameObject.name = GetButtonName(buttonType);
		gameObject.transform.parent = parentObject.transform;
		gameObject.SetActive(false);

		Button button = gameObject.GetComponent<Button>();
		button.GetComponentInChildren<Text>().text = GetButtonText(buttonType);
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(delegate {
			GetPlayButtonHandler(buttonType).Invoke();
		});

		return gameObject;
	}

	public UnityAction GetPlayButtonHandler(PlayButtonType buttonType)
	{
		switch (buttonType)
		{
			case PlayButtonType.Hit:
				return Hit;
			case PlayButtonType.Stand:
				return StandPlayer;
			case PlayButtonType.NewGame:
				return NewGame;
			case PlayButtonType.QuitGame:
				return QuitGame;
			default:
				return NewGame;
		}
	}

	private void Hit()
    {
		DealCard(blackjackPlayer, true);
    }

	private void StandPlayer()
	{
		blackjackPlayer.isStanding = true;
	}

	private void QuitGame()
	{
		Destroy(blackjackTableContainer);
	}

	public string GetButtonName(PlayButtonType buttonType)
	{
		switch (buttonType)
		{
			case PlayButtonType.Hit:
				return BlackjackConstants.hitButtonName;
			case PlayButtonType.Stand:
				return BlackjackConstants.standButtonName;
			case PlayButtonType.NewGame:
				return BlackjackConstants.newGameButtonName;
			case PlayButtonType.QuitGame:
				return BlackjackConstants.quitGameButtonName;
			default:
				Debug.Log("Button type does not exist.");
				return "";
		}
	}

	public string GetButtonText(PlayButtonType buttonType)
	{
		switch (buttonType)
		{
			case PlayButtonType.Hit:
				return BlackjackConstants.hitButtonText;
			case PlayButtonType.Stand:
				return BlackjackConstants.standButtonText;
			case PlayButtonType.NewGame:
				return BlackjackConstants.newGameButtonText;
			case PlayButtonType.QuitGame:
				return BlackjackConstants.quitGameButtonText;
			default:
				Debug.Log("Button type does not exist.");
				return "";
		}
	}
}