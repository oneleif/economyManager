using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CustomisationType", menuName = "ScriptableObjects/CustomisationType", order = 1)]
public class CustomisationType : ScriptableObject
{
    public Image image;
    public Sprite sprite; 
    public int index;
    public Color color; 
}
