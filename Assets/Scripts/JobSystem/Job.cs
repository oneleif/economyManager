using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "JobD", menuName = "ScriptableObjects/Job", order = 1)]
public class Job : ScriptableObject
{
    public string title; 
    public Vector2 startingPosition; 
}
