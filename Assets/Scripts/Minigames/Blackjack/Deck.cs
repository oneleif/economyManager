using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class Deck
{
    public static List<Card> deck = new List<Card>();
    public static List<Card> drawnCards = new List<Card>();
    public static Sprite cardbackSprite;
    public static Sprite[] cardSprites;

    // Stores cards drawn from the deck so they can be shuffled back into the deck after the game 
    // without having to call InitialiseDeck() again 

    public static void InitialiseDeck()
    {
        cardbackSprite = Resources.Load<Sprite>("Sprites/Cardbacks/cardback1");

		// Sprite names have the following format: [suit][1..9, 91..94] - so that we don't need to use .OrderBy() 
		// [suit][1..13] is not alphabetically ordered
        cardSprites = Resources.LoadAll<Sprite>("Sprites/PlayingCards");
        int index = 0;

        for (int i = 0; i < Enum.GetNames(typeof(Suits)).Length; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                int value = j < 10 ? j : 10;
				Debug.Log("value: " + value + "...cardname: " + cardSprites[index].name);  
                deck.Add(new Card(value, cardSprites[index]));
                index++;
            }
        }
    }

    public static void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, deck.Count);
            Card temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public static void LoadSprites(string path)
    {
        object[] resources = Resources.LoadAll<Sprite>(path);
        foreach (Sprite sprite in resources)
        {
            //sprites.Add(sprite); 
        }
    }

    public static void TestLoad()
    {
        Sprite[] resources = Resources.LoadAll<Sprite>("Sprites/PlayingCards/Clubs");
        for (int i = 0; i < resources.Length; i++)
        {
            //clubs[i] = resources[i];   
        }

        GameObject cardObject = new GameObject();
        cardObject.transform.SetParent(GameObject.Find("CrewPanel").transform);
        //cardObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
        //cardObject.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 0f);
        //cardObject.AddComponent<Image>().sprite = clubs[0];

    }

    public static void TestSingle()
    {
        Sprite cardSprite = Resources.Load<Sprite>("Sprites/PlayingCards/Clubs/club1");
        GameObject cardObject = new GameObject();
        cardObject.transform.SetParent(GameObject.Find("CrewPanel").transform);
        RectTransform RT = cardObject.AddComponent<RectTransform>();
        RT.localPosition = new Vector2(0f, 0f);
        RT.localPosition = new Vector2(0f, 0f);
        Image imageObject = cardObject.AddComponent<Image>();
        imageObject.sprite = cardSprite;
    }
	
	public static void PrintLoop()
	{
		foreach (Card card in deck)
		{
			Debug.Log("Card name: " + card.sprite.name + "... Card value: " + card.value); 
		}
	} 
}