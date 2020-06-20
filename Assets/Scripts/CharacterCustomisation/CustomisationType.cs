using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CustomisationType", menuName = "ScriptableObjects/CustomisationType", order = 1)]
public class CustomisationType : ScriptableObject
{
    public Image customisationImage;
    //public GameObject leftArrow;
    //public GameObject rightArrow;
    public int customisationOptionIndex;
    public Color customisationColor = Color.red; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
