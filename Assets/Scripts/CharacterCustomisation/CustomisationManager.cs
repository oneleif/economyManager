using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationManager : MonoBehaviour
{
    public CustomisationTypeContainer customisationTypeContainer; 
    public GameObject leftArrowPrefab;
    public GameObject rightArrowPrefab; 
    private GameObject customisationContainer;

    private Color[] colors = 
    {
        Color.red, Color.green, Color.blue, Color.yellow, Color.white, Color.black, Color.cyan, Color.grey
    };

    public enum Direction { Left = 0, Right = 1 }; 

    void Start()
    {
        GenerateCustomisationOptions();
        LogCustomisationState(); 
    }

    private void GenerateCustomisationOptions()
    {
        // This is the overarching parent object 
        customisationContainer = new GameObject(CustomisationConstants.customisationContainerName);
        customisationContainer.transform.SetParent(gameObject.transform);
        customisationContainer.transform.localPosition = Vector3.zero;

        RectTransform rectTransform = customisationContainer.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0f, 0f);
        rectTransform.anchorMax = new Vector2(1f, 1f);
        rectTransform.localScale = Vector3.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        VerticalLayoutGroup verticalLayoutGroup = customisationContainer.AddComponent<VerticalLayoutGroup>();
        verticalLayoutGroup.spacing = CustomisationConstants.customisationContainerSpacing; 

        foreach (CustomisationType customisationType in customisationTypeContainer.customisationTypes)
        {
            // This is the parent object for each individual row of the customisation container  
            GameObject customisationOption = new GameObject(CustomisationConstants.customisationOptionName);
            customisationOption.transform.SetParent(customisationContainer.transform);
            RectTransform customisationOptionRT = customisationOption.AddComponent<RectTransform>();
            customisationOption.AddComponent<HorizontalLayoutGroup>();

            GameObject leftArrow = Instantiate(leftArrowPrefab);
            leftArrow.transform.SetParent(customisationOption.transform);
            leftArrow.transform.localScale = new Vector2(0.25f, 0.25f); 
            Button leftArrowBtn = leftArrow.GetComponent<Button>();
            leftArrowBtn.onClick.RemoveAllListeners(); 

            GameObject imageObject = new GameObject(CustomisationConstants.customisationImageName);
            imageObject.transform.SetParent(customisationOption.transform);
            imageObject.transform.localScale = Vector2.one;
            customisationType.image = imageObject.AddComponent<Image>();
            customisationType.image.sprite = customisationType.sprite; 
            customisationType.image.color = customisationType.color;

            GameObject rightArrow = Instantiate(rightArrowPrefab);
            rightArrow.transform.SetParent(customisationOption.transform);
            rightArrow.transform.localScale = new Vector2(0.25f, 0.25f); 
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
            // This wraps around if the user reaches the end of the customisation options for that group 
            if (customisationType.index > 0)
            {
                customisationType.index--; 
            }
            else
            {
                customisationType.index = colors.Length - 1;  
            }
        }
        else if (direction == Direction.Right)
        {
            if (customisationType.index < colors.Length - 1)
            {
                customisationType.index++;
            }
            else
            {
                customisationType.index = 0; 
            }
        }
        customisationType.color = colors[customisationType.index];
        customisationType.image.color = customisationType.color;

        LogCustomisationState(); 
    }

    void LogCustomisationState()
    {
        for (int i = 0; i < customisationTypeContainer.customisationTypes.Length; i++)
        {
            Debug.Log($"Customisation Type {i} value: {customisationTypeContainer.customisationTypes[i].index}"); 
        }
    }
}
