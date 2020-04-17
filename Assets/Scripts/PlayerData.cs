using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public double playerMoney;
    public int playerMissionStreak;
    public float playerRewardMultiplier;
    public int numberOfPilots; 
}