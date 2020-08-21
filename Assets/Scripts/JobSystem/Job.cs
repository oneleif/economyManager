using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "JobData", menuName = "ScriptableObjects/Job", order = 1)]
public class Job : ScriptableObject
{
    public string title;
    public Sprite sprite = Resources.Load<Sprite>(JobConstants.jobSpritePath); 
    public Vector2 startingPosition; 
}
