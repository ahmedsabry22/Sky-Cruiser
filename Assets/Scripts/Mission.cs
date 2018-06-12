using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public int missionIndex;
    public Vector3 aircraftStartPosition;
    public static Mission Instance;
    public MissionType missionType;

    public int coinAward;      // Number of coins recieved after finishing the mission successfully.

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void OnMissionDone()
    {
        Popup.Instance.Show("Mission Done!");
        PlayerPrefs.SetInt(Constants.LAST_MISSION_KEY, missionIndex + 1);
        CoinController.IncreaseDecreaseCoins(coinAward);
    }
}