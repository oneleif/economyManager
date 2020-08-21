using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobConstants : MonoBehaviour
{
    // UI element dimensions  
    public static Vector2 availableContainerAnchorMin = new Vector2(1f, 1f);
    public static Vector2 availableContainerAnchorMax = new Vector2(1f, 1f);
    public static Vector2 scheduleContainerAnchorMin = new Vector2(1f, 1f);
    public static Vector2 scheduleContainerAnchorMax = new Vector2(1f, 1f);

    // Game object names 
    public static string availableContainerName = "AvailableJobsContainer";
    public static string scheduleContainerName = "ScheduleContainer";

    // Paths to png files 
    public static string scheduleBackgroundSpritePath = "Assets/Resources/Sprites/ScheduleBackground"; 
    public static string scheduleSlotSpritePath = "Assets/Resources/Sprites/ScheduleSlot";
    public static string jobSpritePath = "Assets/Resources/Sprites/Briefcase"; 

    // Drag and drop parameters 
    public static float dragAlpha = 0.5f;
    public static float dropAlpha = 1f; 


}
