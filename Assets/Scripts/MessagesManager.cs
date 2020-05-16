using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MessagesManager : MonoBehaviour
{
    public GameObject messageParent;
    public GameObject messageItemPrefab;
    public MessageContainer messageContainer;
    public PlayerData playerData;

    public GameObject messagesListView;
    public GameObject messagesDetailView;
    public MessageDetailView messageDetailViewHandler;
    public Button backButton;

    private void Start()
    {
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(GoToListView);
        GoToListView();
    }

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
                buttonHandler.button.onClick.AddListener( delegate { GoToDetailView(message); });
            }
        }
    }

    private void GoToDetailView(Message message)
    {
        messagesListView.SetActive(false);
        messagesDetailView.SetActive(true);
        messageDetailViewHandler.SetMessage(message);
    }

    private void GoToListView()
    {
        messagesListView.SetActive(true);
        messagesDetailView.SetActive(false);
    }
}
