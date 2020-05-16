using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Message", order = 1)]
public class Message : ScriptableObject
{
    public string sender;
    public string subject;
    public string body;
    public int condition;
    public bool unlocked;
}
