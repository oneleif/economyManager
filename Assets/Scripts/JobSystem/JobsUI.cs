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

    private Sprite containerBackgroundSprite;
    private Sprite scheduleSlotSprite; 
    private Sprite jobSprite;

    private int scheduleSlots = JobConstants.scheduleSlots; 

    private void Start()
    {
        Debug.Log("JobsContainer length: " + jobsContainer.jobsContainer.Length);

        // Load sprites 
        containerBackgroundSprite = Resources.Load<Sprite>(JobConstants.uiBackgroundSpritePath);
        scheduleSlotSprite = Resources.Load<Sprite>(JobConstants.scheduleSlotSpritePath);
        jobSprite = Resources.Load<Sprite>(JobConstants.jobSpritePath);

        // Create UI elements and populate with jobs 
        availableJobsContainer = InitialiseAvailableJobsContainer();
        scheduleContainer = InitialiseScheduleContainer();
        AddAvailableJobs();
    }

    private GameObject InitialiseAvailableJobsContainer()
    {
        GameObject containerObject = new GameObject(JobConstants.availableJobsContainerName);
        containerObject.transform.parent = gameObject.transform;

        Image containerImage = containerObject.AddComponent<Image>();
        containerImage.sprite = containerBackgroundSprite;

        RectTransform rectTransform = containerObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = JobConstants.availableJobsContainerAnchorMin;
        rectTransform.anchorMax = JobConstants.availableJobsContainerAnchorMax;
        rectTransform.localPosition = Vector2.left; 
        rectTransform.localScale = new Vector2(0.75f, 0.75f);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        JobsUtils.SetupGridLayoutGroup(containerObject); 

        return containerObject; 
    }

    private GameObject InitialiseScheduleContainer()
    {
        GameObject scheduleObject = new GameObject(JobConstants.scheduleContainerName);
        scheduleObject.transform.parent = gameObject.transform;

        Image scheduleImage = scheduleObject.AddComponent<Image>();
        scheduleImage.sprite = containerBackgroundSprite;

        scheduleObject.AddComponent<CanvasGroup>();
        scheduleObject.AddComponent<Schedule>(); 

        RectTransform rectTransform = scheduleObject.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(0.5f, 0f);
        rectTransform.anchorMin = JobConstants.scheduleContainerAnchorMin;
        rectTransform.anchorMax = JobConstants.scheduleContainerAnchorMax;
        rectTransform.localScale = JobConstants.scheduleContainerLocalScale;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        JobsUtils.SetupGridLayoutGroup(scheduleObject); 

        return scheduleObject;
    }

    private void AddAvailableJobs()
    {
        foreach (Job job in jobsContainer.jobsContainer)
        {
            GameObject newJob = new GameObject(job.title); 
            newJob.transform.parent = availableJobsContainer.transform;

            // Store location data for repositioning on failed drop.  
            // Assign the individual floats to prevent copying the reference 
            job.startingPosition = new Vector2(newJob.transform.position.x, newJob.transform.position.y);

            Image jobImage = newJob.AddComponent<Image>();
            jobImage.sprite = jobSprite; 

            DragNDrop dragNDrop = newJob.AddComponent<DragNDrop>();
            dragNDrop.availableJobsContainer = availableJobsContainer;
            dragNDrop.scheduleContainer = scheduleContainer;
            dragNDrop.job = job;

            // Needs to be false otherwise Schedule can't fire events 
            newJob.AddComponent<CanvasGroup>().interactable = false; 
            
        }
    }

    private void InitialiseScheduleSlots()
    {
        for (int i = 0; i < scheduleSlots; i++)
        {
            GameObject scheduleSlot = new GameObject(JobConstants.scheduleSlotName);
            scheduleSlot.transform.parent = scheduleContainer.transform;
            Image slotImage = scheduleSlot.AddComponent<Image>();
            slotImage.sprite = scheduleSlotSprite;
            slotImage.color = Color.blue;
        }
    }
}
