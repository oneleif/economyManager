using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas canvas; 

    public void ActivateCanvas()
    {
        canvas.gameObject.SetActive(true); 
    }

    public void DeactivateCanvas()
    {
        canvas.gameObject.SetActive(false); 
    }

    public void ToggleCanvas()
    {
        canvas.gameObject.SetActive(!gameObject.activeSelf);
    }
}
