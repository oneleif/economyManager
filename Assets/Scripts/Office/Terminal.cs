﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public GameObject player;
    public GameObject canvasManagerObject;

    private Rigidbody rb; 
    private CanvasManager canvasManager;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        rb.WakeUp(); 
        canvasManager = canvasManagerObject.GetComponent<CanvasManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (Input.GetKeyUp(PlayerConstants.action))
            {
                canvasManager.ActivateCanvas();
            }
            else if (Input.GetKeyUp(PlayerConstants.exit))
            {
                canvasManager.DeactivateCanvas();
            }
        }
    }
}
