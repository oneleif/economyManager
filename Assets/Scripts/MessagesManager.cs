using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MessagesManager : MonoBehaviour
{
    public GameObject messageParent;
    public GameObject messageItemPrefab;
    public MessageContainer messageContainer;
    public PlayerData playerData;

    public void CheckMessagesToAdd(long moneyMade)
    {
        UnlockMessages();
        CleanScrollView();
        AddMessages();
    }

    private void UnlockMessages()
    {
        foreach (Message message in messageContainer.messages)
        {
            if (!message.unlocked)
            {
                if (playerData.playerMoney > message.condition)
                {
                    message.unlocked = true;
                }
            }
        }
    }

    private void CleanScrollView()
    {
        foreach(Transform child in messageParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddMessages()
    {
        foreach (Message message in messageContainer.messages)
        {
            if (message.unlocked)
            {
                GameObject newMessage = Instantiate(messageItemPrefab, messageParent.transform);
                MessageButtonHandler buttonHandler = newMessage.GetComponent<MessageButtonHandler>();
                buttonHandler.SetMessage(message);
            }
        }
    }
}
