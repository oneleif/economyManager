using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MessageContainer", menuName = "ScriptableObjects/MessageContainer", order = 1)]
public class MessageContainer : ScriptableObject
{
    public Message[] messages;
}

