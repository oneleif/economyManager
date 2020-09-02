using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotsConstants : MonoBehaviour
{
    // GameObject names 
    public static string profilePanelName = "PilotProfilePanel";
    public static string buttonGroupName = "PilotButtonGroup";
    public static string nameTextName = "PilotName";
    public static string descriptionObjectName = "PilotDescription";
    public static string avatarObjectName = "PilotAvatar";

    public static string backButtonName = "BackButton";
    public static string backButtonText = "Back"; 

    // UI dimensions 
    public static Vector2 buttonGroupAnchorMin = new Vector2(0.25f, 0.5f); 
    public static Vector2 buttonGroupAnchorMax = new Vector2(0.5f, 0.5f);

    public static Vector2 nameTextAnchorMin = new Vector2(0f, 0.75f);
    public static Vector2 nameTextAnchorMax = new Vector2(0.5f, 1f);

    public static Vector2 descriptionAnchorMin = new Vector2(0f, 0.5f);
    public static Vector2 descriptionAnchorMax = new Vector2(0.5f, 0.75f);

    public static Vector2 avatarAnchorMin = Vector2.one / 2f; 
    public static Vector2 avatarAnchorMax = Vector2.one; 

    public static Vector2 backButtonAnchorMin = new Vector2(0.25f, 0f);
    public static Vector2 backButtonAnchorMax = new Vector2(0.75f, 0.25f);
}
