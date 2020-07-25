using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackjackUtils : MonoBehaviour
{
	// public static void InitialiseNewSessionButton(GameObject parentObject, GameObject newSessionButtonObject, GameObject blackjackButtonPrefab) 
	// {
		// newSessionButtonObject = Instantiate(blackjackButtonPrefab);
		// newSessionButtonObject.name = BlackjackConstants.newSessionButtonName;  
		// newSessionButtonObject.transform.parent = parentObject.transform; 
		// newSessionButtonObject.transform.localPosition = Vector2.one / 2f;  
		
		// Button newSessionButton = newSessionButtonObject.GetComponent<Button>();
        // newSessionButton.GetComponentInChildren<Text>().text = BlackjackConstants.newSessionButtonText; 
        // newSessionButton.onClick.RemoveAllListeners();
        // newSessionButton.onClick.AddListener(delegate { SetupTable(); });
	// } 
	
	// public static void SetupListeners(Button button, EventHandler handler) 
	// {
		// button.onClick.RemoveAllListeners();
		// button.AddListener(handler); 
	// }
	
	public static void InitialiseTableContainer(GameObject parentObject, GameObject blackjackTableContainer) 
	{
		blackjackTableContainer = new GameObject(BlackjackConstants.tableContainerName);
        LayoutElement layoutElement = blackjackTableContainer.AddComponent<LayoutElement>();
        VerticalLayoutGroup verticalLayoutGroup = blackjackTableContainer.AddComponent<VerticalLayoutGroup>();
        verticalLayoutGroup.childControlWidth = true;
        verticalLayoutGroup.childControlHeight = true;
        verticalLayoutGroup.childForceExpandWidth = true;
        verticalLayoutGroup.childForceExpandHeight = true;
        verticalLayoutGroup.spacing = BlackjackConstants.tableSpacing; 
		verticalLayoutGroup.padding.left = BlackjackConstants.tablePaddingLeft;
        blackjackTableContainer.AddComponent<Image>().color = BlackjackConstants.tableColor;

        RectTransform rectTransform = blackjackTableContainer.GetComponent<RectTransform>();
        RectTransform parentRectTransform = parentObject.GetComponent<RectTransform>();
		blackjackTableContainer.transform.parent = parentObject.transform;
        rectTransform.anchoredPosition = parentRectTransform.position;
        rectTransform.anchorMin = Vector2.zero; 
        rectTransform.anchorMax = Vector2.one; 
        rectTransform.pivot = Vector2.one / 2f; 
		rectTransform.localScale = Vector3.one; 
		rectTransform.offsetMin = Vector2.zero; 
		rectTransform.offsetMax = Vector2.zero; 
	}
	
	public static void InitialiseHeader(GameObject parentObject, GameObject headerTextObject) 
	{
		// GameObject headerTextObject = new GameObject(BlackjackConstants.headerName);
		headerTextObject = new GameObject(BlackjackConstants.headerName); 
        headerTextObject.transform.parent = parentObject.transform;
        Text headerText = headerTextObject.AddComponent<Text>();
		headerText.color = Color.black; 
        headerText.text = BlackjackConstants.headerText;
        
	}
	
	public static void InitialiseGameInfo(GameObject parentObject, GameObject gameInfoObject, Text gameInfoText, BlackjackPlayer blackjackPlayer) 
	{
		gameInfoObject = new GameObject(BlackjackConstants.gameInfoName);
        gameInfoObject.transform.parent = parentObject.transform; 
		gameInfoText = gameInfoObject.AddComponent<Text>(); 
		gameInfoText.color = Color.black; 
		SetFont(gameInfoText); 
		gameInfoText.text = $"You have {blackjackPlayer.chips} chips. Place a bet to begin the game!";
	}
	
	public static void InitialiseCardContainer(GameObject parentObject, GameObject cardContainer, GameObject totalObject, Text totalText, bool isDealer) 
	{
		string cardContainerName = isDealer ? BlackjackConstants.dealerContainerName : BlackjackConstants.playerContainerName; 
		cardContainer = new GameObject(cardContainerName); 
		
		cardContainer.transform.parent = parentObject.transform;
		RectTransform rectTransform = cardContainer.AddComponent<RectTransform>();
		// 
        rectTransform.offsetMax = BlackjackConstants.containerOffsetMax; //  
        cardContainer.AddComponent<GridLayoutGroup>().cellSize = BlackjackConstants.cardCellSize;
		
		string totalObjectName = isDealer ? BlackjackConstants.dealerTotalName : BlackjackConstants.playerTotalName;
        totalObject = new GameObject(totalObjectName);
		
        totalObject.transform.parent = cardContainer.transform;
        totalText = totalObject.AddComponent<Text>();
		SetFont(totalText); 
		totalText.color = Color.black;
	}
	
	public static void InitialiseButtonContainer(GameObject parentObject, GameObject blackjackButtonContainer)  
	{
		blackjackButtonContainer = new GameObject(BlackjackConstants.buttonContainerName);
        blackjackButtonContainer.transform.parent = parentObject.transform;
        blackjackButtonContainer.AddComponent<GridLayoutGroup>().cellSize = BlackjackConstants.buttonCellSize; 
	}
	
	private static void InitialiseWagerButtonObject(GameObject parentObject, GameObject buttonObject, GameObject blackjackButtonPrefab, BlackjackPlayer blackjackPlayer, int wager) 
	{
		string buttonObjectName = $"Wager{wager}Button"; 
		buttonObject = Instantiate(blackjackButtonPrefab); 
		buttonObject.name = buttonObjectName;
		buttonObject.transform.parent = parentObject.transform; 
		Button button = buttonObject.AddComponent<Button>(); 
		button.GetComponentInChildren<Text>().text = $"Wager {wager}";
		button.onClick.RemoveAllListeners(); 
		// button.onClick.AddListener(delegate { HandleWager(_blackjackPlayer, wager); }); 
	}
	
	public static void InitialiseHitButton(GameObject parentObject, GameObject hitButtonObject, GameObject blackjackButtonPrefab) 
	{
		hitButtonObject = Instantiate(blackjackButtonPrefab);
		hitButtonObject.name = BlackjackConstants.hitButtonName; 
        hitButtonObject.transform.parent = parentObject.transform;
		hitButtonObject.SetActive(false); 

        Button hitButton = hitButtonObject.GetComponent<Button>();
        hitButton.GetComponentInChildren<Text>().text = BlackjackConstants.hitButtonText; 
        hitButton.onClick.RemoveAllListeners();
        // hitButton.onClick.AddListener(delegate { DealCard(blackjackPlayer, true); });
	}
	
	public static void InitialiseStandButton(GameObject parentObject, GameObject standButtonObject, GameObject blackjackButtonPrefab)
	{
		standButtonObject = Instantiate(blackjackButtonPrefab);
		standButtonObject.name = BlackjackConstants.standButtonName; 
        standButtonObject.transform.parent = parentObject.transform;
		standButtonObject.SetActive(false); 

        Button standButton = standButtonObject.GetComponent<Button>();
        standButton.GetComponentInChildren<Text>().text = BlackjackConstants.standButtonText; 
        standButton.onClick.RemoveAllListeners();
        // standButton.onClick.AddListener(delegate { StandPlayer(); });
	}
	
	public static void InitialiseNewGameButton(GameObject parentObject, GameObject newGameButtonObject, GameObject blackjackButtonPrefab)
	{
		newGameButtonObject = Instantiate(blackjackButtonPrefab);
		newGameButtonObject.name = BlackjackConstants.newGameButtonName;  
        newGameButtonObject.transform.parent = parentObject.transform;
		newGameButtonObject.SetActive(false); 
        Button newGameButton = newGameButtonObject.GetComponent<Button>();
        newGameButton.GetComponentInChildren<Text>().text = BlackjackConstants.newGameButtonText;
		newGameButton.onClick.RemoveAllListeners(); 
		// newGameButton.onClick.AddListener(delegate { NewGame(); });
	}
	
	public static void InitialiseQuitGameButton(GameObject parentObject, GameObject quitGameButtonObject, GameObject blackjackButtonPrefab)
	{
		quitGameButtonObject = Instantiate(blackjackButtonPrefab);
		quitGameButtonObject.name = BlackjackConstants.quitGameButtonName;  
        quitGameButtonObject.transform.parent = parentObject.transform;
		quitGameButtonObject.SetActive(false); 
        Button quitGameButton = quitGameButtonObject.GetComponent<Button>();
        quitGameButton.GetComponentInChildren<Text>().text = BlackjackConstants.quitGameButtonText; 
		quitGameButton.onClick.RemoveAllListeners();
		// quitGameButton.onClick.AddListener(delegate { QuitGame(); }); 
	}

	public static void SetFont(Text textComponent) 
	{
		textComponent.font = Resources.GetBuiltinResource(typeof(Font), BlackjackConstants.fontName) as Font;
	} 
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
