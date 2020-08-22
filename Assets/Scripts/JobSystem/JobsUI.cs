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

    private int scheduleSlots = 4; 

    private void Start()
    {
        Debug.Log("JobsContainer length: " + jobsContainer.jobsContainer.Length);

        containerBackgroundSprite = Resources.Load<Sprite>("Sprites/ScheduleBackground.png");
        scheduleSlotSprite = Resources.Load<Sprite>("Sprites/ScheduleSlot.png");
        jobSprite = Resources.Load<Sprite>("Sprites/Briefcase.png");

        availableJobsContainer = InitialiseAvailableJobsContainer();
        scheduleContainer = InitialiseScheduleContainer();
        AddAvailableJobs();

    }

    private void Update()
    {
        
    }

    private GameObject InitialiseAvailableJobsContainer()
    {
        GameObject containerObject = new GameObject(JobConstants.availableJobsContainerName);
        containerObject.transform.parent = gameObject.transform;

        //containerObject.AddComponent<Image>().sprite = Resources.Load<Sprite>(JobConstants.scheduleBackgroundSpritePath);
        Image containerImage = containerObject.AddComponent<Image>();
        containerImage.sprite = containerBackgroundSprite;

        RectTransform rectTransform = containerObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = JobConstants.availableJobsContainerAnchorMin;
        rectTransform.anchorMax = JobConstants.availableJobsContainerAnchorMax;
        rectTransform.localPosition = new Vector2(-1f, 0f);
        rectTransform.localScale = new Vector2(0.75f, 0.75f);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        GridLayoutGroup gridLayoutGroup = containerObject.AddComponent<GridLayoutGroup>();
        gridLayoutGroup.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayoutGroup.spacing = JobConstants.availableJobsGridSpacing;
        //gridLayoutGroup.cellSize = JobConstants.availableJobsGridCellSize; 

        return containerObject; 
    }

    private GameObject InitialiseScheduleContainer()
    {
        GameObject scheduleObject = new GameObject(JobConstants.scheduleContainerName);
        scheduleObject.transform.parent = gameObject.transform;

        //scheduleObject.AddComponent<Image>().sprite = Resources.Load<Sprite>(JobConstants.scheduleBackgroundSpritePath); 
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


        //GridLayoutGroup gridLayoutGroup = scheduleObject.AddComponent<GridLayoutGroup>();
        HorizontalLayoutGroup horizontalLayoutGroup = scheduleObject.AddComponent<HorizontalLayoutGroup>();
        horizontalLayoutGroup.childControlWidth = true;
        horizontalLayoutGroup.childControlHeight = true;
        horizontalLayoutGroup.childForceExpandWidth = true;
        horizontalLayoutGroup.childForceExpandHeight = true;
        horizontalLayoutGroup.spacing = JobConstants.scheduleContainerSpacing;
        horizontalLayoutGroup.padding.left = JobConstants.scheduleContainerLeftPadding;

        // loop through weeks, add HLG for each? (calendar rows) 

        return scheduleObject;
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


    private void AddAvailableJobs()
    {
        foreach (Job job in jobsContainer.jobsContainer)
        {
            GameObject newJob = new GameObject(); 
            newJob.transform.parent = availableJobsContainer.transform;

            Debug.Log("New Job position: "+ newJob.transform.position);
            Debug.Log("New Job local position: " + newJob.transform.localPosition);

            // Store location data for repositioning on failed drop.  
            // Assign the individual floats to prevent copying the reference? 
            job.startingPosition = new Vector2(newJob.transform.position.x, newJob.transform.position.y);

            //newJob.AddComponent<Image>().sprite = Resources.Load<Sprite>(JobConstants.jobSpritePath);
            Image jobImage = newJob.AddComponent<Image>();
            jobImage.color = Color.black;

            DragNDrop dragNDrop = newJob.AddComponent<DragNDrop>();
            dragNDrop.availableJobsContainer = availableJobsContainer;
            dragNDrop.scheduleContainer = scheduleContainer;
            dragNDrop.job = job;

            // Needs to be false otherwise Schedule can't fire events 
            newJob.AddComponent<CanvasGroup>().interactable = false; 
            
        }
    }
}
