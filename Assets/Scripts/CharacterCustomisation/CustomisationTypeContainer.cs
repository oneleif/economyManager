using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomisationTypeContainer", menuName = "ScriptableObjects/CustomisationOptionContainer", order = 1)]
public class CustomisationTypeContainer : ScriptableObject
{
    public CustomisationType[] customisationTypes;
}
