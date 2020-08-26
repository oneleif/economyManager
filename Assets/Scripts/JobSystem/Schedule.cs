using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Schedule : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // Parent job to the schedule if dropped inside 
            eventData.pointerDrag.transform.parent = gameObject.transform;
        }
    }
}
