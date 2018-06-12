using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftSpawner : MonoBehaviour
{
    public GameObject[] aircraftPrefabs;


    private int aircraftIndex;

    private void Awake()
    {
        aircraftIndex = PlayerPrefs.GetInt(Constants.AIRCRAFT_KEY, 0);

        GameObject aircraft = Instantiate(aircraftPrefabs[aircraftIndex]);   //, MissionSpawner.Instance.missionPrefabs[PlayerPrefs.GetInt(Constants.MISSION_KEY)].GetComponent<Mission>().aircraftStartPosition, Quaternion.identity);
    }
}