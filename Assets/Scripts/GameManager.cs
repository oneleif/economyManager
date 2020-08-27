using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public MissionContainer missionContainer;
    public PlayerData playerData;

    public GameObject missionButtonPrefab;

    public GameObject missionsPanel;

    public Text playerMoneyText;

    void Start()
    {
        playerMoneyText.text = "$" + playerData.playerMoney;
        GenerateMissionButtons();
    }

    void GenerateMissionButtons()
    {
        missionsPanel.GetComponentInChildren<GridLayoutGroup>();    

        foreach(Mission mission in missionContainer.missions)
        {
            GameObject newButton = Instantiate(missionButtonPrefab);
            MissionButton missionButton = newButton.GetComponent<MissionButton>();

            missionButton.Setup(mission);

            newButton.transform.parent = missionsPanel.transform;

            missionButton.button.onClick.AddListener( delegate { PerformMission(missionButton); } );
        }
    }

    void PerformMission(MissionButton missionButton)
    {
        if (missionButton.mission.inProgress)
        {
            return;
        }
        missionButton.slider.value = 0f; 
        StartCoroutine(WaitForMission(missionButton));
    }

    IEnumerator WaitForMission(MissionButton missionButton)
    {
        Mission mission = missionButton.mission;
        mission.inProgress = true;

        // Increase timer by a factor of n 
        // to match the progress increments
        int currentTimer = mission.missionDurationInSeconds * MissionConstants.progressScaleFactor;
        while(currentTimer > 0)
        {
            // Wait for a fraction of a second 
            // in order to accomodate missions 
            // under a second in length 
            yield return new WaitForSeconds(1 / MissionConstants.progressScaleFactor);
            currentTimer--;
            missionButton.SetMissionTime(currentTimer / MissionConstants.progressScaleFactor);
        }
        
        playerData.playerMoney += mission.missionValue;
        missionButton.ResetMissionTime();
        playerMoneyText.text = "$" + playerData.playerMoney;
        mission.inProgress = false; 
    }

    void Update()
    {
        
    }
}
