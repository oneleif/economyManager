using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour
{
    public GameObject buttonObject;
    public Button button;
    public Slider slider; 

    public GameObject pilotTextObject;
    public Text pilotText; 

    public GameObject countdownTextObject;
    public Text countdownText;

    public Mission mission;
    public bool performingMission;  

    public void Setup(Mission mission)
    {
        performingMission = false;
        pilotText.text = mission.pilotsName;
        this.mission = mission;

        ResetMissionTime();
    }

    public void SetMissionTime(int timeLeftInSeconds)
    {
        TimeSpan missionDuration = new TimeSpan(0, 0, timeLeftInSeconds);
        countdownText.text = missionDuration.ToString("c");
    }

    public void ResetMissionTime()
    {
        TimeSpan missionDuration = new TimeSpan(0, 0, mission.missionDurationInSeconds);
        countdownText.text = missionDuration.ToString("c");
    }
}
