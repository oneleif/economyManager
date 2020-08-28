using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public enum Channel { Red = 0, Green = 1, Blue = 2 }; 

public class ColourSliders : MonoBehaviour
{
    public GameObject colourSliderPrefab; 
    private GameObject sliderContainer;
    private GameObject imageObject;

    private Image sliderImage; 
    private Color startingColor = Color.black;

    private GameObject redSlider;
    private GameObject greenSlider;
    private GameObject blueSlider;

    private void Start()
    {
        InitialiseSliderContainer();
        InitialiseImageObject();

        redSlider = InitialiseSlider(Channel.Red);
        greenSlider = InitialiseSlider(Channel.Green);
        blueSlider = InitialiseSlider(Channel.Blue);
    }

    private void InitialiseSliderContainer()
    {
        sliderContainer = new GameObject("SliderContainer");
        sliderContainer.transform.parent = transform;
        sliderContainer.transform.localPosition = Vector2.zero;
        sliderContainer.AddComponent<VerticalLayoutGroup>();
    }

    private void InitialiseImageObject()
    {
        imageObject = new GameObject("SliderImage");
        imageObject.transform.parent = sliderContainer.transform;
        sliderImage = imageObject.AddComponent<Image>();
        sliderImage.color = startingColor;

    }

    private GameObject InitialiseSlider(Channel channel)
    {
        GameObject sliderObject = Instantiate(colourSliderPrefab);  
        sliderObject.transform.parent = sliderContainer.transform; 

        Slider slider = sliderObject.GetComponent<Slider>();
        slider.minValue = 0f; 
        slider.maxValue = 255f;
        slider.onValueChanged.AddListener(delegate { UpdateImageColour(channel, slider.value); });

        return sliderObject;
    }

    private void UpdateImageColour(Channel channel, float value)
    {
        switch (channel)
        {
            case Channel.Red:
                // Divide by 255 to normalise the value 
                sliderImage.color = new Color(value / 255f, sliderImage.color.g, sliderImage.color.b);
                break;
            case Channel.Green:
                sliderImage.color = new Color(sliderImage.color.r, value / 255f, sliderImage.color.b);
                break;
            case Channel.Blue:
                sliderImage.color = new Color(sliderImage.color.r, sliderImage.color.g, value / 225f);
                break; 

        }
    }

    private void LogSliderValues()
    {
        Debug.Log("Red slider value: " + redSlider.GetComponent<Slider>().value);
        Debug.Log("Green slider value: " + greenSlider.GetComponent<Slider>().value);
        Debug.Log("Blue slider value: " + blueSlider.GetComponent<Slider>().value);
    }
}
