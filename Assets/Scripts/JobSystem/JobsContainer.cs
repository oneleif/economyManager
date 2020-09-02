using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JobsContainer", menuName = "ScriptableObjects/JobsContainer", order = 1)]
public class JobsContainer : ScriptableObject
{
    public Job[] jobsContainer; 
}
