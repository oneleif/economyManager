using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageButtonHandler : MonoBehaviour
{
    public Button button;
    public Text text;

    public void SetMessage(Message message)
    {
        text.text = $"{message.sender} - {message.subject} : {message.body}";
    }
}
