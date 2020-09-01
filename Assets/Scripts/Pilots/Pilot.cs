using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Species
{
    Human, Helicid, Myorijiin, Oshunian, HelmetGuy, Vesta
}

[CreateAssetMenu(fileName = "Pilot", menuName = "ScriptableObjects/Pilot", order = 1)]
public class Pilot : ScriptableObject
{
    public string pilotName, shipName, description;
    public Species species;
    public int level, experience, missionsCompleted;
    public bool hired, onMission;

    public Sprite avatar; 

}
