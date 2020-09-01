using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generalisable to other card games 
// Even though in Blackjack suits are irrelevant
public enum Suits
{
    Clubs = 0, Diamonds = 1, Hearts = 2, Spades = 3
}
public class Card
{
    public Suits suit;
    public int value;
	// public bool isAce; 
	public bool isHigh; 
    public Sprite sprite;

    public Card(int value, bool isHigh, Sprite sprite)
    {
        this.value = value;
		this.isHigh = isHigh; 
        this.sprite = sprite;
    }
}

