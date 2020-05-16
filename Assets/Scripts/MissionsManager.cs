using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    public GameObject missionButtonPrefab;
    public GameObject missionsPanel;
    public MissionContainer missionContainer;
    public PlayerData playerData;

    public GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();

        if(gameManager == null)
        {
            Debug.LogError("no gamemanager found, check the object hierarchy");
        }

        GenerateMissionButtons();
    }

    void GenerateMissionButtons()
    {
        foreach (Mission mission in missionContainer.missions)
        {
            GameObject newButton = Instantiate(missionButtonPrefab);
            MissionButton missionButton = newButton.GetComponent<MissionButton>();

            missionButton.Setup(mission);

            newButton.transform.parent = missionsPanel.transform;

            missionButton.button.onClick.AddListener(delegate { PerformMission(missionButton); });
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
        while (currentTimer > 0)
        {
            yield return new WaitForSeconds(1);
            currentTimer--;
            missionButton.SetMissionTime(currentTimer);
        }

        gameManager.updateMoneyEvent.Invoke(mission.missionValue);
        missionButton.ResetMissionTime();
    }
}
