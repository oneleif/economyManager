using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Crew : MonoBehaviour
{
	private enum Gender { male, female, robot };

	private string[] maleNames;
	private string[] femaleNames;
	
	private string randomName;
	private string firstName;
	private char initial;
	private int prefixLength = 3;
	private int suffixLength = 4;

	[SerializeField]
	private Button randomMaleNameButton, randomFemaleNameButton, randomRobotNameButton;

	[SerializeField]
	private Text randomMaleNameText, randomFemaleNameText, randomRobotNameText;

	public void Awake()
	{
		maleNames = PilotNameDataSingleton.Instance.MaleNames;
		femaleNames = PilotNameDataSingleton.Instance.FemaleNames;

		randomMaleNameButton.onClick.AddListener(delegate { randomPilotName(Gender.male); });
		randomFemaleNameButton.onClick.AddListener(delegate { randomPilotName(Gender.female); });
		randomRobotNameButton.onClick.AddListener(delegate { randomPilotName(Gender.robot); });
	}

	private void randomPilotName(Gender gender)
	{
		if (gender == Gender.male)
		{
			string firstName = maleNames[Random.Range(0, maleNames.Length)];
			char initial = char.ToUpper((char)('a' + Random.Range(0, 26)));
			randomName = $"{firstName} {initial}.";
			randomMaleNameText.text = randomName;

		}
		else if (gender == Gender.female)
		{
			string firstName = femaleNames[Random.Range(0, femaleNames.Length)];
			char initial = char.ToUpper((char)('a' + Random.Range(0, 26)));
			randomName = $"{firstName} {initial}.";
			randomFemaleNameText.text = randomName;
		}
		else if (gender == Gender.robot) 
		{
			string prefix = string.Empty; 
			string suffix = string.Empty;

			for (int i = 0; i < prefixLength; i++)
			{
				prefix += char.ToUpper((char)('a' + Random.Range(0, 26)));
				Debug.Log("prefix " + suffix);
			}
			for (int i = 0; i < suffixLength; i++)
			{
				suffix += Random.Range(0, 9).ToString(); 
				Debug.Log("suffix " + suffix);
			}

			randomName = $"{prefix}-{suffix}";
			randomRobotNameText.text = randomName;
		}
	}
}
