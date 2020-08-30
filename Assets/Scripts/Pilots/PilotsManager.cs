using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PilotsManager : MonoBehaviour
{
	public GameObject crewPanel;
	public GameObject pilotButtonPrefab;
	public PilotsContainer pilotsContainer;

	private GameObject pilotProfilePanel;
	private Text pilotNameText;
	private Text pilotDescriptionText; 
	private Image pilotAvatar; 

	private void Awake()
	{
		GeneratePilotsUI(); 


        //pilotProfilePanel.SetActive(false);
        //pilotProfilePanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { ClosePilotProfilePanel(); });
    }

	private void GeneratePilotsUI()
    {
		GeneratePilotProfilePanel();
		GeneratePilotButtons();
		GeneratePilotNameText();
        GeneratePilotDescription();
		GeneratePilotAvatar();
		GenerateBackButton(); 
	}

	private void GeneratePilotButtons()
	{
		GameObject pilotButtonGroup = new GameObject("PilotButtonGroup");
		pilotButtonGroup.transform.parent = crewPanel.transform;
		RectTransform rectTransform = pilotButtonGroup.AddComponent<RectTransform>();
		rectTransform.localPosition = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

        VerticalLayoutGroup verticalLayoutGroup = pilotButtonGroup.AddComponent<VerticalLayoutGroup>();
		verticalLayoutGroup.childControlWidth = true;
		verticalLayoutGroup.childControlHeight = true;

		foreach (Pilot pilot in pilotsContainer.pilots)
		{
			GameObject pilotButton = Instantiate(pilotButtonPrefab);
			pilotButton.transform.parent = pilotButtonGroup.transform;
			pilotButton.GetComponentInChildren<Text>().text = pilot.pilotName;
			Button button = pilotButton.GetComponent<Button>();
			button.onClick.RemoveAllListeners(); 
			button.onClick.AddListener(delegate { OpenPilotProfilePanel(pilot); });

			// these will be set by the hire pilots logic later on
			pilot.hired = true; 
			pilot.onMission = false; 
		}
	}

	private void OpenPilotProfilePanel(Pilot pilot)
	{
		crewPanel.SetActive(false);
        pilotProfilePanel.SetActive(true);
		pilotNameText.text = pilot.name;
		pilotDescriptionText.text = pilot.description;
		pilotAvatar.sprite = pilot.avatar;
    }

	private void GeneratePilotProfilePanel()
	{
		pilotProfilePanel = new GameObject("PilotProfilePanel");

		// Sibling of the crew panel so that it can be active
		// while the crew panel is inactive. 
		pilotProfilePanel.transform.parent = crewPanel.transform.parent.transform;

		RectTransform rectTransform = pilotProfilePanel.AddComponent<RectTransform>();
		rectTransform.anchorMin = Vector2.zero;
		rectTransform.anchorMax = Vector2.one;
		rectTransform.localScale = Vector3.one;
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		pilotProfilePanel.SetActive(false); 
	}

	private void GeneratePilotNameText()
    {
		GameObject pilotNameObject = new GameObject("PilotName");
		pilotNameObject.transform.parent = pilotProfilePanel.transform;

		RectTransform rectTransform = pilotNameObject.AddComponent<RectTransform>();
		rectTransform.anchorMin = new Vector2(0f, 0.75f);
		rectTransform.anchorMax = new Vector2(0.5f, 1f); 
		//rectTransform.localScale = Vector3.one;
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		pilotNameText = pilotNameObject.AddComponent<Text>(); 
		//pilotNameText.text = 
    }

	private void GeneratePilotDescription()
    {
		GameObject pilotDescriptionObject = new GameObject("PilotDescription");
		pilotDescriptionObject.transform.parent = pilotProfilePanel.transform;

		RectTransform rectTransform = pilotDescriptionObject.AddComponent<RectTransform>();
		rectTransform.anchorMin = new Vector2(0f, 0.5f);
		rectTransform.anchorMax = new Vector2(0.5f, 0.75f);
		//rectTransform.localScale = Vector3.one;
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		pilotDescriptionText = pilotDescriptionObject.AddComponent<Text>();
	}

	private void GeneratePilotAvatar()
    {
		GameObject pilotAvatarObject = new GameObject("PilotAvatar");
		pilotAvatarObject.transform.parent = pilotProfilePanel.transform;

		RectTransform rectTransform = pilotAvatarObject.AddComponent<RectTransform>();
		rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // v-one / 2f 
		rectTransform.anchorMax = Vector2.one; 
		//rectTransform.localScale = Vector3.one;
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		pilotAvatar = pilotAvatarObject.AddComponent<Image>();
	}

	private void GenerateBackButton()
    {
		GameObject backButton = new GameObject("BackButton");
		if (pilotProfilePanel != null)
        {
			backButton.transform.parent = pilotProfilePanel.transform; 
        }

		RectTransform rectTransform = backButton.AddComponent<RectTransform>();
		rectTransform.anchorMin = new Vector2(0.25f, 0f); // v-one / 2f 
		rectTransform.anchorMax = new Vector2(0.75f, 0.25f); 
		//rectTransform.localScale = Vector3.one;
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;
		// padding? 

		Button button = backButton.AddComponent<Button>();
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(delegate { ClosePilotProfilePanel(); });

		GameObject textObject = new GameObject("BackButonText");
		textObject.transform.parent = textObject.transform;
		textObject.AddComponent<Text>().text = "Back"; 
    }

	private void ClosePilotProfilePanel()
	{
		crewPanel.SetActive(true);
		pilotProfilePanel.SetActive(false); 
	}

	private void CycleThroughProfiles()
	{
		// would be nice to have a button that cycles through subsequent pilot profiles
		// rather than going back to the hangar then going to the next pilot
		// getting a reference to the next pilot in the GeneratePilotButtons() method? 
		
		// index the pilotsContainer; update it each time a pilotButton is clicked.
	}
}