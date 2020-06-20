using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationManager : MonoBehaviour
{
    public CustomisationTypeContainer customisationTypeContainer; 
    public GameObject arrowButtonPrefab;
    private GameObject customisationContainer;
    private Color[] customisationColors = 
    { 
        Color.red, Color.green, Color.blue, Color.yellow, Color.white, Color.black, Color.cyan, Color.grey, Color.magenta
    };

    public enum Direction { Left = 0, Right = 1 }; 

    void Start()
    {
        GenerateCustomisationOptions();
    }

    private void GenerateCustomisationOptions()
    {
        // this is the overarching parent object 
        customisationContainer = new GameObject("CustomisationContainer");
        customisationContainer.transform.SetParent(gameObject.transform);
        customisationContainer.transform.localPosition = Vector3.zero;
        VerticalLayoutGroup verticalLayoutGroup = customisationContainer.AddComponent<VerticalLayoutGroup>();
        verticalLayoutGroup.spacing = 16;

        foreach (CustomisationType customisationType in customisationTypeContainer.customisationTypes)
        {
            // this is the parent object for each individual row of the customisation container  
            GameObject customisationOption = new GameObject("CustomisationOption");
            customisationOption.transform.SetParent(customisationContainer.transform);
            RectTransform customisationOptionRT = customisationOption.AddComponent<RectTransform>();
            customisationOption.AddComponent<HorizontalLayoutGroup>();

            GameObject leftArrow = Instantiate(arrowButtonPrefab);
            leftArrow.transform.SetParent(customisationOption.transform);
            Button leftArrowBtn = leftArrow.GetComponent<Button>();
            leftArrowBtn.onClick.RemoveAllListeners();

            GameObject imageObject = new GameObject("CustomisationImage");
            imageObject.transform.SetParent(customisationOption.transform);
            RectTransform imageObjectRT = imageObject.GetComponent<RectTransform>();
            customisationType.customisationImage = imageObject.AddComponent<Image>();
            customisationType.customisationImage.color = customisationType.customisationColor;

            GameObject rightArrow = Instantiate(arrowButtonPrefab);
            rightArrow.transform.SetParent(customisationOption.transform);
            Button rightArrowBtn = rightArrow.GetComponent<Button>();
            rightArrowBtn.onClick.RemoveAllListeners();

            leftArrowBtn.onClick.AddListener(delegate { CycleThroughOptions(customisationType, Direction.Left); });
            rightArrowBtn.onClick.AddListener(delegate { CycleThroughOptions(customisationType, Direction.Right); });
        }
    }

    private void CycleThroughOptions(CustomisationType customisationType, Direction direction)
    {
        if (direction == Direction.Left)
        {
            // this wraps around if the user reaches the end of the customisation options for that group 
            if (customisationType.customisationOptionIndex > 0)
            {
                customisationType.customisationOptionIndex--; 
            }
            else
            {
                customisationType.customisationOptionIndex = customisationColors.Length - 1;  
            }
        }
        else if (direction == Direction.Right)
        {
            if (customisationType.customisationOptionIndex < customisationColors.Length - 1)
            {
                customisationType.customisationOptionIndex++;
            }
            else
            {
                customisationType.customisationOptionIndex = 0; 
            }
        }
        customisationType.customisationColor = customisationColors[customisationType.customisationOptionIndex];
        customisationType.customisationImage.color = customisationType.customisationColor;
    }
}
