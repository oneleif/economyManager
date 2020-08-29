using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotsUtils : MonoBehaviour
{






	public static GameObject GeneratePilotDescription(GameObject parentObject)
	{
		GameObject pilotDescriptionObject = new GameObject("PilotDescription");
		pilotDescriptionObject.transform.parent = parentObject.transform;

		RectTransform rectTransform = pilotDescriptionObject.GetComponent<RectTransform>();
		rectTransform.anchorMin = new Vector2(0f, 0.5f);
		rectTransform.anchorMax = new Vector2(0.5f, 0.75f);
		//rectTransform.localScale = Vector3.one;
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;

		Text pilotDescriptionText = pilotDescriptionObject.AddComponent<Text>();

		return pilotDescriptionObject;
	}
}
