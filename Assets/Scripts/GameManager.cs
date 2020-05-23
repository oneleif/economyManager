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

        StartCoroutine(WaitForMission(missionButton));
    }

    IEnumerator WaitForMission(MissionButton missionButton)
    {
        Mission mission = missionButton.mission;
        mission.inProgress = true;

        int currentTimer = mission.missionDurationInSeconds;
        while(currentTimer > 0)
        {
            yield return new WaitForSeconds(1);
            currentTimer--;
            missionButton.SetMissionTime(currentTimer);
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
