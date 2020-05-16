using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDetailView : MonoBehaviour
{
    public Text messageDetail;

    public void SetMessage(Message message)
    {
        messageDetail.text = message.sender + "\n";
        messageDetail.text += message.subject + "\n";
        messageDetail.text += message.body;
    }
}
