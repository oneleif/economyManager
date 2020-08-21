using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class JobsUI : MonoBehaviour
{
    public JobsContainer jobsContainer;
    public GameObject jobPrefab; 
    private GameObject availableJobsContainer;
    private GameObject scheduleContainer; 


    private void Start()
    {
        availableJobsContainer = InitialiseJobsContainer(JobConstants.availableContainerName, Vector2.one, Vector2.one);
        AddAvailableJobs();
        
    }

    private void Update()
    {
        
    }

    private GameObject InitialiseJobsContainer(string name, Vector2 anchorMin, Vector2 anchorMax)
    {
        GameObject containerObject = new GameObject(name);
        containerObject.transform.parent = gameObject.transform;

        containerObject.AddComponent<Image>().sprite = Resources.Load<Sprite>(JobConstants.scheduleBackgroundSpritePath); 

        RectTransform rectTransform = containerObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

        GridLayoutGroup gridLayoutGroup = containerObject.AddComponent<GridLayoutGroup>();


        return containerObject; 
    }

    private GameObject InitialiseScheduleContainer(string name, Vector2 anchorMin, Vector2 anchorMax)
    {
        GameObject scheduleObject = new GameObject(name);
        scheduleObject.transform.parent = gameObject.transform;

        scheduleObject.AddComponent<Image>().sprite = Resources.Load<Sprite>(JobConstants.scheduleBackgroundSpritePath); 
        scheduleObject.AddComponent<CanvasGroup>();
        scheduleObject.AddComponent<Schedule>(); 

        RectTransform rectTransform = scheduleObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

        GridLayoutGroup gridLayoutGroup = scheduleObject.AddComponent<GridLayoutGroup>();

        // loop through weeks, add HLG for each? (calendar rows) 

        return scheduleObject;
    }


    private void AddAvailableJobs()
    {
        foreach (Job job in jobsContainer.jobsContainer)
        {
            GameObject newJob = new GameObject(); 
            newJob.transform.parent = availableJobsContainer.transform;

            // Store location data for repositioning on drop.  
            // Assign the individual floats to prevent copying the reference? 
            job.startingPosition = new Vector2(newJob.transform.position.x, newJob.transform.position.y); 

            newJob.AddComponent<DragNDrop>();

            // Needs to be false otherwise Schedule can't fire events 
            newJob.AddComponent<CanvasGroup>().interactable = false; 
            
        }
    }
}
