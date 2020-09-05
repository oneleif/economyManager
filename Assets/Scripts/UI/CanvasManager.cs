using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas canvas; 

    private void Start()
    {
            
    }

    public void ActivateCanvas()
    {
        Debug.Log("ActivateCanvas"); 
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
