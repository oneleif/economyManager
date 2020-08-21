using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject mainCanvas;
    public GameObject schedule;
    public GameObject testSchedule; 
    public Job job;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Start()
    {
        mainCanvas = GameObject.Find("Canvas");
        //schedule = GameObject.Find("ScheduleContainer"); 
        //testSchedule = GameObject.Find("TestSchedule");
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        Debug.Log("Test Schedule position: " + testSchedule.transform.position); 
    }

    private void Update()
    {
        //Debug.Log("Job Rect: " + rectTransform.rect.position);
        //Debug.Log("Sched Rect: " + schedule.GetComponent<RectTransform>().rect); 

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag fired.");
        canvasGroup.alpha = JobConstants.dragAlpha; 
        canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Delta is distance that the mouse moved since previous frame
        // Divide by canvas scale factor to prevent object from overshooting 
        rectTransform.anchoredPosition += eventData.delta / mainCanvas.GetComponent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag event data position: " + eventData.position);

        // Return to starting position if not scheduled 
        if (!IsInsideSchedule())
        {
            gameObject.transform.position = job.startingPosition;
        }

        // Raycast will pass through and hit the schedule 
        canvasGroup.alpha = JobConstants.dropAlpha; 
        canvasGroup.blocksRaycasts = true;


    }

    // Can we get rid of this and check eventData.anchoredPosition 
    // in Schedule's OnDrop method instead?
    private bool IsInsideSchedule()
    {
        RectTransform scheduleRectTransform = schedule.GetComponent<RectTransform>(); 

        if (rectTransform.position.x <= scheduleRectTransform.position.x &&
            rectTransform.position.x + rectTransform.rect.size.x >= scheduleRectTransform.position.x + scheduleRectTransform.rect.size.x &&
            rectTransform.position.y <= scheduleRectTransform.position.y && 
            rectTransform.position.y + rectTransform.rect.size.x >= scheduleRectTransform.position.x + scheduleRectTransform.rect.size.x)
        {
            Debug.Log("IsInsideSchedule."); 
            return true; 
        }

        return false; 
    }
}
