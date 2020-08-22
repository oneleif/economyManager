using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject mainCanvas;
    private GameObject messagesPanel;
    public GameObject availableJobsContainer; 
    public GameObject scheduleContainer;
    public GameObject testSchedule; 
    public Job job;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Start()
    {
        mainCanvas = GameObject.Find("Canvas");
        messagesPanel = GameObject.Find("MessagesPanel"); 

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        Debug.Log("Schedule pos: " + scheduleContainer.transform.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag fired.");
        gameObject.transform.parent = messagesPanel.transform;

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
        Debug.Log("Local pos: " + gameObject.transform.localPosition);

        // Return to starting position if not droopped in schedule 
        if (IsInsideSchedule())
        {
            gameObject.transform.parent = availableJobsContainer.transform.parent;
            //gameObject.transform.position = job.startingPosition;
            //gameObject.transform.localPosition = job.startingPosition; 
        }

        // Raycast will pass through and hit the schedule 
        canvasGroup.alpha = JobConstants.dropAlpha; 
        canvasGroup.blocksRaycasts = true;
    }

    // Can we get rid of this and check eventData.anchoredPosition 
    // in Schedule's OnDrop method instead?
    private bool IsInsideSchedule()
    {
        RectTransform scheduleRectTransform = scheduleContainer.GetComponent<RectTransform>();

        return rectTransform.rect.x < scheduleRectTransform.rect.x + scheduleRectTransform.rect.width &&
            scheduleRectTransform.rect.x < rectTransform.rect.x + rectTransform.rect.width &&
            rectTransform.rect.y < scheduleRectTransform.rect.y + scheduleRectTransform.rect.height &&
            scheduleRectTransform.rect.y < rectTransform.rect.y + rectTransform.rect.height; 
    }
}
