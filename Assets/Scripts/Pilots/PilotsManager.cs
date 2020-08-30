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

	private void GeneratePilotProfilePanel()
	{
		pilotProfilePanel = new GameObject(PilotsConstants.profilePanelName);

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

	private void GeneratePilotButtons()
	{
		GameObject pilotButtonGroup = new GameObject(PilotsConstants.buttonGroupName);
		pilotButtonGroup.transform.parent = crewPanel.transform;
		RectTransform rectTransform = pilotButtonGroup.AddComponent<RectTransform>();
		rectTransform.localPosition = Vector2.zero;
		rectTransform.anchorMin = PilotsConstants.buttonGroupAnchorMin; 
		rectTransform.anchorMax = PilotsConstants.buttonGroupAnchorMax; 

        VerticalLayoutGroup verticalLayoutGroup = pilotButtonGroup.AddComponent<VerticalLayoutGroup>();
		verticalLayoutGroup.childControlWidth = true;
		verticalLayoutGroup.childControlHeight = true;

		foreach (Pilot pilot in pilotsContainer.pilots)
		{
			GameObject pilotButton = Instantiate(pilotButtonPrefab);
			pilotButton.name = $"{pilot.name}Button"; 
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

	private void GeneratePilotNameText()
    {
		GameObject pilotNameObject = new GameObject(PilotsConstants.nameTextName);
		pilotNameObject.transform.parent = pilotProfilePanel.transform;

		RectTransform rectTransform = pilotNameObject.AddComponent<RectTransform>();
		rectTransform.anchorMin = PilotsConstants.nameTextAnchorMin;
		rectTransform.anchorMax = PilotsConstants.nameTextAnchorMax; 
        rectTransform.localScale = Vector3.one;
        rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		pilotNameText = pilotNameObject.AddComponent<Text>();
		SetFont(pilotNameText); 
	}

	private void GeneratePilotDescription()
    {
		GameObject pilotDescriptionObject = new GameObject(PilotsConstants.descriptionObjectName);
		pilotDescriptionObject.transform.parent = pilotProfilePanel.transform;

		RectTransform rectTransform = pilotDescriptionObject.AddComponent<RectTransform>();
		rectTransform.anchorMin = PilotsConstants.descriptionAnchorMin;
		rectTransform.anchorMax = PilotsConstants.descriptionAnchorMax;
        rectTransform.localScale = Vector3.one;
        rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		pilotDescriptionText = pilotDescriptionObject.AddComponent<Text>();
		SetFont(pilotDescriptionText); 
	}

	private void GeneratePilotAvatar()
    {
		GameObject pilotAvatarObject = new GameObject(PilotsConstants.avatarObjectName);
		pilotAvatarObject.transform.parent = pilotProfilePanel.transform;

		RectTransform rectTransform = pilotAvatarObject.AddComponent<RectTransform>();
		rectTransform.anchorMin = PilotsConstants.avatarAnchorMin;
		rectTransform.anchorMax = PilotsConstants.avatarAnchorMax; 
        rectTransform.localScale = Vector3.one;
        rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		pilotAvatar = pilotAvatarObject.AddComponent<Image>();
	}

	private void GenerateBackButton()
    {
		GameObject backButton = Instantiate(pilotButtonPrefab);
		backButton.name = PilotsConstants.backButtonName;

		if (pilotProfilePanel != null)
        {
			backButton.transform.parent = pilotProfilePanel.transform; 
        }

		RectTransform rectTransform = backButton.GetComponent<RectTransform>();
		rectTransform.anchorMin = PilotsConstants.backButtonAnchorMin;
		rectTransform.anchorMax = PilotsConstants.backButtonAnchorMax;
		rectTransform.localScale = Vector2.one; 
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		Button button = backButton.GetComponent<Button>();
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(delegate { ClosePilotProfilePanel(); });

		backButton.GetComponentInChildren<Text>().text = PilotsConstants.backButtonText; 
    }

	private void ClosePilotProfilePanel()
	{
		crewPanel.SetActive(true);
		pilotProfilePanel.SetActive(false); 
	}

	private void SetFont(Text text)
    {
		text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
	}

	private void CycleThroughProfiles()
	{
		// would be nice to have a button that cycles through subsequent pilot profiles
		// rather than going back to the hangar then going to the next pilot
		// getting a reference to the next pilot in the GeneratePilotButtons() method? 
		
		// index the pilotsContainer; update it each time a pilotButton is clicked.
	}
}