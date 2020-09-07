using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTabbedUIManager : MonoBehaviour
{
    public Button missionsButton;
    public Button messagesButton;
    public Button analyticsButton;
    public Button crewButton;
    public Button upgradesButton;

    public GameObject missionsPanel;
    public GameObject messagesPanel;
    public GameObject analyticsPanel;
    public GameObject crewPanel;
    public GameObject upgradesPanel;

    public enum Tab
    {
        missions, messages, analytics, crew, upgrades
    }

    void Start()
    {
        SetupButtonListeners();
        TabButtonClicked(Tab.missions);
    }

    void Update()
    {

    }

    private void SetupButtonListeners()
    {
        missionsButton.onClick.RemoveAllListeners();
        missionsButton.onClick.AddListener(delegate { TabButtonClicked(Tab.missions); });

        messagesButton.onClick.RemoveAllListeners();
        messagesButton.onClick.AddListener(delegate { TabButtonClicked(Tab.messages); });

        analyticsButton.onClick.RemoveAllListeners();
        analyticsButton.onClick.AddListener(delegate { TabButtonClicked(Tab.analytics); });

        crewButton.onClick.RemoveAllListeners();
        crewButton.onClick.AddListener(delegate { TabButtonClicked(Tab.crew); });

        upgradesButton.onClick.RemoveAllListeners();
        upgradesButton.onClick.AddListener(delegate { TabButtonClicked(Tab.upgrades); });
    }

    private void TabButtonClicked(Tab tabClicked)
    {
        ClearPanels();
        switch (tabClicked)
        {
            case Tab.missions:
                missionsPanel.SetActive(true);
                break;
            case Tab.messages:
                messagesPanel.SetActive(true);
                break;
            case Tab.analytics:
                analyticsPanel.SetActive(true);
                break;
            case Tab.crew:
                crewPanel.SetActive(true);
                break;
            case Tab.upgrades:
                upgradesPanel.SetActive(true);
                break;
        }
    }

    private void ClearPanels()
    {
        missionsPanel.SetActive(false);
        messagesPanel.SetActive(false);
        analyticsPanel.SetActive(false);
        crewPanel.SetActive(false);
        upgradesPanel.SetActive(false);
    }
}
