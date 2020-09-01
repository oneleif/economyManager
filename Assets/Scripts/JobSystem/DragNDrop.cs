using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
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
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Unparent the job object from the available jobs UI 
        gameObject.transform.parent = messagesPanel.transform;

        canvasGroup.alpha = JobConstants.dragAlpha; 
        canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Delta is the distance that the mouse moved since previous frame
        // Divide by canvas scale factor to prevent object from overshooting 
        rectTransform.anchoredPosition += eventData.delta / mainCanvas.GetComponent<Canvas>().scaleFactor;
    }

    // This seems to be called before the Scehdule's OnDrop method
    public void OnEndDrag(PointerEventData eventData)
    {
        // Raycast will pass through and hit the schedule 
        canvasGroup.blocksRaycasts = true;

        canvasGroup.alpha = JobConstants.dropAlpha; 

        // Return to starting position if not dropped in schedule 
        if (gameObject.transform.parent != scheduleContainer.transform)
        {
            gameObject.transform.parent = availableJobsContainer.transform;
        }        
    }

    private bool IsInsideSchedule()
    {
        RectTransform scheduleRectTransform = scheduleContainer.GetComponent<RectTransform>();

        return rectTransform.rect.x < scheduleRectTransform.rect.x + scheduleRectTransform.rect.width &&
            scheduleRectTransform.rect.x < rectTransform.rect.x + rectTransform.rect.width &&
            rectTransform.rect.y < scheduleRectTransform.rect.y + scheduleRectTransform.rect.height &&
            scheduleRectTransform.rect.y < rectTransform.rect.y + rectTransform.rect.height; 
    }
}
