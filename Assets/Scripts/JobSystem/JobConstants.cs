using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobConstants : MonoBehaviour
{
    // AvailableJobsContainer dimensions  
    public static Vector2 availableJobsContainerAnchorMin = Vector2.zero;
    public static Vector2 availableJobsContainerAnchorMax = new Vector2(0.5f, 1f);
    public static Vector2 availableJobsContainerLocalPosition = new Vector2(-0.5f, -0.5f); 
    public static Vector2 availableJobsContainerLocalScale = new Vector2(0.75f, 0.75f);
    public static Vector2 availableJobsGridCellSize = new Vector2(128f, 128f);
    public static Vector2 availableJobsGridSpacing = new Vector2(64f, 64f);
    public static Vector2 availableJobsContainerLeftPadding = new Vector2(64f, 64f);

    // ScheduleContainer dimensions 
    public static int scheduleSlots = 4; 
    public static Vector2 scheduleContainerAnchorMin = new Vector2(0.5f, 0f);
    public static Vector2 scheduleContainerAnchorMax = Vector2.one;
    public static Vector2 scheduleContainerLocalScale = new Vector2(0.75f, 0.75f);
    public static int scheduleContainerSpacing = 32;
    public static int scheduleContainerLeftPadding = 32;

    // Common dimensions 
    public static int gridTopPadding = 16; 
    public static int gridLeftPadding = 16;
    public static Vector2 gridCellSize = new Vector2(80f, 80f);  
    public static Vector2 gridSpacing = new Vector2(32f, 32f); 

    // Game object names 
    public static string availableJobsContainerName = "AvailableJobsContainer";
    public static string scheduleContainerName = "ScheduleContainer";
    public static string scheduleSlotName = "ScheduleSlot"; 

    // Paths to png files 
    public static string uiBackgroundSpritePath = "Sprites/ScheduleBackground"; 
    public static string scheduleSlotSpritePath = "Sprites/ScheduleSlot";
    public static string jobSpritePath = "Sprites/Briefcase"; 

    // Drag and drop parameters 
    public static float dragAlpha = 0.5f;
    public static float dropAlpha = 1f; 


}
