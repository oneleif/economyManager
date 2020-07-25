using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

// Should we make this derive from MonoBehaviour
// And then get a reference to the ScriptableObjects? 
public static class BlackjackConstants 
{ 

	// Dimensions
	public static int tableSpacing = 32;
	public static int tablePaddingLeft = 32;
	public static Vector2 cardCellSize = new Vector2(60f, 84f); 
	public static Vector2 buttonCellSize = new Vector2(128f, 32f);
	public static Vector2 containerOffsetMax = new Vector2(16f, 16f); 
	
	// GameObject names 
	public static string headerName = "BlackjackHeader"; 
	public static string gameInfoName = "BlackjackGameInfo"; 
	public static string tableContainerName = "BlackjackTableContainer"; 
	public static string playerContainerName = "PlayerCardContainer"; 
	public static string playerTotalName = "PlayerTotalText"; 
	public static string dealerContainerName = "DealerCardContainer";
	public static string dealerTotalName = "DealerTotalText"; 
	public static string buttonContainerName = "BlackjackButtonContainer"; 
	public static string newSessionButtonName = "NewBlackjackSessionButton"; 
	public static string wager100ButtonName = "Wager100Button";
	public static string wager500ButtonName = "Wager500Button";
	public static string wager1000ButtonName = "Wager1000Button";
	public static string hitButtonName = "HitButton"; 
	public static string standButtonName = "StandButton"; 
	public static string newGameButtonName = "NewGameButton"; 
	public static string quitGameButtonName = "QuitGameButton";  
	
	// Text 
	public static string headerText = "Blackjack Table";
	public static string newSessionButtonText = "Play Blackjack"; 
	public static string hitButtonText = "Hit!";
	public static string standButtonText = "Stand";
	public static string newGameButtonText = "Play again";
	public static string quitGameButtonText = "Leave table";
	public static string wager100ButtonText = "Wager 100";
	public static string wager500ButtonText = "Wager 500";
	public static string wager1000ButtonText = "Wager 1000";
	public static string dealerUnknownTotalText = "Dealer total: ?"; 
	
	// Misc 
	public static string fontName = "Arial.ttf"; 
	public static Color tableColor = Color.green; 
}


// YAGNI 
	// public static string gameInfoText = "";
	// public static string playerTotalText = ""; 
	// public static string dealerTotalText = "";