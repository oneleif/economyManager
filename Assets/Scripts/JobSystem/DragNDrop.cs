using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject schedule;
    [SerializeField] private Job job;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private Vector2 startingPosition = new Vector2(0f, 0f);


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>(); 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = JobConstants.dragAlpha; 
        canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Delta is distance that the mouse moved since previous frame
        // Divide by canvas scale factor to prevent object from overshooting 
        rectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        // Return to starting position if not scheduled 
        if (!IsInsideSchedule())
        {
            gameObject.transform.position = startingPosition;
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
            return true; 
        }

        return false; 
    }
}
