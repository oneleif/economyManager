using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This SO allows for multiple players if a container object is added 

[CreateAssetMenu(fileName = "BlackjackPlayer", menuName = "ScriptableObjects/BlackjackPlayer", order = 1)]
public class BlackjackPlayer : ScriptableObject
{
    public List<Card> hand = new List<Card>(); 
    public int handTotal;
    public int chips; 
    public int wager; 
	public bool hasWagered; 
    public bool isStanding;
    public bool isBust;
    public bool isDealer;
}
