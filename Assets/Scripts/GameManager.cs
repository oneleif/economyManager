using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UnityEvent<long> updateMoneyEvent;

    public MessagesManager messagesManager;
    public MoneyManager moneyManager;
    public MissionsManager missionsManager;

    void Start()
    {
        GetRequiredComponents();

        updateMoneyEvent = new UpdateMoneyEvent();
        updateMoneyEvent.RemoveAllListeners();
        updateMoneyEvent.AddListener(moneyManager.UpdatePlayerMoney);
        updateMoneyEvent.AddListener(messagesManager.CheckMessagesToAdd);
        updateMoneyEvent.Invoke(0);
    }

    private void GetRequiredComponents()
    {
        messagesManager = GetComponent<MessagesManager>();
        if (messagesManager == null)
        {
            Debug.LogError("no messagesManager found, check the object hierarchy");
        }

        moneyManager = GetComponent<MoneyManager>();
        if (moneyManager == null)
        {
            Debug.LogError("no moneyManager found, check the object hierarchy");
        }

        missionsManager = GetComponent<MissionsManager>();
        if (missionsManager == null)
        {
            Debug.LogError("no missionsManager found, check the object hierarchy");
        }
    }

    void Update()
    {
        
    }

    [System.Serializable]
    public class UpdateMoneyEvent: UnityEvent<long>
    {

    }
}
