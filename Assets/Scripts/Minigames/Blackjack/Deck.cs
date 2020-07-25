using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class Deck
{
    public static List<Card> deck = new List<Card>();
    public static List<Card> defaultDeck = new List<Card>();
    public static List<Card> drawnCards = new List<Card>();
    public static Sprite cardbackSprite;
    public static Sprite[] cardSprites;

    public static void InitialiseDeck()
    {
        cardbackSprite = Resources.Load<Sprite>("Sprites/Cardbacks/cardback1");

		// Sprite names have the following format: [suit][1..9, 91..94] - so that we don't need to use .OrderBy() 
		// [suit][1..13] is not alphabetically ordered
        cardSprites = Resources.LoadAll<Sprite>("Sprites/PlayingCards");
        int index = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                int value = j < 10 ? j : 10;
                defaultDeck.Add(new Card(value, false, cardSprites[index]));
                index++;
            }
        }

        deck = defaultDeck;
    }

    public static void ShuffleDeck()
    {
        deck = defaultDeck;

        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            Card temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public static Card GetRandomCard()
    {
        int randomIndex = Random.Range(0, deck.Count);
        Card drawnCard = deck[randomIndex];
        deck.RemoveAt(randomIndex);
        drawnCards.Add(drawnCard);
        return drawnCard;
    }
}