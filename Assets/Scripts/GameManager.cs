using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public MissionContainer missionContainer;
    public PlayerData playerData;

    public GameObject missionButtonPrefab;

    public GameObject gamePanel;
        
    public Text playerMoneyText;
    public Text currentStreakText; 
    public Text missionOutcomeText;
    public Text pilotText; 
    public Text hirePilotText; 

    public int availablePilots;
    public int costToHirePilot;  

    void Start()
    {
        playerMoneyText.text = "$" + playerData.playerMoney;
        availablePilots = playerData.numberOfPilots;
        pilotText.text = $"You have {playerData.numberOfPilots} pilots. {availablePilots} available."; 
        costToHirePilot = 50000; 
        GameObject.Find("HirePilotButton").GetComponent<Button>().onClick.AddListener(delegate { HirePilot(); });

        //Button restartButton = GameObject.Find("RestartButton").GetComponent<Button>().onClick.AddListener(delegate { RestartGame()});

        GenerateMissionButtons();
    }

    void GenerateMissionButtons()
    {
        foreach(Mission mission in missionContainer.missions)
        {
            GameObject newButton = Instantiate(missionButtonPrefab);
            MissionButton missionButton = newButton.GetComponent<MissionButton>();

            missionButton.Setup(mission);

            newButton.transform.parent = gamePanel.transform;

            missionButton.button.onClick.AddListener(delegate { PerformMission(missionButton); });
        }
    }

    void PerformMission(MissionButton missionButton)
    {
        if (missionButton.mission.inProgress || availablePilots == 0)
        {
            return;
        }

        availablePilots--;
        // change this to update text reactively  
        pilotText.text = $"You have {playerData.numberOfPilots} pilots. {availablePilots} available.";

        StartCoroutine(WaitForMission(missionButton));
    }

    IEnumerator WaitForMission(MissionButton missionButton)
    {
        Mission mission = missionButton.mission;
        mission.inProgress = true;
        Debug.Log("here");

        int currentTimer = mission.missionDurationInSeconds;
        while(currentTimer > 0)
        {
            yield return new WaitForSeconds(1);
            currentTimer--;
            missionButton.SetMissionTime(currentTimer);

        }

        if (Random.Range(0, 100) > mission.chanceOfSuccess)
        {
            missionButton.ResetMissionTime();
            missionOutcomeText.text = "Mission Failed! You earned nothing...";

            // end streak and reset multiplier 
            playerData.playerMissionStreak = 0;
            playerData.playerRewardMultiplier = 1;

            currentStreakText.text = "Streak: 0 (1x multiplier)";  
        }
        else
        {
            //if(mission.chanceOfSuccess != 1)
            //{
                playerData.playerMissionStreak++;
            //}

            // increase multiplier every 10 successful missions 
            if (playerData.playerMissionStreak % 10 == 0)
            {
                playerData.playerRewardMultiplier += 0.5f; 
            }

            playerData.playerMoney += mission.missionValue * playerData.playerRewardMultiplier; 
            missionButton.ResetMissionTime();
            playerMoneyText.text = "$" + playerData.playerMoney;
            currentStreakText.text = $"Streak: {playerData.playerMissionStreak} ({playerData.playerRewardMultiplier}x multiplier)";
            missionOutcomeText.text = $"Mission Accomplished! You earned: ${mission.missionValue * playerData.playerRewardMultiplier}!";

            Debug.Log($"Multiplier: {playerData.playerRewardMultiplier}x");
        }

        mission.inProgress = false;
        availablePilots++;
        pilotText.text = $"You have {playerData.numberOfPilots} pilots. {availablePilots} available.";

    }

    void HirePilot()
    {
        if (playerData.playerMoney >= costToHirePilot)
        {
            playerData.numberOfPilots++;
            availablePilots++; 
            playerData.playerMoney -= costToHirePilot;
            pilotText.text = $"You have {playerData.numberOfPilots} pilots. {availablePilots} available.";
            playerMoneyText.text = "$" + playerData.playerMoney;

        }
        else
        {
            hirePilotText.text = "Insufficient funds."; 
        }
    }

    void Update()
    {
        
    }
}
