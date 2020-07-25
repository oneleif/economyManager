using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlackjackUtils : MonoBehaviour
{
    public static void SetupListeners(Button button, UnityAction handler)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(handler);
    }

    public static GameObject InitialiseTableContainer(GameObject parentObject) 
	{
		GameObject blackjackTableContainer = new GameObject(BlackjackConstants.tableContainerName);
		blackjackTableContainer.transform.parent = parentObject.transform;

		LayoutElement layoCutElement = blackjackTableContainer.AddComponent<LayoutElement>();
        VerticalLayoutGroup verticalLayoutGroup = blackjackTableContainer.AddComponent<VerticalLayoutGroup>();
        verticalLayoutGroup.childControlWidth = true;
        verticalLayoutGroup.childControlHeight = true;
        verticalLayoutGroup.childForceExpandWidth = true;
        verticalLayoutGroup.childForceExpandHeight = true;
        verticalLayoutGroup.spacing = BlackjackConstants.tableSpacing; 
		verticalLayoutGroup.padding.left = BlackjackConstants.tablePaddingLeft;

        blackjackTableContainer.AddComponent<Image>().color = BlackjackConstants.tableColor;

        RectTransform rectTransform = blackjackTableContainer.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero; 
        rectTransform.anchorMax = Vector2.one; 
		rectTransform.localScale = Vector3.one; 
		rectTransform.offsetMin = Vector2.zero; 
		rectTransform.offsetMax = Vector2.zero;

		return blackjackTableContainer;
	}
	
	public static void InitialiseHeader(GameObject parentObject) 
	{
		GameObject headerTextObject = new GameObject(BlackjackConstants.headerName); 
        headerTextObject.transform.parent = parentObject.transform;
        Text headerText = headerTextObject.AddComponent<Text>();
		headerText.color = Color.black; 
        headerText.text = BlackjackConstants.headerText;
		SetFont(headerText);
	}
	
	public static Text InitialiseGameInfo(GameObject parentObject, BlackjackPlayer blackjackPlayer) 
	{
		GameObject gameInfoObject = new GameObject(BlackjackConstants.gameInfoName);
        gameInfoObject.transform.parent = parentObject.transform; 
		Text gameInfoText = gameInfoObject.AddComponent<Text>(); 
		gameInfoText.color = Color.black;
		SetFont(gameInfoText); 
		gameInfoText.text = $"You have {blackjackPlayer.chips} chips. Place a bet to begin the game!";

		return gameInfoText;
	}
	
	public static GameObject InitialiseCardContainer(GameObject parentObject, bool isDealer) 
	{
		string cardContainerName = isDealer ? BlackjackConstants.dealerContainerName : BlackjackConstants.playerContainerName;
		GameObject cardContainer = new GameObject(cardContainerName); 
		
		cardContainer.transform.parent = parentObject.transform;
		
        cardContainer.AddComponent<GridLayoutGroup>().cellSize = BlackjackConstants.cardCellSize;
		return cardContainer;
	}

	public static Text InitializeTotalText(GameObject parentObject, bool isDealer)
    {
		string totalObjectName = isDealer ? BlackjackConstants.dealerTotalName : BlackjackConstants.playerTotalName;
		GameObject totalObject = new GameObject(totalObjectName);
		totalObject.transform.parent = parentObject.transform;

		Text totalText = totalObject.AddComponent<Text>();
		SetFont(totalText);
		totalText.color = Color.black;
		return totalText;
	}
	
	public static GameObject InitialiseButtonContainer(GameObject parentObject)  
	{
		GameObject blackjackButtonContainer = new GameObject(BlackjackConstants.buttonContainerName);
        blackjackButtonContainer.transform.parent = parentObject.transform;
        blackjackButtonContainer.AddComponent<GridLayoutGroup>().cellSize = BlackjackConstants.buttonCellSize;
		return blackjackButtonContainer;
	}

	public static void SetFont(Text textComponent) 
	{
		textComponent.font = Resources.GetBuiltinResource(typeof(Font), BlackjackConstants.fontName) as Font;
	}
}