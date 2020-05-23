using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PilotsManager : MonoBehaviour
{
	public GameObject crewPanel;
	public GameObject pilotButtonPrefab;
	public GameObject pilotProfilePanel;
	public PilotsContainer pilotsContainer;

	private void Awake()
	{
		pilotProfilePanel.SetActive(false); 
		pilotProfilePanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { ClosePilotProfilePanel(); });
		GeneratePilotButtons(); 
	}

	private void GeneratePilotButtons()
	{
		foreach (Pilot pilot in pilotsContainer.pilots)
		{
			GameObject pilotButton = Instantiate(pilotButtonPrefab);
			pilotButton.transform.parent = crewPanel.transform.Find("PilotButtonsGroup");
			pilotButton.GetComponentInChildren<Text>().text = pilot.pilotName;
			pilotButton.GetComponent<Button>().onClick.AddListener(delegate { OpenPilotProfilePanel(pilot); });

			// these will be set by the hire pilots logic later on
			pilot.hired = true; 
			pilot.onMission = false; 
		}
	}

	private void OpenPilotProfilePanel(Pilot pilot)
	{
		crewPanel.SetActive(false);
		pilotProfilePanel.SetActive(true);
		pilotProfilePanel.transform.Find("PilotProfileName").GetComponent<Text>().text = pilot.pilotName;
		pilotProfilePanel.transform.Find("PilotProfileDescription").GetComponent<Text>().text = pilot.description;
		pilotProfilePanel.transform.Find("PilotProfileAvatar").GetComponent<Image>().sprite = pilot.avatar;
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
	}
}