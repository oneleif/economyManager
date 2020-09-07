using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PilotNameDataSingleton : MonoBehaviour
{
    public static PilotNameDataSingleton Instance { get; private set; }
    public string[] MaleNames { get; private set; } 
    public string[] FemaleNames { get; private set; } 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            MaleNames = File.ReadAllLines("./Assets/ImportedAssets/male_names.txt"); 
            FemaleNames = File.ReadAllLines("./Assets/ImportedAssets/female_names.txt");
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
