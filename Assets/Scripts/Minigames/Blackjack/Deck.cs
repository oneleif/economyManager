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
    public static Sprite cardBackSprite;
    public static Sprite[] cardSprites;

    // Stores cards drawn from the deck so they can be shuffled back into the deck after the game 
    // Without having to call InitialiseDeck() again 


    //public static 

    public static void InitialiseDeck()
    {
        cardBackSprite = Resources.Load<Sprite>("Sprites/Cardbacks/cardback1");

        cardSprites = Resources.LoadAll<Sprite>("Sprites/PlayingCards");
        int index = 0;

        for (int i = 0; i < Enum.GetNames(typeof(Suits)).Length; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                int value = j < 10 ? j : 10;
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
}