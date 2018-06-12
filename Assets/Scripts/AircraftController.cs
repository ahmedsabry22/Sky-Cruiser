using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AircraftType
{
    Normal, Military
}

public class AircraftController : MonoBehaviour
{
    public static AircraftController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ChangeActiveAircraft(int index)
    {
        PlayerPrefs.SetInt(Constants.AIRCRAFT_KEY, index);
    }
}