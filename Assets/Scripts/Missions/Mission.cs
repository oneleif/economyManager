using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Mission", order = 1)]
public class Mission : ScriptableObject
{
    public string pilotsName;
    public int missionDurationInSeconds;
    public long missionValue;

    public bool inProgress;
}
